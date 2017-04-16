from django.test import TestCase
from core.models import User
from django.utils import timezone
import time
from messaging.models import Thread, Message, MessageTag, Membership, TuneManager, LISTENING_RENEWAL_RATE
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

class ThreadTestCase(TestCase):
    def setUp(self):
        self.adam = User.objects.create(username="adam", email="adam@cherti.name")
        self.mehdi = User.objects.create(username="mehdi", email="mehdi@cherti.name")
        self.yassine = User.objects.create(username="yassine", email="yassine@cherti.name")
        self.outsider = User.objects.create(username="outsider", email="outsider@cherti.name")

    def test_channel(self):
        title = "Basketball Channel"
        description = "May the force be with you"
        channel = Thread.objects.create_channel(
            dict(title=title, 
                 type=Thread.PRIVATE_CHANNEL,
                 description=description), self.adam)
        self.assertEqual(channel.title, title)
        self.assertEqual(channel.slug, "basketball-channel")
        self.assertEqual(channel.description, description)
        self.assertEqual(channel.creator_user.pk, self.adam.pk)
        self.assertEqual(channel.manager_user.pk, self.adam.pk)
        self.assertTrue(channel.active)
        self.assertEqual(channel.type, Thread.PRIVATE_CHANNEL)

        self.assertTrue(channel.is_channel() and channel.is_private_channel() and not channel.is_public_channel() and not channel.is_private_discussion())

        with self.assertRaises(ExistingMember):
            channel.add_member(self.adam)
        channel.add_member(self.mehdi)
        channel.add_member(self.yassine)

        members = channel.active_members
        self.assertEqual(len(members), 3)
        self.assertEqual(members[0].pk, self.adam.pk)
        self.assertEqual(members[1].pk,self.mehdi.pk)
        self.assertEqual(members[2].pk, self.yassine.pk)

        memberships = channel.active_memberships
        self.assertEqual(len(memberships), 3)
        self.assertEqual(memberships[0].user.pk, self.adam.pk)
        self.assertEqual(memberships[1].user.pk, self.mehdi.pk)
        self.assertEqual(memberships[2].user.pk, self.yassine.pk)

        self.assertTrue(Thread.objects.can_manage(self.adam, channel))
        self.assertTrue(Thread.objects.is_manager(self.adam, channel))
        self.assertFalse(Thread.objects.can_manage(self.mehdi, channel))
        self.assertFalse(Thread.objects.can_manage(self.yassine, channel))


        self.assertTrue(Thread.objects.can_send(self.adam, channel))
        self.assertTrue(Thread.objects.can_send(self.mehdi, channel))
        self.assertTrue(Thread.objects.can_send(self.yassine, channel))

        with self.assertRaises(UnauthorizedSender):
            self.assertFalse(Thread.objects.can_send(self.outsider, channel))

        self.assertEqual(Thread.objects.get_manager(channel).pk, self.adam.pk)

    def test_private_discussion(self):
        discussion = Thread.objects.create_discussion(self.adam, self.mehdi)

        self.assertTrue(Thread.objects.private_discussion_exists(self.adam, self.mehdi))

        adam_private_discussions = Thread.objects.private_discussions(self.adam)
        mehdi_private_discussions = Thread.objects.private_discussions(self.mehdi)
        self.assertEqual(adam_private_discussions.count(), 1)
        self.assertEqual(mehdi_private_discussions.count(), 1)

        self.assertEqual(adam_private_discussions[0].pk, discussion.pk)
        self.assertEqual(mehdi_private_discussions[0].pk, discussion.pk)

        members = discussion.members.all()
        assert members.count() == 2

        self.assertEqual(members[0].pk, self.adam.pk)
        self.assertEqual(members[1].pk, self.mehdi.pk)
        self.assertEqual(discussion.creator_user.pk, self.adam.pk)
        self.assertTrue(discussion.is_private_discussion())
        self.assertFalse(discussion.is_channel())
        self.assertFalse(discussion.is_private_channel())
        self.assertFalse(discussion.is_public_channel())

        self.assertTrue(Thread.objects.can_send(self.adam, discussion))
        self.assertTrue(Thread.objects.can_send(self.mehdi, discussion))

        with self.assertRaises(UnauthorizedSender):
            self.assertFalse(Thread.objects.can_send(self.yassine, discussion))

        self.assertFalse(Thread.objects.can_manage(self.adam, discussion))
        self.assertFalse(Thread.objects.can_manage(self.mehdi, discussion))
        self.assertFalse(Thread.objects.can_manage(self.yassine, discussion))

        self.assertFalse(Thread.objects.is_manager(self.adam, discussion))
        self.assertFalse(Thread.objects.is_manager(self.mehdi, discussion))
        self.assertFalse(Thread.objects.is_manager(self.yassine, discussion))       

        with self.assertRaises(ExistingDiscussion):
            same_discussion = Thread.objects.create_discussion(self.adam, self.mehdi)
        with self.assertRaises(ExistingDiscussion):
            same_discussion_2 = Thread.objects.create_discussion(self.mehdi, self.adam)

        exactly_same_discussion = Thread.objects.discussion_between(self.adam, self.mehdi)
        exactly_same_discussion_2 = Thread.objects.find_discussion_between(self.adam, self.mehdi)
        self.assertEqual(exactly_same_discussion.pk, discussion.pk)
        self.assertEqual(exactly_same_discussion_2.pk, discussion.pk)

        with self.assertRaises(AssertionError):
            Thread.objects.add_member(self.yassine, discussion)

        with self.assertRaises(UnmanagedThread):        
            Thread.objects.get_manager(discussion)

