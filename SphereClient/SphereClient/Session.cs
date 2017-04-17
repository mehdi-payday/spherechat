using System;

namespace SphereClient {
    public class Session : IDisposable {
        public REST.Session REST { get; set; }
        public Sockets.Session WS { get; set; }

        public Session(string username, string password) {
            REST = new REST.Session(username, password);
        }

        public void ConnectWS() {
            WS = new Sockets.Session(new Sockets.Configuration("spherechat.tk", 4242, "/ec", DateTime.Now.Ticks.ToString()), REST);
        }

        public void Dispose() {
            WS.Dispose();
            REST.Dispose();
        }
        ~Session() { Dispose(); }
    }
}
