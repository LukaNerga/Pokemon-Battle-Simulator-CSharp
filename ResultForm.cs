using System.Drawing;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class ResultForm : Form
    {
        public ResultForm(bool isWin)
        {
            Text = "Game Result";
            Size = new Size(600, 350);
            StartPosition = FormStartPosition.CenterScreen;

            
            if (isWin)
                BackColor = Color.LightGreen;
            else
                BackColor = Color.LightCoral;

            Label lblResult = new Label();

            
            if (isWin)
                lblResult.Text = "Congratulations! You defeated all opponents!";
            else
                lblResult.Text = "Game Over! Your Pokemon fainted.";

            lblResult.Font = new Font("Arial", 18, FontStyle.Bold);
            lblResult.AutoSize = false;
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            lblResult.Size = new Size(500, 80);
            lblResult.Location = new Point(50, 60);

            Button btnMenu = new Button();
            btnMenu.Text = "Back to Main Menu";
            btnMenu.Size = new Size(200, 45);
            btnMenu.Location = new Point(200, 180);
            btnMenu.Font = new Font("Arial", 12, FontStyle.Bold);

            
            btnMenu.Click += BtnMenu_Click;

            Controls.Add(lblResult);
            Controls.Add(btnMenu);
        }

        private void BtnMenu_Click(object sender, System.EventArgs e)
        {
            GameManager.StartNewGame();
            MainMenuForm menu = new MainMenuForm();
            menu.Show();
            Close();
        }
    }
}