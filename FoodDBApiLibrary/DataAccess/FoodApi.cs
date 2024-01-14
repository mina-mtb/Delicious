namespace FoodDBApiLibrary.DataAccess;

using FoodDBApiLibrary.Models;
using Newtonsoft.Json.Linq;

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
		var jobjectFood = JObject.Parse(json);

		var ingredients = new List<Ingredient>();
		var counter = 1;
		while (!string.IsNullOrEmpty((string)jobjectFood[$"strIngredient{counter}"]))
		{
			ingredients.Add(new Ingredient()
			{
				Name = (string)jobjectFood[$"strIngredient{counter}"],
				Measure = (string)jobjectFood[$"strMeasure{counter}"]
			});
			counter++;
		}

		return new Food()
		{
			Id = (int)jobjectFood["idMeal"],
			Name = (string)jobjectFood["strMeal"],
			DrinkAlternate = (string)jobjectFood["strDrinkAlternate"],
			Group = new FoodGroup()
			{
				Name = (string)jobjectFood["strCategory"]
			},
			Area = new Area()
			{
				Name = (string)jobjectFood["strArea"]
			},
			Recipe = (string)jobjectFood["strInstructions"],
			Thumbnail = (Uri)jobjectFood["strMealThumb"],
			Tags = (string)jobjectFood["strTags"],
			Youtube = (Uri)jobjectFood["strYoutube"],
			Ingredients = ingredients,
			Source = (Uri)jobjectFood["strSource"],
			ImageSource = (Uri)jobjectFood["strImageSource"],
			CreativeCommons = (bool)jobjectFood["strCreativeCommonsConfirmed"],
			DateModified = (DateTime)jobjectFood["dateModified"]
		};
	}

	private string? CallApi(string uri)
	{
		var client = new HttpClient();

		var response = client.GetAsync(uri).Result;

		//response.EnsureSuccessStatusCode();

		return response.Content.ReadAsStringAsync().Result;
	}
}
