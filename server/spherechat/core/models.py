# -*- coding: utf-8 -*-

from django.db import models
from django.contrib.auth.models import (AbstractBaseUser, PermissionsMixin, UserManager)
from django.db.models import Manager
from django.utils.translation import ugettext_lazy as _
from django.core import validators
from django.utils import timezone

class UserManager(UserManager):
    def get_admin_user(self):
        return self.get(is_staff=True)

    def active_users(self):
        return self.filter(is_active=True)

class User(AbstractBaseUser, PermissionsMixin):
    """
    An abstract base class implementing a fully featured User model
    Email and password are required.
    Other fields are optional.
    """
    objects = UserManager()

    username = models.CharField(
        _('username'),
        max_length=254,
        help_text=_('Required. 254 characters or fewer. Letters, digits and @/./+/-/_ only.'),
        validators = [validators.RegexValidator(r'^[\w.@+-]+$', _('Enter a valid username. This value may contain only ''letters, numbers ' 'and @/./+/-/_ characters.')),],
        error_messages={'unique': _("A user with that username already exists."),
        },
        unique=True,
        blank=True,
    )
    first_name = models.CharField(_('first name'), max_length=30, blank=True)
    last_name = models.CharField(_('last name'), max_length=30, blank=True)
    email = models.EmailField(
        _('email address'),
        blank=False,
        unique=True,
        error_messages={
            'unique': _("A user with that email already exists."),
        },
    )
    is_staff = models.BooleanField(
        _('staff status'),
        default=False,
        help_text=_('Designates whether the user can log into this admin site.'),
    )
    is_active = models.BooleanField(
        _('active'),
        default=True,
        help_text=_(
            'Designates whether this user should be treated as active. '
            'Unselect this instead of deleting accounts.'
        ),
    )
    date_joined = models.DateTimeField(
        _('date joined'),
        default=timezone.now
        )

    objects = UserManager()

    picture = models.ImageField(
        'profile_picture',
        upload_to='uploads',
        null=True,
        blank=True
    )
    HUMAN = "HUM"
    BOT = "BOT"
    USER_TYPE_CHOICES = (
        (HUMAN, "Human user"),
        (BOT, "Bot")
    )
    type = models.CharField(max_length=4, choices=USER_TYPE_CHOICES, default=HUMAN)
    listening_thread = models.ForeignKey("messaging.Thread", null=True, blank=True)
    last_listening_date = models.DateTimeField(null=True)

    USERNAME_FIELD = 'username'
    REQUIRED_FIELDS = []

    def is_human(self):
        return self.type == self.__class__.HUMAN

    def is_bot(self):
        return self.type == self.__class__.BOT

    def get_author(self):
        return self

    def __unicode__(self):
        return self.get_full_name()

    def __str__(self):
        return self.__unicode__()

    def get_full_name(self):
        """
        Returns the first_name plus the last_name, with a space in between.
        """
        full_name = '%s %s' % (self.first_name, self.last_name)
        if self.first_name == "" and self.last_name == "":
            full_name = self.username
        return full_name.strip()

    def get_short_name(self):
        "Returns the short name for the user."
        return self.first_name

    def email_user(self, subject, message, from_email=None, **kwargs):
        """
        Sends an email to this User.
        """
        send_mail(subject, message, from_email, [self.email], **kwargs)      
