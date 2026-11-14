using System;
using System.Windows.Forms;

namespace dev422_midterm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Open the main window.
            Application.Run(new Form1());
        }
    }
}
