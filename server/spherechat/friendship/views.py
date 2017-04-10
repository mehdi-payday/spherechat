from django.utils import timezone
from django.contrib.auth.models import Group
from django.contrib.auth import get_user_model
from django.core.exceptions import ValidationError
from rest_framework import serializers
from rest_framework import mixins
from rest_framework import viewsets
from rest_framework import filters
from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.pagination import CursorPagination
from rest_framework.decorators import detail_route, list_route
from core.models import User
from friendship.models import Friendship
from friendship.serializers import FriendshipSerializer
from rest_framework import status
from rest_framework.response import Response
# from rest_framework import filters
from rest_framework_extensions.mixins import NestedViewSetMixin
import django_filters
import logging

logger = logging.getLogger(__name__)

def get_user_from_request(request):
    """
    Gets the authenticated user from the request
    """
    return request.user

def get_user_from_view(view):
    """
    Gets the authenticated user from a view instance
    """
    request = view.request
    member = get_user_from_request(request)

    return member

class FriendshipViewSet(mixins.CreateModelMixin,
                     mixins.ListModelMixin,
                     mixins.RetrieveModelMixin,
                     viewsets.GenericViewSet):

    serializer_class = FriendshipSerializer

    def get_queryset(self):
        return Friendship.objects.filter(addresser_user=get_user_from_view(self), status="PEND")

    def perform_create(self, serializer):
        serializer.save(
            requester_user=self.request.user,
            status="PEND",
        )

