# -*- coding: utf-8 -*-
# Generated by Django 1.11 on 2017-04-16 18:49
from __future__ import unicode_literals

import datetime
from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='Membership',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('last_seen_date', models.DateTimeField(null=True)),
                ('active', models.BooleanField(default=True)),
                ('join_date', models.DateTimeField(default=datetime.datetime.now)),
            ],
        ),
        migrations.CreateModel(
            name='Message',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('contents', models.CharField(max_length=250)),
                ('sent_date', models.DateTimeField(default=datetime.datetime.now)),
                ('attachment', models.FileField(blank=True, null=True, upload_to='uploads/')),
                ('message_type', models.CharField(choices=[('system', 'System message'), ('user', 'User message')], default='user', max_length=10)),
            ],
        ),
        migrations.CreateModel(
            name='MessageTag',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('placeholder_position', models.PositiveIntegerField()),
                ('message', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='tags', to='messaging.Message')),
                ('tagged_user', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.CreateModel(
            name='Thread',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=50)),
                ('slug', models.CharField(max_length=60, unique=True)),
                ('type', models.CharField(choices=[('discussion', 'Private discussion'), ('public_channel', 'Public Channel'), ('private_channel', 'Private Channel')], max_length=50)),
                ('description', models.CharField(default='', max_length=150)),
                ('active', models.BooleanField(default=True)),
                ('creator_user', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, related_name='created_threads', to=settings.AUTH_USER_MODEL)),
                ('manager_user', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, related_name='managed_threads', to=settings.AUTH_USER_MODEL)),
                ('members', models.ManyToManyField(through='messaging.Membership', to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.AddField(
            model_name='message',
            name='thread',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='messages', to='messaging.Thread'),
        ),
        migrations.AddField(
            model_name='message',
            name='user_sender',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='membership',
            name='last_seen_message',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='messaging.Message'),
        ),
        migrations.AddField(
            model_name='membership',
            name='thread',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='memberships', to='messaging.Thread'),
        ),
        migrations.AddField(
            model_name='membership',
            name='user',
            field=models.ForeignKey(default=1, on_delete=django.db.models.deletion.CASCADE, related_name='memberships', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AlterUniqueTogether(
            name='messagetag',
            unique_together=set([('tagged_user', 'message', 'placeholder_position')]),
        ),
        migrations.AlterUniqueTogether(
            name='membership',
            unique_together=set([('user', 'thread')]),
        ),
    ]
