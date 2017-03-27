from django.contrib.auth.models import Group
from django.contrib.auth import get_user_model
from rest_framework import viewsets, views
from core.serializers import UserSerializer, GroupSerializer
from rest_framework.response import Response
from rest_framework import status


class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """
    queryset = get_user_model().objects.filter(is_active=True).order_by('-date_joined')
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
