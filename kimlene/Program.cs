using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kimlene
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (GlobalClass.terminateApp)
            {
                return;
            }
            GlobalClass.connectDB();
            if (GlobalClass.terminateApp)
            {
                return;
            }
            Application.Run(new Kisiler());
        }

    }
}
