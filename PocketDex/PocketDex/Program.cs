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
			// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
			var httpClient = HttpClientFactory.Create();
			BasicPokemonHandler MyPokemon = new BasicPokemonHandler();

			try
			{
				string data = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=1118");

				MyPokemon = JsonConvert.DeserializeObject<BasicPokemonHandler>(data);
			}
			catch
			{
				Console.WriteLine("error");
			}

			
			foreach (BasicPokemon Pokemon in MyPokemon.results)
			{
				if (Pokemon.name.Contains("-resolute"))
				{
					Console.WriteLine(Pokemon.name);
				}
			}

			Console.ReadLine();

			for (int i = MyPokemon.results.Count-1; i >= 0; i--)
			{
				if (MyPokemon.results[i].name.Contains("-gmax"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-mega"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-totem"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-partner"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-partner"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-eternamax"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-own-tempo"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-own-tempo"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("minior") && !MyPokemon.results[i].name.Contains("red"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-ash"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-battle-bond"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-busted"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
				if (MyPokemon.results[i].name.Contains("-resolute"))
				{
					MyPokemon.results.RemoveAt(i);
					MyPokemon.count--;
					continue;
				}
			}
			
			foreach (BasicPokemon Pokemon in MyPokemon.results)
			{
				Console.WriteLine(Pokemon.name);
			}
			
		}
	}
}
