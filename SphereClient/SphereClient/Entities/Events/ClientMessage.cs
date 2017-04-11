using System;

namespace SphereClient.Entities.Events {
    public class ClientMessage : Message {
        new public int ThreadId { get { throw new InvalidPropertyException(); } set { throw new InvalidPropertyException(); } }
        new public int UserId { get { throw new InvalidPropertyException(); } set { throw new InvalidPropertyException(); } }
        new public User UserDetails { get { throw new InvalidPropertyException(); } set { throw new InvalidPropertyException(); } }

        class InvalidPropertyException : Exception { }
    }
}
