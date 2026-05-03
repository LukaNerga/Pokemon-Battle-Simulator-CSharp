using System;
using System.Collections.Generic;

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

        // Required for IComparable — sorts by Attack (Damage)
        public int CompareTo(Pokemon other)
        {
            if (other == null)
            {
                return 1;
            }
            return this.Attack.CompareTo(other.Attack);
        }

        // Comparer for sorting by HP
        public class ByHPComparer : IComparer<Pokemon>
        {
            public int Compare(Pokemon x, Pokemon y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return x.MaxHP.CompareTo(y.MaxHP);
            }
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
