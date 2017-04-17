using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereClient.Sockets.Buffers.WebSocket {
    public class Read : Buffers.Read {
        public Read(byte[] buffer) : base(buffer) { }

        public void Parse() {
            List<char> bits = new List<char>();
            for (int a = 0; a < Data.Count; a++)
                bits.AddRange(Convert.ToString(Data[a], 2).PadLeft(8, '0').ToArray());
            int index = 0;

            FIN = bits[index++] == '1';

            index += 3; // SKIP RSV[1-3]

            OPCODE = new char[] { '0', '0', '0', '0', bits[index++], bits[index++], bits[index++], bits[index++] };
            var opcode = Convert.ToInt32(new string(OPCODE), 2);
            Type = (DataType)opcode;

            Masked = bits[index++] == '1';

            int len = Convert.ToInt32(new string(new char[] { '0', bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++] }), 2);
            if (len < 126) {
                Length = len;
            }
            else {
                List<char> bitnb = new List<char>();
                if (len == 126) {
                    for (int a = 0; a < 16; a++)
                        bitnb.Add(bits[index++]);
                    Length = Convert.ToInt16(new string(bitnb.ToArray()), 2);
                }
                else {
                    for (int a = 0; a < 64; a++)
                        bitnb.Add(bits[index++]);
                    Length = Convert.ToInt64(new string(bitnb.ToArray()), 2);
                }
            }
            Length *= 8;

            if (Length > 0) {
                StringBuilder sb = new StringBuilder();
                for (int curr = index; index < curr + Length;) {
                    sb.Append((char)Convert.ToInt32(new string(new char[] { bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++], bits[index++] }), 2));
                }

                Payload = Encoding.ASCII.GetBytes(sb.ToString());
            }
            else {
                Payload = new byte[0];
            }
        }

        public bool FIN { get; set; }
        public char[] OPCODE { get; set; }
        public bool Masked { get; set; }
        public char[] Mask { get; set; }
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
