# -*- coding: utf-8 -*-

from messaging.models import (Message, MessageTag, Membership, Thread)
from rest_framework import serializers
from core.models import User
from django.db.models import Q


def get_user_from_serializer(serializer):
    request = serializer.context.get('request', None)

    if 'member' in serializer.context:
        return serializer.context.get('member')
    elif request:
        return request.user

    raise Exception("Cannot get member from serializer"
                    + " if request is not in the context")

class MembershipSerializer(serializers.ModelSerializer):
    class Meta:
        model = Thread
        fields = ("user", "thread", "last_seen_date", "last_seen_message", "is_participant", "join_date")

class UserMembershipSerializer(MembershipSerializer):
    class Meta(MembershipSerializer.Meta):
        fields = MembershipSerializer.Meta.fields + ("user_details", )

    user_details = UserSerializer(source="user", read_only=True)

class ThreadSerializer(serializers.ModelSerializer):
    class Meta:
        model = Thread
        fields = ("id", "slug", "title", "type", "description", "creator_user", "manager_user", "manager_details")
        extra_kwargs = {
            "creator_user": {"read_only": True},
            "manager_user": {"read_only": True},
        }
    manager_details = UserSerializer(source="manager_user", read_only=True)
    membership = serializers.SerializerMethodField()
    memberships = MembershipSerializer(many=True, read_only=True)

    def get_membership(self, thread):
        try:
            membership = Membership.objects.get_membership(user, thread)
        except:
            return None
        return MembershipSerializer(membership, context=self.context).data

class PrivateDiscussionSerializer(serializers.ModelSerializer):
     class Meta(ThreadSerializer.Meta):
        fields = ThreadSerializer.Meta.fields + ("interlocutor_membership", "with_user")
        extra_kwargs = {
            "creator_user": {"read_only": True},
            "manager_user": {"read_only": True},
            "slug": {"read_only": True},
            "title": {"read_only": True},
            "type": {"read_only": True},
            "description": {"read_only": True},
        }

    interlocutor_membership = serializers.SerializerMethodField()
    with_user = PrimaryKeyRelatedField(queryset=User.objects.active_users(), write_only=True, required=True)

    def get_interlocutor_membership(self, discussion):
        user = get_user_from_serializer(self)
        interlocutor_membership = discussion.memberships.get(~Q(user=user))

        return MembershipSerializer(interlocutor_membership, context=self.context).data

    def create(self, validated_data):
        initiator = get_user_from_serializer(self
        target = validated_data["with_user"]

        return Thread.objects.discussion_between(initiator, target)

    def update(self, discussion, data):
        raise NotImplemented()


class ChannelSerializer(ThreadSerializer):
    class Meta(ThreadSerializer.Meta):
        fields = ThreadSerializer.Meta.fields + ("members", )
        extra_kwargs = {
            "creator_user": {"read_only": True},
            "manager_user": {"read_only": True},
            "type": {
                "choices": (
                    (Thread.PRIVATE_CHANNEL, "Private channel"), 
                    (Thread.PUBLIC_CHANNEL, "Public channel"),
                )
            }
        }

    members = PrimaryKeyRelatedField(many=True, write_only=True, queryset=User.objects.active_users())

    def create(self, validated_data):
        creator = get_user_from_serializer(self)
        members = validated_data.pop("members")

        return Thread.objects.create_channel(validated_data,
                                            creator,
                                            initial_members=members)

    def update(self, channel, validated_data):
        raise NotImplemented()

    def delete(self, channel):
        raise NotImplemented()


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
        fields = ("id", "user_sender", "sender_details", "thread", "contents", "sent_date", "message_type", "tags")
        extra_kwargs = {
            "message_type": {"read_only": True},
            "user_sender": {"read_only": True},
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
        data['sender'] = get_user_from_serializer(self)

        return data

    def create(self, validated_data):
        message = Message(**validated_data)
        thread = validated_data.get("thread")
        message = Message.objects.send(message, thread)

        return message

