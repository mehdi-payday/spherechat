"""
spherechat_server URL Configuration
"""

from django.conf.urls import url, include
from django.conf.urls.static import static
from django.conf import settings
from rest_framework import routers
from django.contrib import admin
from core import views
import messaging
import friendship

router = routers.DefaultRouter()
router.register(r'users', views.UserViewSet, base_name='users')
router.register(r'groups', views.GroupViewSet, base_name='groups')

urlpatterns = [
    url(r'^api/', include(router.urls)),
    url(r'^api/django-auth/', include('rest_framework.urls', namespace='rest_framework')),
    url(r'^api/messaging/', include('messaging.urls')),
    url(r'^api/friendship/', include('friendship.urls')),
    url(r'^api/me/$', views.MeUserView.as_view()),
    url(r'^api/me/change_password/', views.ChangePasswordView.as_view()),
    url(r'^api/auth/', include('rest_auth.urls')),
    url(r'^api/auth/registration/', include('rest_auth.registration.urls'))
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
