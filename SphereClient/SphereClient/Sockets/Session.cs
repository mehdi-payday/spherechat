using SphereClient.Entities;
using SphereClient.REST;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereClient.Sockets {

    public delegate void MessageReceived(Message message);
    public delegate void DiscussionChange(PrivateDiscussion privatediscussion);
    public delegate void ChannelChange(Channel channel);
    public delegate void NewFriendRequest(FriendRequest friendrequest);
    public delegate void FriendshipAdressed(Friendship friendship);

    public class Session : IDisposable {
        private Connection conn { get; set; }
        private REST.Session session { get; set; }

        public event MessageReceived OnMessageReceived;
        public event DiscussionChange OnDiscussionChange;
        public event ChannelChange OnChannelChange;
        public event NewFriendRequest OnNewFriendshipRequest;
        public event FriendshipAdressed OnFriendshipAdressed;

        private Dictionary<int, Entity> subscriptions = new Dictionary<int, Entity>();

        /// <summary>
        /// Contructeur de la session WS
        /// </summary>
        /// <param name="config">Objet de configurtion du WS</param>
        /// <param name="session">Objet du REST API pour l'authentification</param>
        public Session(Configuration config, REST.Session session) {
            conn = new Connection(config, false);
            this.session = session;

            conn.OnConnected += Conn_OnConnected;
            conn.OnConnecting += Conn_OnConnecting;
            conn.OnDisconnect += Conn_OnDisconnect;
            conn.OnReceive += Conn_OnReceive;

            conn.Connect();
        }

        /// <summary>
        /// Envoie un message dans la connexion du WS
        /// </summary>
        /// <param name="data">Données a envoyer</param>
        private void Send(string data) {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes(data)));
        }

        /// <summary>
        /// Évènement déclenché lors de la réception de données du serveur
        /// </summary>
        /// <param name="data">Données venant du serveur</param>
        private void Conn_OnReceive(string data) {
            string command = data.Split(':')[0];
            dynamic json = JSON.Parse(data.Substring(data.IndexOf(':') + 1));

            Console.WriteLine(data);
            switch ((string)json.type.ToString()) {
                case "authenticate":
                    if (json.success == true) {
                        Send("!subscribe:messaging,users." + session.GetProfile().UserId);

                        foreach (var c in session.GetAllChannels()) {
                            Send("!subscribe:messaging,threads." + c.ThreadId);
                            subscriptions.Add(c.ThreadId, c);
                        }

                        foreach (var p in session.GetAllPrivateDiscussions()) {
                            Send("!subscribe:messaging,threads." + p.ThreadId);
                            subscriptions.Add(p.ThreadId, p);
                        }


                        Console.WriteLine("Authenticated !");
                    }
                    else {
                        Console.WriteLine("Oops.. check your credentitals..");
                    }
                    break;
                case "subscribe":
                    if (json.success == true) {
                        Console.WriteLine("Subscribed to " + json.payload.channel);
                    }
                    else {
                        Console.WriteLine("Unable to subscribe to " + json.payload.channel);
                        subscriptions.Remove(Convert.ToInt32(((string)json.payload.channel).Split('.')[1]));
                    }
                    break;
                case "discussion_change":
                    PrivateDiscussion discussion = Parser.JSONtoEntity(json.payload, typeof(PrivateDiscussion));
                    if (!subscriptions.ContainsKey(discussion.ThreadId)) {
                        subscriptions.Add(discussion.ThreadId, discussion);
                        Send("!subscribe:messaging,threads." + discussion.ThreadId);
                    }

                    OnDiscussionChange?.Invoke(Parser.JSONtoEntity(json.payload, typeof(PrivateDiscussion)));
                    break;
                case "channel_change":
                    Channel channel = Parser.JSONtoEntity(json.payload, typeof(Channel));
                    if (!subscriptions.ContainsKey(channel.ThreadId)) {
                        subscriptions.Add(channel.ThreadId, channel);
                        Send("!subscribe:messaging,threads." + channel.ThreadId);
                    }

                    OnChannelChange?.Invoke(channel);
                    break;
                case "new_friend_request":
                    OnNewFriendshipRequest?.Invoke(Parser.JSONtoEntity(json.payload, typeof(FriendRequest)));
                    break;
                case "friendship_addressed":
                    OnFriendshipAdressed?.Invoke(Parser.JSONtoEntity(json.payload, typeof(Friendship)));
                    break;
                case "message":
                    OnMessageReceived?.Invoke(Parser.JSONtoEntity(json.payload, typeof(Message)));
                    break;
                default:
                    Console.WriteLine("Unhandled event " + json.type);
                    break;
            }
        }

        /// <summary>
        /// Évènement déclenché lors de la déconnexion du WS
        /// </summary>
        private void Conn_OnDisconnect() {
            Console.WriteLine("WebSocket disconnected.. Reconnecting in 5 seconds");
            new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(5000);
                conn.Connect();
            }).Start();
        }

        /// <summary>
        /// Évènement déclenché lors du handshake du WS
        /// </summary>
        private void Conn_OnConnecting() {
            Console.WriteLine("Connecting..");
        }

        /// <summary>
        /// Évènement déclenché lors de la connexion du WS
        /// </summary>
        private Guid guid = Guid.NewGuid();
        private void Conn_OnConnected() {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes("!authenticate:" + guid.ToString() + ":" + session.Auth.Token)));
        }

        public void Dispose() {
            conn.Dispose();
        }
        ~Session() { Dispose(); }
    }
}
