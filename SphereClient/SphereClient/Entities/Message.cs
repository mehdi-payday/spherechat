using System;

namespace SphereClient.Entities {
    [Serializable]
    class Message : Entity {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public User UserDetails { get; set; }
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
