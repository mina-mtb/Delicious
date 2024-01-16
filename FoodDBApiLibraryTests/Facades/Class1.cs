using FoodDBApiLibrary.Facades;
using FoodDBApiLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDBApiLibraryTests.Facades
{
     private class TestApi : ApplicationId 
    {
        private readonly Dictionary<int, Food> foods = new Dictionary<int, Food>();

        public Food GetFoodById(int id)
        {
            return foods.ContainsKey(id) ? foods[id] : null;
        }

        public void AddFood(Food food)
        {
            foods[food.Id] = food;
        }
    }

    [TestMethod]
    public void GetFoodById_From_Database()
    {
        // Arrange
        var database = new TestDatabase();
        var api = new TestApi();
        var facade = new FoodFacade(database, api);
        var expectedFood = new Food { Id = 1, Name = "TestFood" };
        database.SaveFood(expectedFood);

        // Act
        var result = facade.GetFoodById(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedFood, result);
    }

    [TestMethod]
    public void GetFoodById_From_Api()
    {
        // Arrange
        var database = new TestDatabase();
        var api = new TestApi();
        var facade = new FoodFacade(database, api);
        var expectedFood = new Food { Id = 1, Name = "TestFood" };
        api.AddFood(expectedFood);

        // Act
        var result = facade.GetFoodById(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedFood, result);

        var savedFood = database.GetFoodById(1);
        Assert.IsNotNull(savedFood);
        Assert.AreEqual(expectedFood, savedFood);
    }

    [TestMethod]
    public void SearchForFoodByIngredient_In_DB()
    {
        // Arrange
        var database = new TestDatabase();
        var api = new TestApi();
        var facade = new FoodFacade(database, api);
        var expectedDictionary = new Dictionary<int, string> { { 1, "TestFood" } };
        var food = new Food { Id = 1, Name = "TestFood", Ingredients = new List<string> { "Ingredient1" } };
        database.SaveFood(food);

        // Act
        var result = facade.SearchForFoodByIngredient("Ingredient1");

        // Assert
        CollectionAssert.AreEqual(expectedDictionary, result);
    }

    [TestMethod]
    public void GetAllIngredients_From_DB()
    {
        // Arrange
        var database = new TestDatabase();
        var api = new TestApi();
        var facade = new FoodFacade(database, api);
        var expectedIngredients = new List<string> { "Ingredient1", "Ingredient2" };
        var food1 = new Food { Id = 1, Name = "TestFood1", Ingredients = new List<string> { "Ingredient1" } };
        var food2 = new Food { Id = 2, Name = "TestFood2", Ingredients = new List<string> { "Ingredient2" } };
        database.SaveFood(food1);
        database.SaveFood(food2);

        // Act
        var result = facade.GetAllIngredients();

        // Assert
        CollectionAssert.AreEqual(expectedIngredients, result);
    }

    [TestMethod]
    public void FilterIngredients_From_DB()
    {
        // Arrange
        var database = new TestDatabase();
        var api = new TestApi();
        var facade = new FoodFacade(database, api);
        var allIngredients = new List<string> { "Test1", "Test2" };
        var expectedFilteredIngredients = new List<string> { "Test" };
        var food1 = new Food { Id = 1, Name = "Test Test", Ingredients = new List<string> { "Test1" } };
        database.SaveFood(food1);
      
        // Act
        var result = facade.FilterIngredientList("T");

        // Assert
        CollectionAssert.AreEqual(expectedFilteredIngredients, result);
    }
}


