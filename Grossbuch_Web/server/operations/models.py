from django.db import models
from django.contrib.auth.models import User
import datetime

class Account(models.Model):
    id2 = models.IntegerField(blank=True, default=0)
    title = models.CharField(max_length=200, blank=True, default='')
    balance = models.FloatField(blank=True, default=0)
    plannedSum = models.FloatField(blank=True, default=0)
    totalSum = models.FloatField(blank=True, default=0)
    isAccount = models.BooleanField(blank=True, default=0)
    updateDate = models.DateTimeField(default=datetime.datetime.now())
    user = models.CharField(max_length=200, blank=True, default='')

    def get_absolute_url(self):
        return reverse('account-detail-view', args=[str(self.id)])

    def __str__(self):
        return self.title

class Category(models.Model):
    id2 = models.IntegerField(blank=True, default=0)
    title = models.CharField(max_length=200, blank=True, default='')
    totalSum = models.FloatField(null=0)
    updateDate = models.DateTimeField(default=datetime.datetime.now())
    user = models.CharField(max_length=200, blank=True, default='')

    def get_absolute_url(self):
        return reverse('category-detail-view', args=[str(self.id)])

    def __str__(self):
        return self.title

class Currency(models.Model):
    key = models.CharField(max_length=5, blank=True, default='')
    title = models.CharField(max_length=200, blank=True, default='')
    coefficient = models.FloatField(blank=True, default=0)
    totalSum = models.FloatField(blank=True, default=0)

    def get_absolute_url(self):
        return reverse('currency-detail-view', args=[str(self.id)])

    def __str__(self):
        return self.title

class Operation(models.Model):
    id2 = models.IntegerField(blank=True, default=0)
    date = models.DateTimeField(default=datetime.datetime.now())
    
    account = models.ForeignKey(Account, on_delete=models.CASCADE, null=-1)
    category = models.ForeignKey('Category', on_delete=models.CASCADE, null=-1)
    currency = models.ForeignKey('Currency', on_delete=models.CASCADE, null=-1)
    purpose = models.ForeignKey('Account', related_name='Operation', on_delete=models.CASCADE, blank=True, null=True)
    user = models.CharField(max_length=200, blank=True, default='')

    type = models.IntegerField(default=2)
    summ = models.FloatField()
    description = models.TextField(blank=True, default='')
    performed = models.BooleanField(default=True)

    updateDate = models.DateTimeField(default=datetime.datetime.now())

    def get_absolute_url(self):
        return reverse('operation-detail-view', args=[str(self.id)])

    def __str__(self):
        return self.title
