using System;

namespace SphereClient.Entities {
    [Serializable]
    class PrivateDiscussion : Thread {
        public Membership InterlocutorMembership { get; set; }
    }
}
