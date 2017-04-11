namespace SphereClient.REST.Messaging.PrivateDiscussion.Message {
    class Base : Request {
        public Base(int cId) : base("/messaging/privatediscussion/" + cId + "/messages/") { }
        public Base(int cId, int mId) : base("/messaging/privatediscussion/" + cId + "/messages" + mId + "/") { }
    }
}
