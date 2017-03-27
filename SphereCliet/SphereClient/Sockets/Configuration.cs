using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace SphereClient.Sockets {
    class Configuration {
        private const string NEW_LINE = "\r\n";

        public Configuration(IPEndPoint ep, string path, string key) {
            EP = ep;
            Path = path;
            Key = key;
        }

        public IPEndPoint EP { get; set; }
        public string Path { get; set; }
        public string Key { get; set; }

        static private string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        private string AcceptKey(string key) {
            string longKey = key + guid;
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hashBytes = sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(longKey));
            return Convert.ToBase64String(hashBytes);
        }

        public Buffers.HTTP.Write HelloServer {
            get {
                return new Buffers.HTTP.Write(new byte[0], new Dictionary<string, string>() {
                    { "Host", "localhost" },
                    { "Upgrade", "websocket" },
                    { "Connection", "Upgrade" },
                    { "Sec-WebSocket-Key", AcceptKey(guid) },
                    { "Origin", "http://localhost" },
                    { "Sec-WebSocket-Protocol", "chat, superchat" },
                    { "Sec-WebSocket-Version", "13" }
                });
            }
        }
    }
}
