from omnibus.factories import (
    websocket_connection_factory, 
    sockjs_connection_factory)
import simplejson
from importlib import import_module


def connection_factory(auth_class, pubsub):
    class BasicConnection(websocket_connection_factory(auth_class, pubsub)):
        def on_message(self, msg):
            super(BasicConnection, self).on_message(msg)

        """
        @return {boolean} publish : publish the message
        """
        def call_handler(self, channel, payload, **kwargs):
            module = ''
            handler = ''
            subchannel = ''

            comma_index = channel.index(",")

            first = channel[0:comma_index]
            last = channel[comma_index+1:]

            print "First"
            print first
            print "Last"
            print last

#       parts = first.split(".")
            module = first       
#       if len(parts) < 1:
#           return True
#       else:
#           schema_name = parts[0]
#           module = ".".join(parts[1:])

            parts = last.split(".")

            print "Parts"
            print parts

            if len(parts) is not 2:
                return True
            else:
                handler = parts[0]
                subchannel = parts[1]

            print "Parts"
            print parts

#            connection.schema_name = schema_name
            twodots_index = payload.index(":")

            data = simplejson.loads(payload[twodots_index+1:])
            mod = import_module(module + ".channels")

            handler_class = getattr(mod, handler)

#       handler_class = getattr(import_module(module), handler)
            print "Handler"
            print handler_class
        
            handler_instance = handler_class(self)

            action = handler_instance.handle_channel_message(subchannel, data)

            return action

        def on_channel_message(self, channel, payload):
            if (channel in self.subscriber.channels
                    and self.authenticator.can_publish(channel)):
                print "channel"
                print channel

                publish_it = self.call_handler(channel, payload)

                print "Republish it ??"
                print publish_it
    
                if publish_it == True:
                    self.publish(payload)

    return BasicConnection
