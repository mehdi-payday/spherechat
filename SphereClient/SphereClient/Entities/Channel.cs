﻿namespace SphereClient.Entities {
    public struct Channel : Entity {
        public int ChannelId { get; set; }
        public int ThreadId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Types Type { get; set; }
        public string Description { get; set; }
        public int ManagerUser { get; set; }
        public User ManagerDetails { get; set; }
        public Membership Membership { get; set; }
        public Membership[] Memberships { get; set; }
        public bool IsNull { get; set; }

        public enum Types {
            DISCUSSION = 0,
            PRIVATE_CHANNEL = 1,
            PUBLIC_CHANNEL = 2
        }
    }
}
