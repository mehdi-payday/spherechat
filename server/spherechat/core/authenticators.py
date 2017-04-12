from omnibus.authenticators import UserAuthenticator
from rest_framework import authentication
from rest_framework import exceptions
from core.models import User

class UserTokenAuthenticator(UserAuthenticator):
    def __init__(self, identifier, user):
        self.identifier = identifier
        self.user = user

    @classmethod
    def authenticate(cls, args):
        # Check if we found a auth_token
        print "Authentication"
        print args

        if ':' in args:
            # auth token available, try to validate
            identifier = ''
            token = ''
            try:
                identifier, token = args.split(':')
            except ValueError:
                return None
            try:
                user, token = cls.validate_auth_token(token)
            except exceptions.AuthenticationFailed:
                return None
        else:
            raise Exception("Bad format %s" % args)
            identifier = args
            user = None

        return cls(identifier, user)

    @classmethod
    def validate_auth_token(cls, token):
        auth = authentication.TokenAuthentication()
        user, token = auth.authenticate_credentials(token)

        return (user, token)
  
    def can_subscribe(self, channel):
        return super(UserTokenAuthenticator, self).can_subscribe(channel)

    def can_unsubscribe(self, channel):
        return super(UserTokenAuthenticator, self).can_unsubscribe(channel)

    def can_publish(self, channel):
        return self.user is not None
