using System.Drawing;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    public class RulesForm : Form
    {
        public RulesForm()
        {
            Text = "Rules";
            Size = new Size(700, 500);
            StartPosition = FormStartPosition.CenterParent;

            // Read-only textbox is used to show all rules.
            TextBox txtRules = new TextBox();
            txtRules.Multiline = true;
            txtRules.ReadOnly = true;
            txtRules.ScrollBars = ScrollBars.Vertical;
            txtRules.Dock = DockStyle.Fill;
            txtRules.Font = new Font("Arial", 11);

            txtRules.Text = @"HOW TO PLAY
1. Click Play New to start a new game.
2. Choose 1 Pokemon from the list of 10.
3. Defeat enemies in this order:
- Level 1: Fire
- Level 2: Water
- Level 3: Grass
- Final Boss: Pikachu
4. In battle, choose Light, Medium, or Heavy attack.
5. Stronger attacks do more damage, but have a higher chance to miss.
6. The enemy attacks after a short delay.
7. If your HP reaches 0, you lose.
8. Your HP resets at the start of every new level.
9. If enemy HP reaches 0, you move to the next level.
10. Your progress is saved automatically after each win.
11. Load Game lets you use 3 save slots.
12. Continue loads the last saved progress.

Type Advantage:
- Fire > Grass
- Water > Fire
- Grass > Water
- Electric > Water

Have Fun!";

            Controls.Add(txtRules);
        }
    }
}
