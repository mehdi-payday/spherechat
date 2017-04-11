using SphereClient.REST;
using System;
using System.Text;

namespace SphereClient.Sockets {

    public delegate void MessageReceived();
    public delegate void DiscussionChange();
    public delegate void ChannelChange();
    public delegate void NewFriendRequest();
    public delegate void FriendRequestAdressed();

    class Session : IDisposable {
        private Connection conn { get; set; }
        private Entities.Auth auth { get; set; }

        public Session(Configuration config, Entities.Auth auth) {
            conn = new Connection(config, false);
            this.auth = auth;

            conn.OnConnected += Conn_OnConnected;
            conn.OnConnecting += Conn_OnConnecting;
            conn.OnDisconnect += Conn_OnDisconnect;
            conn.OnReady += Conn_OnReady;
            conn.OnReceive += Conn_OnReceive;
            conn.OnSending += Conn_OnSending;
            conn.OnSent += Conn_OnSent;

            conn.Connect();
        }

        public void Send(Entities.Entity entity) {
            conn.Send(
                new Buffers.WebSocket.Write(
                    Encoding.UTF8.GetBytes(
                        JSON.Stringify(
                            Parser.EntitytoJSON(entity, entity.GetType())))));
        }

        private void Conn_OnSent() {
            Console.WriteLine("Sent !");
        }

        private void Conn_OnSending() {
            Console.WriteLine("Sending..");
        }

        private void Conn_OnReceive(string data) {
            Console.WriteLine(data);
        }

        private void Conn_OnReady() {
            conn.Send(new Buffers.WebSocket.Write(Encoding.UTF8.GetBytes("!authenticate:" + Guid.NewGuid().ToString() + ":" + auth.Token)));
        }

        private void Conn_OnDisconnect() {
            Console.WriteLine("Disconnected !");
        }

        private void Conn_OnConnecting() {
            Console.WriteLine("Connecting..");
        }

        private void Conn_OnConnected() {
            Console.WriteLine("Connected !");
        }

        public void Dispose() {
            conn.Dispose();
        }
        ~Session() { Dispose(); }
    }
}
