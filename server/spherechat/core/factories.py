from core.authenticators import UserTokenAuthenticator

def usertokenauthenticator_factory():
    """
    `userauthenticator_factory` returns the Token Authenticator class (uses rest  _framework.authentication.TokenAuthentication) 
    """
    return UserTokenAuthenticator
