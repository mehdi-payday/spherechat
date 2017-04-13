# -*- coding: utf-8 -*-

from messaging.models import (Message, MessageTag, Membership, Thread, TuneManager)
from core.serializers import UserSerializer
from rest_framework import serializers
from datetime import datetime
from core.models import User
from django.db.models import Q
from messaging.exceptions import UnexistentMembership

def get_user_from_serializer(serializer):
    request = serializer.context.get('request', None)

    if 'user' in serializer.context:
        return serializer.context.get('user')
    elif request:
        return request.user

    raise Exception("Cannot get member from serializer"
                    + " if `request` or `user` is not in the context")

class MembershipSerializer(serializers.ModelSerializer):
    class Meta:
        model = Membership
        fields = ("id", "user", "thread", "last_seen_date", "last_seen_message", "active", "join_date", "user_details", "unchecked_count")
        extra_kwargs = {
        }

    user_details = UserSerializer(source="user", read_only=True)
    unchecked_count = serializers.IntegerField(read_only=True)

class UserMembershipSerializer(MembershipSerializer):
    class Meta(MembershipSerializer.Meta):
        fields = MembershipSerializer.Meta.fields + ("user_details", )

    user_details = UserSerializer(source="user", read_only=True)

class ThreadSerializer(serializers.ModelSerializer):
    class Meta:
        model = Thread
        fields = ("id", "slug", "title", "type", "description", "creator_user", "manager_user", "manager_details", "membership", "memberships", "is_tuned")
        extra_kwargs = {
            "creator_user": {"read_only": True},
            "manager_user": {"read_only": True},
            "slug": {"read_only": True},
        }
    manager_details = UserSerializer(source="manager_user", read_only=True)
    membership = serializers.SerializerMethodField()
    memberships = MembershipSerializer(many=True, read_only=True, source="active_memberships")
    is_tuned = serializers.SerializerMethodField()

    def get_is_tuned(self, thread):
        return TuneManager.get().is_tuned(get_user_from_serializer(self), thread)

    def get_membership(self, thread):
        try:
            user = get_user_from_serializer(self)
            membership = Membership.objects.get_membership(user, thread)
        except UnexistentMembership:
            return None
        return MembershipSerializer(membership, context=self.context).data

class PrivateDiscussionSerializer(ThreadSerializer):
    class Meta(ThreadSerializer.Meta):
        fields = ThreadSerializer.Meta.fields + ("interlocutor_membership", "with_user")
        extra_kwargs = dict(
            title={"read_only": True},
            type={"read_only": True},
            description={"read_only": True},
            **ThreadSerializer.Meta.extra_kwargs)

    interlocutor_membership = serializers.SerializerMethodField()
    with_user = serializers.PrimaryKeyRelatedField(queryset=User.objects.active_users(), write_only=True, required=True)

    def get_interlocutor_membership(self, discussion):
        user = get_user_from_serializer(self)
        interlocutor_membership = discussion.memberships.get(~Q(user=user))

        return MembershipSerializer(interlocutor_membership, context=self.context).data

    def create(self, validated_data):
        initiator = get_user_from_serializer(self)
        target = validated_data["with_user"]

        return Thread.objects.discussion_between(initiator, target)

    def update(self, discussion, data):
        raise NotImplementedError()


class ChannelSerializer(ThreadSerializer):
    class Meta(ThreadSerializer.Meta):
        fields = ThreadSerializer.Meta.fields + ("members", )
        extra_kwargs = dict(
            type={
                "choices": (
                    (Thread.PRIVATE_CHANNEL, "Private channel"), 
                    (Thread.PUBLIC_CHANNEL, "Public channel"),
                )
            }, **ThreadSerializer.Meta.extra_kwargs)

    members = serializers.PrimaryKeyRelatedField(many=True, write_only=True, queryset=User.objects.active_users())

    def create(self, validated_data):
        creator = get_user_from_serializer(self)
        members = validated_data.pop("members")

        return Thread.objects.create_channel(validated_data,
                                            creator,
                                            initial_members=members)

    def update(self, channel, validated_data):
        raise NotImplementedError()

    def delete(self, channel):
        raise NotImplementedError()


class MessageTagSerializer(serializers.ModelSerializer):
    class Meta:
        model = MessageTag
        fields = ("tagged_user", "tagged_user_details", "message", "placeholder_position")
        extra_kwargs = {
            "message": {"read_only": True},
        }
    tagged_user_details = UserSerializer(source="tagged_user", read_only=True)

class MessageSerializer(serializers.ModelSerializer):
    class Meta:
        model = Message
        fields = ("id", 
                  "user_sender", 
                  "sender_details", 
                  "thread", 
                  "contents", 
                  "sent_date", 
                  "message_type", 
                  "tags", 
                   "attachment")
        extra_kwargs = {
            "message_type": {"read_only": True},
            "user_sender": {"read_only": True},
            "sent_date": {"read_only": True},
#            "thread": {"read_only": True},
        }

    sender_details = UserSerializer(source="sender", read_only=True)
    tags = MessageTagSerializer(many=True)

    def validate_context(self):
        assert 'request' in self.context or 'user' in self.context, \
            "To use %s in write mode, it needs 'user' or 'request' to be in the context." \
            % (self.__class__,)

    def validate(self, data):
        data = super(MessageSerializer, self).validate(data)
        data['user_sender'] = get_user_from_serializer(self)
        data['sent_date'] = datetime.now()

        return data

    def create(self, validated_data):
        tags = validated_data.pop("tags", [])
        message = Message(**validated_data)
        thread = validated_data.get("thread")
        message = Message.objects.send(message, thread, tags=tags)

        return message
