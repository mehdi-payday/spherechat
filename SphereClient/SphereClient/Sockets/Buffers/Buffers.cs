using System;
using System.Collections.Generic;
using System.Linq;

namespace SphereClient.Sockets.Buffers {
    public abstract class Read : IDisposable {
        public Read(byte[] buffer) { Buffer = buffer; Data = new List<byte>(); }

        public List<byte> Data { get; set; }

        public byte[] Buffer { get; set; }
        public int Size { get { return Buffer.Length; } }

        public void Append(int read) {
            Data.AddRange(Buffer.Take(read));
            Buffer = new byte[Buffer.Length];
        }

        public void Dispose() {
            Data.Clear();
            Buffer = new byte[0] { };
        }

        ~Read() {
            Dispose();
        }
    }

    public abstract class Write : IDisposable {
        public Write(byte[] buffer) { Buffer = buffer; }

        public byte[] Buffer { get; set; }
        public int Size { get { return Buffer.Length; } }

        public void Dispose() {
            Buffer = new byte[0] { };
        }

        ~Write() {
            Dispose();
        }
    }
}
