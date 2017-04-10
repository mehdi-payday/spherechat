namespace SphereClient.REST.Users {
    public class Base : Request {
        public Base(int id) : base("/users/" + id) {

        }
    }
}
