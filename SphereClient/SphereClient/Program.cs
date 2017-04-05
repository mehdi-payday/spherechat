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
            Application.Run( Form1.Instance );
/*
            using (var rest = new REST.Session("sphereman", "spherique")) {
                foreach (var a in rest.GetChannels()) {
                    Console.WriteLine(a.ToJSON());

                    foreach (var b in rest.GetMessages(a)) {
                        Console.WriteLine(b.ToJSON());
                    }
                }
            }
            Console.ReadLine();*/
        }
    }
}
