using System;

namespace SphereClient.Entities {
    [Serializable]
    abstract class Entity {
        public string ToJSON() {
            return JSON.Stringify(this);
        }
    }
}
