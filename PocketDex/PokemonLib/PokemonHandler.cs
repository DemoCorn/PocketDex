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

        public async Task asyncLoad()
		{
            if (Directory.Exists("PocketDex/Pokemon")) {
                string[] Entries = Directory.GetFiles("PocketDex/Pokemon");
                if (Entries.Length != 0)
                {
                    foreach (string PKmon in Entries)
                    {
                        try
                        {
                            using (StreamReader reader = new StreamReader(PKmon))
                            {
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
            BasicPokemonHandler PokemonBasics = new BasicPokemonHandler();

            var httpClient = HttpClientFactory.Create();

            try
            {
                string data = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=1118");

                PokemonBasics = JsonConvert.DeserializeObject<BasicPokemonHandler>(data);
            }
            catch
            {
                Console.WriteLine("error");
            }

            PokemonBasics.results.RemoveAll(ContainsChar);
            PokemonBasics.count = PokemonBasics.results.Count;

            Directory.CreateDirectory("PocketDex");
            Directory.CreateDirectory("PocketDex/Pokemon");

            foreach (BasicPokemon PKmon in PokemonBasics.results) {
                try
                {
                    string data = await httpClient.GetStringAsync(PKmon.url);

                    Add(JsonConvert.DeserializeObject<Pokemon>(data));

                    //Console.WriteLine("Finished Getting Pokemon " + PKmon.name);
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }

            Save();
        }
        public void Save()
        {
            foreach (Pokemon PKmon in this)
            {
                try
                {
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
    }
}
