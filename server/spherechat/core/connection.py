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
        Calls the appropriate channel handler following the channel name.
        If no handler is found, the function returns True, which just broadcasts the message to the 
        omnibus channel.
        
        If the right format of channel is used. The function imports {handler} from {module}.channels and
        calls the handler function passing to it {subchannel} as the channel name.
        
        Parameters :
        ------------
        channel - str
            The channel "route".
            {module},{handler}.{subchannel}
        Return 
        ------
        boolean - True to publish right away the message to the omnibus channel; False to do nothing.
        """
        def call_handler(self, channel, payload, **kwargs):
            module = ''
            handler = ''
            subchannel = ''

            comma_index = channel.index(",")
            first = channel[0:comma_index]
            last = channel[comma_index+1:]
            
            module = first       

            parts = last.split(".")

            if len(parts) is not 2:
                return True
            else:
                handler = parts[0]
                subchannel = parts[1]
                
            twodots_index = payload.index(":")

            data = simplejson.loads(payload[twodots_index+1:])
            mod = import_module(module + ".channels")

            handler_class = getattr(mod, handler)
            
            handler_instance = handler_class(self)

            action = handler_instance.handle_channel_message(subchannel, data)

            return action

        def on_channel_message(self, channel, payload):

            if (channel in self.subscriber.channels
                    and self.authenticator.can_publish(channel)):
                
                publish_it = self.call_handler(channel, payload)

                if publish_it == True:
                    self.publish(payload)

    return BasicConnection
