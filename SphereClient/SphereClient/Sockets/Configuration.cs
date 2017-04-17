using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace SphereClient.Sockets {
    public class Configuration {
        public Configuration(string hostname, int port, string path, string key) {
            EP = new IPEndPoint(Dns.GetHostAddresses(hostname)[0], port);
            Hostname = hostname;
            Port = port;
            Path = path;
            Key = key;
        }

        public IPEndPoint EP { get; set; }
        public string Path { get; set; }
        public string Key { get; set; }
        public int Port { get; set; }
        public string Hostname { get; set; }

        /// <summary>
        /// Identifieur unique pour le WS
        /// </summary>
        static private string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        private string AcceptKey(string key) {
            string longKey = key + guid;
            SHA1 sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(longKey));
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Handshake pour passer de HTTP à WS
        /// </summary>
        public Buffers.HTTP.Write HelloServer {
            get {
                return new Buffers.HTTP.Write(new byte[] { }, new Dictionary<string, string>() {
                    { "Host", Hostname + ":" + Port },
                    { "Upgrade", "websocket" },
                    { "Connection", "Upgrade" },
                    { "Origin", "http://" + Hostname },
                    { "Sec-WebSocket-Key", AcceptKey(Key) },
                    { "Sec-WebSocket-Protocol", "chat, superchat" },
                    { "Sec-WebSocket-Version", "13" }
                });
            }
        }
    }
}
