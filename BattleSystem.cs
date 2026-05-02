using System;

namespace PokemonBattleSimulatorGUI
{
    public static class BattleSystem
    {
        private static readonly Random random = new Random();

        public static double GetTypeMultiplier(string attackType, string defendType)
        {
            if (attackType == "Fire" && defendType == "Grass") return 1.5;
            if (attackType == "Grass" && defendType == "Water") return 1.5;
            if (attackType == "Water" && defendType == "Fire") return 1.5;
            if (attackType == "Electric" && defendType == "Water") return 1.5;

            if (attackType == "Grass" && defendType == "Fire") return 0.75;
            if (attackType == "Water" && defendType == "Grass") return 0.75;
            if (attackType == "Fire" && defendType == "Water") return 0.75;
            if (attackType == "Water" && defendType == "Electric") return 0.75;

            return 1.0;
        }

        public static string GetEffectText(double multiplier)
        {
            if (multiplier > 1.0) return "It is super effective!";
            if (multiplier < 1.0) return "It is not very effective.";
            return "Normal damage.";
        }

        public static int CalculateAttackDamage(Pokemon attacker, Pokemon defender, string attackType, out bool missed, out bool critical)
        {
            missed = false;
            critical = false;

            double multiplier = GetTypeMultiplier(attacker.Type, defender.Type);
            int bonus = random.Next(0, 4);

            double damageScale = 1.0;
            int missChance = 0;

            if (attackType == "Light")
            {
                damageScale = 0.8;
                missChance = 10;
            }
            else if (attackType == "Medium")
            {
                damageScale = 1.0;
                missChance = 20;
            }
            else if (attackType == "Heavy")
            {
                damageScale = 1.4;
                missChance = 35;
            }

            int roll = random.Next(1, 101);
            if (roll <= missChance)
            {
                missed = true;
                return 0;
            }

            int damage = (int)(attacker.Attack * damageScale * multiplier) + bonus;

            bool isCritical = random.Next(1, 101) <= 15; // 15% chance

            if (isCritical)
            {
                damage = (int)(damage * 1.5);
                critical = true;
            }

            if (damage < 1)
                damage = 1;

            return damage;
        }
           
    }
}