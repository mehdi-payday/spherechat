using SphereClient.Entities;
using System;
using System.Collections.Generic;

namespace SphereClient.REST {
    public class Parser {
        public static dynamic EntitytoJSON(dynamic entity, Type type) {
            if (type == typeof(Channel)) {
                Channel channel = (Channel)entity;
                return new {
                    id = channel.ThreadId,
                    slug = channel.Slug,
                    title = channel.Title,
                    type = EnumToString(channel.Type, typeof(Channel.Types)),
                    description = channel.Description,
                    membership = EntitytoJSON(channel.Membership, typeof(Membership)),
                    memberships = EntitytoJSON(channel.Memberships ?? new Membership[] { }, typeof(Membership[])),
                    manager_user = channel.ManagerUser,
                    manager_details = EntitytoJSON(channel.ManagerDetails, typeof(User)),
                    members = channel.Members
                };
            }
            else if (type == typeof(Channel[])) {
                List<dynamic> channels = new List<dynamic>();
                foreach (var channel in (entity as Channel[])) {
                    channels.Add(EntitytoJSON(channel, typeof(Channel)));
                }
                return channels.ToArray();
            }
            else if (type == typeof(FriendRequest)) {
                FriendRequest friendrequest = (FriendRequest)entity;
                return new {
                    requester = friendrequest.Requester,
                    addresser = friendrequest.Addresser,
                    friend_detail = EntitytoJSON(friendrequest.FriendDetails, typeof(User)),
                    request_date = friendrequest.RequestDate.ToUniversalTime(),
                    approval_date = friendrequest.ApprovalDate.ToUniversalTime(),
                    friendship_end_date = friendrequest.FriendshipEndDate.ToUniversalTime(),
                    active = friendrequest.Active
                };
            }
            else if (type == typeof(FriendRequest[])) {
                List<dynamic> friendrequests = new List<dynamic>();
                foreach (var friendrequest in (entity as FriendRequest[])) {
                    friendrequests.Add(EntitytoJSON(friendrequest, typeof(FriendRequest)));
                }
                return friendrequests;
            }
            else if (type == typeof(Entities.Friendship)) {
                Entities.Friendship friendship = (Entities.Friendship)entity;
                return new {
                    id = friendship.FriendshipId,
                    requester_user = friendship.Requester,
                    addresser_user = friendship.Addresser,
                    request_date = friendship.RequestDate.ToUniversalTime(),
                    approval_date = friendship.ApprovalDate.ToUniversalTime(),
                    friendship_end_date = friendship.FriendshipEndDate.ToUniversalTime(),
                    active = friendship.Active
                };
            }
            else if (type == typeof(Entities.Friendship[])) {
                List<dynamic> friendships = new List<dynamic>();
                foreach (var friendship in (entity as Entities.Friendship[])) {
                    friendships.Add(EntitytoJSON(friendship, typeof(Entities.Friendship)));
                }
                return friendships.ToArray();
            }
            else if (type == typeof(Membership)) {
                Membership membership = (Membership)entity;
                return new {
                    id = membership.MembershipId,
                    thread = membership.ThreadId,
                    user = membership.UserId,
                    last_seen_date = membership.LastSeenDate.ToUniversalTime(),
                    last_seen_message = membership.LastSeenMessageId,
                    active = membership.IsParticipant,
                    join_date = membership.JoinDate.ToUniversalTime(),
                    user_details = EntitytoJSON(membership.UserDetails, typeof(User)),
                    unchecked_count = membership.UncheckedCount
                };
            }
            else if (type == typeof(Membership[])) {
                List<dynamic> memberships = new List<dynamic>();
                foreach (var membership in (entity as Membership[])) {
                    memberships.Add(EntitytoJSON(membership, typeof(Membership)));
                }
                return memberships.ToArray();
            }
            else if (type == typeof(Message)) {
                Message message = (Message)entity;
                return new {
                    id = message.MessageId,
                    thread = message.ThreadId,
                    user_sender = message.UserId,
                    contents = message.Contents,
                    sent_date = message.SentDate.ToUniversalTime(),
                    type = EnumToString(message.Type, typeof(Message.Types)),
                    tags = EntitytoJSON(message.Tags ?? new MessageTag[] { }, typeof(MessageTag[]))
                };
            }
            else if (type == typeof(Message[])) {
                List<dynamic> messages = new List<dynamic>();
                foreach (var message in (entity as Message[])) {
                    messages.Add(JSONtoEntity(message, typeof(Message)));
                }
                return messages.ToArray();
            }
            else if (type == typeof(MessageTag)) {
                MessageTag tag = (MessageTag)entity;
                return new {
                    tagger_user = tag.TaggedUserId,
                    message = tag.MessageId,
                    placeholder_position = tag.PlaceholderPosition,
                    tagged_user_details = EntitytoJSON(tag.TaggedUserDetails, typeof(User))
                };
            }
            else if (type == typeof(MessageTag[])) {
                List<dynamic> messagetags = new List<dynamic>();
                foreach (var messagetag in (entity as MessageTag[])) {
                    messagetags.Add(JSONtoEntity(messagetag, typeof(MessageTag)));
                }
                return messagetags.ToArray();
            }
            else if (type == typeof(PrivateDiscussion)) {
                PrivateDiscussion privatediscussion = (PrivateDiscussion)entity;
                return new {
                    id = privatediscussion.ThreadId,
                    slug = privatediscussion.Slug,
                    title = privatediscussion.Title,
                    type = EnumToString(privatediscussion.Type, typeof(PrivateDiscussion.Types)),
                    description = privatediscussion.Description,
                    creator_user = privatediscussion.CreatorUser,
                    manager_user = privatediscussion.ManagerUser,
                    manager_details = EntitytoJSON(privatediscussion.ManagerDetails, typeof(User)),
                    membership = EntitytoJSON(privatediscussion.Membership, typeof(Membership)),
                    memberships = EntitytoJSON(privatediscussion.Memberships, typeof(Membership[])),
                    interlocutor_membership = EntitytoJSON(privatediscussion.InterlocutorMembership, typeof(Membership))
                };
            }
            else if (type == typeof(PrivateDiscussion[])) {
                List<dynamic> privatediscussions = new List<dynamic>();
                foreach (var privatediscussion in (entity as PrivateDiscussion[])) {
                    privatediscussions.Add(EntitytoJSON(privatediscussion, typeof(PrivateDiscussion)));
                }
                return privatediscussions.ToArray();
            }
            else if (type == typeof(Thread)) {
                Thread thread = (Thread)entity;
                return new {
                    id = thread.ThreadId,
                    slug = thread.Slug,
                    title = thread.Title,
                    type = EnumToString(thread.Type, typeof(Thread.Types)),
                    description = thread.Description,
                    creator_user = thread.CreatorUser,
                    manager_user = thread.ManagerUser,
                    manager_details = EntitytoJSON(thread.ManagerDetails, typeof(User)),
                    membership = EntitytoJSON(thread.Membership, typeof(Membership)),
                    memberships = EntitytoJSON(thread.Memberships, typeof(Membership[]))
                };
            }
            else if (type == typeof(Thread[])) {
                List<dynamic> threads = new List<dynamic>();
                foreach (var thread in (entity as Thread[])) {
                    threads.Add(EntitytoJSON(thread, typeof(PrivateDiscussion)));
                }
                return threads.ToArray();
            }
            else if (type == typeof(User)) {
                User user = (User)entity;
                return new {
                    id = user.UserId,
                    username = user.Username,
                    first_name = user.FirstName,
                    last_name = user.LastName,
                    type = EnumToString(user.Type, typeof(User.Types)),
                };
            }
            else if (type == typeof(User[])) {
                List<dynamic> users = new List<dynamic>();
                foreach (var user in (entity as User[])) {
                    users.Add(EntitytoJSON(user, typeof(User)));
                }
                return users.ToArray();
            }
            else {
                throw new Exception("Unhandled Type " + type.ToString());
            }
        }

