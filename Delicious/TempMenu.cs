namespace ConsoleApplication;
internal class TempMenu
{
	public string FindIngredient(List<string> ingredients)
	{
		bool isSearching = true;
		string search = "";
		Console.Write($"Search: {search}");
		while (isSearching)
		{
			search = TempInput.LetterByLetterInput(search);
			Console.Clear();
			Console.Write($"Search: {search}");
			Console.WriteLine();
			foreach (var item in ingredients.Where(x => x.ToLower().StartsWith(search.ToLower())))
			{
				Console.WriteLine(item);
			}
		}
		return search;
	}
}
