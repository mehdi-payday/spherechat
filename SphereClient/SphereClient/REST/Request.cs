using System;
using System.Net;

namespace SphereClient.REST {
    public class Request : IDisposable {
        public string API = "http://spherechat.tk:8000/api";

        public Request(string method) {
            DefaultHeaders = new WebHeaderCollection();
            Method = method;
        }

        public Request POST(dynamic json = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                if (json.GetType() != typeof(String))
                    json = JSON.Stringify(json);

                Payload = JSON.Parse(wc.UploadString(API + Method, "POST", json ?? "{}"));
            }
            return this;
        }

        public Request GET(WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                Payload = JSON.Parse(wc.DownloadString(API + Method));
            }
            return this;
        }

        public Request PUT(string json = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                if (json.GetType() != typeof(String))
                    json = JSON.Stringify(json);

                Payload = JSON.Parse(wc.UploadString(API + Method, "PUT", json ?? "{}"));
            }
            return this;
        }

        public Request DELETE(WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                Payload = JSON.Parse(wc.UploadString(API + Method, "DELETE", "{}"));
            }
            return this;
        }

        public Request PATCH(string json = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                if (json.GetType() != typeof(String))
                    json = JSON.Stringify(json);

                Payload = JSON.Parse(wc.UploadString(API + Method, "PATCH", json ?? "{}"));
            }
            return this;
        }

        public Request OPTIONS(string json = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                if (json.GetType() != typeof(String))
                    json = JSON.Stringify(json);

                Payload = JSON.Parse(wc.UploadString(API + Method, "OPTONS", json ?? "{}"));
            }
            return this;
        }

        public WebHeaderCollection DefaultHeaders { get; set; }
        public string Method { get; set; }
        public dynamic Payload { get; set; }
        public Entities.Entity Entity { get; set; }

        public void Dispose() { }
        ~Request() {
            Dispose();
        }
    }
}
