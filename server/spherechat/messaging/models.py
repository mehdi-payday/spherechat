# -*- coding: utf-8 -*-

from __future__ import unicode_literals
from preconditions import preconditions
from django.template.defaultfilters import slugify
from django.db import models
from django.utils import timezone
from django.db.models import Count, Avg
from datetime import datetime
from django.conf import settings
from django.contrib.auth import get_user_model
from django.db.models import (Manager, Model, ObjectDoesNotExist)
import random
from core.mixins import ObservableManagerMixin
from core.models import User
from messaging.exceptions import (UnmanagedThread, 
    UnauthorizedSender,
    UnauthorizedAction,
    ExistingMember, 
    UntitledThread, 
    ImmutableMembersList,
    UnexistentMembership,
    IncorrectTags,
    ExistingDiscussion,
    MessageAlreadySent,
    UserNotTuned)
import re


# Every 60 seconds, a "listening to Thread" signal should be sent by the client in order for him to be considered as connected to thread
LISTENING_RENEWAL_RATE = 60

class MembershipManager(ObservableManagerMixin, Manager):
    def create_membership(self, user, thread, **kwargs):
        if self.belongs(user, thread):
            raise ExistingMember("User %s is already a member of thread %s" % (user, thread))
        return self.create(user=user, thread=thread)

    def get_or_create_membership(self, user, thread, **kwargs):
        try:
            return self.get_membership(user, thread)
        except UnexistentMembership:
            return self.create(user=user, thread=thread)
 
    def cancel_membership(self, membership):
#        membership.is_participant = False
        membership.active = False
        membership.save()
        return membership

    @preconditions(lambda user, thread: isinstance(user, User) and isinstance(thread,Thread))
    def has_seen_thread(self, user, thread):
        try:
            last_message = thread.get_last_message()
        except IndexError:
            last_message = None
        seen = self.has_seen_message(user, last_message)

        return seen

    @preconditions(lambda user, message: isinstance(user, User) and isinstance(message, Message))
    def has_seen_message(self, user, message):
        assert message.thread != None
        assert message.sent_date != None

        thread = message.thread
        membership = self.get_membership(user, thread)

        if membership.last_seen_date is None or membership.last_seen_message is None:
            seen = False
        else:
            seen = membership.last_seen_date >= message.sent_date \
                and membership.last_seen_message.pk >= message.pk

        return seen

    @property
    def active_memberships(self):
        return self.filter(active=True)

    def active_members(self, thread=None):
        if thread is None:
            return User.objects.filter(memberships__active=True)
        return User.objects.filter(memberships__active=True, memberships__thread=thread)

    def get_membership(self, user, thread):
        try:
            return self.active_memberships.get(user=user, thread=thread)
        except ObjectDoesNotExist as doesNotExist:
            raise UnexistentMembership("No membership of user %s to thread %s" % (user, thread), doesNotExist)

    def belongs(self, user, thread):
        return self.filter(user=user, thread=thread, active=True).exists()

    def list_unchecked_messages(self, user, thread, membership=None):
        membership = self.get_membership(user, thread)
        last_seen_date = membership.last_seen_date
        return thread.messages.filter(sent_date__gt=last_seen_date)

    def get_last_seen_date(self, user, thread):
        last_seen_date = None

        if isinstance(user, User):
            try:
                last_seen_date = self.get_membership(user, thread).last_seen_date
            except UnexistentMembership:
                pass
        elif isinstance(user, Membership):
            membership = user
            last_seen_date = membership.last_seen_date

        return last_seen_date

    def see_thread(
        self, 
        user_or_membership, 
        thread, 
        seen_date, 
        last_seen_message=None):

        assert isinstance(seen_date, datetime), ValueError("Argument `seen_date` must be an instance of datetime")
        assert isinstance(user_or_membership, User) or isinstance(user_or_membership, Membership)

        membership = self.get_membership(user_or_membership, thread) if isinstance(user_or_membership, User) else user_or_membership
        membership.last_seen_date = seen_date

        if last_seen_message is not None:
#            if membership.last_seen_message is None \
#                or membership.last_seen_message.pk < last_seen_message.pk:
            membership.last_seen_message = last_seen_message
        
        membership.save()

        self.notify_observers("on_thread_seen", membership)


