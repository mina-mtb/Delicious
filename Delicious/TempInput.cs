namespace ConsoleApplication;
internal static class TempInput
{
	public static string LetterByLetterInput(string previous)
	{
		var input = Console.ReadKey();
		var key = input.Key;
		if (key >= ConsoleKey.A && key <= ConsoleKey.Z)
		{
			return previous + input.KeyChar;
		}
		else if (key == ConsoleKey.Backspace && previous.Length > 0)
		{
			return previous[0..(previous.Length - 1)];
		}
		else
		{
			return previous;
		}
	}
}
