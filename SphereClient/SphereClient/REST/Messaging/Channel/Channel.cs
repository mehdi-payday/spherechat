namespace SphereClient.REST.Messaging.Channel {
    class Base : Request {
        public Base() : base("/messaging/channel/") { }
        public Base(int id) : base("/messaging/channel/" + id + "/") { }
    }
}
