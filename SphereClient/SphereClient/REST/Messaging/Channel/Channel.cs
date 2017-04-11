namespace SphereClient.REST.Messaging.Channel {
    class Base : Request {
        public Base() : base("/messaging/channel/") { }
        public Base(int cId) : base("/messaging/channel/" + cId + "/") { }
    }
}