#    def set_last_seen_date(self, user, thread, last_seen_date):
#        membership = Membership.objects.get_membership(user, thread)
#        membership.last_seen_date = last_seen_date
#        membership.save()


class TuneManager(object):
    @classmethod
    def get(cls):
        if not hasattr(cls, "_instance"):
            cls._instance = TuneManager()
        return cls._instance

    def can_tune(self, user, thread):
        is_private = not thread.is_public_channel()
        user_belongs_to_thread = Membership.objects.belongs(user, thread)

        if is_private and not user_belongs_to_thread:
            return False

        return True

    def tune(self, user, thread, listening_date=None, **kwargs):
        assert isinstance(listening_date, datetime) or listening_date is None
        assert isinstance(user, User) and isinstance(thread, Thread)

        if not self.can_tune(user, thread):
            raise UnauthorizedAction("User %s can't tune to thread %s" % (user, thread))

        already_tuned = True

        if user.listening_thread is None \
            or (user.listening_thread.pk != thread.pk):
            user.listening_thread = thread
            already_tuned = False

        if listening_date is None:
            listening_date = timezone.now()
        
        user.last_listening_date = listening_date

        user.save()

#    def update_tuning_state(self, user, thread, listening_date):
#        if listening_date is None:
#            listening_date = timezone.now()
#
#        if not isinstance(listening_date, datetime):
#            raise ValueError("Listening date should be an instance of datetime. Got : %s" % listening_date)
#
#        self.tune(user, thread, listening_date=listening_date)
  
    def is_tuned(self, user, thread):
        if not self.can_tune(user, thread):
            return False

        if user.listening_thread is None:
            return False

        heartbeat_ancienty = timezone.now() - user.last_listening_date
        print "Here is heartbeat anciert %s "% heartbeat_ancienty

        is_tuned = user.listening_thread.pk == thread.pk \
            and heartbeat_ancienty.seconds <= LISTENING_RENEWAL_RATE

        print "Is user %s tuned to %s ? %s" % (
                user, 
                thread, 
                is_tuned)

        return is_tuned

    def get_last_heartbeat_date(self, user, thread):
        if not self.is_tuned(user, thread):
            raise UserNotTuned("User '%s' must be tuned to thread '%s' in order to know his tuning date" % (user, thread))
        return user.last_listening_date 

class ThreadManager(ObservableManagerMixin, Manager):
    def can_manage(self, user, thread):
        assert isinstance(user, User) and isinstance(thread, Thread)

        if not thread.is_channel():
            return False
        
        assert thread.manager_user is not None, "Thread should have a manager"
        
        if thread.manager_user != None \
            and thread.manager_user.pk == user.pk:
            return True

        return False
        
    def is_channel(self, thread):
        return thread.type in (Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL)

    def is_private_channel(self, thread):
        return thread.type == Thread.PRIVATE_CHANNEL

    def is_public_channel(self, thread):
        return thread.type == Thread.PUBLIC_CHANNEL

    def is_private_discussion(self, thread):
        return thread.type == Thread.PRIVATE_DISCUSSION

    def get_last_message(self, thread):
        return self.get_messages(thread).order_by("-id")[0]

    def get_messages(self, thread):
        return thread.messages

    def can_send(self, user, thread, raise_exception=True):
        is_private = not thread.is_public_channel()
        user_belongs_to_thread = Membership.objects.belongs(user, thread)

        if is_private and not user_belongs_to_thread:
            if raise_exception:
                raise UnauthorizedSender("User %s is not authorized to send messages to thread %s "  % (user, thread) \
                    + "because the thread is not a public channel and the user does not belong to the thread")
            return False

        return True

    def add_member(self, user, thread):
        assert thread.is_channel(), "Thread must be a channel in order to add members"
        membership = Membership.objects.create_membership(user, thread)
        self.notify_observers("on_member_added", membership)

    def _join(self, user, thread):
        Membership.objects.create_membership(user, thread)
#       TODO : Send system message "X joined the channel"

    def _leave(self, user, thread):
        Membership.objects.get_membership(user, thread).cancel()
