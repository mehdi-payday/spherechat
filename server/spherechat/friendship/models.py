# -*- coding: utf-8 -*-

from __future__ import unicode_literals
from django.db import models
from datetime import datetime
from django.conf import settings
from core.models import User
from core.serializers import UserSerializer
from django.db.models import Q
from django.contrib.auth import get_user_model
from django.db.models import (Manager, Model, ObjectDoesNotExist)
from friendship.exceptions import SameUserException, FriendshipExists

class FriendshipManager(Manager):
    def send_friend_request(self, addresser_user, requester_user ):
        if requester_user == addresser_user:
            raise SameUserException("Cannot send friendship request to the same user")
        if self.belongs(addresser_user, requester_user):
            raise FriendshipExists("Friendship already exist and is active")
        else:
            return self.create(addresser_user=addresser_user,
                               requester_user=requester_user,
                               status="PEND")

    def accept_friend_request(self, pending_friendship):      
        pending_friendship.status = "ACPT"
        if pending_friendship.approval_date == None:
            pending_friendship.approval_date = datetime.now()
     
        pending_friendship.save()
        return pending_friendship

    def decline_friend_request(self, pending_friendship):
        pending_friendship.status = "RJCT"
        pending_friendship.active = False
        
        pending_friendship.save()
        return pending_friendship


    def get_friendships(self, user):
        return self.filter(Q(addresser_user=user) | Q(requester_user=user) , status="ACPT", active=True)
    # Get pending friend requests
    def get_friend_requests(self, user):
        return self.filter(addresser_user=user, status="PEND", active=True)
    # TODO Get list of your accepted friend requests
    def get_friends(self, user):
        accepted_users = self.filter(addresser_user=user, status="ACPT", active=True)
        
        #return User.objects.filter(requester_user__in=accepted_users)
        return self.filter(addresser_user=user, status="ACPT", active=True)

    def belongs(self, addresser_user, requester_user):
        return self.filter(requester_user=requester_user, addresser_user=addresser_user , active=True).exists()

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
