using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereCliet.Sockets.Buffers.HTTP {
    class Read : Buffers.Read {
        private const string NEW_LINE = "\r\n";

        public Read(byte[] buffer) : base(buffer) {
            Headers = new List<KeyValuePair<string, string>>();
        }

        public void Parse() {
            string[] data = Encoding.UTF8.GetString(Data.ToArray()).Split(new string[] { NEW_LINE }, StringSplitOptions.None);
            for (int a = 0; a < data.Length; a++) {
                if (a == 0) {
                    string[] parts = data[a].Split(' ');
                    HTTP_VERSION = Convert.ToDouble(parts[0].Substring(5).Replace('.', ','));
                    HTTP_STATUS = (ushort)Convert.ToInt16(parts[1]);
                    for (int b = 2; b < parts.Length; b++) {
                        HTTP_RESULT += parts[b] + " ";
                    }
                    HTTP_RESULT = HTTP_RESULT.Trim();
                }
                else if (data[a].IndexOf(':') != -1) {
                    string[] parts = data[a].Split(':');
                    if (parts.Length == 2) {
                        Headers.Add(new KeyValuePair<string, string>(parts[0].Trim(), parts[1].Trim()));
                    }
                }
                else if (string.IsNullOrEmpty(data[a])) {
                    Payload = string.Join(NEW_LINE, data.Skip(a)).Remove(0, 2);
                    break;
                }
            }
        }

        public double HTTP_VERSION { get; set; }
        public ushort HTTP_STATUS { get; set; }
        public string HTTP_RESULT { get; set; }

        public string Payload { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }
    }

}
