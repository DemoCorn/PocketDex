using System;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PocketDex
{
	class Program
	{
		static async Task Main(string[] args)
		{
			PokemonHandler MyPokemon = new PokemonHandler();
			Task t = MyPokemon.asyncLoad();
			await t;

			foreach (Pokemon PKmon in MyPokemon.Pokemonlist)
			{
				Console.WriteLine(PKmon.name);
			}
		}
	}
}
