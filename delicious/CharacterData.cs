namespace Se.Dsve.Starapi.Classes;

public class CharacterData
{
    // Konstruktor
    public CharacterData(string name, string homeWorld, long population, string birthYear, string starship)
    {
        // Todo: Sätt attributen till värdena som skickas in
        // Name, HomeWorld, Population, BirthYear, Starship
        Name = name;
        HomeWorld = homeWorld;
        Population = population;
        BirthYear = birthYear;
        Starship = starship;
    }

    // Getter- och setter-metoder
    public string Name { get; set; }

    public string HomeWorld { get; set; }

    public long Population { get; set; }

    public string BirthYear { get; set; }

    public string Starship { get; set; }

    public override string ToString()
    {
        return $"CharacterData{{name='{Name}', homeWorld='{HomeWorld}', population={Population}, birthYear='{BirthYear}', starship='{Starship}'}}";
    }
}