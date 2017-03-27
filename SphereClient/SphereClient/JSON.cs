namespace SphereClient {
    public static class JSON {
        public static dynamic Parse(string json) {
            return Newtonsoft.Json.Linq.JObject.Parse(json);
        }

        public static string Stringify(object obj) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
