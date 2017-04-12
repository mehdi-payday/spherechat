# -*- coding: utf-8 -*-

from __future__ import unicode_literals
from django.db import models
from datetime import datetime
from django.conf import settings
from django.contrib.auth import get_user_model
from django.db.models import (Manager, Model, ObjectDoesNotExist)


class FriendshipManager(Manager):
    def send_friend_request(self, requester, addresser):
        pass

    def accept_friend_request(self, pending_friendship):
        pass

    def get_friendships(self, user):
        pass

    def get_friend_requests(self, user):
        pass

    def get_friends(self, user):
        pass

class Friendship(Model):
    objects = FriendshipManager()

    requester_user = models.ForeignKey(settings.AUTH_USER_MODEL, blank=False, null=False, related_name='friendship_initiated_requests')
    addresser_user = models.ForeignKey(settings.AUTH_USER_MODEL, blank=False, null=False, related_name='friendship_received_requests')
    
    STATUS_PENDING = 'PEND'
    STATUS_REJECTED = 'RJCT'
    STATUS_ACCEPTED = 'ACPT'
    STATUS_CHOICES = (
        (STATUS_PENDING, 'Pending request'),
        (STATUS_REJECTED, 'Friendship rejected'),
        (STATUS_ACCEPTED, 'Friendship accepted'),
    )

    status = models.CharField(max_length=10, choices=STATUS_CHOICES, blank=False, null=False)
    request_date = models.DateTimeField(default=datetime.now, blank=True)
    approval_date = models.DateTimeField(null=True, blank=True)
    friendship_end_date = models.DateTimeField(null=True, blank=True)
    active = models.BooleanField(default=True)
