# Generated by Django 2.1.7 on 2019-05-23 17:28

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('operations', '0031_auto_20190523_1602'),
    ]

    operations = [
        migrations.AlterField(
            model_name='account',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 23, 20, 28, 2, 319044)),
        ),
        migrations.AlterField(
            model_name='category',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 23, 20, 28, 2, 319540)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='date',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 23, 20, 28, 2, 320532)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 23, 20, 28, 2, 320532)),
        ),
    ]
