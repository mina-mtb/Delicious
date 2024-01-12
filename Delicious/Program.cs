using ConsoleApplication;

namespace Delicious;

internal class Program
{
	static void Main(string[] args)
	{
		List<string> ingr = new()
		{
			"Chicken",
			"Tomato",
			"Cheese",
			"Garlic",
			"Olive Oil",
			"Salt",
			"Salmon",
			"Potato",
			"Pepper",
			"Beef",
			"Rice",
			"Soy Sauce",
			"Carrot",
			"Broccoli",
			"Chili"
		};

		var m = new TempMenu();
		m.FindIngredient(ingr);
	}
}
