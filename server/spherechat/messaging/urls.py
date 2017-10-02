from django.conf.urls import url
from rest_framework.routers import DefaultRouter
from django.conf.urls import include
from rest_framework_extensions.routers import ExtendedSimpleRouter
from messaging import views

router = ExtendedSimpleRouter()

channel_router = router.register(r'channel',
                                views.ChannelViewSet,
                                base_name='channel')

privatediscussion_router = router.register(r'privatediscussion',
                                views.PrivateDiscussionViewSet,
                                base_name='privatediscussion')

channel_router.register(r'messages',
                        views.MessageViewSet,
                        base_name='channel-messages',
                        parents_query_lookups=['thread'] )

privatediscussion_router.register(r'messages',
                                  views.MessageViewSet,
                                  base_name='privatediscussion-messages',
                                  parents_query_lookups=['thread'])

urlpatterns = [
        url(r'^', include(router.urls)),
]
