namespace PokemonBattleSimulatorGUI
{
    public class Pokemon
    {
        public string Name { get; set; } = "";
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int Attack { get; set; }
        public string Type { get; set; } = "";

        public Pokemon() { }

        public Pokemon(string name, int hp, int attack, string type)
        {
            Name = name;
            MaxHP = hp;
            CurrentHP = hp;
            Attack = attack;
            Type = type;
        }

        public Pokemon Clone()
        {
            return new Pokemon(Name, MaxHP, Attack, Type)
            {
                CurrentHP = CurrentHP
            };
        }
    }
}
