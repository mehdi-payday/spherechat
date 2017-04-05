using SphereClient.Entities;
using System;
using System.Collections.Generic;

namespace SphereClient.REST {
    public class Parser {
        public static dynamic Parse(dynamic json, Type type) {
            if (type == typeof(Entities.Auth)) {
                if (json == null || json.ToString() == "") {
                    return new Entities.Auth() {
                        IsNull = true
                    };
                }
                else {
                    return new Entities.Auth() {
                        Token = json.key
                    };
                }
            }
            else if (type == typeof(User)) {
                if (json == null || json.ToString() == "") {
                    return new User() {
                        IsNull = true
                    };
                }
                else {
                    return new User() {
                        UserId = ParseInt(json.id.ToString()),
                        Username = json.username.ToString(),
                        FirstName = json.first_name.ToString(),
                        LastName = json.last_name.ToString(),
                        Type = (User.Types)Enum.Parse(typeof(User.Types), json.type.ToString().ToUpper())
                    };
                }
            }
            else if (type == typeof(Channel)) {
                if (json == null || json.ToString() == "") {
                    return new Channel() {
                        IsNull = true
                    };
                }
                else {
                    return new Channel() {
                        Id = ParseInt(json.id.ToString()),
                        Slug = json.slug.ToString(),
                        Title = json.title.ToString(),
                        Type = (Thread.Types)Enum.Parse(typeof(Thread.Types), json.type.ToString().ToUpper()),
                        Description = json.description.ToString(),
                        Membership = Parse(json.membership, typeof(Membership)),
                        Memberships = Parse(json.memberships, typeof(Membership[])),
                        ManagerUser = ParseInt(json.manager_user.ToString()),
                        ManagerDetails = Parse(json.manager_details, typeof(User))
                    };
                }
            }
            else if (type == typeof(Channel[])) {
                List<Channel> channels = new List<Channel>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        channels.Add(Parse(a, typeof(Channel)));
                    }
                }
                return channels.ToArray();
            }
            else if (type == typeof(Membership)) {
                if (json == null || json.ToString() == "") {
                    return new Membership() {
                        IsNull = true
                    };
                }
                else {
                    return new Membership() {
                        Id = ParseInt(json.id.ToString()),
                        ThreadId = ParseInt(json.thread.ToString()),
                        UserId = ParseInt(json.user.ToString()),
                        LastSeenDate = ParseDate(json.last_seen_date.ToString()),
                        LastSeenMessageId = ParseInt(json.last_seen_message.ToString()),
                        IsParticipant = Convert.ToBoolean(json.active.ToString()),
                        JoinDate = ParseDate(json.join_date.ToString()),
                        UserDetails = Parse(json.user_details, typeof(User)),
                        UncheckedCount = ParseInt(json.unchecked_count.ToString())
                    };
                }
            }
            else if (type == typeof(Membership[])) {
                List<Membership> memberships = new List<Membership>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json) {
                        memberships.Add(Parse(a, typeof(Membership)));
                    }
                }
                return memberships.ToArray();
            }
            else if (type == typeof(Message)) {
                if (json == null || json.ToString() == "") {
                    return new Message() {
                        IsNull = true
                    };
                }
                else {
                    return new Message() {
                        Id = ParseInt(json.id.ToString()),
                        ThreadId = ParseInt(json.thread.ToString()),
                        UserId = ParseInt(json.user_sender.ToString()),
                        Contents = json.contents.ToString(),
                        SentDate = ParseDate(json.sent_date.ToString()),
                        Type = (Message.Types)Enum.Parse(typeof(Message.Types), (json.message_type.ToString() == "" ? "user" : json.message_type.ToString()).ToUpper()),
                        Tags = Parse(json.tags, typeof(MessageTag[]))
                    };
                }
            }
            else if (type == typeof(Message[])) {
                List<Message> message = new List<Message>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        message.Add(Parse(a, typeof(Message)));
                    }
                }
                return message.ToArray();
            }
            else if (type == typeof(MessageTag)) {
                if (json == null || json.ToString() == "") {
                    return new MessageTag() {
                        IsNull = true
                    };
                }
                else {
                    return new MessageTag() {
                        TaggedUserId = ParseInt(json.tagged_user.ToString()),
                        MessageId = ParseInt(json.messsage.ToString()),
                        PlaceholderPosition = ParseInt(json.placeholder_position.ToString()),
                        TaggedUserDetails = Parse(json.tagged_user_details, typeof(User))
                    };
                }
            }
            else if (type == typeof(MessageTag[])) {
                List<MessageTag> messagetag = new List<MessageTag>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json) {
                        messagetag.Add(Parse(a, typeof(MessageTag)));
                    }
                }
                return messagetag.ToArray();
            }
            else {
                throw new Exception("Unhandled Type " + type.ToString());
            }
        }

        private static DateTime ParseDate(string value) {
            DateTime result;
            return DateTime.TryParse(value, out result) ? result : new DateTime();
        }

        private static double ParseDouble(string value) {
            double result;
            return double.TryParse(value, out result) ? result : -1;
        }

        private static int ParseInt(string value) {
            int result;
            return int.TryParse(value, out result) ? result : -1;
        }
    }
}
