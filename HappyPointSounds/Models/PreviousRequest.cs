﻿using HappyPointSounds.Enums;

namespace HappyPointSounds.Models
{
    /// <summary>Model representing the previous request.</summary>
    public class PreviousRequest
    {
        /// <summary>Unique communication token.</summary>
        public string Nonce { get; protected set; }
        /// <summary>PubSub request type.</summary>
        public RequestType RequestType { get; protected set; }
        /// <summary>Topic that we are interested in.</summary>
        public string Topic { get; protected set; }

        /// <summary>PreviousRequest model constructor.</summary>
        public PreviousRequest(string nonce, RequestType requestType, string topic = "none set")
        {
            Nonce = nonce;
            RequestType = requestType;
            Topic = topic;
        }

    }
}