class BaseTestCase(TestCase):
    def setUp(self):
        self.adam = User.objects.create(username="adam", email="adam@cherti.name")
        self.mehdi = User.objects.create(username="mehdi", email="mehdi@cherti.name")
        self.yassine = User.objects.create(username="yassine", email="yassine@cherti.name")
        self.outsider = User.objects.create(username="outsider", email="outsider@cherti.name")
        self.channel = Thread.objects.create_channel(dict(title="Basketball Channel", type=Thread.PRIVATE_CHANNEL), self.adam)

class MembershipTestCase(BaseTestCase):
    def test_memberships(self):
        thread = self.channel
        
        membership = Membership.objects.get_membership(self.adam, thread)
#        self.assertEqual(Membership.objects.get_membership(self.mehdi, self.thread).pk, membership.pk)

        self.assertTrue(Membership.objects.belongs(self.adam, thread))
        self.assertFalse(Membership.objects.belongs(self.mehdi, thread))
        self.assertFalse(Membership.objects.belongs(self.outsider, thread))

        memberships = Membership.objects.active_memberships
        self.assertEqual(memberships.count(), 1)
        all_memberships = Membership.objects.all()
        self.assertEqual(all_memberships.count(), 1)

        members = Membership.objects.active_members(thread)
        self.assertEqual(members.count(), 1)
        self.assertEqual(members[0].pk, self.adam.pk)

        self.assertEqual(memberships[0].pk, membership.pk)
        self.assertEqual(all_memberships[0].pk, membership.pk)

        with self.assertRaises(ExistingMember):
            Membership.objects.create_membership(self.adam, thread)

        same_membership = Membership.objects.get_or_create_membership(self.mehdi, thread)
        self.assertTrue(same_membership.pk, membership.pk)

        Membership.objects.cancel_membership(same_membership)
        Membership.objects.cancel_membership(membership)

        self.assertEqual(len(Membership.objects.active_memberships), 0)

        with self.assertRaises(UnexistentMembership):
            Membership.objects.get_membership(self.mehdi, thread)

    def test_seen_mentions(self):
        thread = self.channel

        seen_date = timezone.now()

        with self.assertRaises(UnexistentMembership):
            Membership.objects.see_thread(self.mehdi, thread, seen_date)
        with self.assertRaises(ExistingMember):
            adam_membership = Membership.objects.create_membership(self.adam, thread)
        mehdi_membership = Membership.objects.create_membership(self.mehdi, thread)

        first_message = Message.objects.send(Message(contents="Salut"), thread, self.mehdi)
        second_message = Message.objects.send(Message(contents="Hey :)"), thread, self.adam)
        seen_date = timezone.now()
        Membership.objects.see_thread(self.adam, thread, seen_date, last_seen_message=second_message)

        third_message = Message.objects.send(Message(contents="Ca va ?"), thread, self.mehdi)

        self.assertFalse(Membership.objects.has_seen_thread(self.adam, thread))

        memb = Membership.objects.get_membership(self.adam, thread)
