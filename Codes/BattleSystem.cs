using System;

namespace PokemonBattleSimulatorGUI
{
    public static class BattleSystem
    {
        private static readonly Random random = new Random();

        public static double GetTypeMultiplier(string attackType, string defendType)
        {
            // Super effective matchups
            
            if (attackType == "Fire" && defendType == "Grass") return 1.5;
            if (attackType == "Water" && defendType == "Fire") return 1.5;
            if (attackType == "Grass" && defendType == "Water") return 1.5;
            if (attackType == "Electric" && defendType == "Water") return 1.5;

            if (attackType == "Rock" && defendType == "Fire") return 1.5;
            if (attackType == "Rock" && defendType == "Flying") return 1.5;

            if (attackType == "Flying" && defendType == "Grass") return 1.5;
            if (attackType == "Flying" && defendType == "Fighting") return 1.5;

            if (attackType == "Fighting" && defendType == "Normal") return 1.5;
            if (attackType == "Fighting" && defendType == "Rock") return 1.5;

            if (attackType == "Rock" && defendType == "Electric") return 1.5;
            
            if (attackType == "Ghost" && defendType == "Ghost") return 1.5;

            if (attackType == "Dragon" && defendType == "Dragon") return 1.5;

            // Not very effective matchups
            
            if (attackType == "Fire" && defendType == "Water") return 0.75;
            if (attackType == "Water" && defendType == "Grass") return 0.75;
            if (attackType == "Grass" && defendType == "Fire") return 0.75;
            if (attackType == "Electric" && defendType == "Grass") return 0.75;

            if (attackType == "Fire" && defendType == "Rock") return 0.75;
            if (attackType == "Flying" && defendType == "Rock") return 0.75;

            if (attackType == "Normal" && defendType == "Rock") return 0.75;
            if (attackType == "Normal" && defendType == "Ghost") return 0.75;

            if (attackType == "Fighting" && defendType == "Flying") return 0.75;
            if (attackType == "Fighting" && defendType == "Ghost") return 0.75;

            if (attackType == "Ghost" && defendType == "Normal") return 0.75;

            if (attackType == "Electric" && defendType == "Rock") return 0.75;

            // Neutral damage
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
