from messaging.models import MessageManager, TuneManager, Membership, MessageTagManager

class MembershipSeenUpdater(object):
    @classmethod
    def on_message_saved(cls, message):
        tune_manager = TuneManager.get()

        for membership in message.thread.active_memberships:
            user = message.user_sender
            thread = message.thread

            if tune_manager.is_tuned(user, message.thread):
                last_seen_date = tune_manager.get_last_heartbeat_date(user, thread)
                Membership.objects.see_thread(user, thread, last_seen_date)


class MessageTagObserver(object):
    def on_create(self, message_tag):
        if message_tag.is_bot_tagged():
            bot = message_tag.tagged_user
            # TODO : Generate a bot response

MessageManager.register_observer(MembershipSeenUpdater)
MessageTagManager.register_observer(MessageTagObserver)
