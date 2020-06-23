# Generated by Django 2.1.7 on 2019-05-22 17:20

import datetime
from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('operations', '0026_auto_20190516_1735'),
    ]

    operations = [
        migrations.AddField(
            model_name='account',
            name='plannedSum',
            field=models.FloatField(blank=True, default=0),
        ),
        migrations.AddField(
            model_name='account',
            name='user',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
            preserve_default=-1,
        ),
        migrations.AddField(
            model_name='category',
            name='user',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
            preserve_default=-1,
        ),
        migrations.AddField(
            model_name='operation',
            name='id2',
            field=models.IntegerField(blank=True, default=0),
        ),
        migrations.AddField(
            model_name='operation',
            name='performed',
            field=models.BooleanField(default=True),
        ),
        migrations.AddField(
            model_name='operation',
            name='type',
            field=models.IntegerField(default=2),
        ),
        migrations.AddField(
            model_name='operation',
            name='user',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
            preserve_default=-1,
        ),
        migrations.AlterField(
            model_name='account',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 22, 20, 20, 58, 405351)),
        ),
        migrations.AlterField(
            model_name='category',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 22, 20, 20, 58, 406343)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='date',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 22, 20, 20, 58, 407334)),
        ),
        migrations.AlterField(
            model_name='operation',
            name='updateDate',
            field=models.DateTimeField(default=datetime.datetime(2019, 5, 22, 20, 20, 58, 407334)),
        ),
    ]
