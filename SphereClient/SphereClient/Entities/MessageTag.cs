using System;

namespace SphereClient.Entities {
    [Serializable]
    class MessageTag : Entity {
        public int TaggedUserId { get; set; }
        public int MessageId { get; set; }
        public int PlaceholderPosition { get; set; }
        public User TaggedUserDetails { get; set; }
    }
}
