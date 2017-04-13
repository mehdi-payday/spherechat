from django.test import TestCase
from core.models import User
from django.utils import timezone
from messaging.models import Thread, Message, MessageTag, Membership
from messaging.exceptions import (UnmanagedThread, 
    UnauthorizedSender,
    UnauthorizedAction,
    ExistingMember, 
    UntitledThread, 
    ImmutableMembersList,
    UnexistentMembership,
    IncorrectTags,
    ExistingDiscussion,
    MessageAlreadySent)

class ThreadTestCase(TestCase):
    def setUp(self):
        self.adam = User.objects.create(username="adam")
        self.mehdi = User.objects.create(username="mehdi")
        self.yassine = User.objects.create(username="yassine")
        self.outsider = User.objects.create(username="outsider")

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


class MembershipTestCase(TestCase):
    def  setUp(self):
        self.adam = User.objects.create(username="adam")
        self.mehdi = User.objects.create(username="mehdi")
        self.yassine = User.objects.create(username="yassine")
        self.outsider = User.objects.create(username="outsider")
        self.thread = Thread.objects.create(title="Nice thread")


    def test_memberships(self):
        thread = self.thread
        
        membership = Membership.objects.create_membership(self.adam, thread)
        self.assertEqual(Membership.objects.get_membership(self.mehdi, self.thread).pk, membership.pk)

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

        self.assertEqual(Membership.objects.count(), 0)

        with self.assertRaises(UnexistentMembership):
            Membership.objects.get_membership(self.mehdi, self.thread)

    def test_seen_mentions(self):
        thread = self.thread

        seen_date = timezone.now()
        with self.assertRaises(UnexistentMembership):
            Membership.objects.see_thread(self.adam, thread, seen_date)
        
        adam_membership = Membership.objects.create_membership(self.adam, thread)
        mehdi_membership = Membership.objects.create_membership(self.mehdi, thread)

        first_message = Message.objects.send(Message(contents="Salut"), thread, self.mehdi)
        second_message = Message.objects.send(Message(contents="Hey :)"), thread, self.adam)
        third_message = Message.objects.send(Message(contents="Ca va ?"), thread, self.adam)

        Membership.objects.see_thread(self.adam, thread, seen_date, last_seen_message=second_message)

        self.assertFalse(Membership.objects.has_seen_thread(self.adam, thread))
        self.assertTrue(Membership.objects.has_seen_message(self.adam, first_message))
        self.assertTrue(Membership.objects.has_seen_message(self.adam, second_message))
        self.assertFalse(Membership.objects.has_seen_message(self.adam, third_message))

        same_seen_date = Membership.objects.get_last_seen_date(self.adam, thread)

        self.assertEqual(same_seen_date, seen_date)

        adam_membership = Membership.objects.create_membership(self.adam, thread)
            
        self.assertEqual(adam_membership.last_seen_date, seen_date)
        self.assertEqual(adam_membership.last_seen_message.pk, second_message.pk)

        self.assertEqual(adam_membership.unchecked_count, 1)

        fourth_message = Message.objects.send(Message(contents="Ca va et toi ?"), thread, self.mehdi)

        self.assertEqual(adam_membership.unchecked_count, 2)

