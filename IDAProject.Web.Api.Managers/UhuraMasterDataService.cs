public class UhuraMasterDataService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UhuraMasterDataService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<UhuraArticleResponse>> GetArticlesAsync(string token, List<string> articleNumbers)
    {
        var client = _httpClientFactory.CreateClient("UhuraClient");
        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://bis1.prod.apimanagement.eu20.hana.ondemand.com/p/v1/masterdata-api/api/v1/master-data");

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var body = new
        {
            articles = articleNumbers,
            destination_country = "SR", 
            select_fields = new string[] { }
        };

        string json = System.Text.Json.JsonSerializer.Serialize(body);
        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Uhura MasterData error: {response.StatusCode} - {error}");
        }

        var content = await response.Content.ReadAsStringAsync();

        var articles = System.Text.Json.JsonSerializer.Deserialize<UhuraMasterDataResponse>(content);

        return articles?.Articles ?? new List<UhuraArticleResponse>();
    }
}

public class UhuraMasterDataResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("articles")]
    public List<UhuraArticleResponse> Articles { get; set; }
}

public class UhuraArticleResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("article_number")]
    public string ArticleNumber { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("article_description_sr")]
    public string ArticleName { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("delivery_unit")]
    public int DeliveryUnit { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("ean")]
    public string Ean { get; set; }

}