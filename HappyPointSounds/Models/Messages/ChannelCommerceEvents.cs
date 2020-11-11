using System.Text.Json;

namespace HappyPointSounds.Models.Messages
{
    /// <inheritdoc />
    /// <summary>Model representing the data in a channel commerce event.</summary>
    public class ChannelCommerceEvents : MessageData
    {
        /// <summary>Username of the buyer.</summary>
        public string Username { get; protected set; }
        /// <summary>Display name of the buyer.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>The channel the purchase was made in.</summary>
        public string ChannelName { get; protected set; }
        /// <summary>User ID of the buyer.</summary>
        public string UserId { get; protected set; }
        /// <summary>Channel/User ID the purchase was made for/in.</summary>
        public string ChannelId { get; protected set; }
        /// <summary>Time stamp of the event.</summary>
        public string Time { get; protected set; }
        /// <summary>URL for the item's image.</summary>
        public string ItemImageURL { get; protected set; }
        /// <summary>Description of the item.</summary>
        public string ItemDescription { get; protected set; }
        /// <summary>Does this purchase support the channel?</summary>
        public bool SupportsChannel { get; protected set; }
        /// <summary>Chat message that accompanied the purchase.</summary>
        public string PurchaseMessage { get; protected set; }

        /// <summary>ChannelBitsEvent model constructor.</summary>
        public ChannelCommerceEvents(string jsonStr)
        {
            JsonElement jsonData = JsonDocument.Parse(jsonStr).RootElement.GetProperty("data");
            Username = jsonData.GetProperty("user_name").ToString() ?? string.Empty;
            DisplayName = jsonData.GetProperty("display_name").ToString() ?? string.Empty;
            ChannelName = jsonData.GetProperty("channel_name").ToString() ?? string.Empty;
            UserId = jsonData.GetProperty("user_id").ToString() ?? string.Empty;
            ChannelId = jsonData.GetProperty("channel_id").ToString() ?? string.Empty;
            Time = jsonData.GetProperty("time").ToString() ?? string.Empty;
            ItemImageURL = jsonData.GetProperty("image_item_url").ToString() ?? string.Empty;
            ItemDescription = jsonData.GetProperty("item_description").ToString() ?? string.Empty;
            SupportsChannel = bool.Parse(jsonData.GetProperty("supports_channel").ToString() ?? string.Empty);
            PurchaseMessage = jsonData.GetProperty("purchase_message").GetProperty("message").ToString() ?? string.Empty;
        }
    }
}
