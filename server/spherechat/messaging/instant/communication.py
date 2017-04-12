from messaging.channels import (
#    discussion_channels as discussion_channels_comm,
#    private_discussions as private_discussions_comm,
    threads as threads_comm,
    users as users_comm)
from messaging.serializers import (
    ChannelSerializer, 
    PrivateDiscussionSerializer, 
    MessageSerializer)
from messaging.models import (
    Message, 
    TuneManager, 
    Membership, 
    MessageTag)


class CommunicationService(object):
    instance = None

    @classmethod
    def get(cls):
        if cls.instance is None:
            cls.instance = cls()

        return cls.instance

class ThreadCommunicationService(CommunicationService):
    MESSAGE = 'message'
    CHANNEL_CHANGE = 'channel_change'
    DISCUSSION_CHANGE = 'discussion_change'

    @classmethod
    def _notify_members_about_channel_change(
            cls,
            channel):
        print "Send channel status : {}" \
            .format(channel)

        for user in channel.active_members:
            data = ChannelSerializer(channel, context={'user': user}).data
            users_comm.server_publish(
                users_comm.user_channel_name(user),
                cls.CHANNEL_CHANGE,
                data,
            )

    @classmethod
    def _notify_members_about_discussion_change(
            cls,
            private_discussion):
        print "Send channel status : {}" \
            .format(private_discussion)
    
        for user in private_discussion.active_members:
            data = PrivateDiscussionSerializer(private_discussion, context={'user': user}).data
            users_comm.server_publish(
                users_comm.user_channel_name(user),
                cls.DISCUSSION_CHANGE,
                data,
            )

    @classmethod
    def thread_change(cls, thread):
        if thread.is_channel():
            cls._notify_members_about_channel_change(thread)
        elif thread.is_private_discussion():
            cls._notify_members_about_discussion_change(thread)

    @classmethod
    def new_message(cls, message):
        thread = message.thread
        event = cls.MESSAGE
        data = MessageSerializer(message).data

        threads_comm.server_publish(
            threads_comm.thread_channel_name(thread),
            event,
            data
        )
