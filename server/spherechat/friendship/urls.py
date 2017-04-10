from django.conf.urls import url
from django.conf.urls import include
from rest_framework_extensions.routers import ExtendedSimpleRouter
from friendship import views

router = ExtendedSimpleRouter()

channel_router = router.register(r'friendship',
                                 views.FriendshipViewSet,
                                 base_name='friendship')

urlpatterns = [
        url(r'^', include(router.urls)),
]
