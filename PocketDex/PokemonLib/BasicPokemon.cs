using System;
using System.Collections.Generic;
using System.Text; 
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PokemonLib
{
    public class BasicPokemon
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class BasicPokemonHandler
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<BasicPokemon> results { get; set; }
	}
}


    // 0: Normal
    // 1: Fire
    // 2: Water
    // 3: Grass
    // 4: Fighting
    // 5: Electric
    // 6: Flying
    // 7: Rock
    // 8: Ground
    // 9: Psychic
    // 10: Dark
    // 11: Ghost
    // 12: Bug
    // 13: Poison
    // 14: Ice
    // 15: Steel
    // 16: Dragon
    // 17: Fairy

