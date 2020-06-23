# Generated by Django 2.1.7 on 2019-04-07 10:54

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('operations', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='Currency',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=200)),
                ('coefficient', models.FloatField()),
            ],
        ),
        migrations.CreateModel(
            name='Purpose',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=200)),
            ],
        ),
        migrations.RemoveField(
            model_name='operation',
            name='date',
        ),
        migrations.AddField(
            model_name='operation',
            name='user',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
            preserve_default=-1,
        ),
        migrations.AddField(
            model_name='operation',
            name='currency',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to='operations.Currency'),
            preserve_default=-1,
        ),
        migrations.AddField(
            model_name='operation',
            name='purpose',
            field=models.ForeignKey(null=-1, on_delete=django.db.models.deletion.CASCADE, to='operations.Purpose'),
            preserve_default=-1,
        ),
    ]
