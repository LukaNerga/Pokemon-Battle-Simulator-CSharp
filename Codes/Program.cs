using System;
using System.Windows.Forms;

namespace PokemonBattleSimulatorGUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Starts Windows Forms app from main menu.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenuForm());
        }
    }
}
