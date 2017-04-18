using System;
using System.Text.RegularExpressions;

namespace SphereClient {
    public class Session : IDisposable {
        public REST.Session REST { get; set; }
        public Sockets.Session WS { get; set; }

        public Session(string username, string password) {
            try {
                REST = new REST.Session(username, password);
                WS = new Sockets.Session(new Sockets.Configuration("spherechat.tk", 8080, "/ec", DateTime.Now.Ticks.ToString()), REST);
            }
            catch (System.Net.WebException e) {
                if (new Regex(@".*\(400\).*").IsMatch(e.Message)) {
                    throw new InvalidUserOrPasswordException();
                }
                else {
                    throw new Exception("Erreur de connexion.", e);
                }
            }
        }

        public class InvalidUserOrPasswordException : Exception { }

        public void Dispose() {
            REST?.Dispose();
            WS?.Dispose();
        }
        ~Session() { Dispose(); }
    }
}
