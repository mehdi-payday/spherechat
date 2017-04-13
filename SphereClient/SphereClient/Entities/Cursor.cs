using System.Collections.Generic;

namespace SphereClient.Entities {
    public class Cursor<T> : List<T> {
        public string Next { get; set; }
        public string Previous { get; set; }
    }
}
