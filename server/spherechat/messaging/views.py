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
from messaging.models import TuneManager
from messaging.exceptions import UnexistentMembership
from messaging.serializers import (MessageTagSerializer, 
    ThreadSerializer, 
    MessageSerializer, 
    MembershipSerializer,
    ChannelSerializer,
    PrivateDiscussionSerializer)
from messaging.models import (Message, Thread, Membership, MessageTag)
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

class SeeThreadSerializer(serializers.Serializer):
    last_seen_message = serializers.PrimaryKeyRelatedField(queryset=Message.objects.all(), required=False)
    seen_date = serializers.DateTimeField(default=timezone.now)
    thread = serializers.PrimaryKeyRelatedField(queryset=Thread.objects.all())
    user = serializers.PrimaryKeyRelatedField(queryset=User.objects.all())
    
    def validate(self, data):
        thread = data["thread"]
        last_seen_message = data["last_seen_message"]

        if last_seen_message and not thread.messages.filter(id=last_seen_message.pk).exists():
            raise serializers.ValidationError("message %i does not belong to thread %s" % (last_seen_message, thread))

        return super(SeeThreadSerializer, self).validate(data)

    def save(self):
        validated_data = self.validated_data

        user = validated_data["user"]
        thread = validated_data["thread"]
        seen_date = validated_data["seen_date"]
        last_seen_message = validated_data["last_seen_message"]

        Thread.objects.see_thread(user, thread, seen_date, last_seen_message)

class TuneMixin(object):
    @detail_route(methods=['post'])
    def tune(self, request, pk):
        thread = self.get_object()
        user = get_user_from_view(self)

        try:
            TuneManager.get().tune(user, thread, listening_date=timezone.now())
            return Response({
                "listening_thread": user.listening_thread,
                "last_listening_date": user.last_listening_date
            })
        except UnexistentMembership as unexistentMembership:
            return Response(str(unexistentMembership), status=status.HTTP_401_UNAUTHORIZED)

    @detail_route(methods=['post'])
    def see(self, request, pk):
        thread = self.get_object()
        user = get_user_from_view(self)
        seen_date = request.data.pop("seen_date", timezone.now())
        last_seen_message = request.data.pop("last_seen_message", None)
        
        data = request.data
        data['thread'] = thread
        data['user'] = user
        
        serializer = SeeThreadSerializer(data=data, context={'request': request})

        if not serializer.is_valid():
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

        try:
            serializer.save()
        except UnexistentMembership as unexistentMembership:
            return Response(str(unexistentMembership), status=status.HTTP_401_UNAUTHORIZED)

        return self.retrieve(request, pk)

class ChannelViewSet(TuneMixin,
                     mixins.CreateModelMixin,
                     mixins.ListModelMixin,
                     mixins.RetrieveModelMixin, 
                     viewsets.GenericViewSet):
    """
    Permits to list and create channels
    """
    serializer_class = ChannelSerializer

    filter_backends = (filters.SearchFilter, )
    search_fields = ('title', )

    def get_queryset(self):
        return Thread.objects.channels(get_user_from_view(self))

class PrivateDiscussionViewSet(TuneMixin,
                               mixins.CreateModelMixin,
                               mixins.ListModelMixin,
                               mixins.RetrieveModelMixin,
                               viewsets.GenericViewSet):
    serializer_class = PrivateDiscussionSerializer

    def get_queryset(self):
        return Thread.objects.private_discussions(get_user_from_view(self))

class MessageFilter(django_filters.rest_framework.FilterSet):
    class Meta:
        model = Message
        fields = ('user_sender', 'contents', 'attachment_name', 'sent_date')

    attachment_name = django_filters.CharFilter(method='filter_by_attachment_name')

    def filter_by_attachment_name(self, queryset, value):
        return queryset.filter(attachment__name__icontains=value)

class MessagePagination(CursorPagination):
    page_size = 20
    ordering = '-sent_date'

class MessageViewSet(NestedViewSetMixin,
                     mixins.CreateModelMixin,
                     mixins.ListModelMixin,
                     mixins.RetrieveModelMixin,
                     viewsets.GenericViewSet):
    queryset = Message.objects.all()
    serializer_class = MessageSerializer

    filter_class = MessageFilter
    filter_backends = (filters.SearchFilter,
                       django_filters.rest_framework.DjangoFilterBackend,
                       filters.OrderingFilter)
    search_fields = ('contents', 'attachment')

    pagination_class = CursorPagination

    ordering_fields = ('sent_date',)
    ordering = ('-sent_date',)

    def _get_message_thread(self, request, **kwargs):
        """
        Gets thread from the request
        """
        if 'parent_lookup_thread' in kwargs:
            thread = Thread.objects.get(pk=int(kwargs['parent_lookup_thread']))
            return thread
        else:
            return None

    def create(self, request, **kwargs):
        thread = self._get_message_thread(request, **kwargs)
        logger.debug("Preparing to create a message in thread %r", thread)

        sender = get_user_from_request(request)
        context = {
            'request': request,
            'user': sender,
        }

        data = request.data

        if thread is not None:
            data['thread'] = thread.pk

        message_serializer = MessageSerializer(
            data=data,
            context=context)
        message_serializer.is_valid(raise_exception=True)
        sent_message = message_serializer.save()

        return Response(MessageSerializer(sent_message, context=context).data)