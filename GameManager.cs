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

        public static Pokemon GetEnemyForLevel(int level)
        {
            if (level == 1) return new Pokemon("Vulpix", 90, 16, "Fire");
            if (level == 2) return new Pokemon("Psyduck", 100, 18, "Water");
            if (level == 3) return new Pokemon("Oddish", 110, 19, "Grass");
            return new Pokemon("Pikachu Boss", 130, 24, "Electric");
        }

        public static string GetLevelName(int level)
        {
            if (level == 1) return "Level 1 - Fire Opponent";
            if (level == 2) return "Level 2 - Water Opponent";
            if (level == 3) return "Level 3 - Grass Opponent";
            return "Final Boss - Pikachu";
        }
    }
}