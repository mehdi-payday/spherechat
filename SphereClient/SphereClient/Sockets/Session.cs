using SphereClient.Entities;
using SphereClient.REST;
using System;
using System.Text;

namespace SphereClient.Sockets {

    public delegate void MessageReceived(Message message);
    public delegate void DiscussionChange(Thread thread);
    public delegate void ChannelChange(Channel channel);
    public delegate void NewFriendRequest(FriendRequest friendrequest);
    public delegate void FriendRequestAdressed(FriendRequest friendrequest);

    class Session : IDisposable {
        private Connection conn { get; set; }
        private REST.Session session { get; set; }

        public event MessageReceived OnMessageReceived;
        public event DiscussionChange OnDiscussionChange;
        public event ChannelChange OnChannelChange;
        public event NewFriendRequest OnNewFriendshipRequest;
        public event FriendRequestAdressed OnFriendRequestAdressed;

        public Session(Configuration config, REST.Session session) {
            conn = new Connection(config, false);
            this.session = session;

            conn.OnConnected += Conn_OnConnected;
            conn.OnConnecting += Conn_OnConnecting;
            conn.OnDisconnect += Conn_OnDisconnect;
            conn.OnReceive += Conn_OnReceive;

            conn.Connect();
        }

        public void Send(Entity entity) {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes(
                        JSON.Stringify(
                            Parser.EntitytoJSON(entity, entity.GetType())))));
        }

        public void Send(string data) {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes(data)));
        }

        private void Conn_OnReceive(string data) {
            string command = data.Split(':')[0];
            dynamic json = JSON.Parse(data.Substring(data.IndexOf(':') + 1));
            switch (json.type.ToString()) {
                case "authenticate":
                    if (json.success == true) {
                        foreach (var channel in session.GetAllChannels()) {
                            Send("!subscribe:messaging,threads." + channel.ChannelId);
                        }

                        foreach (var friendship in session.GetAllFriendships()) {
                            if (friendship.Active)
                                Send("!subscribe:messaging,users." + friendship.Addresser);
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
                    }
                    break;
                case "channel_change":
                    OnChannelChange?.Invoke(Parser.JSONtoEntity(json.payload, typeof(Channel)));
                    break;
                case "message":
                    OnMessageReceived?.Invoke(Parser.JSONtoEntity(json.payload, typeof(Message)));
                    break;
                default:
                    Console.WriteLine("Unhandled event " + json.type);
                    break;
            }
        }

        private void Conn_OnDisconnect() {
            Console.WriteLine("WebSocket disconnected.. Reconnecting in 5 seconds");
            new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(5000);
                conn.Connect();
            }).Start();
        }

        private void Conn_OnConnecting() {
            Console.WriteLine("Connecting..");
        }

        private void Conn_OnConnected() {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes("!authenticate:" + Guid.NewGuid().ToString() + ":" + session.Auth.Token)));
        }

        public void Dispose() {
            conn.Dispose();
        }
        ~Session() { Dispose(); }
    }
}
