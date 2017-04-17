using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

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

        public Request GET(Dictionary<string, string> qs = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                string query = "?";
                if (qs != null) {
                    foreach (var q in qs) {
                        query += q.Key + "=" + Regex.Unescape(q.Value);
                    }
                }

                Payload = JSON.Parse(wc.DownloadString(API + Method + (query != "?" ? query : "")));
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

        public Request DELETE(Dictionary<string, string> qs = null, WebHeaderCollection additionalHeaders = null) {
            using (var wc = new WebClient()) {
                if (additionalHeaders != null)
                    wc.Headers.Add(additionalHeaders);
                wc.Headers.Add(DefaultHeaders);

                string query = "?";
                if (qs != null) {
                    foreach (var q in qs) {
                        query += q.Key + "=" + Regex.Unescape(q.Value);
                    }
                }

                Payload = JSON.Parse(wc.UploadString(API + Method + (query != "?" ? query : ""), "DELETE", "{}"));
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
