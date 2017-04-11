using System;

namespace SphereClient.Entities {
    public struct FriendRequest {
        public int Requester { get; set; }
        public User RequesterDetails { get; set; }
        public Statuses Status { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsNull { get; set; }

        public enum Statuses {
            PEND = 0,
            ACPT = 1,
            RJCT = 2
        }
    }
}
