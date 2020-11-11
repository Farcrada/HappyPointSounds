using System.Text.Json;

namespace HappyPointSounds.Models.Messages
{
	/// <summary>PubSub Message model.</summary>
	public class Message
	{
		/// <summary>Topic that the message is relevant to.</summary>
		public string Topic { get; protected set; }
		/// <summary>Model containing data of the message.</summary>
		public readonly MessageData MessageData;

		/// <summary>PubSub Message model constructor.</summary>
		public Message(string jsonStr)
		{
			JsonDocument doc = JsonDocument.Parse(jsonStr);

			Topic = doc.RootElement.GetProperty("data").GetProperty("topic").ToString() ?? string.Empty;
		}
	}
}
