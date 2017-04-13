using System;

namespace SphereClient.Entities {
    public struct Friendship : Entity {
        public int FriendshipId { get; set; }
        public int Requester { get; set; }
        public int Addresser { get; set; }
        public User FriendDetails { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime FriendshipEndDate { get; set; }
        public bool Active { get; set; }
        public bool IsNull { get; set; }

        public string ToText() {
            return JSON.Stringify(this);
        }
    }
}
