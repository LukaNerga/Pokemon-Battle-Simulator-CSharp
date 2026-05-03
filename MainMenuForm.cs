using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class MainMenuForm : Form
    {
        private SoundPlayer player;

        public MainMenuForm()
        {
            Text = "Pokemon Type Battle Simulator";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackgroundImageLayout = ImageLayout.Stretch;

            string bgPath = Path.Combine(Application.StartupPath, "MenuImages", "menu_bg.png");

            if (File.Exists(bgPath))
            {
                BackgroundImage = Image.FromFile(bgPath);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                BackColor = Color.LightSkyBlue;
            }

            TryPlayMusic();

            Label titleLabel = new Label
            {
                Text = "Pokemon Type Battle Simulator",
                Font = new Font("Arial", 24, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(500, 60),
                Location = new Point(200, 40),
                BackColor = Color.FromArgb(180, Color.White)
            };

            Button btnPlay = CreateMenuButton("Play New", 180);
            Button btnContinue = CreateMenuButton("Continue", 250);
            Button btnLoad = CreateMenuButton("Load Game", 320);
            Button btnRules = CreateMenuButton("Rules", 390);
            Button btnQuit = CreateMenuButton("Quit", 460);

            btnPlay.Click += BtnPlay_Click;
            btnContinue.Click += BtnContinue_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRules.Click += BtnRules_Click;
            btnQuit.Click += BtnQuit_Click;

            Controls.Add(titleLabel);
            Controls.Add(btnPlay);
            Controls.Add(btnContinue);
            Controls.Add(btnLoad);
            Controls.Add(btnRules);
            Controls.Add(btnQuit);
        }

        private void BtnPlay_Click(object sender, System.EventArgs e)
        {
            GameManager.StartNewGame();
            CharacterSelectionForm selectionForm = new CharacterSelectionForm();
            selectionForm.Show();
            Hide();
        }

        private void BtnContinue_Click(object sender, System.EventArgs e)
        {
            if (SaveManager.ContinueLastSave())
            {
                BattleForm battleForm = new BattleForm();
                battleForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("No save found.");
            }
        }

        private void BtnLoad_Click(object sender, System.EventArgs e)
        {
            LoadGameForm loadGameForm = new LoadGameForm();
            loadGameForm.ShowDialog();
        }

        private void BtnRules_Click(object sender, System.EventArgs e)
        {
            RulesForm rulesForm = new RulesForm();
            rulesForm.ShowDialog();
        }

        private void BtnQuit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private Button CreateMenuButton(string text, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(220, 50);
            button.Location = new Point(340, y);
            button.Font = new Font("Arial", 12, FontStyle.Bold);
            button.BackColor = Color.White;

            return button;
        }

        private void TryPlayMusic()
        {
            try
            {
                string musicPath = Path.Combine(Application.StartupPath, "intro.wav");

                if (File.Exists(musicPath))
                {
                    player = new SoundPlayer(musicPath);
                    player.PlayLooping();
                }
            }
            catch
            {
            }
        }
    }
}
