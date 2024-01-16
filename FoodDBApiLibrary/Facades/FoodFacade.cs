using FoodDBApiLibrary.DataAccess;
using FoodDBApiLibrary.Models;

namespace FoodDBApiLibrary.Facades;

public class FoodFacade
{
    private FoodDB _foodDB;
    private FoodApi _foodApi;

    public FoodFacade()
    {
        _foodDB = new FoodDB();
        _foodApi = new FoodApi();
    }

    public Food GetFoodById(int id)
    {
        // Kolla först i databasen
        Food food = _foodDB.GetFoodById(id);

        // Om det inte finns i databasen, hämta från API och spara i databasen
        if (food == null)
        {
            food = _foodApi.GetFoodById(id);
            _foodDB.SaveFood(food);
        }

        return food;
    }

    public Dictionary<int, string> SearchForFoodByIngredient(string ingredient)
    {
        // Använd databasen för att söka efter mat baserat på ingrediens
        return _foodDB.SearchForFoodByIngredient(ingredient);
    }

    public List<string> GetAllIngredients()
    {
        // Hämta alla ingredienser från databasen
        return _foodDB.GetAllIngredients();


    public List<string> FilterIngredientList(string startsWith)
    {
        // Filtrera ingredienslistan baserat på givet prefix
        List<string> allIngredients = _foodDB.GetAllIngredients();
        return allIngredients.Where(ingredient => ingredient.StartsWith(startsWith)).ToList();
    }
  }
}
