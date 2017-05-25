"""
WSGI config for spherechat_server project.

It exposes the WSGI callable as a module-level variable named ``application``.

For more information on this file, see
https://docs.djangoproject.com/en/1.10/howto/deployment/wsgi/
"""

import os
import sys

sys.path.append('/var/www/spherechat/server/spherechat')
sys.path.append('/var/www/spherechat/server/spherechat/spherechat_server')

from django.core.wsgi import get_wsgi_application

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "spherechat_server.settings")

print 'IMPORTING', __name__, 'from', __file__, 'in', os.getpid()

try:
    application = get_wsgi_application()
except Exception:
    # Error loading applications.

    if 'mod_wsgi' in sys.modules:
        os.kill(os.getpid(), signal.SIGINT)
        
    raise
