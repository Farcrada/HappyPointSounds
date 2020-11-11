using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HappyPointSounds.Events;
using HappyPointSounds.Models;

namespace HappyPointSounds.Clients
{
	public class HappySocket
	{
		public TimeSpan DefaultKeepAliveInterval { get; set; }
		public bool IsConnected => Client?.State == WebSocketState.Open;
		ReconnectionPolicy _reconnectionPolicy { get; set; }
		public ClientWebSocket Client { get; private set; }

		public event EventHandler OnConnected;
		public event EventHandler OnDisconnected;
		public event EventHandler OnCleaning;
		public event EventHandler OnReady;
		public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
		public event EventHandler<OnMessageEventArgs> OnMessage;
		public event EventHandler<OnErrorEventArgs> OnError;

		private string _url { get { return "wss://pubsub-edge.twitch.tv:443"; } }
		private CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private bool _stopServices;
		private bool _networkServicesRunning;
		private Task[] _networkTasks;
		private Task _monitorTask;

		private void InitializeClient()
		{
			Client?.Abort();
			Client = new ClientWebSocket();

			if (_monitorTask == null)
			{
				_monitorTask = StartMonitorTask();
				return;
			}

			if (_monitorTask.IsCompleted) _monitorTask = StartMonitorTask();
		}

		public bool Open()
		{
			try
			{
				if (IsConnected) return true;

				InitializeClient();
				Client.ConnectAsync(new Uri(_url), _tokenSource.Token).Wait(10000);
				if (!IsConnected) return Open();

				StartNetworkServices();
				return true;
			}
			catch (WebSocketException)
			{
				InitializeClient();
				return false;
			}
		}

		public void Close(bool callDisconnect = true)
		{
			Client?.Abort();
			_stopServices = callDisconnect;
			CleanupServices();
			InitializeClient();
			OnDisconnected?.Invoke(this, null);
		}

		public void Reconnect()
		{
			Close();
			Open();
		}

		public bool Send(string message)
		{
			try
			{
				if (!IsConnected)
					return false;

				Client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, _tokenSource.Token);

				return true;
			}
			catch (Exception ex)
			{
				OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
				throw;
			}
		}

		private void StartNetworkServices()
		{
			_networkServicesRunning = true;
			_networkTasks = new[]
			{
				StartListenerTask()
			}.ToArray();

			if (!_networkTasks.Any(c => c.IsFaulted)) return;
			_networkServicesRunning = false;
			CleanupServices();
		}

		private Task StartListenerTask()
		{
			return Task.Run(async () =>
			{
				string message = "";

				while (IsConnected && _networkServicesRunning)
				{
					WebSocketReceiveResult result;
					byte[] buffer = new byte[1024];

					try
					{
						result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), _tokenSource.Token);
					}
					catch
					{
						InitializeClient();
						break;
					}

					if (result == null) continue;

					switch (result.MessageType)
					{
						case WebSocketMessageType.Close:
							Close();
							break;
						case WebSocketMessageType.Text when !result.EndOfMessage:
							message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');
							continue;
						case WebSocketMessageType.Text:
							message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');
							OnMessage?.Invoke(this, new OnMessageEventArgs() { Message = message });
							break;
						case WebSocketMessageType.Binary:
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}

					message = "";
				}
			});
		}

		private Task StartMonitorTask()
		{
			return Task.Run(() =>
			{
				bool needsReconnect = false;
				try
				{
					bool lastState = IsConnected;
					while (!_tokenSource.IsCancellationRequested)
					{
						if (lastState == IsConnected)
						{
							Thread.Sleep(200);
							continue;
						}
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { IsConnected = Client.State == WebSocketState.Open, WasConnected = lastState });

						if (IsConnected)
							OnConnected?.Invoke(this, null);

						if (!IsConnected && !_stopServices)
						{
							if (lastState && _reconnectionPolicy != null && !_reconnectionPolicy.AreAttemptsComplete())
							{
								needsReconnect = true;
								break;
							}

							OnDisconnected?.Invoke(this, null);
							if (Client.CloseStatus != null && Client.CloseStatus != WebSocketCloseStatus.NormalClosure)
								OnError?.Invoke(this, new OnErrorEventArgs { Exception = new Exception(Client.CloseStatus + " " + Client.CloseStatusDescription) });
						}

						lastState = IsConnected;
					}
				}
				catch (Exception ex)
				{
					OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
				}

				if (needsReconnect && !_stopServices)
					Reconnect();
			}, _tokenSource.Token);
		}

		private void CleanupServices()
		{
			OnCleaning?.Invoke(this, null);
			_tokenSource.Cancel();
			_tokenSource = new CancellationTokenSource();

			if (!_stopServices)
			{
				OnReady?.Invoke(this, null);
				return;
			}
			if (!(_networkTasks?.Length > 0))
			{
				OnReady?.Invoke(this, null);
				return;
			}
			if (Task.WaitAll(_networkTasks, 15000))
			{
				OnReady?.Invoke(this, null);
				return;
			}

			_stopServices = false;
			_networkServicesRunning = false;

			OnReady?.Invoke(this, null);
		}

		public void Dispose()
		{
			OnCleaning?.Invoke(this, null);

			Close();
			_tokenSource.Cancel();
			Thread.Sleep(500);
			_tokenSource.Dispose();
			Client?.Dispose();
			GC.Collect();

			OnReady?.Invoke(this, null);
		}
	}
}