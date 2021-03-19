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

			Title = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PKMon.name); ;

			TypeBlock.Text = "";
			foreach (PokemonLib.Type TypeName in PKMon.types)
			{
				TypeBlock.Text += TypeName.type.name + "/";
			}
			TypeBlock.Text = TypeBlock.Text.Remove(TypeBlock.Text.Length - 1);

			HP.Text = "HP: " + PKMon.stats[0].base_stat;
			Atk.Text = "Attack: " + PKMon.stats[1].base_stat;
			Def.Text = "Defense: " + PKMon.stats[2].base_stat;
			SAtk.Text = "Sp. Atk: " + PKMon.stats[3].base_stat;
			SDef.Text = "Sp. Def: " + PKMon.stats[4].base_stat;
			Spd.Text = "Speed: " + PKMon.stats[5].base_stat;

			List<float> Matchups = PKMon.GetMatchups();
			
			Normal.Text += Matchups[0].ToString();
			Fire.Text += Matchups[1].ToString();
			Water.Text += Matchups[2].ToString();
			Grass.Text += Matchups[3].ToString();
			Fighting.Text += Matchups[4].ToString();
			Electric.Text += Matchups[5].ToString();
			Flying.Text += Matchups[6].ToString();
			Rock.Text += Matchups[7].ToString();
			Ground.Text += Matchups[8].ToString();
			Psychic.Text += Matchups[9].ToString();
			Dark.Text += Matchups[10].ToString();
			Ghost.Text += Matchups[11].ToString();
			Bug.Text += Matchups[12].ToString();
			Poison.Text += Matchups[13].ToString();
			Ice.Text += Matchups[14].ToString();
			Steel.Text += Matchups[15].ToString();
			Dragon.Text += Matchups[16].ToString();
			Fairy.Text += Matchups[17].ToString();

			PokemonPicture.Source = new BitmapImage(new Uri(PKMon.sprites.front_default));

			lbAbilites.ItemsSource = PKMon.abilities;

			List<string> PokemonMoves = new List<string>();

			foreach (Move mv in PKMon.moves)
			{
				PokemonMoves.Add(mv.ToString());
			}

			List<string> LevelMoves = new List<string>();
			List<string> TMMoves = new List<string>();
			List<string> TutorMoves = new List<string>();
			List<string> EggMoves = new List<string>();

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

			lbMoves.ItemsSource = LevelMoves;
			lbMachineMoves.ItemsSource = TMMoves;
			lbTutorMoves.ItemsSource = TutorMoves;
			lbEggMoves.ItemsSource = EggMoves;

			List<string> PokemonEncounters = PKMon.GetEncounters();
			lbLocations.ItemsSource = PokemonEncounters;
		}
	}
}
