namespace SphereClient.Entities {
    public struct Auth : Entity {
        public string Token { get; set; }
        public bool IsNull { get; set; }

        public string ToText() {
            return JSON.Stringify(this);
        }

        public string ToTypeString() {
            return "Auth";
        }
    }
}
