using System;

namespace SphereClient.Entities {
    [Serializable]
    public class FriendRequest {
        public int Requester { get; set; }
        public User RequesterDetails { get; set; }
        public Statuses Status { get; set; }
        public DateTime RequestDate { get; set; }

        public enum Statuses {
            PEND = 0,
            ACPT = 1,
            RJCT = 2
        }
    }
}