#        raise Exception("Memb last seen date is %s while message sent date is %s" % (memb.last_seen_date, first_message.sent_date))
        self.assertTrue(Membership.objects.has_seen_message(self.adam, first_message))
        self.assertTrue(Membership.objects.has_seen_message(self.adam, second_message))
        self.assertFalse(Membership.objects.has_seen_message(self.adam, third_message))

        same_seen_date = Membership.objects.get_last_seen_date(self.adam, thread)

        self.assertEqual(same_seen_date, seen_date)

        adam_membership = Membership.objects.get_membership(self.adam, thread)
            
        self.assertEqual(adam_membership.last_seen_date, seen_date)
        self.assertEqual(adam_membership.last_seen_message.pk, second_message.pk)

        self.assertEqual(adam_membership.unchecked_count, 1)

        fourth_message = Message.objects.send(Message(contents="Ca va et toi ?"), thread, self.mehdi)

        self.assertEqual(adam_membership.unchecked_count, 2)

class TuneTestCase(BaseTestCase):
    def test_tune(self):
        channel = self.channel

        tune_manager = TuneManager.get()

#        Thread.objects.add_member(self.adam, channel)
        Thread.objects.add_member(self.mehdi, channel)

        self.assertTrue(tune_manager.can_tune(self.adam, channel))
        self.assertTrue(tune_manager.can_tune(self.mehdi, channel))
        self.assertFalse(tune_manager.can_tune(self.yassine, channel))
        self.assertFalse(tune_manager.can_tune(self.outsider, channel))

        with self.assertRaises(UnauthorizedAction):
            tune_manager.tune(self.outsider, channel)

        listening_date = timezone.now()
        tune_manager.tune(self.adam, channel, listening_date)

        self.adam.refresh_from_db()

        self.assertEqual(self.adam.listening_thread.pk, channel.pk)
        self.assertEqual(self.adam.last_listening_date, listening_date)

        self.adam.last_listening_date = None
        self.adam.listening_thread = None
        self.adam.save()

        with self.assertRaises(UserNotTuned):
            tune_manager.get_last_heartbeat_date(self.adam, channel)

        tune_manager.tune(self.adam, channel, listening_date)
        self.adam.refresh_from_db()

        self.assertFalse(self.adam.last_listening_date == None)

        self.assertTrue(tune_manager.is_tuned(self.adam, channel))
        self.assertFalse(tune_manager.is_tuned(self.yassine, channel))
        self.assertFalse(tune_manager.is_tuned(self.mehdi, channel))
        
        self.assertEqual(tune_manager.get_last_heartbeat_date(self.adam, channel), listening_date)

    def test_tune_last_seen_message(self):
        channel = self.channel
        
        tune_manager = TuneManager.get()

        Thread.objects.add_member(self.mehdi, channel)
        
        listening_date = timezone.now()        
        tune_manager.tune(self.adam, channel, listening_date)

        first_message = channel.send(Message(contents="Hey"), self.adam)

        memb = Membership.objects.get_membership(self.adam, channel)
        self.assertTrue(Membership.objects.has_seen_message(self.adam, first_message))
        self.assertFalse(Membership.objects.has_seen_message(self.mehdi, first_message))
        self.assertTrue(Membership.objects.has_seen_thread(self.adam, channel))
        self.assertFalse(Membership.objects.has_seen_thread(self.mehdi, channel))
        
        time.sleep(LISTENING_RENEWAL_RATE)
        second_message = channel.send(Message(contents="Hey :)"), self.mehdi)

        self.assertFalse(Membership.objects.has_seen_message(self.adam, second_message))
        self.assertFalse(Membership.objects.has_seen_message(self.mehdi, second_message))
        self.assertFalse(Membership.objects.has_seen_thread(self.adam, channel))
        self.assertFalse(Membership.objects.has_seen_thread(self.mehdi, channel))

