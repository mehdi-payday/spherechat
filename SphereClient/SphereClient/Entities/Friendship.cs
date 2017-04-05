using System;

namespace SphereClient.Entities {
    [Serializable]
    public class Friendship : Entity {
        public int Requester { get; set; }
        public int Addresser { get; set; }
        public User FriendDetails { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime FriendshipEndDate { get; set; }
        public bool Active { get; set; }
    }
}
