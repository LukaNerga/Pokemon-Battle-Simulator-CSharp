namespace PokemonBattleSimulatorGUI
{
    // Base class for all characters in the game.
    public class GameCharacter
    {
        public string Name { get; set; } = "";
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }

        // Virtual method, so child classes can change this info.
        public virtual string GetInfo()
        {
            return "Character: " + Name;
        }
    }
}
