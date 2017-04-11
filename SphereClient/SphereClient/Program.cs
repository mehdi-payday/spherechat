﻿using System;
using System.Text;

namespace SphereClient {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main() {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );*/

            using (var rest = new REST.Session("sphereman", "spherique")) {
                using (var ws = new Sockets.Session(new Sockets.Configuration("spherechat.tk", 4242, "/ec", "123456789"), rest.Auth)) {
                    string line = null;
                    while ((line = Console.ReadLine()) != null) {
                        ws.Send(new Sockets.Buffers.WebSocket.Write(Encoding.UTF8.GetBytes(line)));
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
