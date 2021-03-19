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
    public class Pokemon
    {
        public List<Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public List<Form> forms { get; set; }
        public List<GameIndice> game_indices { get; set; }
        public int height { get; set; }
        public List<HeldItem> held_items { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public List<Move> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public List<Stat> past_types { get; set; }
        public Species species { get; set; }
        public Sprites sprites { get; set; }
        public List<Stat> stats { get; set; }
        public List<Type> types { get; set; }
        public int weight { get; set; }

        public override string ToString()
		{
            //string returnvalue = name + " " + types[0].type.name;

            string returnvalue = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name) + " " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[0].type.name);

            if (types.Count == 2)
			{
                returnvalue += "/" + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[1].type.name);
            }
            return returnvalue;
		}

        public bool HasType(string TypeToTest)
		{
            TypeToTest = TypeToTest.ToLower();
            if (TypeToTest == "none")
			{
                return true;
			}
            foreach (Type TypeTester in types)
			{
                if (TypeTester.type.name == TypeToTest)
				{
                    return true;
				}
			}
            return false;
		}

        public List<float> GetMatchups()
		{
            if (!Directory.Exists("PocketDex/Types"))
            {
                Directory.CreateDirectory("PocketDex/Types");
            }

            List<float> Matchups = new List<float> {
                    1, // 0: Normal
                    1, // 1: Fire
                    1, // 2: Water
                    1, // 3: Grass
                    1, // 4: Fighting
                    1, // 5: Electric
                    1, // 6: Flying
                    1, // 7: Rock
                    1, // 8: Ground
                    1, // 9: Psychic
                    1, // 10: Dark
                    1, // 11: Ghost
                    1, // 12: Bug
                    1, // 13: Poison
                    1, // 14: Ice
                    1, // 15: Steel
                    1, // 16: Dragon
                    1 // 17: Fairy
                };
            List<float> Save;

            foreach (Type TestType in types)
            {
                if (!File.Exists("PocketDex/Types/" + TestType.type.name + ".json"))
				{
                    CreateMatchups();
				}
                using (StreamReader reader = new StreamReader("PocketDex/Types/" + TestType.type.name + ".json"))
                {
                    Save = (JsonConvert.DeserializeObject<List<float>>(reader.ReadToEnd()));
                }
                for(int i = 0; i < Matchups.Count; i++)
				{
                    Matchups[i] *= Save[i];
				}
            }
            return Matchups;
        }

        private void CreateMatchups()
		{
            List<float> Matchups;
            foreach (Type TestType in types)
            {
                Matchups = new List<float> {
                    1, // 0: Normal
                    1, // 1: Fire
                    1, // 2: Water
                    1, // 3: Grass
                    1, // 4: Fighting
                    1, // 5: Electric
                    1, // 6: Flying
                    1, // 7: Rock
                    1, // 8: Ground
                    1, // 9: Psychic
                    1, // 10: Dark
                    1, // 11: Ghost
                    1, // 12: Bug
                    1, // 13: Poison
                    1, // 14: Ice
                    1, // 15: Steel
                    1, // 16: Dragon
                    1 // 17: Fairy
                };

                if (TestType.type.name == "normal")
                {
                    Matchups[4] = 2f;
                    Matchups[11] = 0f;
                }
                else if (TestType.type.name == "fire")
                {
                    Matchups[12] = 0.5f;
                    Matchups[17] = 0.5f;
                    Matchups[1] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[14] = 0.5f;
                    Matchups[15] = 0.5f;
                    Matchups[8] = 2f;
                    Matchups[7] = 2f;
                    Matchups[2] = 2f;
                }
                else if (TestType.type.name == "water")
                {
                    Matchups[1] = 0.5f;
                    Matchups[14] = 0.5f;
                    Matchups[2] = 0.5f;
                    Matchups[5] = 2f;
                    Matchups[3] = 2f;
                }
                else if (TestType.type.name == "grass")
                {
                    Matchups[5] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[8] = 0.5f;
                    Matchups[2] = 0.5f;
                    Matchups[12] = 2f;
                    Matchups[1] = 2f;
                    Matchups[6] = 2f;
                    Matchups[14] = 2f;
                    Matchups[13] = 2f;
                }
                else if (TestType.type.name == "fighting")
                {
                    Matchups[12] = 0.5f;
                    Matchups[10] = 0.5f;
                    Matchups[7] = 0.5f;
                    Matchups[17] = 2f;
                    Matchups[6] = 2f;
                    Matchups[9] = 2f;
                }
                else if (TestType.type.name == "electric")
                {
                    Matchups[5] = 0.5f;
                    Matchups[6] = 0.5f;
                    Matchups[15] = 0.5f;
                    Matchups[8] = 2f;
                }
                else if (TestType.type.name == "flying")
                {
                    Matchups[12] = 0.5f;
                    Matchups[4] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[5] = 2f;
                    Matchups[14] = 2f;
                    Matchups[7] = 2f;
                    Matchups[8] = 0f;
                }
                else if (TestType.type.name == "rock")
                {
                    Matchups[1] = 0.5f;
                    Matchups[6] = 0.5f;
                    Matchups[0] = 0.5f;
                    Matchups[13] = 0.5f;
                    Matchups[4] = 2f;
                    Matchups[3] = 2f;
                    Matchups[8] = 2f;
                    Matchups[15] = 2f;
                    Matchups[2] = 2f;
                }
                else if (TestType.type.name == "ground")
                {
                    Matchups[13] = 0.5f;
                    Matchups[7] = 0.5f;
                    Matchups[3] = 2f;
                    Matchups[14] = 2f;
                    Matchups[2] = 2f;
                    Matchups[5] = 0f;
                }
                else if (TestType.type.name == "psychic")
                {
                    Matchups[4] = 0.5f;
                    Matchups[9] = 0.5f;
                    Matchups[12] = 0.5f;
                    Matchups[10] = 0.5f;
                    Matchups[11] = 0.5f;
                }
                else if (TestType.type.name == "dark")
                {
                    Matchups[10] = 0.5f;
                    Matchups[11] = 0.5f;
                    Matchups[12] = 2f;
                    Matchups[17] = 2f;
                    Matchups[4] = 2f;
                    Matchups[9] = 0f;
                }
                else if (TestType.type.name == "ghost")
                {
                    Matchups[12] = 0.5f;
                    Matchups[13] = 0.5f;
                    Matchups[10] = 2f;
                    Matchups[11] = 2f;
                    Matchups[1] = 0f;
                    Matchups[4] = 0f;
                }
                else if (TestType.type.name == "bug")
                {
                    Matchups[4] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[8] = 0.5f;
                    Matchups[1] = 2f;
                    Matchups[6] = 2f;
                    Matchups[7] = 2f;
                }
                else if (TestType.type.name == "poison")
                {
                    Matchups[4] = 0.5f;
                    Matchups[13] = 0.5f;
                    Matchups[12] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[17] = 0.5f;
                    Matchups[8] = 2f;
                    Matchups[9] = 2f;
                }
                else if (TestType.type.name == "ice")
                {
                    Matchups[14] = 0.5f;
                    Matchups[4] = 2f;
                    Matchups[1] = 2f;
                    Matchups[7] = 2f;
                    Matchups[15] = 2f;
                }
                else if (TestType.type.name == "steel")
                {
                    Matchups[12] = 0.5f;
                    Matchups[16] = 0.5f;
                    Matchups[17] = 0.5f;
                    Matchups[6] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[14] = 0.5f;
                    Matchups[1] = 0.5f;
                    Matchups[9] = 0.5f;
                    Matchups[7] = 0.5f;
                    Matchups[15] = 0.5f;
                    Matchups[4] = 2f;
                    Matchups[1] = 2f;
                    Matchups[8] = 2f;
                    Matchups[13] = 0f;
                }
                else if (TestType.type.name == "dragon")
                {
                    Matchups[5] = 0.5f;
                    Matchups[1] = 0.5f;
                    Matchups[2] = 0.5f;
                    Matchups[3] = 0.5f;
                    Matchups[16] = 2f;
                    Matchups[17] = 2f;
                    Matchups[14] = 2f;
                }
                else if (TestType.type.name == "fairy")
                {
                    Matchups[12] = 0.5f;
                    Matchups[10] = 0.5f;
                    Matchups[4] = 0.5f;
                    Matchups[13] = 2f;
                    Matchups[15] = 2f;
                    Matchups[16] = 0f;
                }

                try
                {
                    using (StreamWriter writer = new StreamWriter("PocketDex/Types/" + TestType.type.name + ".json"))
                    {
                        writer.Write(System.Text.Json.JsonSerializer.Serialize<List<float>>(Matchups));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

		}

        public List<string> GetEncounters()
		{
            PokemonLocations Encounters = new PokemonLocations();
            Encounters.Load(id, location_area_encounters);
            List<string> EncounterList = new List<string>();

            foreach (PokemonLocation Location in Encounters)
			{
                EncounterList.Add(Location.location_area.name);
			}

            return EncounterList;
		}
    }
}
