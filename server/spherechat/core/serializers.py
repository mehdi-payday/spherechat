from django.contrib.auth.models import Group
from core.models import User
from rest_framework import serializers


class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ('id', 'username', 'email', 'first_name', 'last_name', 'is_staff', 'is_active', 'groups')
        extra_kwargs = {
            "is_staff": {"read_only": True},
            "is_active": {"read_only": True},
            "groups": {"read_only": True},
        }


class GroupSerializer(serializers.ModelSerializer):
    class Meta:
        model = Group
        fields = ('name')
