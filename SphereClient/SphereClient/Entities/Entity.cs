using System;

namespace SphereClient.Entities {
    [Serializable]
    abstract class Entity {
        public int Id { get; set; }

        private bool isnull = false;
        public bool IsNull { get { return isnull; } set { isnull = value; } }

        public string ToJSON() {
            return JSON.Stringify(this);
        }
    }
}
