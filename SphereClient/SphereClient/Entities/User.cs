using System;

namespace SphereClient.Entities {
    public struct User : Entity {
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
        public bool IsNull { get; set; }

        public string ToText() {
            return JSON.Stringify(this);
        }

        public enum Types {
            HUM = 0,
            BOT = 1
        }
        public string ToTypeString() {
            return "User";
        }
    }
}
