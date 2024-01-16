using FoodDBApiLibrary.Models;

namespace FoodDBApiLibrary.DataAccess;
public interface IApi;
public class FoodApi : IApilinking
{
    private readonly Dictionary<int, Food> foods = new Dictionary<int, Food>();

    public Food GetFoodById(int id)
    {
        return foods.ContainsKey(id) ? foods[id] : null;
    }
}
