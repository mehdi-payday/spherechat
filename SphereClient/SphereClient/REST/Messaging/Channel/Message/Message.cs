namespace SphereClient.REST.Messaging.Channel.Message {
    class Base : Request {
        public Base(int cId) : base("/messaging/channel/" + cId + "/messages/") { }
        public Base(int cId, int mId) : base("/messaging/channel/" + cId + "/messages" + mId + "/") { }
    }
}
