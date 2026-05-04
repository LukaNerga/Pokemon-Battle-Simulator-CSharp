using System.Collections.Generic;

namespace PokemonBattleSimulatorGUI
{
    public static class PokemonFactory
    {
        public static List<Pokemon> GetAllPokemon()
        {
            // Main list of pokemon player can choose from.
            return new List<Pokemon>
            {
                new Pokemon("Charmander", 100, 20, "Fire"),
                new Pokemon("Squirtle", 110, 18, "Water"),
                new Pokemon("Bulbasaur", 105, 19, "Grass"),
                new Pokemon("Pikachu", 95, 22, "Electric"),
                new Pokemon("Eevee", 100, 18, "Normal"),
                new Pokemon("Pidgey", 90, 17, "Flying"),
                new Pokemon("Geodude", 120, 16, "Rock"),
                new Pokemon("Gastly", 85, 23, "Ghost"),
                new Pokemon("Machop", 115, 20, "Fighting"),
                new Pokemon("Dratini", 108, 21, "Dragon")
            };
        }
    }
}
