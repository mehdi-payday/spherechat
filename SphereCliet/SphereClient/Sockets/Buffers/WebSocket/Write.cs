using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereClient.Sockets.Buffers.WebSocket {
    class Write : Buffers.Read {
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
                var bits = new List<bool>();

                List<bool> mask = new List<bool>();
                for (; mask.Count < 32; mask.Add(r.Next(0, 2) == 0)) ;

                // FIN
                bits.Add(true);

                // RSV1
                bits.Add(false);

                // RSV2
                bits.Add(false);

                // RSV3
                bits.Add(false);

                // OPCODE
                bits.AddRange(Convert.ToString((byte)Type, 2).PadLeft(8, '0').Substring(4).Select(x => x == '1' ? true : false).ToArray());

                // MASK
                bits.Add(Masked);

                // LENGTH
                if (Buffer.Length < 126) {
                    // 7bit number
                    bits.AddRange(Convert.ToString(Buffer.Length, 2).PadLeft(7, '0').Select(x => x == '1' ? true : false).ToArray());
                }
                else if (Buffer.Length == 126) {
                    // 126 + 16bit number
                    bits.AddRange(new bool[] { true, true, true, true, true, true, false }.Concat(Convert.ToString(Buffer.Length, 2).PadLeft(16, '0').Select(x => x == '1' ? true : false)).ToArray());
                }
                else {
                    // 127 + 64bit number
                    bits.AddRange(new bool[] { true, true, true, true, true, true, true }.Concat(Convert.ToString(Buffer.Length, 2).PadLeft(64, '0').Select(x => x == '1' ? true : false)).ToArray());
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
                            bool m = mask[c % 32],
                                 v = bin[b] == '1' ? true : false,
                                 xor = ((!m && v) || (m && !v)) ? true : false; // XOR
                            Console.Write(m ? '1' : '0');
                            sb.Append(xor ? '1' : '0');
                        }
                        else {
                            sb.Append(bin[b]);
                        }
                    }
                }
                bits.AddRange(sb.ToString().Select(x => x == '1' ? true : false).ToArray());

                // CONVERT TO BYTE ARRAY
                byte[] bytes = bits.Select((v, i) => new { Value = v ? '1' : '0', Index = i })
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
