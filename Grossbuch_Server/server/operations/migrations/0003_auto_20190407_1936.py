# Generated by Django 2.1.7 on 2019-04-07 16:36

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('operations', '0002_auto_20190407_1354'),
    ]

    operations = [
        migrations.AddField(
            model_name='operation',
            name='created_at',
            field=models.DateTimeField(auto_now_add=True, null=datetime.datetime(2019, 4, 7, 19, 36, 42, 400404)),
            preserve_default=datetime.datetime(2019, 4, 7, 19, 36, 42, 400404),
        ),
        migrations.AddField(
            model_name='operation',
            name='updated_at',
            field=models.DateTimeField(auto_now=True, null=datetime.datetime(2019, 4, 7, 19, 36, 42, 400404)),
            preserve_default=datetime.datetime(2019, 4, 7, 19, 36, 42, 400404),
        ),
    ]
