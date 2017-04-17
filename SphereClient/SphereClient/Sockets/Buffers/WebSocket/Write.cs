using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereClient.Sockets.Buffers.WebSocket {
    public class Write : Buffers.Read {
        public Write(byte[] buffer, bool masked = true) : base(buffer) {
            Type = DataType.TXT;
            Masked = masked;
        }
        public Write(byte[] buffer, DataType type, bool masked = true) : base(buffer) {
            Type = type;
            Masked = masked;
        }

        private Random r = new Random();

        public byte[] Parsed {
            get {
                var bits = new List<char>();

                List<char> mask = new List<char>();
                for (; mask.Count < 32; mask.Add(r.Next(0, 2) == 0 ? '1' : '0')) ;

                // FIN
                bits.Add('1');

                // RSV1
                bits.Add('0');

                // RSV2
                bits.Add('0');

                // RSV3
                bits.Add('0');

                // OPCODE
                bits.AddRange(Convert.ToString((byte)Type, 2).PadLeft(8, '0').Substring(4).ToArray());

                // MASK
                bits.Add(Masked ? '1' : '0');

                // LENGTH
                if (Buffer.Length < 126) {
                    // 7bit number
                    bits.AddRange(Convert.ToString(Buffer.Length, 2).PadLeft(7, '0').ToArray());
                }
                else if (Buffer.Length == 126) {
                    // 126 + 16bit number
                    bits.AddRange("1111110");
                    bits.AddRange(Convert.ToString(Buffer.Length, 2).PadLeft(16, '0').ToArray());
                }
                else {
                    // 127 + 64bit number
                    bits.AddRange("1111111");
                    bits.AddRange(Convert.ToString(Buffer.Length, 2).PadLeft(64, '0').ToArray());
                }

                // MASK
                if (Masked)
                    bits.AddRange(mask);

                // PAYLOAD
                StringBuilder sb = new StringBuilder();
                for (int a = 0, c = 0; a < Buffer.Length; a++) {
                    string bin = Convert.ToString(Buffer[a], 2).PadLeft(8, '0');
                    for (int b = 0; b < bin.Length; b++, c++) {
                        if (Masked) {
                            char m = mask[c % 32],
                                 v = bin[b];
                            sb.Append(((m == '0' && v == '1') || (m == '1' && v == '0')) ? '1' : '0');// XOR
                        }
                        else {
                            sb.Append(bin[b]);
                        }
                    }
                }
                bits.AddRange(sb.ToString().ToArray());

                // CONVERT TO BYTE ARRAY
                byte[] bytes = bits.Select((v, i) => new { Value = v, Index = i })
                    .GroupBy(x => x.Index / 8)
                    .Select(g => new string(g.Select(x => x.Value).ToArray()))
                    .Select(s => Convert.ToByte(s, 2)).ToArray();

                return bytes;
            }
        }
        public bool Masked { get; set; }
        public DataType Type { get; set; }

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
