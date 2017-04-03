namespace SphereClient.REST.Users {
    class Base : Request {
        public Base(int id) : base("/users/" + id) {

        }
    }
}
