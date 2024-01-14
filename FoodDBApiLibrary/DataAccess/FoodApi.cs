namespace FoodDBApiLibrary.DataAccess;

using FoodDBApiLibrary.Models;

public delegate string? CallApi(string uri);

public class FoodApi
{
	private const string _baseUri = "www.themealdb.com/api/json/v1/";
	private string _apiKey;
	private CallApi _callApi;

    public FoodApi(string apiKey = "1")
    {
		_apiKey = apiKey;
		_callApi = CallApi;
    }
    public FoodApi(CallApi callApi, string apiKey = "1") : this(apiKey)
    {
		_callApi = callApi;
    }

    public string Uri { get; set; }

    public Food GetFoodById(int id)
	{
		var uriAppend = $"lookup.php?i={id}";
		Uri = $"{_baseUri}{_apiKey}/{uriAppend}";

		var json = _callApi(Uri);

		return DeserializeFoodObject(json);
	}

	private Food DeserializeFoodObject(string json)
	{

	}

	private string? CallApi(string uri)
	{
		var client = new HttpClient();

		var response = client.GetAsync(uri).Result;
		
		//response.EnsureSuccessStatusCode();

		return response.Content.ReadAsStringAsync().Result;
	}
}
