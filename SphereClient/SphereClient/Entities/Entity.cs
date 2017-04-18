namespace SphereClient.Entities {
    public interface Entity {
        bool IsNull { get; set; }
        string ToText();
        string ToTypeString();
    }
}
