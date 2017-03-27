from omnibus.factories import websocket_connection_factory, sockjs_connection_factory
from core.connection import BasicConnection


def sphere_connection_factory(auth_class, pubsub):
    # Generate a new connection class using the default websocket connection
    # factory (we have to pass an auth class - provided by the server and a
    # pubsub singleton, also provided by the omnibusd server
    return BasicConnection

