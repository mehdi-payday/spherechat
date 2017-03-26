from __future__ import unicode_literals

from django.db import models
from django.db import models
from datetime import datetime
from django.conf import settings
from django.contrib.auth.models import User



class Channel(models.Model):
    title = models.CharField(max_length=50, blank=False, null=False)
    slug = models.CharField(max_length=50 ,blank=False, null=False)
    type = models.CharField(max_length=50 ,blank=False, null=False)
    description = models.CharField(max_length=150)
    manager_user_id = models.ForeignKey(User, default=1)
    creator_user_id = models.ForeignKey(settings.AUTH_USER_MODEL, default=1)

class Friendship(models.Model):
    requester_user_id = models.ForeignKey(settings.AUTH_USER_MODEL)
    addressee_user_id = models.ForeignKey(User)
    status = models.CharField(max_length=10 ,blank=False, null=False)
    request_date = models.DateTimeField(default=datetime.now)
    approval_date = models.DateTimeField(default=datetime.now)
    friendship_end_date = models.DateTimeField(default=datetime.now)
    active = models.BooleanField(default=True)

class Message(models.Model):
    user_sender_id = models.ForeignKey(settings.AUTH_USER_MODEL, default=1)
    channel_id = models.ForeignKey(Channel, blank=False, null=False, on_delete=models.CASCADE);
    contents = models.CharField(max_length=150, blank=False, null=False)
    sent_date = models.DateTimeField(default=datetime.now)
    message_type = models.CharField(max_length=150, blank=False, null=False)

class MessageTag(models.Model):
    taged_user_id = models.ForeignKey(User, default=1)
    message_id =  models.ForeignKey(Message, blank=False, null=False, on_delete=models.CASCADE);
    placeholder_position = models.IntegerField()

class Membership(models.Model):
    user_id = models.ForeignKey(settings.AUTH_USER_MODEL, default=1)
    channel_id = models.ForeignKey(Channel, blank=False, null=False, on_delete=models.CASCADE);
    last_seen_date = models.DateTimeField(default=datetime.now)
    last_seen_message_id = models.ForeignKey(Message, blank=False, null=False, on_delete=models.CASCADE);
    is_participant = models.BooleanField(default=False)
    join_date = models.DateTimeField(default=datetime.now)
