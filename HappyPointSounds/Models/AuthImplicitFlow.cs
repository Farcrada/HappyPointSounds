namespace HappyPointSounds.Models
{
	public class AuthImplicitFlow
	{
		public const string ClientID = "uekt7ipa38ko7mpwf9gi9guunzkfuz";
		public const string RedirectUrl = "http://localhost:8969";
		public static string[] Scopes = {
		"channel:read:redemptions",
		"user:read:email"
		};
		public static string ScopeString = string.Join("+", value: Scopes);

		public string AuthRedirectUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=token&client_id={ClientID}&redirect_uri={RedirectUrl}&scope={ScopeString}&state=123456";
	}
}
