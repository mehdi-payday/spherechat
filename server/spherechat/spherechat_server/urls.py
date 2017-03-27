"""spherechat_server URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.10/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""

from django.conf.urls import url, include
from rest_framework import routers
from django.contrib import admin
from core import views
import messaging

router = routers.DefaultRouter()
router.register(r'users', views.UserViewSet, base_name='users')
router.register(r'groups', views.GroupViewSet, base_name='groups')

# Wire up our API using automatic URL routing.
# Additionally, we include login URLs for the browsable API.
urlpatterns = [
    url(r'^api/', include(router.urls)),
#    url(r'^admin/', include('admin.site.urls')),
    url(r'^api/django-auth/', include('rest_framework.urls', namespace='rest_framework')),
    url(r'^api/messaging/', include('messaging.urls')),
    url(r'^api/me/', views.MeUserView.as_view()),
    url(r'^api/auth/', include('rest_auth.urls')),
    url(r'^api/auth/registration/', include('rest_auth.registration.urls'))
]
