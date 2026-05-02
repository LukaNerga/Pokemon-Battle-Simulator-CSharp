using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class BattleForm : Form
    {
        private readonly Pokemon player;
        private readonly Pokemon enemy;
        private readonly Label lblPlayer;
        private readonly Label lblEnemy;
        private readonly ProgressBar playerBar;
        private readonly ProgressBar enemyBar;
        private readonly TextBox txtLog;
        private readonly Button btnLightAttack;
        private readonly Button btnMediumAttack;
        private readonly Button btnHeavyAttack;
        private readonly Button btnSave;
        private readonly Button btnHeal;
        private bool hasHealed;
        private PictureBox picPlayer;
        private PictureBox picEnemy;

        public BattleForm()
        {
            if (GameManager.PlayerPokemon == null)
            {
                throw new InvalidOperationException("Player Pokemon is not set.");
            }

            player = GameManager.PlayerPokemon;
            player.CurrentHP = player.MaxHP;
            enemy = GameManager.GetEnemyForLevel(GameManager.CurrentLevel);
            hasHealed = false;

            Text = "Battle Screen";
            Size = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            string bgPath = Path.Combine(Application.StartupPath, "BattleImages", "battle_bg.png");

            if (File.Exists(bgPath))
            {
                BackgroundImage = Image.FromFile(bgPath);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            

            Label lblTitle = new Label
            {
                Text = GameManager.GetLevelName(GameManager.CurrentLevel),
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(280, 20)
            };

            picPlayer = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(120, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            picEnemy = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(650, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            playerBar = new ProgressBar
            {
                Size = new Size(300, 25),
                Location = new Point(60, 230),
                Maximum = player.MaxHP
            };

            enemyBar = new ProgressBar
            {
                Size = new Size(300, 25),
                Location = new Point(520, 230),
                Maximum = enemy.MaxHP
            };

            lblPlayer = new Label
            {
                Size = new Size(300, 80),
                Location = new Point(60, 270),
                Font = new Font("Arial", 12),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            lblEnemy = new Label
            {
                Size = new Size(300, 80),
                Location = new Point(520, 270),
                Font = new Font("Arial", 12),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            txtLog = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Size = new Size(760, 140),
                Location = new Point(60, 370),
                Font = new Font("Consolas", 11)
            };

            btnLightAttack = new Button
            {
                Text = "Light Attack",
                Size = new Size(150, 45),
                Location = new Point(200, 530),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            btnMediumAttack = new Button
            {
                Text = "Medium Attack",
                Size = new Size(150, 45),
                Location = new Point(360, 530),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            btnHeavyAttack = new Button
            {
                Text = "Heavy Attack",
                Size = new Size(150, 45),
                Location = new Point(520, 530),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };
            
            btnHeal = new Button
            {
                Text = "Heal",
                Size = new Size(150, 45),
                Location = new Point(40, 530),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            btnSave = new Button
            {
                Text = "Save Game",
                Size = new Size(150, 45),
                Location = new Point(680, 530),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            btnLightAttack.Click += BtnLightAttack_Click;
            btnMediumAttack.Click += BtnMediumAttack_Click;
            btnHeavyAttack.Click += BtnHeavyAttack_Click;
            btnHeal.Click += BtnHeal_Click;
            btnSave.Click += BtnSave_Click;

            Controls.Add(lblTitle);
            Controls.Add(picPlayer);
            Controls.Add(picEnemy);
            Controls.Add(playerBar);
            Controls.Add(enemyBar);
            Controls.Add(lblPlayer);
            Controls.Add(lblEnemy);
            Controls.Add(txtLog);
            Controls.Add(btnLightAttack);
            Controls.Add(btnMediumAttack);
            Controls.Add(btnHeavyAttack);
            Controls.Add(btnHeal);
            Controls.Add(btnSave);

            UpdateUI();
            LoadBattleImages();
            AddLog("Battle Start! " + player.Name + " vs " + enemy.Name);
        }

        private void LoadBattleImages()
        {
            picPlayer.Image = null;
            picEnemy.Image = null;

            try
            {
                string playerFile = player.Name.ToLower() + ".png";
                string enemyFile = enemy.Name.ToLower().Split(' ')[0] + ".png";

                string playerPath = Path.Combine(Application.StartupPath, "Images", playerFile);
                string enemyPath = Path.Combine(Application.StartupPath, "Images", enemyFile);

                if (File.Exists(playerPath))
                {
                    picPlayer.Image = Image.FromFile(playerPath);
                }

                if (File.Exists(enemyPath))
                {
                    picEnemy.Image = Image.FromFile(enemyPath);
                }
            }
            catch
            {
                picPlayer.Image = null;
                picEnemy.Image = null;
            }
        }

        private void SetAttackButtonsEnabled(bool enabled)
        {
            btnLightAttack.Enabled = enabled;
            btnMediumAttack.Enabled = enabled;
            btnHeavyAttack.Enabled = enabled;

            if (!hasHealed)
            {
                btnHeal.Enabled = enabled;
            }
        }
    

        private async void BtnLightAttack_Click(object sender, EventArgs e)
        {
            await PlayerAttack("Light");
        }

        private async void BtnMediumAttack_Click(object sender, EventArgs e)
        {
            await PlayerAttack("Medium");
        }

        private async void BtnHeavyAttack_Click(object sender, EventArgs e)
        {
            await PlayerAttack("Heavy");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveManager.SaveAuto();
            MessageBox.Show("Game saved.");
        }

        private async Task PlayerAttack(string attackType)
        {
            SetAttackButtonsEnabled(false);

            bool playerMissed;
            bool playerCritical;

            int playerDamage = BattleSystem.CalculateAttackDamage(player, enemy, attackType, out playerMissed, out playerCritical);
            double playerMultiplier = BattleSystem.GetTypeMultiplier(player.Type, enemy.Type);

            if (playerMissed)
            {
                AddLog(player.Name + " used " + attackType + " Attack but missed!");
            }
            else
            {
                enemy.CurrentHP -= playerDamage;

                if (enemy.CurrentHP < 0)
                {
                    enemy.CurrentHP = 0;
                }

                AddLog(player.Name + " used " + attackType + " Attack on " + enemy.Name + " for " + playerDamage + " damage.");
                AddLog(BattleSystem.GetEffectText(playerMultiplier));
            }

            if (playerCritical)
            {
                AddLog("Critical hit!");
            }


            UpdateUI();
            LoadBattleImages();

            if (enemy.CurrentHP <= 0)
            {
                AddLog(enemy.Name + " fainted!");
                SaveManager.SaveAuto();
                HandleWin();
                return;
            }

            AddLog(enemy.Name + " is preparing an attack...");
            await Task.Delay(2000);

            EnemyTurn();
        }

        private async void BtnHeal_Click(object sender, EventArgs e)
        {
            if (hasHealed)
            {
                MessageBox.Show("You can only heal once per battle.");
                return;
            }

            hasHealed = true;

            player.CurrentHP += 25;

            if (player.CurrentHP > player.MaxHP)
            {
                player.CurrentHP = player.MaxHP;
            }

            AddLog(player.Name + " healed for 25 HP!");
            UpdateUI();

            SetAttackButtonsEnabled(false);
            btnHeal.Enabled = false;

            AddLog(enemy.Name + " is preparing an attack...");
            await Task.Delay(2000);

            EnemyTurn();
        }
        private void EnemyTurn()
        {
            string[] attackOptions = { "Light", "Medium", "Heavy" };
            Random rng = new Random();
            string enemyAttackType = attackOptions[rng.Next(attackOptions.Length)];

            bool enemyMissed;
            bool enemyCritical;
            int enemyDamage = BattleSystem.CalculateAttackDamage(enemy, player, enemyAttackType, out enemyMissed, out enemyCritical);
            double enemyMultiplier = BattleSystem.GetTypeMultiplier(enemy.Type, player.Type);

            if (enemyMissed)
            {
                AddLog(enemy.Name + " used " + enemyAttackType + " Attack but missed!");
            }
            else
            {
                player.CurrentHP -= enemyDamage;

                if (player.CurrentHP < 0)
                {
                    player.CurrentHP = 0;
                }

                AddLog(enemy.Name + " used " + enemyAttackType + " Attack on " + player.Name + " for " + enemyDamage + " damage.");
                AddLog(BattleSystem.GetEffectText(enemyMultiplier));
            }
            
            if (enemyCritical)
            {
                AddLog("Critical hit!");
            }

            UpdateUI();

            if (player.CurrentHP <= 0)
            {
                AddLog(player.Name + " fainted!");
                HandleLose();
                return;
            }

            SetAttackButtonsEnabled(true);
        }

        private void HandleWin()
        {
            SetAttackButtonsEnabled(false);

            if (GameManager.CurrentLevel < 4)
            {
                GameManager.CurrentLevel++;
                MessageBox.Show("You won this battle! HP restored. Moving to next level.");

                BattleForm nextBattle = new BattleForm();
                nextBattle.Show();
                Close();
            }
            else
            {
                ResultForm resultForm = new ResultForm(true);
                resultForm.Show();
                Close();
            }
        }

        private void HandleLose()
        {
            SetAttackButtonsEnabled(false);

            ResultForm resultForm = new ResultForm(false);
            resultForm.Show();
            Close();
        }

        private void UpdateUI()
        {
            lblPlayer.Text =
                "Player Pokemon: " + player.Name + "\n" +
                "Type: " + player.Type + "\n" +
                "HP: " + player.CurrentHP + "/" + player.MaxHP + "\n" +
                "Attack: " + player.Attack;

            lblEnemy.Text =
                "Enemy Pokemon: " + enemy.Name + "\n" +
                "Type: " + enemy.Type + "\n" +
                "HP: " + enemy.CurrentHP + "/" + enemy.MaxHP + "\n" +
                "Attack: " + enemy.Attack;

            playerBar.Value = Math.Max(0, Math.Min(player.CurrentHP, playerBar.Maximum));
            enemyBar.Value = Math.Max(0, Math.Min(enemy.CurrentHP, enemyBar.Maximum));
        }

        private void AddLog(string message)
        {
            txtLog.AppendText(message + Environment.NewLine);
        }
    }
}