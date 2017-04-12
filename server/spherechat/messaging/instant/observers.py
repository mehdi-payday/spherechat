from messaging.models import Message, TuneManager, Membership
from messaging.instant.communication import ThreadCommunicationService

class MessageBroadcaster(object):
    @classmethod
    def on_message_saved(cls, message):
        thread_comm_service = ThreadCommunicationService()
        thread_comm_service.new_message(message)

class ThreadBroadcaster(object):
    @classmethod
    def on_thread_seen(cls, membership):
        thread = membership.thread
        thread_comm_service = ThreadCommunicationService()
        thread_comm_service.thread_change(thread)

    @classmethod
    def on_message_saved(cls, message):
        thread = message.thread
        thread_comm_service = ThreadCommunicationService()
        thread_comm_service.thread_change(thread)       

Message.objects.register_observer(MessageBroadcaster)
Message.objects.register_observer(ThreadBroadcaster)
Membership.objects.register_observer(ThreadBroadcaster)
