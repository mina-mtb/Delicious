namespace FoodDBApiLibrary.DataAccess;

using FoodDBApiLibrary.Models;
using Newtonsoft.Json.Linq;

public delegate string? CallApi(Uri uri);

public class FoodApi
{
	private readonly Uri _baseUri;
	private readonly CallApi _callApi;

    public FoodApi(string apiKey = "1")
    {
		_baseUri = new Uri($"https://www.themealdb.com/api/json/v1/{apiKey}/");
		_callApi = CallApi;
    }
    public FoodApi(CallApi callApi, string apiKey = "1") : this(apiKey)
    {
		_callApi = callApi;
    }

    public Food GetFoodById(int id)
	{
		var uri = new Uri(_baseUri, $"lookup.php?i={id}");

		var json = _callApi(uri);

		return DeserializeFoodObject(json);
	}

	private string? CallApi(Uri uri)
	{
		var client = new HttpClient();

		var response = client.GetAsync(uri).Result;

		//response.EnsureSuccessStatusCode();

		return response.Content.ReadAsStringAsync().Result;
	}

	private Food? DeserializeFoodObject(string json)
	{
		var food = (JObject)JObject.Parse(json)["meals"][0];
		if (food is null) return null;

		var ingredients = new List<Ingredient>();
		var counter = 1;
		while (!string.IsNullOrEmpty((string)food[$"strIngredient{counter}"]))
		{
			ingredients.Add(new Ingredient()
			{
				Name = (string)food[$"strIngredient{counter}"],
				Measure = (string)food[$"strMeasure{counter}"]
			});
			counter++;
		}

		return new Food()
		{
			Id = (int)food["idMeal"],
			Name = (string)food["strMeal"],
			DrinkAlternate = (string)food["strDrinkAlternate"],
			Group = new FoodGroup()
			{
				Name = (string)food["strCategory"]
			},
			Area = new Area()
			{
				Name = (string)food["strArea"]
			},
			Recipe = (string)food["strInstructions"],
			Thumbnail = (Uri)food["strMealThumb"],
			Tags = (string)food["strTags"],
			Youtube = (Uri)food["strYoutube"],
			Ingredients = ingredients,
			Source = (Uri)food["strSource"],
			ImageSource = (Uri)food["strImageSource"],
			CreativeCommons = (string)food["strCreativeCommonsConfirmed"],
			DateModified = DateTime.TryParse((string)food["dateModified"], out DateTime result)
				? result
				: null
		};
	}
}
