using System;
using System.Net;

namespace SphereClient.REST {
    public class Session : IDisposable {
        public Session(string username, string password) {
            Token = Login(username, password).Token;
        }

        private Entities.Auth Login(string username, string password) {
            using (var request = new Auth.Login.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" }
                }
            }.POST(JSON.Stringify(new {
                username = username,
                password = password
            }))) {
                return (Entities.Auth)Parser.Parse(request.Payload, typeof(Entities.Auth));
            }
        }

        public Entities.User GetProfile() {
            using (var request = new Me.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Token }
                }
            }.GET()) {
                return (Entities.User)Parser.Parse(request.Payload, typeof(Entities.User));
            }
        }

        public Entities.Channel[] GetChannels() {
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Token }
                }
            }.GET()) {
                return (Entities.Channel[])Parser.Parse(request.Payload, typeof(Entities.Channel[]));
            }
        }

        public Entities.Message[] GetMessages(Entities.Channel channel) {
            using (var request = new Messaging.Channel.Message.Base(channel.Id) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Token }
                }
            }.GET()) {
                return (Entities.Message[])Parser.Parse(request.Payload, typeof(Entities.Message[]));
            }
        }

        public string Token { get; private set; }
        public Entities.User Me { get; private set; }

        public void Dispose() { }
        ~Session() {
            Dispose();
        }
    }
}