#       TODO : Send system message "X left the channel"

    def is_manager(self, user, thread):
        try:
            return self.get_manager(thread).pk is user.pk
        except UnmanagedThread:
            return False

    def get_manager(self, thread):
        if thread.type not in (Thread.PUBLIC_CHANNEL, Thread.PRIVATE_CHANNEL):
            raise UnmanagedThread("Thread %s is not a channel. " \
                + "Rather, it is a %s, so it has no manager." % thread, thread.type)

        if thread.manager_user is None:
            raise UnmanagedThread("Thread %s has no associated manager." % thread)

        return thread.manager_user

    def create_discussion(self, initiator, target):
        if self.private_discussion_exists(initiator, target):
            raise ExistingDiscussion("Private discussion already existent between %s and %s" % (initiator, target))

        if initiator.pk == target.pk:
            raise ValueError("The initiator can't be the discussion target")

        title = "Private discussion between %s and %s" % (initiator, target)
        description = "Private discussion between %s and %s" % (initiator, target)

        private_discussion = dict(
            type=Thread.PRIVATE_DISCUSSION,
            title=title,
            description=description,
            creator_user=initiator,
        )
        private_discussion = self.__create(**private_discussion)

        try:
            self._join(initiator, private_discussion)
            self._join(target, private_discussion)
        except:
            private_discussion.delete()
            raise
#            raise RuntimeException("Could not join ")
        self.notify_observers("on_new_discussion", private_discussion)

        return private_discussion

    def create_channel(self, channel_dict, creator, initial_members=[]):
        if "type" not in channel_dict:
            raise ValueError("Channel `type` must be present in channel_dict")
#            channel_dict["type"] = Thread.PUBLIC_CHANNEL
        
        if not channel_dict["type"] in (Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL):
            raise ValueError("Incorrect channel type '%s'. Must be either '%s' or '%s'" \
                % (channel_dict["type"], Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL))

        channel_dict["manager_user"] = creator
        channel_dict["creator_user"] = creator

        channel = self.__create(**channel_dict)
        try:
            self._join(creator, channel)
    
            for member in initial_members:
                try:
                    if creator.pk != member.pk:
                        self._join(member, channel)
                except ExistingMember:
                    pass
        except:
            channel.delete()
            raise

        self.notify_observers("on_new_channel", channel)

        return channel

    def create(self, **kwargs):
        raise NotImplementedError()

    def __create(self, **kwargs):
        if "slug" not in kwargs:
            kwargs["slug"] = self._form_slug(kwargs["title"])
        return super(ThreadManager, self).create(**kwargs)

    def _form_slug(self, title):
        slug = slugify(title)
        while self.filter(slug=slug).exists():

            nb = random.randint(1, 100000)
            slug = "{}-{}".format(slug, nb)
        return slug

    def discussion_between(self, initiator, target):
        """
        Retrieves or creates a discussion (in case it does not exist) 
        between `initiator` and `target`.
        """
        try:
            return self.find_discussion_between(initiator, target)
        except ObjectDoesNotExist:
            return self.create_discussion(initiator, target)

    def find_discussion_between(self, user1, user2):
        """
        Retrieves the discussion thread between `user1` and `user2`
        """
        return self.private_discussions().filter(memberships__user=user1) \
            .get(memberships__user=user2)

    def private_discussion_exists(self, user1, user2):
        return self.private_discussions().filter(memberships__user=user1).filter(memberships__user=user2).exists()

    def private_discussions(self, participant_user=None, order_by='-unseen_mention'):
        return self.threads(participant_user=participant_user, types=(Thread.PRIVATE_DISCUSSION, ), order_by=order_by)

    def private_channels(self, participant_user=None, order_by='-unseen_mention'):
        return self.channels(
            participant_user=participant_user, 
            channel_type=Thread.PRIVATE_CHANNEL, 
            order_by=order_by)

    def public_channels(self, participant_user=None, order_by='-unseen_mention'):
        return self.channels(participant_user=participant_user, channel_type=Thread.PUBLIC_CHANNEL, order_by=order_by)

    def _annotate_unseen_mentions(self, threads_query, user, unseen_mention_keyword):
        threads_query = threads_query.extra(
            select={
                unseen_mention_keyword: \
                    'SELECT COUNT(*) FROM messaging_message INNER JOIN messaging_thread ON messaging_message.thread_id = messaging_thread.id ' \
                    + 'INNER JOIN messaging_membership ON messaging_membership.thread_id = messaging_thread.id WHERE messaging_membership.user_id = %i ' % user.id \
                    + ' AND (messaging_message.sent_date > messaging_membership.last_seen_date OR messaging_membership.last_seen_date is null)'
            }
        )
