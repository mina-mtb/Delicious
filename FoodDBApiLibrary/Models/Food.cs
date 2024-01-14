namespace FoodDBApiLibrary.Models;
public class Food
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string? DrinkAlternate { get; set; }
	public FoodGroup Group { get; set; }
	public Area Area { get; set; }
	public string Recipe { get; set; }
    public Uri? Thumbnail { get; set; }
    public string? Tags { get; set; }
    public Uri? Youtube { get; set; }
	public List<Ingredient> Ingredients { get; set; }
	public Uri? Source { get; set; }
    public Uri? ImageSource { get; set; }
	public bool? CreativeCommons { get; set; }
    public DateTime? DateModified { get; set; }
}
