using FoodDBApiLibrary.Models;

namespace FoodDBApiLibrary.DataAccess;

public interface IDatabaseAccess;

public class FoodDatabase : IDatabase
{
    private readonly Dictionary<int, Food> foods = new Dictionary<int, Food>();
    private readonly Dictionary<string, List<int>> ingredientsIndex = new Dictionary<string, List<int>>();

    public Food GetFoodById(int id)
    {
        return foods.ContainsKey(id) ? foods[id] : null;
    }

    public void SaveFood(Food food)
    {
        foods[food.Id] = food;

        // Update ingredients index
        foreach (var ingredient in food.Ingredients)
        {
            if (!ingredientsIndex.ContainsKey(ingredient))
            {
                ingredientsIndex[ingredient] = new List<int>();
            }
            ingredientsIndex[ingredient].Add(food.Id);
        }
    }

    public Dictionary<int, string> SearchForFoodByIngredient(string ingredient)
    {
        if (ingredientsIndex.ContainsKey(ingredient))
        {
            return ingredientsIndex[ingredient].ToDictionary(id => id, id => foods[id].Name);
        }

        return new Dictionary<int, string>();
    }

    public List<string> GetAllIngredients()
    {
        return ingredientsIndex.Keys.ToList();
    }
}



