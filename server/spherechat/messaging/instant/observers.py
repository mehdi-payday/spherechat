from messaging.models import MessageManager, TuneManager, Membership

class MessageBroadcaster(object):
    @classmethod
    def on_message_saved(cls, message):
        pass


MessageManager.register_observer(MessageBroadcaster)
