namespace FoodDBApiLibrary.DataAccess.Tests;

using FoodDBApiLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass()]
public class FoodApiTests
{
    private FoodApi _api;
    private string _baseUri;
    private string _apiTestKey;

    [TestInitialize]
    public void Setup()
    {
        _apiTestKey = "1";
        _baseUri = $"www.themealdb.com/api/json/v1/";
		_api = new FoodApi(MockCallApi ,_apiTestKey);
	}

    [TestMethod]
    [DataRow(1)]
    [DataRow(10)]
    public void GetFoodByIdTest(int id)
    {
        var actual = _api.GetFoodById(id);

        Assert.IsTrue(actual is Food);
        Assert.IsTrue(actual.Id == id);
    }

    [TestMethod]
	[DataRow(1, "1")]
	[DataRow(10, "10")]
	public void GetFoodByIdUsesCorrectApiKeyAndId(int id, string key)
    {
        var api = new FoodApi(key);

        var testUri = $"{_baseUri}{key}/lookup.php?i=";

        var actual = api.GetFoodById(id);

        Assert.IsTrue($"{testUri}{id}" == api.Uri);
    }

    [TestMethod]
    [DataRow(52850)]
    public void GetFoodByIdReturnsObjectWithRelevantPropertiesSet(int id)
    {
        var actual = _api.GetFoodById(id);

        Assert.IsFalse(string.IsNullOrWhiteSpace(actual.Name));
        Assert.IsTrue(actual.Ingredients.Count > 0);
    }

    private string? MockCallApi(string uri)
    {
        return "{\"meals\":[{\"idMeal\":\"52850\",\"strMeal\":\"Chicken Couscous\",\"strDrinkAlternate\":null,\"strCategory\":\"Chicken\",\"strArea\":\"Moroccan\",\"strInstructions\":\"Heat the olive oil in a large frying pan and cook the onion for 1-2 mins just until softened. Add the chicken and fry for 7-10 mins until cooked through and the onions have turned golden. Grate over the ginger, stir through the harissa to coat everything and cook for 1 min more.\\r\\n\\r\\nTip in the apricots, chickpeas and couscous, then pour over the stock and stir once. Cover with a lid or tightly cover the pan with foil and leave for about 5 mins until the couscous has soaked up all the stock and is soft. Fluff up the couscous with a fork and scatter over the coriander to serve. Serve with extra harissa, if you like.\",\"strMealThumb\":\"https:\\/\\/www.themealdb.com\\/images\\/media\\/meals\\/qxytrx1511304021.jpg\",\"strTags\":null,\"strYoutube\":\"https:\\/\\/www.youtube.com\\/watch?v=GZQGy9oscVk\",\"strIngredient1\":\"Olive Oil\",\"strIngredient2\":\"Onion\",\"strIngredient3\":\"Chicken Breast\",\"strIngredient4\":\"Ginger\",\"strIngredient5\":\"Harissa Spice\",\"strIngredient6\":\"Dried Apricots\",\"strIngredient7\":\"Chickpeas\",\"strIngredient8\":\"Couscous\",\"strIngredient9\":\"Chicken Stock\",\"strIngredient10\":\"Coriander\",\"strIngredient11\":\"\",\"strIngredient12\":\"\",\"strIngredient13\":\"\",\"strIngredient14\":\"\",\"strIngredient15\":\"\",\"strIngredient16\":\"\",\"strIngredient17\":\"\",\"strIngredient18\":\"\",\"strIngredient19\":\"\",\"strIngredient20\":\"\",\"strMeasure1\":\"1 tbsp\",\"strMeasure2\":\"1 chopped\",\"strMeasure3\":\"200g\",\"strMeasure4\":\"pinch\",\"strMeasure5\":\"2 tblsp \",\"strMeasure6\":\"10\",\"strMeasure7\":\"220g\",\"strMeasure8\":\"200g\",\"strMeasure9\":\"200ml\",\"strMeasure10\":\"Handful\",\"strMeasure11\":\"\",\"strMeasure12\":\"\",\"strMeasure13\":\"\",\"strMeasure14\":\"\",\"strMeasure15\":\"\",\"strMeasure16\":\"\",\"strMeasure17\":\"\",\"strMeasure18\":\"\",\"strMeasure19\":\"\",\"strMeasure20\":\"\",\"strSource\":\"https:\\/\\/www.bbcgoodfood.com\\/recipes\\/13139\\/onepan-chicken-couscous\",\"strImageSource\":null,\"strCreativeCommonsConfirmed\":null,\"dateModified\":null}]}";
	}
}