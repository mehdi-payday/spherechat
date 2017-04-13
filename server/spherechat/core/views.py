from django.contrib.auth.models import Group
from django.contrib.auth import get_user_model
from rest_framework import viewsets, views
from core.serializers import UserSerializer, GroupSerializer
from rest_framework.response import Response
from rest_framework import status
from rest_framework import filters
from django.db.models import Q
import django_filters


class UserFilter(django_filters.rest_framework.FilterSet):
    class Meta:
        model = get_user_model()
        fields = []

    belongs_to = django_filters.CharFilter(method='filter_by_belonging_to_thread')
    does_not_belong_to = django_filters.CharFilter(method='filter_by_not_belonging_to_thread')

    def filter_by_belonging_to_thread(self, queryset, name, value):
        return queryset.filter(memberships__thread=value)

    def filter_by_non_belonging_to_thread(self, queryset, name, value):
        return queryset.filter(~Q(memberships__thread=value))


class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """
    queryset = get_user_model().objects.filter(is_active=True).order_by('-date_joined')
    filter_class = UserFilter
    filter_backends = (filters.SearchFilter,
                       django_filters.rest_framework.DjangoFilterBackend)
    search_fields = ('first_name', 'last_name', 'username', 'email')

    serializer_class = UserSerializer


class GroupViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows groups to be viewed or edited.
    """
    queryset = Group.objects.all()
    serializer_class = GroupSerializer


class MeUserView(views.APIView):
    def get(self, request, format=None):
        return Response(UserSerializer(request.user, context={'request': request}).data)

    def post(self, request, format=None):
        serializer = UserSerializer(request.user, data=request.data, context={'request': request})

        if not serializer.is_valid():
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

        serializer.save()

        return Response(serializer.data)
