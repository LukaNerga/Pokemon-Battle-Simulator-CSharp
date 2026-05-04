
namespace PokemonBattleSimulatorGUI
{
    public static class GameManager
    {
        public static Pokemon? PlayerPokemon { get; set; }
        public static int CurrentLevel { get; set; } = 1;

        public static void StartNewGame()
        {
            PlayerPokemon = null;
            CurrentLevel = 1;
        }

        private static Random random = new Random();

        public static Pokemon GetEnemyForLevel(int level)
        {
            List<Pokemon> enemies = new List<Pokemon>();

            enemies.Add(new Pokemon("Vulpix", 90, 16, "Fire"));
            enemies.Add(new Pokemon("Psyduck", 100, 18, "Water"));
            enemies.Add(new Pokemon("Oddish", 110, 19, "Grass"));
            enemies.Add(new Pokemon("Pidgey", 95, 17, "Flying"));
            enemies.Add(new Pokemon("Geodude", 120, 16, "Rock"));
            enemies.Add(new Pokemon("Machop", 115, 20, "Fighting"));

            if (level == 4)
            {
                return new Pokemon("Pikachu Boss", 130, 24, "Electric");
            }

            int index = random.Next(enemies.Count);
            return enemies[index];
        }

        public static string GetLevelName(int level)
        {
            if (level == 1) return "Level 1";
            if (level == 2) return "Level 2";
            if (level == 3) return "Level 3";
            return "Final Boss - Pikachu";
        }
    }
}
