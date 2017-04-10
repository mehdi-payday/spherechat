namespace SphereClient.REST.Messaging.Channel {
    public class Base : Request {
        public Base() : base("/messaging/channel/") { }
        public Base(int id) : base("/messaging/channel/" + id + "/") { }
    }
}
