using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace PokemonLib
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class LocationArea
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ConditionValue
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Method
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EncounterDetail
    {
        public int chance { get; set; }
        public List<ConditionValue> condition_values { get; set; }
        public int max_level { get; set; }
        public Method method { get; set; }
        public int min_level { get; set; }
    }

    public class LocationVersionDetail
    {
        public List<EncounterDetail> encounter_details { get; set; }
        public int max_chance { get; set; }
        public Version version { get; set; }
    }

    public class PokemonLocation
    {
        public LocationArea location_area { get; set; }
        public List<LocationVersionDetail> version_details { get; set; }
    }

    public class PokemonLocations : List<PokemonLocation>
	{
        public void Load(int id, string url)
		{
            if (!Directory.Exists("PocketDex/EncounterTables"))
            {
                Directory.CreateDirectory("PocketDex/EncounterTables");
            }
            if (!File.Exists("PocketDex/EncounterTables/" + id + ".json"))
            {
                Task.Run(() => GetEncounters(id, url)).Wait();
            }
            else
			{
                try
                {
                    using (StreamReader reader = File.OpenText("PocketDex/EncounterTables/" + id + ".json"))
                    {
                        AddRange(JsonConvert.DeserializeObject<PokemonLocations>(reader.ReadToEnd()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task GetEncounters(int id, string url)
        {
            var httpClient = HttpClientFactory.Create();

            try
            {
                string data = await httpClient.GetStringAsync(url);

                AddRange(JsonConvert.DeserializeObject<PokemonLocations>(data));
            }
            catch
            {
                Console.WriteLine("error");
            }
            try
            {
                using (StreamWriter writer = new StreamWriter("PocketDex/EncounterTables/" + id + ".json"))
                {
                    writer.Write(System.Text.Json.JsonSerializer.Serialize(this));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }


}
