using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Timers;
using HappyPointSounds.Clients;
using HappyPointSounds.Events;
using HappyPointSounds.Models;
using HappyPointSounds.Models.Messages;

namespace HappyPointSounds
{
	class PubSubHandler
	{

		private static readonly Random Random = new Random();
		public static string GenerateNonce => new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
				.Select(s => s[Random.Next(s.Length)]).ToArray());


		private HappySocket _socket;
		private readonly List<PreviousRequest> _previousRequests = new List<PreviousRequest>();
		private readonly Timer _pingTimer = new Timer();

		#region Events
		/// <summary>Fires when PubSub Service is connected.</summary>
		public event EventHandler OnSocketConnected;
		/// <summary>Fires when PubSub Service is closed.</summary>
		public event EventHandler OnSocketClosed;
		/// <summary>Fires when the socket is cleaning up.</summary>
		public event EventHandler OnSocketCleaning;
		/// <summary>Fires when the socket is cleaning up.</summary>
		public event EventHandler OnSocketReady;
		/// <summary>Fires when PubSub Service has an error.</summary>
		public event EventHandler<OnErrorEventArgs> OnSocketError;
		/// <summary>Fires when PubSub receives any response.</summary>
		public event EventHandler<OnListenResponseArgs> OnListenResponse;
		/// <summary>Fires when PubSub receives notice of a commerce transaction.</summary>
		public event EventHandler<OnChannelCommerceReceivedArgs> OnChannelCommerceReceived;
		#endregion

		/// <summary>
		/// Constructor for a client that interface's with Twitch's PubSub system.
		/// </summary>
		public PubSubHandler()
		{
			_socket = new HappySocket();

			_socket.OnConnected += Socket_OnConnected;
			_socket.OnError += OnError;
			_socket.OnMessage += OnMessage;
			_socket.OnDisconnected += Socket_OnDisconnected;
			_socket.OnCleaning += OnCleaning;
			_socket.OnReady += OnReady;
		}

		private void OnReady(object sender, EventArgs e)
		{
			OnSocketReady?.Invoke(this, null);
		}

		private void OnCleaning(object sender, EventArgs e)
		{
			OnSocketCleaning?.Invoke(this, null);
		}

		private void OnError(object sender, OnErrorEventArgs e)
		{
			OnSocketError?.Invoke(this, new OnErrorEventArgs { Exception = e.Exception });
		}

		private void OnMessage(object sender, OnMessageEventArgs e)
		{
			ParseMessage(e.Message);
		}

		private void Socket_OnDisconnected(object sender, EventArgs e)
		{
			_pingTimer.Stop();
			OnSocketClosed?.Invoke(this, null);
		}

		private void Socket_OnConnected(object sender, EventArgs e)
		{
			_pingTimer.Interval = 180000;
			_pingTimer.Elapsed += PingTimerTick;
			_pingTimer.Start();
			OnSocketConnected?.Invoke(this, null);
		}

		private void PingTimerTick(object sender, ElapsedEventArgs e)
		{
			string json = string.Empty;

			using (MemoryStream stream = new MemoryStream())
			{
				using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
				{
					writer.WriteStartObject();

					writer.WriteString("type", "PING");
				}

				json = Encoding.UTF8.GetString(stream.ToArray());
			}

			SendMessage(json);
		}

		private void ParseMessage(string message)
		{
			OnListenResponse?.Invoke(this, new OnListenResponseArgs { Response = new Response(message), Topic = null, Successful = false});

			JsonDocument doc = JsonDocument.Parse(message);

			string type = doc.RootElement.GetProperty("type").ToString() ?? string.Empty;

			switch (type?.ToLower())
			{
				case "response":
					Response resp = new Response(message);
					if (_previousRequests.Count != 0)
					{
						foreach (PreviousRequest request in _previousRequests)
							if (string.Equals(request.Nonce, resp.Nonce, StringComparison.CurrentCultureIgnoreCase))
								OnListenResponse?.Invoke(this, new OnListenResponseArgs { Response = resp, Topic = request.Topic, Successful = resp.Successful });

						return;
					}
					break;
				case "message":
					Message msg = new Message(message);
					switch (msg.Topic.Split('.')[0])
					{
						case "channel-commerce-events-v1":
							if (msg.MessageData is ChannelCommerceEvents cce)
								OnChannelCommerceReceived?.Invoke(this, new OnChannelCommerceReceivedArgs
								{

									Username = cce.Username,
									DisplayName = cce.DisplayName,
									ChannelName = cce.ChannelName,
									UserId = cce.UserId,
									ChannelId = cce.ChannelId,
									Time = cce.Time,
									ItemImageURL = cce.ItemImageURL,
									ItemDescription = cce.ItemDescription,
									SupportsChannel = cce.SupportsChannel,
									PurchaseMessage = cce.PurchaseMessage
								});
							return;
					}
					break;
			}
		}

		public bool SendTopics(string channelID, string oauth = null, bool unlisten = false)
		{
			if (oauth != null && oauth.Contains("oauth:"))
				oauth = oauth.Replace("oauth:", "");

			string nonce = GenerateNonce;
			string json = string.Empty;

			using (MemoryStream stream = new MemoryStream())
			{
				using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
				{
					writer.WriteStartObject();

					writer.WriteString("type", !unlisten ? "LISTEN" : "UNLISTEN");
					writer.WriteString("nonce", nonce);

					writer.WriteStartObject("data");
					writer.WriteStartArray("topics");

					// "Search Channels" with Happy( or in any other case: the user)'s name in it would suffice to get the channelID.
					writer.WriteStringValue($"channel-points-channel-v1.{channelID}");


					writer.WriteEndArray();

					if (oauth != null)
						writer.WriteString("auth_token", oauth);

					writer.WriteEndObject();

					writer.WriteEndObject();
				}

				json = Encoding.UTF8.GetString(stream.ToArray());
			}

			return SendMessage(json);
		}

		public bool SendMessage (string json)
		{
			return _socket.Send(json);
		}

		/// <summary>
		/// Method to connect to Twitch's PubSub service. You MUST listen toOnConnected event and listen to a Topic within 15 seconds of connecting (or be disconnected)
		/// </summary>
		public bool Connect()
		{
			return _socket.Open();
		}

		/// <summary>
		/// What do you think it does? :)
		/// </summary>
		public void Disconnect()
		{
			_socket.Close();
		}

		/// <summary>
		/// This method will send passed json text to the message parser in order to allow forOn-demand parser testing.
		/// </summary>
		/// <param name="testJsonString"></param>
		public void TestMessageParser(string testJsonString)
		{
			ParseMessage(testJsonString);
		}
	}
}
