using System;

namespace SphereClient.Entities {
    public struct Channel : Entity {
        public string title;
        public int ThreadId { get; set; }
        public string Slug { get; set; }
        public string Title {
            get {
                return this.title;
            }
            set { this.title = value; }
        }
        public Types Type { get; set; }
        public string Description { get; set; }
        public int CreatorUser { get; set; }
        public int ManagerUser { get; set; }
        public User ManagerDetails { get; set; }
        public Membership Membership { get; set; }
        public Membership[] Memberships { get; set; }
        public int[] Members { get; set; }
        public bool IsNull { get; set; }


        public string ToText() {
            return Title;//JSON.Stringify(this);
        }

        public enum Types {
            private_channel = 1,
            public_channel = 2
        }
        public string ToTypeString() {
            return Type == Types.private_channel ? "private_channel":"public_channel";
        }
    }
}
