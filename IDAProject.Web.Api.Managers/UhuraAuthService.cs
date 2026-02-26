namespace IDAProject.Web.Api
{
    public class UhuraAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UhuraAuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetTokenAsync(string clientId, string clientSecret)
        {
            var client = _httpClientFactory.CreateClient("UhuraClient");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sso.uhura.bilsteingroup.com/auth/realms/uhura/protocol/openid-connect/token");

            var parameters = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };

            request.Content = new FormUrlEncodedContent(parameters);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Uhura Auth failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(content);

            return tokenResponse?.AccessToken ?? throw new Exception("Token not found in Uhura response");
        }
    }

    public class TokenResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
