# Generated by Django 2.1.7 on 2019-05-16 14:35

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('operations', '0025_auto_20190516_1540'),
    ]

    operations = [
        migrations.AddField(
            model_name='account',
            name='totalSum',
            field=models.FloatField(blank=True, default=0),
        ),
        migrations.AddField(
            model_name='currency',
            name='totalSum',
            field=models.FloatField(blank=True, default=0),
        ),
        migrations.AlterField(
            model_name='account',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 16, 17, 35, 43, 843426)),
        ),
        migrations.AlterField(
            model_name='category',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 16, 17, 35, 43, 843426)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='date',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 16, 17, 35, 43, 844423)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 16, 17, 35, 43, 844423)),
        ),
    ]
