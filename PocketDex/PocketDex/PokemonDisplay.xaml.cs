using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokemonLib;

namespace PocketDex
{
	/// <summary>
	/// Interaction logic for PokemonDisplay.xaml
	/// </summary>
	public partial class PokemonDisplay : Window
	{
		public PokemonDisplay(Pokemon PKMon)
		{
			InitializeComponent();

			// Set title to pokemon name
			Title = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PKMon.name); ;

			// Set the Typing
			TypeBlock.Text = "";
			foreach (PokemonLib.Type TypeName in PKMon.types)
			{
				TypeBlock.Text += System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TypeName.type.name) + "/";
			}
			TypeBlock.Text = TypeBlock.Text.Remove(TypeBlock.Text.Length - 1);

			// Set Stats
			HP.Text = "HP: " + PKMon.stats[0].base_stat;
			Atk.Text = "Attack: " + PKMon.stats[1].base_stat;
			Def.Text = "Defense: " + PKMon.stats[2].base_stat;
			SAtk.Text = "Sp. Atk: " + PKMon.stats[3].base_stat;
			SDef.Text = "Sp. Def: " + PKMon.stats[4].base_stat;
			Spd.Text = "Speed: " + PKMon.stats[5].base_stat;

			// Create Matchups list
			List<float> Matchups = PKMon.GetMatchups();
			
			// Set matchups text
			Normal.Text = Matchups[0].ToString() + "x";
			Fire.Text = Matchups[1].ToString() + "x";
			Water.Text = Matchups[2].ToString() + "x";
			Grass.Text = Matchups[3].ToString() + "x";
			Fighting.Text = Matchups[4].ToString() + "x";
			Electric.Text = Matchups[5].ToString() + "x";
			Flying.Text = Matchups[6].ToString() + "x";
			Rock.Text = Matchups[7].ToString() + "x";
			Ground.Text = Matchups[8].ToString() + "x";
			Psychic.Text = Matchups[9].ToString() + "x";
			Dark.Text = Matchups[10].ToString() + "x";
			Ghost.Text = Matchups[11].ToString() + "x";
			Bug.Text = Matchups[12].ToString() + "x";
			Poison.Text = Matchups[13].ToString() + "x";
			Ice.Text = Matchups[14].ToString() + "x";
			Steel.Text = Matchups[15].ToString() + "x";
			Dragon.Text = Matchups[16].ToString() + "x";
			Fairy.Text = Matchups[17].ToString() + "x";

			// Set sprite
			PokemonPicture.Source = new BitmapImage(new Uri(PKMon.sprites.front_default));

			// Set abilities
			lbAbilites.ItemsSource = PKMon.abilities;

			// Create PokemonMoves List
			List<string> PokemonMoves = new List<string>();

			// Get the pokemon moves
			foreach (Move mv in PKMon.moves)
			{
				PokemonMoves.Add(mv.ToString());
			}

			// Create the lists that will hold the different moves
			List<string> LevelMoves = new List<string>();
			List<string> TMMoves = new List<string>();
			List<string> TutorMoves = new List<string>();
			List<string> EggMoves = new List<string>();

			// Figure out where each move should go given it's defining charecter, then remove the defining charecter
			foreach (string MoveString in PokemonMoves)
			{
				if (MoveString[MoveString.Length - 1] == 'L')
				{
					LevelMoves.Add(MoveString.Remove(MoveString.Length - 1));
				}
				else if (MoveString[MoveString.Length - 1] == 'M')
				{
					TMMoves.Add(MoveString.Remove(MoveString.Length -1));
				}
				else if (MoveString[MoveString.Length - 1] == 'T')
				{
					TutorMoves.Add(MoveString.Remove(MoveString.Length - 1));
				}
				else if (MoveString[MoveString.Length - 1] == 'E')
				{
					EggMoves.Add(MoveString.Remove(MoveString.Length - 1));
				}
			}

			// Set all the boxes for moves
			lbMoves.ItemsSource = LevelMoves;
			lbMachineMoves.ItemsSource = TMMoves;
			lbTutorMoves.ItemsSource = TutorMoves;
			lbEggMoves.ItemsSource = EggMoves;

			// Set the encounters
			List<string> PokemonEncounters = PKMon.GetEncounters();
			lbLocations.ItemsSource = PokemonEncounters;
		}
	}
}
