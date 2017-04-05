using System;

namespace SphereClient.Entities {
    [Serializable]
    public class PrivateDiscussion : Thread {
        public Membership InterlocutorMembership { get; set; }
    }
}
