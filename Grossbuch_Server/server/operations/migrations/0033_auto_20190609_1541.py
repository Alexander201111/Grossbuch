# Generated by Django 2.1.7 on 2019-06-09 12:41

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('operations', '0032_auto_20190523_2028'),
    ]

    operations = [
        migrations.AlterField(
            model_name='account',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 6, 9, 15, 41, 53, 742760)),
        ),
        migrations.AlterField(
            model_name='category',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 6, 9, 15, 41, 53, 744741)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='date',
            field=models.DateTimeField(default=datetime.datetime(2019, 6, 9, 15, 41, 53, 746726)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 6, 9, 15, 41, 53, 747219)),
        ),
    ]
