using SphereClient.Entities;
using System;
using System.Net;

namespace SphereClient.REST {
    public class Session : IDisposable {
        public Channel CurrentChannel;

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

        public User GetProfile() {
            using (var request = new Me.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET()) {
                return (User)Parser.JSONtoEntity(request.Payload, typeof(Entities.User));
            }
        }

        public Entities.Friendship[] GetAllFriendships() {
            Cursor<Entities.Friendship> cursor = new Cursor<Entities.Friendship>();
            do {
                using (var request = new Friendship.Base() {
                    DefaultHeaders = new WebHeaderCollection() {
                        { HttpRequestHeader.Accept, "application/json" },
                        { HttpRequestHeader.ContentType, "application/json" },
                        { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                    }
                }.GET(string.IsNullOrEmpty(cursor.Next) ? null : cursor.Next.Split('?')[1])) {
                    cursor.AddRange((Entities.Friendship[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Friendship[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Cursor<Entities.Friendship> GetFriendships(string pageUrl = null) {
            Cursor<Entities.Friendship> cursor = new Cursor<Entities.Friendship>();
            using (var request = new Friendship.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(pageUrl == null ? null : pageUrl.Split('?')[1])) {
                cursor.AddRange((Entities.Friendship[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Friendship[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Cursor<Channel> GetChannels(string pageUrl = null) {
            Cursor<Channel> cursor = new Cursor<Channel>();
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(pageUrl == null ? null : pageUrl.Split('?')[1])) {
                cursor.AddRange((Channel[])Parser.JSONtoEntity(request.Payload, typeof(Channel[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Channel[] GetAllChannels() {
            Cursor<Channel> cursor = new Cursor<Channel>();
            do {
                using (var request = new Messaging.Channel.Base() {
                    DefaultHeaders = new WebHeaderCollection() {
                        { HttpRequestHeader.Accept, "application/json" },
                        { HttpRequestHeader.ContentType, "application/json" },
                        { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                    }
                }.GET(string.IsNullOrEmpty(cursor.Next) ? null : cursor.Next.Split('?')[1])) {
                    cursor.AddRange((Channel[])Parser.JSONtoEntity(request.Payload, typeof(Channel[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Cursor<Message> GetMessages(Channel channel, string pageUrl = null) {
            Cursor<Message> cursor = new Cursor<Message>();
            using (var request = new Messaging.Channel.Message.Base(channel.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(pageUrl == null ? null : pageUrl.Split('?')[1])) {
                cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Cursor<Message> GetMessages(int channelId, string pageUrl = null) {
            Cursor<Message> cursor = new Cursor<Message>();
            using (var request = new Messaging.Channel.Message.Base(channelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(pageUrl == null ? null : pageUrl.Split('?')[1])) {
                cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Message[] GetAllMessages(Channel channel) {
            Cursor<Message> cursor = new Cursor<Message>();
            do {
                using (var request = new Messaging.Channel.Message.Base(channel.ChannelId) {
                    DefaultHeaders = new WebHeaderCollection() {
                        { HttpRequestHeader.Accept, "application/json" },
                        { HttpRequestHeader.ContentType, "application/json" },
                        { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                    }
                }.GET(string.IsNullOrEmpty(cursor.Next) ? null : cursor.Next.Split('?')[1])) {
                    cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Message[] GetAllMessages(int channelId) {
            Cursor<Message> cursor = new Cursor<Message>();
            do {
                using (var request = new Messaging.Channel.Message.Base(channelId) {
                    DefaultHeaders = new WebHeaderCollection() {
                        { HttpRequestHeader.Accept, "application/json" },
                        { HttpRequestHeader.ContentType, "application/json" },
                        { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                    }
                }.GET(string.IsNullOrEmpty(cursor.Next) ? null : cursor.Next.Split('?')[1])) {
                    cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public void PostMessageToChannel(Message message, Channel channel) {
            using (var request = new Messaging.Channel.Message.Base(channel.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public void PostMessageToChannel(Message message, int channelId) {
            using (var request = new Messaging.Channel.Message.Base(channelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public void PostMessageToPrivateDiscussion(Entities.Message message, Entities.PrivateDiscussion PrivateDiscussion) {
            using (var request = new Messaging.PrivateDiscussion.Message.Base(PrivateDiscussion.ChannelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public void PostMessageToPrivateDiscussion(Entities.Message message, int channelId) {
            using (var request = new Messaging.PrivateDiscussion.Message.Base(channelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public Entities.Auth Auth { get; private set; }

        public void Dispose() { }
        ~Session() {
            Dispose();
        }
    }
}