#        else:
#            annotations = {unseen_mention_keyword: Count('messages')}
#            threads_query = threads_query.annotate(**annotations)

        return threads_query

    def channels(self, 
                 participant_user=None, 
                 channel_type=None,
                 order_by='-unseen_mention'):
        thread_types = None

        if channel_type:
            assert channel_type in (Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL)
            thread_types = (channel_type, )
        else:
            thread_types = (Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL)

        return self.threads(participant_user, 
                            types=thread_types, 
                            order_by=order_by)

    def threads(self, participant_user=None, types=None, order_by='-unseen_mention'):
        filters = dict(active=True)
        if types:
#            for thread_type in types:
#                assert thread_type in (Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL, Thread.PRIVATE_DISCUSSION)
            if isinstance(types, str):
                types = (types, )
            filters['type__in'] = types

        if participant_user:
            filters["memberships__user"] = participant_user
            query = self.filter(**filters)
            query = self._annotate_unseen_mentions(query, participant_user, 'unseen_mention')

            if order_by:
                query = query.order_by(order_by)

            return query
        else:
            return self.filter(type__in=(Thread.PRIVATE_CHANNEL, Thread.PUBLIC_CHANNEL, Thread.PRIVATE_DISCUSSION))

class MessageManager(ObservableManagerMixin, Manager):
    def send(self, message, thread, sender=None, tags=[]):
        assert isinstance(tags, list), ValueError("tags must be a list of tagged users")
#        assert thread not None, ValueError("Thread must be a valid thread")

        if sender:
            message.user_sender = sender
        else:
            sender = message.user_sender
        message.thread = thread

        if message.pk is not None:
            raise MessageAlreadySent("Message already existent")

        if message.is_system_message() and sender is not None:
            raise ValueError("A system message can't have a sender. Sender was : %s" % sender)
        elif message.is_user_message() and sender is None:
            raise ValueError("A user message must have a sender. Got message : %s" % message)
        Thread.objects.can_send(sender, thread, raise_exception=True)

        message.save()
        
        if len(tags) > 0:
            # throws IncorrectTags
            MessageTag.objects.verify_tags(tags, message.contents)

            for tag in tags:
                MessageTag.objects.create(message=message, **tag)

        # Update seen mentions
        tune_manager = TuneManager.get()

        for membership in message.thread.active_memberships:
            user = message.user_sender
            thread = message.thread
            is_tuned = tune_manager.is_tuned(user, message.thread)

            if is_tuned:
#                last_seen_date = tune_manager.get_last_heartbeat_date(
#                    user, 
#                    thread)
                Membership.objects.see_thread(user, thread, timezone.now(), message)


        self._message_saved(message)

        return message

    def _message_saved(self, message):
        self.notify_observers("on_message_saved", message)

class MessageTagManager(ObservableManagerMixin, Manager):
    def verify_tags(self, tags, message_contents):
        placeholder_positions = dict()
        for m in re.finditer('\@\{([0-9]+)\}', message_contents):
            placeholder_positions[int(m.group(1))] = True

        for tag in tags:
            if int(tag["placeholder_position"]) not in placeholder_positions:
                raise IncorrectTags("Incorrect tags for message contents : \"%s\", tags : %s, placeholder positions : %s" % (message_contents, tags, placeholder_positions))
            placeholder_positions.pop(tag["placeholder_position"])

