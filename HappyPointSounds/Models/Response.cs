﻿using System.Text.Json;

namespace HappyPointSounds.Models
{
	/// <summary>Response object detailing pubsub response</summary>
	public class Response
	{
		//{"type":"RESPONSE","error":"","nonce":"8SYYENPH"}

		/// <summary>IF error exists, it will be here</summary>
		public string Error { get; protected set; }
		/// <summary>Unique communication token</summary>
		public string Nonce { get; protected set; }
		/// <summary>Whether or not successful</summary>
		public bool Successful { get; protected set; }

		/// <summary>Response model constructor.</summary>
		public Response(string json)
		{
			JsonDocument doc = JsonDocument.Parse(json);
			
			Error = doc.RootElement.GetProperty("error").ToString() ?? string.Empty;
			Nonce = doc.RootElement.GetProperty("nonce").ToString() ?? string.Empty;

			if (string.IsNullOrWhiteSpace(Error))
				Successful = true;
		}
	}
}
