using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class CharacterSelectionForm : Form
    {
        private readonly ListBox listPokemon;
        private readonly PictureBox picPokemon;
        private readonly Label lblDetails;
        private readonly List<Pokemon> allPokemon;

        public CharacterSelectionForm()
        {
            Text = "Choose Your Pokemon";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            string bgPath = Path.Combine(Application.StartupPath, "SelectionImages", "select_bg.png");
            if (File.Exists(bgPath))
            {
                BackgroundImage = Image.FromFile(bgPath);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                BackColor = Color.Beige;
            }

            Label title = new Label
            {
                Text = "Select Your Pokemon",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(250, 30)
            };

            listPokemon = new ListBox
            {
                Size = new Size(250, 250),
                Location = new Point(100, 120),
                Font = new Font("Arial", 12)
            };

            // --- Sort by Damage button ---
            Button btnSortDamage = new Button
            {
                Text = "Sort by Damage",
                Size = new Size(120, 25),
                Location = new Point(100, 378),
                Font = new Font("Arial", 8)
            };
            btnSortDamage.Click += BtnSortDamage_Click;

            // --- Sort by HP button ---
            Button btnSortHP = new Button
            {
                Text = "Sort by HP",
                Size = new Size(120, 25),
                Location = new Point(230, 378),
                Font = new Font("Arial", 8)
            };
            btnSortHP.Click += BtnSortHP_Click;

            lblDetails = new Label
            {
                Size = new Size(280, 100),
                Location = new Point(500, 320),
                Font = new Font("Arial", 12),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            picPokemon = new PictureBox
            {
                Size = new Size(180, 180),
                Location = new Point(550, 120),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Button btnStart = new Button
            {
                Text = "Start Battle",
                Size = new Size(180, 45),
                Location = new Point(360, 440),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            Button btnBack = new Button()
            {
                Text = "Back",
                Size = new Size(120, 40),
                Location = new Point(20, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            Button btnQuit = new Button()
            { 
            Text = "Quit",
            Size = new Size(120, 40),
            Location = new Point(650, 20),
            Font = new Font("Arial", 10, FontStyle.Bold)
            };

            btnStart.Click += BtnStart_Click;
            btnBack.Click += BtnBack_Click;
            btnQuit.Click += BtnQuit_Click;

            listPokemon.SelectedIndexChanged += ListPokemon_SelectedIndexChanged;

            allPokemon = PokemonFactory.GetAllPokemon();
            allPokemon.Sort();

            foreach (Pokemon pokemon in allPokemon)
            {
                listPokemon.Items.Add(pokemon.Name);
            }

            Controls.Add(title);
            Controls.Add(listPokemon);
            Controls.Add(btnSortDamage);
            Controls.Add(btnSortHP);
            Controls.Add(btnBack);
            Controls.Add(btnQuit);
            Controls.Add(lblDetails);
            Controls.Add(btnStart);
            Controls.Add(picPokemon);
        }

        private void BtnSortDamage_Click(object sender, EventArgs e)
        {
            allPokemon.Sort(); // uses IComparable<Pokemon>.CompareTo → sorts by Attack
            RefreshList();
        }

        private void BtnSortHP_Click(object sender, EventArgs e)
        {
            allPokemon.Sort(new Pokemon.ByHPComparer()); // uses ByHPComparer → sorts by MaxHP
            RefreshList();
        }

        private void RefreshList()
        {
            listPokemon.Items.Clear();
            foreach (Pokemon pokemon in allPokemon)
            {
                listPokemon.Items.Add(pokemon.Name);
            }
        }

        private void ListPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPokemon.SelectedIndex >= 0)
            {
                Pokemon selectedPokemon = allPokemon[listPokemon.SelectedIndex];
                lblDetails.Text =
                    "Name: " + selectedPokemon.Name + "\n" +
                    "HP: " + selectedPokemon.MaxHP + "\n" +
                    "Attack: " + selectedPokemon.Attack + "\n" +
                    "Type: " + selectedPokemon.Type;
                LoadPokemonImage(selectedPokemon.Name);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (listPokemon.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Pokemon first.");
                return;
            }
            GameManager.PlayerPokemon = allPokemon[listPokemon.SelectedIndex].Clone();
            GameManager.CurrentLevel = 1;
            BattleForm battleForm = new BattleForm();
            battleForm.Show();
            Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            MainMenuForm menu = new MainMenuForm();
            menu.Show();
            Close();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadPokemonImage(string pokemonName)
        {
            try
            {
                string fileName = pokemonName.ToLower() + ".png";
                string imagePath = Path.Combine(Application.StartupPath, "Images", fileName);
                if (File.Exists(imagePath))
                {
                    picPokemon.Image = Image.FromFile(imagePath);
                }
                else
                {
                    picPokemon.Image = null;
                }
            }
            catch
            {
                picPokemon.Image = null;
            }
        }
    }
}
