using System;
using System.Net;

namespace SphereClient.REST {
    public class Session : IDisposable {
        public Entities.Channel CurrentChannel;

        public Session(string username, string password) {
            Auth = Login(username, password);
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
                return (Entities.Auth)Parser.JSONtoEntity(request.Payload, typeof(Entities.Auth));
            }
        }

        public Entities.User GetProfile() {
            using (var request = new Me.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET()) {
                return (Entities.User)Parser.JSONtoEntity(request.Payload, typeof(Entities.User));
            }
        }

        public Entities.Channel[] GetChannels() {
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET()) {
                return (Entities.Channel[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Channel[]));
            }
        }

        public Entities.Message[] GetMessages(Entities.Channel channel) {
            using (var request = new Messaging.Channel.Message.Base(channel.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET()) {
                return (Entities.Message[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Message[]));
            }
        }

        public void PostMessageToChannel(Entities.Message message, Entities.Channel channel) {
            using (var request = new Messaging.Channel.Message.Base(channel.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) {
                Console.WriteLine(request.Payload);
            }
        }

        public void PostMessageToPrivateDiscussion(Entities.Message message, Entities.PrivateDiscussion PrivateDiscussion) {
            using (var request = new Messaging.PrivateDiscussion.Message.Base(PrivateDiscussion.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) {
                Console.WriteLine(request.Payload);
            }
        }

        public Entities.Auth Auth { get; private set; }

        public void Dispose() { }
        ~Session() {
            Dispose();
        }
    }
}
