from django.conf.urls import url
from rest_framework.routers import DefaultRouter
from django.conf.urls import include
# from rest_framework_nested import routers as nested_routers
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

# channel_router.register(r'memberships', views.MembershipViewSet, base_name='channel-memberships',  parents_query_lookups=['thread'])
# channel_router.register(r'member_relations', views.MemberThreadViewSet, base_name='channel-members-relation',  parents_query_lookups=['thread'] )

privatediscussion_router.register(r'messages',
                                  views.MessageViewSet,
                                  base_name='privatediscussion-messages',
                                  parents_query_lookups=['thread'])

urlpatterns = [
        url(r'^', include(router.urls)),
]
