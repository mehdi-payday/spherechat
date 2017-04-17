namespace SphereClient.REST.Users {
    public class Base : Request {
        public Base() : base("/users/") { }
        public Base(int id) : base("/users/" + id) { }
    }
}
