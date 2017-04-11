using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SphereClient.Sockets {

    #region Delegates
    public delegate void Connecting();
    public delegate void Connected();
    public delegate void Disconnect();
    public delegate void Ready();

    public delegate void Sending();
    public delegate void Sent();

    public delegate void Receive(string data);
    #endregion Delegates

    class Connection : IDisposable {
        #region Constructors
        public Connection(Configuration config, bool connect = true) {
            Configuration = config;

            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            if (connect)
                Connect();
        }
        #endregion Constructors

        #region Events
        public event Connecting OnConnecting;
        public event Connected OnConnected;
        public event Disconnect OnDisconnect;
        public event Ready OnReady;

        public event Sending OnSending;
        public event Sent OnSent;

        public event Receive OnReceive;
        #endregion Events

        #region Get/Set
        private Socket Socket { get; set; }
        public Configuration Configuration { get; set; }
        #endregion Get/Set

        public void Connect() {
            OnConnecting?.Invoke();
            Socket.BeginConnect(Configuration.EP, beginConnect, new Buffers.HTTP.Read(new byte[4096]));
        }

        public void Send(Buffers.WebSocket.Write buffer) {
            var parsed = buffer.Parsed;
            Socket.BeginSend(parsed, 0, parsed.Length, 0, beginSend, buffer);
            OnSending?.Invoke();
        }

        private void beginSend(IAsyncResult result) {
            Buffers.WebSocket.Write buffer = (Buffers.WebSocket.Write)result.AsyncState;

            Socket.EndSend(result);

            if (!buffer.Type.HasFlag(Buffers.WebSocket.Write.DataType.PIN | Buffers.WebSocket.Write.DataType.PON))
                OnSent?.Invoke();
        }

        private void beginConnect(IAsyncResult result) {
            Socket.EndConnect(result);
            OnConnected?.Invoke();

            var hello = Encoding.UTF8.GetBytes(Configuration.HelloServer.Header("GET", Configuration.Path));
            Socket.Send(hello, hello.Length, 0);

            Buffers.HTTP.Read buffer = new Buffers.HTTP.Read(new byte[4096]);
            Socket.BeginReceive(buffer.Buffer, 0, buffer.Size, 0, beginReceive, buffer);
        }

        private void beginReceive(IAsyncResult result) {
            Buffers.Read buffer = (Buffers.Read)result.AsyncState;

            if (!Socket.Connected) {
                OnDisconnect?.Invoke();
                return;
            }

            int read = Socket.EndReceive(result);
            if (read == 0) {
                //Socket.Disconnect(true);
                OnDisconnect?.Invoke();
            }
            else {
                Buffers.Read next;

                if (buffer.GetType() == typeof(Buffers.HTTP.Read)) {
                    Buffers.HTTP.Read current = buffer as Buffers.HTTP.Read;
                    current.Append(read);

                    if (read == buffer.Size) {
                        Socket.BeginReceive(current.Buffer, 0, current.Size, 0, beginReceive, current);
                    }
                    else if (read < buffer.Size) {
                        current.Parse();

                        // Upgrade to websocket on server request
                        if (current.HTTP_STATUS == 101 && current.Headers.Contains(new KeyValuePair<string, string>("Upgrade", "websocket"))) {
                            OnReady?.Invoke();
                            next = new Buffers.WebSocket.Read(new byte[4096]);
                        }
                        else {
                            var hello = Encoding.UTF8.GetBytes(Configuration.HelloServer.Header("GET", Configuration.Path));
                            Socket.Send(hello, hello.Length, 0);

                            next = new Buffers.HTTP.Read(new byte[4096]);
                        }

                        string payload = current.Payload.Replace("\0", "").Trim();
                        if (!string.IsNullOrEmpty(payload)) {
                            OnReceive?.Invoke(payload);
                        }

                        Socket.BeginReceive(next.Buffer, 0, next.Size, 0, beginReceive, next);
                    }
                }
                else if (buffer.GetType() == typeof(Buffers.WebSocket.Read)) {
                    Buffers.WebSocket.Read current = buffer as Buffers.WebSocket.Read;
                    current.Append(read);

                    if (read == buffer.Size) {
                        Socket.BeginReceive(current.Buffer, 0, current.Size, 0, beginReceive, current);
                    }
                    else if (read < buffer.Size) {
                        current.Parse();

                        if (current.Type == Buffers.WebSocket.Read.DataType.PIN) { // Answer PON to PIN
                            Buffers.WebSocket.Write pong = new Buffers.WebSocket.Write(new byte[0], Buffers.WebSocket.Write.DataType.PON, false);
                            Socket.BeginSend(pong.Parsed, 0, pong.Parsed.Length, 0, beginSend, pong);
                        }
                        else {
                            OnReceive?.Invoke(Encoding.UTF8.GetString(current.Payload));
                        }

                        next = new Buffers.WebSocket.Read(new byte[4096]);
                        Socket.BeginReceive(next.Buffer, 0, next.Size, 0, beginReceive, next);
                    }
                }
            }
        }

        public void Dispose() { }

        ~Connection() { Dispose(); }
    }

}
