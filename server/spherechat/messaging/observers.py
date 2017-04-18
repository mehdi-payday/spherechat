from messaging.models import Message, TuneManager, Membership, MessageTag
from core.models import User
# from messaging.serializers import ChannelSerializer, PrivateDiscussionSerializer
# from core.channels import personal_user


class MembershipSeenUpdater(object):
    pass
"""
    @classmethod
    def on_message_saved(cls, message):
        tune_manager = TuneManager.get()

        for membership in message.thread.active_memberships:
            user = message.user_sender
            thread = message.thread
            is_tuned = tune_manager.is_tuned(user, message.thread)

            if is_tuned:
                last_seen_date = tune_manager.get_last_heartbeat_date(
                    user, 
                    thread)
                Membership.objects.see_thread(user, thread, last_seen_date)
"""

class MessageTagObserver(object):
    @classmethod
    def on_create(self, message_tag):
        if message_tag.is_bot_tagged():
            bot = message_tag.tagged_user
            # TODO : Generate a bot response

class BotObservingMessages(object):
    @classmethod
    def on_create(self, message):
        thread = message.thread
        bots = message.thread.members.filter(type=User.BOT)

        if len(bots) == 0:
            return

        from chatterbot import ChatBot
        chatbot = ChatBot(
        	"Lucy", 
	        trainer='chatterbot.trainers.ChatterBotCorpusTrainer',
	        input_adapter='chatterbot.input.TerminalAdapter')
        contents = chatbot.get_response(message.contents)

        for bot in bots:
            if bot.pk != message.user_sender.pk:
                thread.send(Message(contents=contents), bot)

Message.objects.register_observer(BotObservingMessages)
Message.objects.register_observer(MembershipSeenUpdater)
MessageTag.objects.register_observer(MessageTagObserver)
