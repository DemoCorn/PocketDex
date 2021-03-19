using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonLib
{
    public class PokemonHandler : List<Pokemon>
    {

        public PokemonHandler()
        {
        }

        public PokemonHandler(PokemonHandler ToCopy)
		{
            foreach(Pokemon Copy in ToCopy)
			{
                Add(Copy);
			}
		}

        public async Task asyncLoad()
		{
            // Check if directory exists
            if (Directory.Exists("PocketDex/Pokemon")) {
                // Getting all files in Pokemon
                string[] Entries = Directory.GetFiles("PocketDex/Pokemon");
                if (Entries.Length != 0)
                {
                    foreach (string PKmon in Entries)
                    {
                        try
                        {
                            using (StreamReader reader = new StreamReader(PKmon))
                            {
                                // Deserialize the object
                                Add(JsonConvert.DeserializeObject<Pokemon>(reader.ReadToEnd()));
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                {
                    Task t = Getlist();
                    await t;
                }
            }
            else
            {
                Task t = Getlist();
                await t;
            }
		}

        public async Task Getlist()
        {
            // Create Pokemon Basic
            BasicPokemonHandler PokemonBasics = new BasicPokemonHandler();

            var httpClient = HttpClientFactory.Create();

            try
            {
                // Call the data for all pokemon (this is hard coded, but faster on first load)
                string data = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=1118");

                PokemonBasics = JsonConvert.DeserializeObject<BasicPokemonHandler>(data);
            }
            catch
            {
                Console.WriteLine("error");
            }

            // Culls the list of uneeded Pokemon
            PokemonBasics.results.RemoveAll(ContainsChar);
            PokemonBasics.count = PokemonBasics.results.Count;

            foreach (BasicPokemon PKmon in PokemonBasics.results) {
                try
                {
                    // Call api for all pokemon in the pokemonbasic
                    string data = await httpClient.GetStringAsync(PKmon.url);

                    Add(JsonConvert.DeserializeObject<Pokemon>(data));
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }

            Save();
        }
        private void Save()
        {
            // Create needed directories
            Directory.CreateDirectory("PocketDex");
            Directory.CreateDirectory("PocketDex/Pokemon");

            foreach (Pokemon PKmon in this)
            {
                try
                {
                    // Make every entry into files
                    using (StreamWriter writer = new StreamWriter("PocketDex/Pokemon/" + PKmon.name + ".json"))
                    {
                        writer.Write(System.Text.Json.JsonSerializer.Serialize<Pokemon>(PKmon));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static bool ContainsChar(BasicPokemon word)
        {
            if (word.name.Contains("-gmax")
                || word.name.Contains("-mega")
                || word.name.Contains("-totem")
                || word.name.Contains("-eternamax")
                || word.name.Contains("-eternamax")
                || word.name.Contains("-own-tempo")
                || (word.name.Contains("minior") && !word.name.Contains("red"))
                || word.name.Contains("-ash")
                || word.name.Contains("-battle-bond")
                || word.name.Contains("-busted")
                || word.name.Contains("-resolute")
                || word.name.Contains("pikachu-")
                || word.name.Contains("-small")
                || word.name.Contains("-large")
                || word.name.Contains("-super")
                || word.name.Contains("-red-striped")
                )
            {
                return true;
            }
            return false;
        }

        public PokemonHandler GetOfType(string type1, string type2 = "None")
		{
            PokemonHandler HandlerToReturn = new PokemonHandler();

            // Find all pokemon with given types
            foreach (Pokemon PKmon in this)
            {
                if (PKmon.HasType(type1) && PKmon.HasType(type2))
				{
                    HandlerToReturn.Add(PKmon);
				}
            }

            return HandlerToReturn;
		}

        public PokemonHandler PokemonContainsName(string name)
        {
            PokemonHandler HandlerToReturn = new PokemonHandler();

            name = name.ToLower();

            // Find all pokemon with given characters in the name
            foreach (Pokemon PKmon in this)
            {
                if (PKmon.name.Contains(name))
				{
                    HandlerToReturn.Add(PKmon);
                }
            }

            return HandlerToReturn;
        }
    }
}
