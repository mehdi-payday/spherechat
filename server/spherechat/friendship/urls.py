from django.conf.urls import url
from django.conf.urls import include
from rest_framework_extensions.routers import ExtendedSimpleRouter
from friendship import views

router = ExtendedSimpleRouter()

urlpatterns = [
        url(r'^', include(router.urls)),
]
