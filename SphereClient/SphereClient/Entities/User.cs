using System;

namespace SphereClient.Entities {
    [Serializable]
    class User : Entity {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Types Type { get; set; }
        public DateTime CreationDate { get; set; }
        public int ListeningThreadId { get; set; }
        public DateTime LastListeningDate { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsFriend { get; set; }

        public enum Types {
            HUM = 0,
            BOT = 1
        }
    }
}
