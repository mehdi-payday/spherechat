using System;
using System.Net;
using System.Text;

namespace SphereCliet {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );*/

            Sockets.Connection conn = new Sockets.Connection(new Sockets.Configuration(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080), "/", "1234567890"));

            conn.OnConnecting += () => {
                Console.WriteLine("Connecting..");
            };

            conn.OnConnected += () => {
                Console.WriteLine("Connected!");
            };

            conn.OnDisconnect += () => {
                Console.WriteLine("Disconnected..");
            };

            conn.OnReceive += (data) => {
                Console.WriteLine("[" + DateTime.Now + "] " + data);
            };

            string line;
            while ((line = Console.ReadLine()) != null) {
                conn.Send(new Sockets.Buffers.WebSocket.Write(Encoding.UTF8.GetBytes(line)));
            }
        }
    }
}
