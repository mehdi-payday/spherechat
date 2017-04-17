namespace SphereClient.REST.Messaging.Channel.AddMembers {
    public class Base : Request {
        public Base(int cId) : base("/messaging/channel/" + cId + "/add_members/") { }
    }
}
