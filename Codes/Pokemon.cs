using System;

namespace PokemonBattleSimulatorGUI
{
    // Pokemon class inherits from GameCharacter and implements IComparable
    public class Pokemon : GameCharacter, IComparable<Pokemon>
    {
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

        // Polymorphism: override base method
        public override string GetInfo()
        {
            return "Pokemon: " + Name +
                   "\nType: " + Type +
                   "\nHP: " + CurrentHP + "/" + MaxHP +
                   "\nAttack: " + Attack;
        }

        // Required for IComparable
        public int CompareTo(Pokemon other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Attack.CompareTo(other.Attack);
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