        public static dynamic JSONtoEntity(dynamic json, Type type) {
            if (type == typeof(Entities.Auth)) {
                if (json == null || json.ToString() == "") {
                    return new Entities.Auth() {
                        IsNull = true
                    };
                }
                else {
                    return new Entities.Auth() {
                        Token = json.key,

                        IsNull = false
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
                        ThreadId = ParseInt(json.id.ToString()),
                        Slug = json.slug.ToString(),
                        Title = json.title.ToString(),
                        Type = ParseEnum<Channel.Types>((json.type ?? "").ToString()),
                        Description = json.description.ToString(),
                        Membership = JSONtoEntity(json.membership, typeof(Membership)),
                        Memberships = JSONtoEntity(json.memberships, typeof(Membership[])),
                        CreatorUser = ParseInt(json.creator_user.ToString()),
                        ManagerUser = ParseInt(json.manager_user.ToString()),
                        ManagerDetails = JSONtoEntity(json.manager_details, typeof(User)),
                        Members = (int[])(string.IsNullOrEmpty(json.members?.ToString() ) ? new int[0] : json.members),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(Channel[])) {
                List<Channel> channels = new List<Channel>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        channels.Add(JSONtoEntity(a, typeof(Channel)));
                    }
                }
                return channels.ToArray();
            }
            else if (type == typeof(Entities.Friendship)) {
                if (json == null || json.ToString() == "") {
                    return new Entities.Friendship() {
                        IsNull = true
                    };
                }
                else {
                    return new Entities.Friendship() {
                        FriendshipId = ParseInt(json.id.ToString()),
                        Requester = ParseInt(json.requester_user.ToString()),
                        Addresser = ParseInt(json.addresser_user.ToString()),
                        RequestDate = ParseDate(json.request_date.ToString()),
                        ApprovalDate = ParseDate(json.approval_date.ToString()),
                        FriendshipEndDate = ParseDate(json.friendship_end_date.ToString()),
                        Active = ParseBool(json.active.ToString()),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(Entities.Friendship[])) {
                List<Entities.Friendship> friendships = new List<Entities.Friendship>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        friendships.Add(JSONtoEntity(a, typeof(Entities.Friendship)));
                    }
                }
                return friendships.ToArray();
            }
            else if (type == typeof(FriendRequest)) {
                if (json == null || json.ToString() == "") {
                    return new FriendRequest() {
                        IsNull = true
                    };
                }
                else {
                    return new FriendRequest() {
                        Requester = ParseInt(json.requester.ToString()),
                        Addresser = ParseInt(json.addresser.ToString()),
                        FriendDetails = JSONtoEntity(json.friend_detail, typeof(User)),
                        RequestDate = ParseDate(json.request_date.ToString()),
                        ApprovalDate = ParseDate(json.approval_date.ToString()),
                        FriendshipEndDate = ParseDate(json.friendship_end_date.ToString()),
                        Active = ParseBool(json.active.ToString()),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(FriendRequest[])) {
                List<FriendRequest> friendrequests = new List<FriendRequest>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        friendrequests.Add(JSONtoEntity(a, typeof(FriendRequest)));
                    }
                }
                return friendrequests.ToArray();
            }
            else if (type == typeof(Membership)) {
                if (json == null || json.ToString() == "") {
                    return new Membership() {
                        IsNull = true
                    };
                }
                else {
                    return new Membership() {
                        MembershipId = ParseInt(json.id.ToString()),
                        ThreadId = ParseInt(json.thread.ToString()),
                        UserId = ParseInt(json.user.ToString()),
                        LastSeenDate = ParseDate(json.last_seen_date.ToString()),
                        LastSeenMessageId = ParseInt(json.last_seen_message.ToString()),
                        IsParticipant = Convert.ToBoolean(json.active.ToString()),
                        JoinDate = ParseDate(json.join_date.ToString()),
                        UserDetails = JSONtoEntity(json.user_details, typeof(User)),
                        UncheckedCount = ParseInt(json.unchecked_count.ToString()),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(Membership[])) {
                List<Membership> memberships = new List<Membership>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json) {
                        memberships.Add(JSONtoEntity(a, typeof(Membership)));
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
                        MessageId = ParseInt(json.id.ToString()),
                        ThreadId = ParseInt(json.thread.ToString()),
                        UserId = ParseInt(json.user_sender.ToString()),
                        Contents = json.contents.ToString(),
                        SentDate = ParseDate(json.sent_date.ToString()),
                        Type = ParseEnum<Message.Types>((json.message_type ?? "").ToString() == "" ? "user" : json.message_type.ToString()),
                        Tags = JSONtoEntity(json.tags, typeof(MessageTag[])),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(Message[])) {
                List<Message> messages = new List<Message>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        messages.Add(JSONtoEntity(a, typeof(Message)));
                    }
                }
                return messages.ToArray();
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
                        TaggedUserDetails = JSONtoEntity(json.tagged_user_details, typeof(User)),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(MessageTag[])) {
                List<MessageTag> messagetag = new List<MessageTag>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json) {
                        messagetag.Add(JSONtoEntity(a, typeof(MessageTag)));
                    }
                }
                return messagetag.ToArray();
            }
            else if (type == typeof(PrivateDiscussion)) {
                if (json == null || json.ToString() == "") {
                    return new PrivateDiscussion() {
                        IsNull = true
                    };
                }
                else {
                    return new PrivateDiscussion() {
                        ThreadId = ParseInt(json.id.ToString()),
                        Slug = json.slug.ToString(),
                        Title = json.title.ToString(),
                        Type = ParseEnum<PrivateDiscussion.Types>(json.type.ToString()),
                        Description = json.description.ToString(),
                        CreatorUser = ParseInt(json.creator_user.ToString()),
                        ManagerUser = ParseInt(json.manager_user.ToString()),
                        ManagerDetails = JSONtoEntity(json.manager_details, typeof(User)),
                        Membership = JSONtoEntity(json.membership, typeof(Membership)),
                        Memberships = JSONtoEntity(json.memberships, typeof(Membership[])),
                        InterlocutorMembership = JSONtoEntity(json.interlocutor_membership, typeof(Membership)),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(PrivateDiscussion[])) {
                List<PrivateDiscussion> privatediscussions = new List<PrivateDiscussion>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        privatediscussions.Add(JSONtoEntity(a, typeof(PrivateDiscussion)));
                    }
                }
                return privatediscussions.ToArray();
            }
            else if (type == typeof(Thread)) {
                if (json == null || json.ToString() == "") {
                    return new Thread() {
                        IsNull = true
                    };
                }
                else {
                    return new Thread() {
                        ThreadId = ParseInt(json.id.ToString()),
                        Slug = json.slug.ToString(),
                        Title = json.title.ToString(),
                        Type = ParseEnum<Thread.Types>(json.type.ToString()),
                        Description = json.description.ToString(),
                        CreatorUser = ParseInt(json.creator_user.ToString()),
                        ManagerUser = ParseInt(json.manager_user.ToString()),
                        ManagerDetails = JSONtoEntity(json.manager_details, typeof(User)),
                        Membership = JSONtoEntity(json.membership, typeof(Membership)),
                        Memberships = JSONtoEntity(json.memberships, typeof(Membership[])),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(Thread[])) {
                List<Thread> threads = new List<Thread>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        threads.Add(JSONtoEntity(a, typeof(Thread)));
                    }
                }
                return threads.ToArray();
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
                        Type = ParseEnum<User.Types>((json.type ?? "").ToString()),

                        IsNull = false
                    };
                }
            }
            else if (type == typeof(User[])) {
                List<User> users = new List<User>();
                if (json == null || json.ToString() != "") {
                    foreach (var a in json.results) {
                        users.Add(JSONtoEntity(a, typeof(User)));
                    }
                }
                return users.ToArray();
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

        private static bool ParseBool(string value) {
            bool result;
            return bool.TryParse(value, out result) ? result : false;
        }

        private static T ParseEnum<T>(string data) {
            try {
                return (T)Convert.ChangeType(Enum.Parse(typeof(T), data.ToString().ToUpper()), typeof(T));
            }
            catch {
                return default(T);
            }
        }

        private static string EnumToString(Enum data, Type type) {
            try {
                return Enum.GetName(type, data);
            }
            catch {
                return null;
            }
        }
    }
}