class Thread(Model):
    objects = ThreadManager()

    PRIVATE_DISCUSSION = "discussion"
    PUBLIC_CHANNEL = "public_channel"
    PRIVATE_CHANNEL = "private_channel"

    THREAD_TYPE_CHOICES = (
        (PRIVATE_DISCUSSION, "Private discussion"),
        (PUBLIC_CHANNEL, "Public Channel"),
        (PRIVATE_CHANNEL, "Private Channel")
    )

    title = models.CharField(max_length=50, blank=False, null=False)
    slug = models.CharField(max_length=60, blank=False, null=False, unique=True)
    members = models.ManyToManyField(settings.AUTH_USER_MODEL, through="Membership")
    type = models.CharField(max_length=50, blank=False, null=False, choices=THREAD_TYPE_CHOICES)
    description = models.CharField(max_length=150, default="")
    manager_user = models.ForeignKey(settings.AUTH_USER_MODEL, blank=True, null=True, related_name="managed_threads")
    creator_user = models.ForeignKey(settings.AUTH_USER_MODEL, blank=True, null=True, related_name="created_threads")
    active = models.BooleanField(default=True)

    def __str__(self):
        return "<Thread %s>" % self.title

    @property
    def active_memberships(self):
        return Membership.objects.active_memberships.filter(thread=self)

    @property
    def active_members(self):
        return Membership.objects.active_members(self)

    def delete(self):
        self.active = False
        self.save()

    def is_channel(self):
        return self.__class__.objects.is_channel(self)

    def is_private_channel(self):
        return self.__class__.objects.is_private_channel(self)

    def is_public_channel(self):
        return self.__class__.objects.is_public_channel(self)

    def is_private_discussion(self):
        return self.__class__.objects.is_private_discussion(self)

    def get_last_message(self):
        return self.__class__.objects.get_last_message(self)

    def send(self, message, sender=None, tags=[]):
        return Message.objects.send(
            message, 
            thread=self, 
            sender=sender, 
            tags=tags)

    def add_member(self, user):
        Thread.objects.add_member(user, self)

class Message(Model):
    objects = MessageManager()

    SYSTEM_MESSAGE  = 'system'
    USER_MESSAGE = 'user'
    MESSAGE_TYPE_CHOICES = (
        (SYSTEM_MESSAGE, 'System message'),
        (USER_MESSAGE, 'User message'),
    )

    user_sender = models.ForeignKey(settings.AUTH_USER_MODEL, null=True)
    thread = models.ForeignKey(Thread, blank=False, null=False, on_delete=models.CASCADE, related_name="messages")
    contents = models.CharField(max_length=250, blank=False, null=False)
    sent_date = models.DateTimeField(default=timezone.now)
    attachment = models.FileField(upload_to='uploads/', null=True, blank=True)
#    active = models.BooleanField(default=True)
    message_type = models.CharField(max_length=10, blank=False, null=False, choices=MESSAGE_TYPE_CHOICES, default=USER_MESSAGE)

    def is_user_message(self):
        return self.message_type == self.__class__.USER_MESSAGE

    def is_system_message(self):
        return self.message_type == self.__class__.SYSTEM_MESSAGE

class MessageTag(Model):
    objects = MessageTagManager()

    tagged_user = models.ForeignKey(
        settings.AUTH_USER_MODEL, 
        blank=False, 
        null=False, 
        on_delete=models.CASCADE)
    message = models.ForeignKey(
        Message, 
        blank=False, 
        null=False, 
        on_delete=models.CASCADE, 
        related_name="tags")
    placeholder_position = models.PositiveIntegerField()

    def is_bot_tagged(self):
        return self.tagged_user.is_bot()

    def is_human_tagged(self):
        return self.tagged_user.is_human()

    class Meta:
        unique_together = ('tagged_user', 'message', 'placeholder_position',)

class Membership(Model):
    objects = MembershipManager()

    class Meta:
        unique_together = ("user", "thread")

    user = models.ForeignKey(settings.AUTH_USER_MODEL, default=1, related_name="memberships")
    thread = models.ForeignKey(Thread, blank=False, null=False, on_delete=models.CASCADE, related_name="memberships")
    last_seen_date = models.DateTimeField(null=True)
    last_seen_message = models.ForeignKey(Message, blank=True, null=True)
    active = models.BooleanField(default=True)
    join_date = models.DateTimeField(default=timezone.now)
    
    @property
    def unchecked_count(self):
#        if hasttr(self.thread, "unseen_mention":
#            return self.thread.unseen_mention
        if self.last_seen_date is not None:
            return self.thread.messages.filter(sent_date__gt=self.last_seen_date, pk__gt=self.last_seen_message.pk).count()
        else:
            return self.thread.messages.count()

    def delete(self):
        self.cancel()

    def cancel(self):
        self.objects.cancel_membership(self)

from messaging.instant.observers import *
from messaging.observers import *

