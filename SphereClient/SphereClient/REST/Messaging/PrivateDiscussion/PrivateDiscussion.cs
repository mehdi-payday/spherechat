namespace SphereClient.REST.Messaging.PrivateDiscussion {
    class Base : Request {
        public Base() : base("/messaging/privatediscussion/") { }
        public Base(int cId) : base("/messaging/privatediscussion/" + cId + "/") { }
    }
}
