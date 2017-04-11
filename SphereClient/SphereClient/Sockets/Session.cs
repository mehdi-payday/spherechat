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

        private void Conn_OnSent() {
            throw new System.NotImplementedException();
        }

        private void Conn_OnSending() {
            throw new System.NotImplementedException();
        }

        private void Conn_OnReceive(string data) {
            throw new System.NotImplementedException();
        }

        private void Conn_OnReady() {
            throw new System.NotImplementedException();
        }

        private void Conn_OnDisconnect() {
            conn.Send(new Buffers.WebSocket.Write(Encoding.UTF8.GetBytes("!authenticate:" + Guid.NewGuid().ToString() + ":" + auth.Token)));
        }

        private void Conn_OnConnecting() {
            throw new System.NotImplementedException();
        }

        private void Conn_OnConnected() {
            throw new System.NotImplementedException();
        }

        public void Dispose() { }
        ~Session() { Dispose(); }
    }
}
