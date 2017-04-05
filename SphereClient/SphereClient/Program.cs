using System;
using System.Windows.Forms;

namespace SphereClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            
            LoginForm f = new LoginForm();
            
            Application.Run(f );
        }
    }
}
