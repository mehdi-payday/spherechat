# -*- coding: utf-8 -*-

from friendship.models import Friendship
from core.serializers import UserSerializer
from rest_framework import serializers
from datetime import datetime
from core.models import User
from django.db.models import Q
#from friendship.exceptions import

def get_user_from_serializer(serializer):
    request = serializer.context.get('request', None)

    if 'member' in serializer.context:
        return serializer.context.get('member')
    elif request:
        return request.user

    raise Exception("Cannot get member from serializer"
                    + " if request is not in the context")

class FriendshipSerializer(serializers.ModelSerializer):
    class Meta:
        model = Friendship
        fields = ("id", "requester_user", "addresser_user", "status", "request_date", "approval_date", "friendship_end_date", "active")

        requester_user = UserSerializer(source="manager_user", read_only=True)
        extra_kwargs = {
            "requester_user": {"read_only": True},
            "request_date": {"read_only": True},
            "approval_date": {"read_only": True},
            "friendship_end_date": {"read_only": True},
            "active": {"read_only": True},
            "status": {"read_only": True},
        }

