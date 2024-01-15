using Newtonsoft.Json.Linq;
using se.dsve.Starapi.api;
using Se.Dsve.Starapi.Classes;
using Se.Dsve.Starapi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Menu
    {       
    
        private readonly InputHelper inputHelper = new();

        public async Task DisplayMenuAsync()
        {
            bool quit = false;
            while (!quit)
            {
                int choice = inputHelper.PromptUserAndGetInt(MenuText());
                if (choice == 6)
                {
                    quit = true;
                }
                else if (choice is > 0 and < 6)
                {
                    Console.WriteLine($"Fetching data for character {choice}...");
                    // get ingredient of Api and put in data base
                    switch (choice)
                    {

                        case 1:
                            {
                                Console.WriteLine("Vegan food");
                                // get vegans food of Api and put in the list of foods                            and show in console
                                Console.WriteLine("Please enter the first letter of                           ingredient");
                                var firstLetterOfFood = Console.ReadLine();
                                // filter by firsrLetterOfFood in list of foods and show                       in console like menu(choos number of food)
                                var choiceFoodNumber = Console.ReadLine();
                                // get food of Api and put in the list of foods                               and show in console

                                break;
                            }
                        case 2:
                            Console.WriteLine("vegetarian food");
                            break;
                        case 3:
                            Console.WriteLine("Sea food");
                            break;
                        case 4:
                            Console.WriteLine("chicken food");
                            break;
                        case 5:
                            Console.WriteLine("meat food");
                            break;
                        default:
                            Console.WriteLine("Invalid selection, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection, please try again.");
                }
            }
            inputHelper.CloseScanner();
        }

        private static string MenuText()
        {
            return @"
Välj en karaktär att hämta information om:
1. Vegan food
2. vegetarian food
3. Sea food
4. chicken food
5. meat food 
6. Quit
Enter your choice (1-6): ";
        }

        public static async Task<CharacterData?> CallApiAsync(int val)
        {
            
            try
            {
                ApiHandler apiHandler = new ApiHandler();
                string apiUrl = $"https://swapi.dev/api/people/{Convert.ToString(val)}";
                // Fetch character data
                JObject? characterResponse = await apiHandler.GetDataAsync(apiUrl);

                if (characterResponse != null)
                {
                    // Extract data from the JSON response
                    string name = characterResponse?.SelectToken("name")?.ToString(),
                    birthYear = characterResponse?.SelectToken("birth_year")?.ToString(),
                    homeworldUrl = characterResponse?.SelectToken("homeworld")?.ToString();

                    // Fetch data from the homeworld URL
                    JObject? homeworldResponse = await apiHandler.GetDataAsync(homeworldUrl);

                    string homeworld = homeworldResponse?.SelectToken("name")?.ToString() ?? "Ingen hemvärld";

                    string populationTemp = homeworldResponse?.SelectToken("population")?.ToString() ?? "unkown";

                    long population = 0;
                    try
                    {
                        population = long.Parse(populationTemp);
                    }
                    catch
                    {
                        Console.WriteLine("Could not parse population");
                    }

                    // Fetch starships data
                    JArray? starshipsArray = characterResponse?.SelectToken("starships") as JArray;
                    string? starship = starshipsArray != null && starshipsArray.Any()
                        ? string.Join("\r\n", starshipsArray.Select(starshipToken => starshipToken?.ToString()))
                        : "Inget skepp";

                    // Create a CharacterData object
                    CharacterData? characterData = new CharacterData("", "", -1, "", "");
                    characterData = new CharacterData(name, homeworld, population, birthYear, starship);

                    return characterData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        private static void PrintCharacterData(CharacterData characterData)
        {
            Console.WriteLine(" : " + characterData.Name);
                        
        }
    }




}

