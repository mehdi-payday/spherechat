from omnibus.api import publish
from django.db import connection


class ChannelHandler(object):
    # route
    name = 'route'

    def __init__(self, connection):
        self.name = self.__class__.name
        self.connection = connection

    @classmethod
    def form_channel_full_name(cls, channel=''):
        # package = __package__
        package = cls.__module__
        idx = package.rindex(".")

        if idx:
            package = package[0:idx]

#        channel_full_name = connection.schema_name \
#            + "." + package \
#            + "," + cls.name
        channel_full_name = package + "," + cls.name

        if isinstance(channel, long) or isinstance(channel, int):
            channel = str(channel)

        if channel:
            channel_full_name = channel_full_name + "." + channel

        return channel_full_name

    def handle_channel_message(self, channel, data):
        """
        @return {boolean} : True to publish the same message. False or None
                            to do nothing.
                {dict}    : Another Message to publish
        """
        # By default, repush the message to the channel

        return True

    def publish(self, channel, payload_type, data):
        """
        Publish a message by the currently connected user
        """
        channel_full_name = self.__class__.form_channel_full_name(channel)

        print "Core Channel Handler - Publish to {}".format(channel_full_name)

        self.connection.pubsub.publish(
            channel_full_name,
            payload_type,
            payload=data,
            sender=self.connection.authenticator.get_identifier()
        )

    @classmethod
    def server_publish(self, channel, payload_type, data, sender='server'):
        publish(
            self.__class__.form_channel_full_name(channel),
            # Send to the root
            payload_type,
            data,
            sender='server'
        )
