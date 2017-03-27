using System;

namespace SphereClient.Entities {
    [Serializable]
    class Channel : Thread {
        public Membership[] Memberships { get; set; }
    }
}
