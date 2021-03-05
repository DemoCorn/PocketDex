﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PocketDex
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
