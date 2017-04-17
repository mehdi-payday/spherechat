using System;

namespace SphereClient.Entities {
    public struct Search : Entity {
        public User[] Users { get; set; }
        public Channel[] Channels { get; set; }

        public bool IsNull { get; set; }
        public string ToText() {
            throw new Exception("Dude");
        }
    }
}
