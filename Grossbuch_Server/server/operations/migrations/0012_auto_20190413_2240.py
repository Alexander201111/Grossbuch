# Generated by Django 2.1.7 on 2019-04-13 19:40

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('operations', '0011_auto_20190413_2212'),
    ]

    operations = [
        migrations.AlterField(
            model_name='operation',
            name='date',
            field=models.DateTimeField(auto_now_add=True, null=datetime.datetime(2019, 4, 13, 22, 40, 24, 492917)),
        ),
    ]
