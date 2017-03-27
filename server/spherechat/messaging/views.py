from django.contrib.auth.models import Group
from django.contrib.auth import get_user_model
from rest_framework import mixins
from rest_framework import viewsets
from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from messaging.serializers import (MessageTagSerializer, 
    ThreadSerializer, 
    MessageSerializer, 
    MembershipSerializer,
    ChannelSerializer,
    PrivateDiscussionSerializer)

import logging

logger = logging.getLogger(__name__)

def get_user_from_request(request):
    return request.user

def get_user_from_view(view):
    request = view.request
    member = get_user_from_request(request)
    return member


class ChannelViewSet(viewsets.ModelViewSet):
    serializer_class = ChannelSerializer

    def get_queryset(self):
        return Thread.objects.channels(get_member_from_view(self))


class PrivateDiscussionViewSet(viewsets.ModelViewSet):
    serializer_class = PrivateDiscussionSerializer

    def get_queryset(self):
        return Thread.objects.private_discussions(get_member_from_view(self))

class MessageViewSet(mixins.CreateModelMixin,
                     mixins.ListModelMixin,
                     mixins.RetrieveModelMixin,
                     viewsets.GenericViewSet):
    queryset = Message.objects.all()
    serializer_class = MessageSerializer

    filter_class = MessageFilter
    filter_backends = (filters.SearchFilter,filters.DjangoFilterBackend,filters.OrderingFilter)
    search_fields = ('contents', 'attachment')

    ordering_fields = ('sent_date',)
    ordering=('-sent_date',)

    def get_message_thread(self, request, **kwargs):
        if 'parent_lookup_thread' in kwargs:
            thread = Thread.objects.get(pk=int(kwargs['parent_lookup_thread']))
            return thread
        else:
            return None

    def create(self, request, **kwargs):
        thread = self.get_message_thread(request, **kwargs)
        logger.debug("Preparing to create a message in thread %r" % thread)

        sender =  get_user_from_request(request)
        context = {
            'request': request,
            'user': sender,
#            'thread': thread,
        }

        data = request.data

        if thread is not None:
            data['thread'] = thread

        message_serializer = MessageSerializer(
            data=data,
            context=context)

        sent_message = message_serializer.save()

        return Response(MessageSerializer(sent_message, context=context).data)
