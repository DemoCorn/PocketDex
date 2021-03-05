Project Concept:
Intro:
PocketDex is intended to be an out of game replacement to the PokeDex in the Pokemon main series games that covers a far wider range of information. While the PokeDex is usually fine for newer players, intermediate and advanced players often need to look elsewhere for the information they require. Currently there are two major places this information comes from Bulbapedia, and Serebii. Bulbapedia has a major issue of hosting all information on a given pokemon on one page. While this is useful for general purposes, it can be annoying when using it as a PokeDex replacement as in that context you do not care about tv show appearances, abilities in other games, or anything else like that. Serebii is currently the largest website leaning fully into the idea of being a PokeDex replacement. They’re information however can often be hard to read, spread across multiple pages, and organized seemingly randomly. The idea behind the PocketDex is to combine Bulbapedias way of sorting and presenting information, while only displaying what a player would want to see, similar to Serebii. It will also have more ways to filter for what pokemon you are looking for then Bulbapedia, Serebii, or the PokeDex.

Who is it for:
Intermediate pokemon players. Newer players would mostly get bogged down by information overload should they use it. In order to keep the page clean, things like height, weight, gender distribution, egg group, capture rate, classification, experience growth, and wild held items will be left out, which may alienate very experienced players.

File Format:
In a base design the PocketDex does not output anything. It does however take input in one of two ways. When information it does not have is called it will call PokeAPI to get the required information. After this is done it will then save that information as a JSON file to be loaded later should the information be called again. All of this information is saved and organized into folders so the user can easily track all of it.

UI: 
The main window will have a listbox to display all pokemon, two combo boxes to filter by typing, and a text box to filter by name. When an item in the listbox is clicked a new page would appear showing more data on the chosen pokemon. Currently there are no plans to add UI to this screen. 

Price:
If I were to put this product out I would likely make it free and ask for donations in some way, as that is the only way to stay competitive against bulbapedia and serebii who do the same.

Timeline:

Feb 18th: Have Design Document complete
Feb 27th: Display all pokemon on the main screen with working filters
Mar 6th: Have information displayed in second window
Mar 13th: Log information rather than calling API every time
Mar 18th: Finish any touch ups or reformatting of displays needed
Extra: Have it so clicking on an ability or move shows you the description for it
