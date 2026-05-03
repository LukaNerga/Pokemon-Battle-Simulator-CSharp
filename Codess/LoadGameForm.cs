using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class LoadGameForm : Form
    {
        public LoadGameForm()
        {
            Text = "Load Game";
            Size = new Size(500, 350);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.WhiteSmoke;

            Label title = new Label
            {
                Text = "Save Slots",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(180, 20)
            };

            Controls.Add(title);
            BuildSlots();
        }

        private void BuildSlots()
        {
            List<SaveData> saves = SaveManager.GetAllSaves();

            for (int i = 1; i <= 3; i++)
            {
                SaveData save = null;

                foreach (SaveData currentSave in saves)
                {
                    if (currentSave.SlotNumber == i)
                    {
                        save = currentSave;
                        break;
                    }
                }

                GroupBox box = new GroupBox
                {
                    Text = "Slot " + i,
                    Size = new Size(420, 70),
                    Location = new Point(30, 60 + ((i - 1) * 80))
                };

                Label info = new Label
                {
                    AutoSize = false,
                    Size = new Size(280, 40),
                    Location = new Point(10, 20)
                };

                if (save == null)
                {
                    info.Text = "Empty";
                }
                else
                {
                    info.Text = save.PlayerPokemon.Name +
                                " | HP: " + save.PlayerPokemon.CurrentHP + "/" + save.PlayerPokemon.MaxHP +
                                " | Level: " + save.CurrentLevel;
                }

                Button btnLoad = new Button
                {
                    Text = "Load",
                    Size = new Size(100, 30),
                    Location = new Point(300, 22),
                    Enabled = (save != null)
                };

                int slotNumber = i;
                btnLoad.Click += delegate
                {
                    LoadSelectedSlot(slotNumber);
                };

                box.Controls.Add(info);
                box.Controls.Add(btnLoad);
                Controls.Add(box);
            }
        }

        private void LoadSelectedSlot(int slotNumber)
        {
            if (SaveManager.LoadFromSlot(slotNumber))
            {
                BattleForm battleForm = new BattleForm();
                battleForm.Show();
                CloseOpenMainMenu();
                Close();
            }
            else
            {
                MessageBox.Show("Could not load save.");
            }
        }

        private void CloseOpenMainMenu()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is MainMenuForm)
                {
                    form.Hide();
                }
            }
        }
    }
}
