using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonLib;

namespace PocketDex
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private PokemonHandler myPokemon;
		private PokemonHandler displayPokemon;
		
		List<string> type = new List<string> { 
			"None",
			"Normal",
			"Grass",
			"Fire",
			"Water",
			"Fighting",
			"Flying",
			"Poison",
			"Ground",
			"Rock",
			"Bug",
			"Ghost",
			"Electric",
			"Psychic",
			"Ice",
			"Dragon",
			"Dark",
			"Steel",
			"Fairy"
		};

		public MainWindow()
		{
			InitializeComponent();
			myPokemon = new PokemonHandler();
			Task.Run(() => myPokemon.asyncLoad()).Wait();
			displayPokemon = new PokemonHandler(myPokemon);
			lbPokemon.ItemsSource = displayPokemon;
			lbPokemon.Items.Refresh();
			
			cbTypes1.ItemsSource = type;
			cbTypes2.ItemsSource = type;
		}

		private void FilterByType(object sender, SelectionChangedEventArgs e)
		{
			if (cbTypes1.SelectedItem != null && cbTypes2.SelectedItem != null)
			{
				displayPokemon.Clear();
				displayPokemon.AddRange(myPokemon.GetOfType(cbTypes1.SelectedItem.ToString(), cbTypes2.SelectedItem.ToString()));
				PokemonSearch();
				lbPokemon.ItemsSource = displayPokemon;
				lbPokemon.Items.Refresh();
			}
		}

		private void FilterByType()
		{
			if (cbTypes1.SelectedItem != null && cbTypes2.SelectedItem != null)
			{
				PokemonHandler SaveHandler = new PokemonHandler();
				SaveHandler.AddRange(displayPokemon.GetOfType(cbTypes1.SelectedItem.ToString(), cbTypes2.SelectedItem.ToString()));
				displayPokemon.Clear();
				displayPokemon.AddRange(SaveHandler);
				lbPokemon.ItemsSource = displayPokemon;
				lbPokemon.Items.Refresh();
			}
		}

		private void TesterFunction(object sender, SelectionChangedEventArgs e)
		{
			cbTypes1.SelectedIndex = 0;
		}

		private void WindowOpening(object sender, RoutedEventArgs e)
		{
			cbTypes1.SelectedIndex = 0;
			cbTypes2.SelectedIndex = 0;
		}

		private void PokemonSearch(object sender, TextChangedEventArgs e)
		{
			displayPokemon.Clear();
			displayPokemon.AddRange(myPokemon.PokemonContainsName(tbSearchBox.Text));
			FilterByType();
			lbPokemon.ItemsSource = displayPokemon;
			lbPokemon.Items.Refresh();
		}

		private void PokemonSearch()
		{
			PokemonHandler SaveHandler = new PokemonHandler();
			SaveHandler.AddRange(displayPokemon.PokemonContainsName(tbSearchBox.Text));
			displayPokemon.Clear();
			displayPokemon.AddRange(SaveHandler);
			lbPokemon.ItemsSource = displayPokemon;
			lbPokemon.Items.Refresh();
		}
	}
}
