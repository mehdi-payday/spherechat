using SphereClient.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

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
                return (User)Parser.JSONtoEntity(request.Payload, typeof(User));
            }
        }

        public void PostProfile(User user) {
            using (var request = new Me.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(user, typeof(User)))) { }
        }

        public void PostFriendship(FriendRequest friendrequest) {
            using (var request = new Friendship.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(friendrequest, typeof(FriendRequest)))) { }
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
                }.GET(!string.IsNullOrEmpty(cursor.Next) ? ExtractQueryString(cursor.Next) : null)) {
                    cursor.AddRange((Entities.Friendship[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Friendship[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Cursor<Entities.Friendship> GetFriendships(string page = "1") {
            Cursor<Entities.Friendship> cursor = new Cursor<Entities.Friendship>();
            using (var request = new Friendship.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(page != null ? new Dictionary<string, string>() {
                { "page", page }
            } : null)) {
                cursor.AddRange((Entities.Friendship[])Parser.JSONtoEntity(request.Payload, typeof(Entities.Friendship[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public void PostPrivateDiscussion(PrivateDiscussion privatediscussion) {
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(privatediscussion, typeof(PrivateDiscussion)))) { }
        }

        public void PostChannel(Channel channel) {
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(channel, typeof(Channel)))) { }
        }

        public Cursor<Channel> GetChannels(string page = "1") {
            Cursor<Channel> cursor = new Cursor<Channel>();
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(page != null ? new Dictionary<string, string>() {
                { "page", page }
            } : null)) {
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
                }.GET(!string.IsNullOrEmpty(cursor.Next) ? ExtractQueryString(cursor.Next) : null)) {
                    cursor.AddRange((Channel[])Parser.JSONtoEntity(request.Payload, typeof(Channel[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Cursor<Message> GetMessages(Channel channel, string page = "1") {
            Cursor<Message> cursor = new Cursor<Message>();
            using (var request = new Messaging.Channel.Message.Base(channel.ThreadId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(page != null ? new Dictionary<string, string>() {
                { "page", page }
            } : null)) {
                cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Cursor<Message> GetMessages(int channelId, string page = "1") {
            Cursor<Message> cursor = new Cursor<Message>();
            using (var request = new Messaging.Channel.Message.Base(channelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(page != null ? new Dictionary<string, string>() {
                { "page", page }
            } : null)) {
                cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Message[] GetAllMessages(Channel channel) {
            Cursor<Message> cursor = new Cursor<Message>();
            do {
                using (var request = new Messaging.Channel.Message.Base(channel.ThreadId) {
                    DefaultHeaders = new WebHeaderCollection() {
                        { HttpRequestHeader.Accept, "application/json" },
                        { HttpRequestHeader.ContentType, "application/json" },
                        { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                    }
                }.GET(!string.IsNullOrEmpty(cursor.Next) ? ExtractQueryString(cursor.Next) : null)) {
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
                }.GET(!string.IsNullOrEmpty(cursor.Next) ? ExtractQueryString(cursor.Next) : null)) {
                    cursor.AddRange((Message[])Parser.JSONtoEntity(request.Payload, typeof(Message[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public void PostMessageToChannel(Message message, Channel channel) {
            using (var request = new Messaging.Channel.Message.Base(channel.ThreadId) {
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

        public void PostMessageToPrivateDiscussion(Message message, PrivateDiscussion PrivateDiscussion) {
            using (var request = new Messaging.PrivateDiscussion.Message.Base(PrivateDiscussion.ThreadId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public void PostMessageToPrivateDiscussion(Message message, int channelId) {
            using (var request = new Messaging.PrivateDiscussion.Message.Base(channelId) {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.POST(Parser.EntitytoJSON(message, message.GetType()))) { }
        }

        public Cursor<User> GetUsers(string search = "", string page = "1") {
            Cursor<User> cursor = new Cursor<User>();
            using (var request = new Users.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(new Dictionary<string, string>() {
                { "search", search },
                { "page", page }
            })) {
                cursor.AddRange((User[])Parser.JSONtoEntity(request.Payload, typeof(User[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public User[] GetAllUsers(string search = "") {
            Cursor<User> cursor = new Cursor<User>();
            string page = "1";

            do {
                if (!string.IsNullOrEmpty(cursor.Next)) {
                    page = ExtractQueryString(cursor.Next)["page"];
                }

                using (var request = new Users.Base() {
                    DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
                }.GET(new Dictionary<string, string>() {
                    { "search", search },
                    { "page", page }
                })) {
                    cursor.AddRange((User[])Parser.JSONtoEntity(request.Payload, typeof(User[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Cursor<Channel> GetChannels(string search = "", string page = "1") {
            Cursor<Channel> cursor = new Cursor<Channel>();
            using (var request = new Messaging.Channel.Base() {
                DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
            }.GET(new Dictionary<string, string>() {
                { "search", search },
                { "page", page }
            })) {
                cursor.AddRange((Channel[])Parser.JSONtoEntity(request.Payload, typeof(Channel[])));
                cursor.Next = request.Payload.next.ToString();
                cursor.Previous = request.Payload.previous.ToString();

                return cursor;
            }
        }

        public Channel[] GetAllChannels(string search = "") {
            Cursor<Channel> cursor = new Cursor<Channel>();
            string page = "1";

            do {
                if (!string.IsNullOrEmpty(cursor.Next)) {
                    page = ExtractQueryString(cursor.Next)["page"];
                }

                using (var request = new Users.Base() {
                    DefaultHeaders = new WebHeaderCollection() {
                    { HttpRequestHeader.Accept, "application/json" },
                    { HttpRequestHeader.ContentType, "application/json" },
                    { HttpRequestHeader.Authorization, "Token " + Auth.Token }
                }
                }.GET(new Dictionary<string, string>() {
                    { "search", search },
                    { "page", page }
                })) {
                    cursor.AddRange((Channel[])Parser.JSONtoEntity(request.Payload, typeof(Channel[])));
                    cursor.Next = request.Payload.next.ToString();
                    cursor.Previous = request.Payload.previous.ToString();
                }
            } while (!string.IsNullOrEmpty(cursor.Next));
            return cursor.ToArray();
        }

        public Search SearchAll(string search) {
            Search s = new Search();
            s.Channels = GetAllChannels(search);
            s.Users = GetAllUsers(search);
            s.IsNull = false;
            return s;
        }

        public Entities.Auth Auth { get; private set; }

        public static Dictionary<string, string> ExtractQueryString(string url) {
            Dictionary<string, string> qs = new Dictionary<string, string>();
            if (url.IndexOf('?') != -1) {
                string query = url.Split('?')[1];
                foreach (string param in query.Split('&')) {
                    string[] vals = param.Split('=');
                    qs.Add(vals[0], Regex.Unescape(vals[1]));
                }
            }
            return qs;
        }

        public void Dispose() { }
        ~Session() {
            Dispose();
        }
    }
}
