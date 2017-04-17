using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SphereClient.Sockets.Buffers.HTTP {
    public class Write : Buffers.Write {
        private const string NEW_LINE = "\r\n";

        public Write(byte[] buffer) : base(buffer) {
            Headers = new Dictionary<string, string>();
            addDefaultHeaders();
        }

        public Write(byte[] buffer, Dictionary<string, string> headers) : base(buffer) {
            Headers = headers;
            addDefaultHeaders();
        }

        private void addDefaultHeaders() {
            if (!Headers.ContainsKey("User-Agent"))
                Headers.Add("User-Agent", "sphereclient " + Application.ProductVersion);
        }

        public string Header(string method = "GET", string path = "/") {
            string headers = method.ToUpper() + " " + path + " HTTP/1.1" + NEW_LINE;
            string payload = Encoding.UTF8.GetString(Buffer);
            foreach (var header in Headers) {
                headers += header.Key + ": " + header.Value + NEW_LINE;
            }
            headers += NEW_LINE;
            headers += payload;

            return headers;
        }

        public Dictionary<string, string> Headers { get; set; }
    }
}
