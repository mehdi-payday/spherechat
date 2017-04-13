
using SphereClient.Entities;
using System;
using System.Windows.Forms;

namespace SphereClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string mode = "console";

            if (mode == "form") {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
            else if (mode == "console") {
                Console.Title = "SphereChat";
                using (var rest = new REST.Session("sphereman", "spherique")) {
                    var curr = rest.GetChannels()[0];
                    using (var ws = new Sockets.Session(new Sockets.Configuration("spherechat.tk", 4242, "/ec", "123456789"), rest)) {
                        ws.OnMessageReceived += (SphereClient.Entities.Message message) => {
                            Console.WriteLine(message.UserId + " => " + message.Contents);
                        };

                        ws.OnChannelChange += (Channel channel) => {
                            Console.WriteLine(channel.Title + " changed");
                        };

                        string line;
                        while ((line = Console.ReadLine()) != null) {
                            rest.PostMessageToChannel(new Entities.Message() {
                                Contents = line,
                                Tags = new Entities.MessageTag[] { }
                            }, curr);
                        }
                    }
                }
            }
        }
    }
}
