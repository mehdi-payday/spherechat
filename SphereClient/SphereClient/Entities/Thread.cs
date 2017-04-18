
namespace SphereClient.Entities {
    public struct Thread : Entity {
        public int ThreadId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Types Type { get; set; }
        public string Description { get; set; }
        public int CreatorUser { get; set; }
        public int ManagerUser { get; set; }
        public User ManagerDetails { get; set; }
        public Membership Membership { get; set; }
        public Membership[] Memberships { get; set; }

        public bool IsNull { get; set; }

        public string ToText() {
            return JSON.Stringify(this);
        }

        public enum Types {
            private_channel = 1,
            public_channel = 2
        }
    }
}
