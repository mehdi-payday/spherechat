using System;

namespace SphereClient.Entities {
    [Serializable]
    public class Channel : Thread {
        public Membership[] Memberships { get; set; }

        public override string ToText() {
            return this.Title;
        }
    }
}
