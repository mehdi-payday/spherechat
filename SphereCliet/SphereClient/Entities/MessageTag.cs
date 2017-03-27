using System;

namespace SphereClient.Entities {
    [Serializable]
    class MessageTag : Entity {
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public int PlaceholderPosition { get; set; }
    }
}
