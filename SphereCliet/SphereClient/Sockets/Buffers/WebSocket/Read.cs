using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereClient.Sockets.Buffers.WebSocket {

    class Read : Buffers.Read {
        public Read(byte[] buffer) : base(buffer) { }

        public void Parse() {
            List<bool> bits = new List<bool>();
            for (int a = 0; a < Data.Count; a++)
                bits.AddRange(Convert.ToString(Data[a], 2).PadLeft(8, '0').Select(x => x == '1' ? true : false).ToArray());
            int index = 0;

            FIN = bits[index++];

            index += 3; // SKIP RSV[1-3]

            OPCODE = new bool[] { false, false, false, false, bits[index++], bits[index++], bits[index++], bits[index++] };
            var opcode = Convert.ToInt32(string.Join("", OPCODE.Select(x => x ? "1" : "0")), 2);
            Type = (DataType)opcode;

            Masked = bits[index++];

            int len = Convert.ToInt32(string.Join("", new bool[] { false, bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++] }.Select(x => x ? "1" : "0")), 2);
            if (len < 126) {
                Length = len;
            }
            else if (len == 126) {
                List<bool> bitnb = new List<bool>();
                for (int a = 0; a < 16; a++)
                    bitnb.Add(bits[index++]);
                Length = Convert.ToInt16(string.Join("", bitnb.Select(x => x ? "1" : "0")), 2);
            }
            else {
                List<bool> bitnb = new List<bool>();
                for (int a = 0; a < 64; a++)
                    bitnb.Add(bits[index++]);
                Length = Convert.ToInt64(string.Join("", bitnb.Select(x => x ? "1" : "0")), 2);
            }
            Length *= 8;

            if (Length > 0) {
                StringBuilder sb = new StringBuilder();
                for (int curr = index; index < curr + Length;) {
                    sb.Append((char)Convert.ToInt32(string.Join("", new bool[] { bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++] }.Select(x => x ? "1" : "0")), 2));
                }

                Payload = Encoding.ASCII.GetBytes(sb.ToString());
            }
            else {
                Payload = new byte[0];
            }
        }

        public bool FIN { get; set; }
        public bool[] OPCODE { get; set; }
        public bool Masked { get; set; }
        public bool[] Mask { get; set; }
        public long Length { get; set; }
        public DataType Type { get; set; }
        public byte[] Payload { get; set; }

        public enum DataType {
            CON = 0x0,
            TXT = 0x1,
            BIN = 0x2,
            END = 0x8,
            PIN = 0x9,
            PON = 0xA
        }
    }
}
