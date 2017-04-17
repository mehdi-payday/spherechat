using System;
using System.Windows.Forms;

namespace SphereClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string mode = "form";

            if (mode == "form") {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
            else if (mode == "console") {
                Console.Title = "SphereChat";
                using (var session = new Session("sphereman", "spherique")) {
                    var curr = session.REST.GetChannels()[0];

                    string line;
                    while (!string.IsNullOrEmpty((line = Console.ReadLine()))) {
                        session.REST.PostMessageToChannel(new Entities.Message() {
                            Contents = line,
                            Tags = new Entities.MessageTag[] { }
                        }, curr);
                    }
                }
            }
        }
    }
}
