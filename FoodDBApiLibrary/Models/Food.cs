namespace FoodDBApiLibrary.Models;
public class Food
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public FoodGroup Group { get; set; }
	public List<Ingredient> Ingredients { get; set; }
	public string Recipe { get; set; }
	public Area Area { get; set; }
}
