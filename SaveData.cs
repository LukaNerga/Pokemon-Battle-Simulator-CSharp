using System;

namespace PokemonBattleSimulatorGUI
{
    public class SaveData
    {
        public int SlotNumber { get; set; }
        public DateTime SaveTime { get; set; }
        public int CurrentLevel { get; set; }
        public Pokemon? PlayerPokemon { get; set; }
    }
}