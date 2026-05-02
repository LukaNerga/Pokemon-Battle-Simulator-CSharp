using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public static class SaveManager
    {
        private static readonly string SaveFolder = Path.Combine(Application.StartupPath, "Saves");
        private static readonly string LatestFile = Path.Combine(SaveFolder, "latest.txt");

        public static void EnsureFolder()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }

        public static string GetSlotPath(int slot)
        {
            EnsureFolder();
            return Path.Combine(SaveFolder, "slot" + slot + ".json");
        }

        public static void SaveToSlot(int slot)
        {
            EnsureFolder();

            SaveData data = new SaveData();
            data.SlotNumber = slot;
            data.SaveTime = DateTime.Now;
            data.CurrentLevel = GameManager.CurrentLevel;

            if (GameManager.PlayerPokemon != null)
            {
                data.PlayerPokemon = GameManager.PlayerPokemon.Clone();
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(data, options);

            File.WriteAllText(GetSlotPath(slot), json);
            File.WriteAllText(LatestFile, slot.ToString());
        }

        public static void SaveAuto()
        {
            EnsureFolder();

            List<SaveData> saves = GetAllSaves();
            int slotToUse = 1;

            if (saves.Count < 3)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (!File.Exists(GetSlotPath(i)))
                    {
                        slotToUse = i;
                        break;
                    }
                }
            }
            else
            {
                SaveData oldest = saves[0];

                foreach (SaveData save in saves)
                {
                    if (save.SaveTime < oldest.SaveTime)
                    {
                        oldest = save;
                    }
                }

                slotToUse = oldest.SlotNumber;
            }

            SaveToSlot(slotToUse);
        }

        public static List<SaveData> GetAllSaves()
        {
            EnsureFolder();

            List<SaveData> saves = new List<SaveData>();

            for (int i = 1; i <= 3; i++)
            {
                string path = GetSlotPath(i);

                if (File.Exists(path))
                {
                    try
                    {
                        string json = File.ReadAllText(path);
                        SaveData data = JsonSerializer.Deserialize<SaveData>(json);

                        if (data != null)
                        {
                            saves.Add(data);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            List<SaveData> sortedSaves = new List<SaveData>();

            for (int slotNumber = 1; slotNumber <= 3; slotNumber++)
            {
                foreach (SaveData save in saves)
                {
                    if (save.SlotNumber == slotNumber)
                    {
                        sortedSaves.Add(save);
                    }
                }
            }

            return sortedSaves;
        }

        public static bool ContinueLastSave()
        {
            EnsureFolder();

            if (!File.Exists(LatestFile))
            {
                return false;
            }

            string text = File.ReadAllText(LatestFile);

            int slot;
            bool success = int.TryParse(text, out slot);

            if (!success)
            {
                return false;
            }

            return LoadFromSlot(slot);
        }

        public static bool LoadFromSlot(int slot)
        {
            string path = GetSlotPath(slot);

            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonSerializer.Deserialize<SaveData>(json);

                if (data == null)
                {
                    return false;
                }

                if (data.PlayerPokemon == null)
                {
                    return false;
                }

                GameManager.PlayerPokemon = data.PlayerPokemon;
                GameManager.CurrentLevel = data.CurrentLevel;

                File.WriteAllText(LatestFile, slot.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}