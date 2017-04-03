using System;

namespace SphereClient.Entities {
    [Serializable]
    class Membership : Entity {
        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public DateTime LastSeenDate { get; set; }
        public int LastSeenMessageId { get; set; }
        public bool IsParticipant { get; set; }
        public DateTime JoinDate { get; set; }
        public User UserDetails { get; set; }
        public int UncheckedCount { get; set; }
    }
}
