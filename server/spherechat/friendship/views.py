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
from core.serializers import UserSerializer
from friendship.models import Friendship
from friendship.serializers import FriendshipSerializer
from friendship.exceptions import SameUserException, FriendshipExists
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

    @list_route(methods=['POST'])
    def send_friend_request(self, request):
        serializer = FriendshipSerializer
        addresser_user = User.objects.get(id=request.data['addresser_user'])
        requester_user = request.user
        
        try:
            friendship = Friendship.objects.send_friend_request(addresser_user, requester_user)
            return Response (FriendshipSerializer(friendship, many=False).data)
        except (FriendshipExists, SameUserException)  as expetion:
            return Response(str(expetion), status=status.HTTP_400_BAD_REQUEST)
    
    @list_route(methods=['GET'])
    def get_friends(self, request):
        friends = Friendship.objects.get_friends(request.user)
        return Response (FriendshipSerializer(friends, many=True).data)

    @list_route(methods=['GET'])
    def get_friend_requests(self, request):
        requests = Friendship.objects.get_friend_requests(request.user)
        return Response(FriendshipSerializer(requests, many=True).data)

    @detail_route(methods=['GET'])
    def accept_friend_request(self, request, pk):
        pending_friendship = Friendship.objects.get(pk=pk)
        requests = Friendship.objects.accept_friend_request(pending_friendship)
        return Response(FriendshipSerializer(requests, many=False).data)
    
    @detail_route(methods=['GET'])
    def end_friendship(self, request, pk):
        friendship = Friendship.objects.get(pk=pk)
        request = Friendship.objects.end_friendship(friendship)
        return Response(FriendshipSerializer(request, many=False).data)
    
    @detail_route(methods=['GET'])
    def decline_friend_request(self, request, pk):
        pending_friendship = Friendship.objects.get(pk=pk)
        requests = Friendship.objects.decline_friend_request(pending_friendship)
        return Response(FriendshipSerializer(requests, many=False).data)
    
    def get_queryset(self):
        user = self.request.user
        requests = Friendship.objects.get_friendships(user)
        return requests

    def validate(self, data):
        addresser_user = int(self.request.data['addresser_user'])
        if self.request.user.id == addresser_user:
            raise serializers.ValidationError("finish must occur after start")
            #return Response(str(SameUserException), status=status.HTTP_400_BAD_REQUEST)
        return data
"""
    def create(self, request):
        serializer = FriendshipSerializer
        addresser_user = User.objects.get(id=request.data['addresser_user'])
        requester_user = request.user
        friendship = Friendship.objects.create( addresser_user=addresser_user,
                   requester_user=requester_user,
                   status="PEND")
        try:
            Friendship.objects.send_friend_request(friendship)
        except (FriendshipExists, SameUserException)  as expetion:
             return Response(str(expetion), status=status.HTTP_400_BAD_REQUEST)
"""
