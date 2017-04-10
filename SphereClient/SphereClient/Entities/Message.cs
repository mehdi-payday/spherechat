using System;

namespace SphereClient.Entities {
    [Serializable]
    public class Message : Entity {
        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public string Contents { get; set; }
        public DateTime SentDate { get; set; }
        public Types Type { get; set; }
        public MessageTag[] Tags { get; set; }

        public enum Types {
            SYSTEM = 0,
            USER = 1
        }
    }
}
