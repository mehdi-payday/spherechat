using System;

namespace SphereClient.Entities {
    public struct Membership : Entity {
        public int MembershipId { get; set; }
        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public DateTime LastSeenDate { get; set; }
        public int LastSeenMessageId { get; set; }
        public bool IsParticipant { get; set; }
        public DateTime JoinDate { get; set; }
        public User UserDetails { get; set; }
        public int UncheckedCount { get; set; }
        public bool IsNull { get; set; }

        public string ToText() {
            return JSON.Stringify(this);
        }
        public string ToTypeString() {
            return "Membership";
        }
    }
}
