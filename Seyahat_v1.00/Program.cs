using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Seyahat_v1._00
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            bool acikmi = false;
            Mutex mtex = new Mutex(true, "Programim", out acikmi);
            if (acikmi)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Admin_Panel());                
            }
            else
            {
                MessageBox.Show("Program Zaten Açık", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }
        }
    }
}
