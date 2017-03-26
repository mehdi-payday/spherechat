using System.Collections.Generic;
using System.Text;

namespace SphereCliet.Sockets.Buffers.HTTP {

    class Write : Buffers.Write {
        private const string NEW_LINE = "\r\n";

        public Write(byte[] buffer) : base(buffer) {
            Headers = new List<KeyValuePair<string, string>>();
        }

        public Write(byte[] buffer, List<KeyValuePair<string, string>> headers) : base(buffer) {
            Headers = headers;
        }

        public string Header(string method = "GET", string path = "/", double http_version = 1.1) {
            string headers = method.ToUpper() + " " + path + " HTTP/" + (http_version.ToString().Replace(',', '.')) + NEW_LINE;
            foreach (var header in Headers) {
                headers += header.Key + ": " + header.Value + NEW_LINE;
            }
            headers += NEW_LINE;
            headers += Encoding.UTF8.GetString(Buffer);

            return headers;
        }

        public List<KeyValuePair<string, string>> Headers { get; set; }
    }
}
