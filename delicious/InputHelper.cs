namespace Se.Dsve.Starapi.Helpers;

using System;

public class InputHelper
{
    public InputHelper()
    {
        // Konstruktorn behöver inte göra något speciellt i C# versionen
    }

    private string ReadString()
    {
        // Läs en sträng från konsolen
        return Console.ReadLine();
    }

    private int ReadInt()
    {
        // Läs ett heltal från konsolen
        int number = 0;
        bool valid = false;
        while (!valid)
        {
            if (int.TryParse(Console.ReadLine(), out number))
            {
                valid = true;
            }
            else
            {
                Console.WriteLine("Det är inte ett giltigt heltal. Försök igen.");
            }
        }
        return number;
    }

    public void Close()
    {
        // I C# behövs inte explicit stängning av Console
    }

    public void CloseScanner()
    {
        // I C# behövs inte explicit stängning av Console
    }

    public string PromptUserAndGetString(string message)
    {
        Console.Write(message);
        return ReadString();
    }

    public int PromptUserAndGetInt(string message)
    {
        Console.Write(message);
        return ReadInt();
    }
}