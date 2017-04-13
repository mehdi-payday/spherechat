from messaging.models import Message, Thread, TuneManager, Thread
from messaging.serializers import MessageSerializer
from django.core.exceptions import ObjectDoesNotExist
from django.utils import timezone
from messaging.exceptions import (
    UnmanagedThread, 
    UnauthorizedSender,
    UnauthorizedAction,
    ExistingMember, 
    UntitledThread, 
    ImmutableMembersList,
    UnexistentMembership,
    IncorrectTags,
    MessageAlreadySent)
from core.models import User
from core.channels import ChannelHandler


class ThreadHandler(ChannelHandler):
    name = 'threads'

    def _get_user(self):
        if hasattr(self.connection.authenticator, "user") \
            and self.connection.authenticator.user != None:
            return User.objects.get(pk=self.connection.authenticator.user.pk)
        else:
            raise RuntimeError("Could not find the authenticated user of this connection")

    @classmethod
    def thread_channel_name(cls, thread):
        return thread.pk

    def _get_thread(self, channel_name):
        return Thread.objects.get(pk=channel_name)

    def handle_channel_message(self, channel, data):
        payload_type = data['type']
    
        try:
            thread = self._get_thread(channel)
        except ObjectDoesNotExist:
            return

        user = self._get_user()

        if payload_type == 'listening':
            TuneManager.get().tune(user, thread)
            
        elif payload_type == 'message':
            message_data = data["payload"]

            print "Message"
            print message_data

            try:
                TuneManager.get().tune(user, thread)
            except UnauthorizedAction as unauthorizedAction:
                raise

            tags = message_data.pop("tags", [])

            try:
                sent_message = thread.send(
                    Message(**message_data), 
                    sender=user, 
                    tags=tags)
            except ValueError as valueError:
                raise
            except MessageAlreadySent as alreadySent:
                raise
            except UnauthorizedAction as unauthorizedAction:
                raise
            except IncorrectTags as incorrectTags:
                raise
            except Exception as exception:
                raise

            serializer = MessageSerializer(sent_message, context={'user': user})
            message_data = serializer.data

            print "Message data"
            print message_data

            self.publish(channel, "message", message_data)

        elif payload_type == 'message_checked':
            payload = data["payload"]
            seen_date = data.pop("seen_date", timezone.now())

            try:
                checked_message = data.pop("last_message_id")
            except KeyError:
                raise

            Thread.objects.see_thread(
                user, 
                thread, 
                seen_date, 
                last_seen_message=checked_message)


# class DiscussionChannelHandler(ThreadHandler):
#    name = 'discussion_channels'

# class PrivateDiscussionHandler(ThreadHandler):
#    name = 'private_discussions'

class PersonalUserHandler(ChannelHandler):
    name = 'users'

    @classmethod
    def user_channel_name(self, user):
        return user.pk

users = PersonalUserHandler
threads = ThreadHandler
# discussion_channels = DiscussionChannelHandler
# private_discussions = PrivateDiscussionHandler
