from django.shortcuts import render
from django.contrib.auth.models import User, Group
from rest_framework import viewsets, permissions
from .serializers import UserSerializer, OperationSerializer, AccountSerializer, CategorySerializer, CurrencySerializer, PurposeSerializer
from .models import Operation, Account, Category, Currency

class UserViewSet(viewsets.ModelViewSet):
    queryset = User.objects.all().order_by('-date_joined')
    serializer_class = UserSerializer
    permission_classes = (permissions.AllowAny, )

class AccountViewSet(viewsets.ModelViewSet):
    queryset = Account.objects.filter(isAccount=True)
    serializer_class = AccountSerializer
    permission_classes = (permissions.IsAuthenticatedOrReadOnly, )

class CategoryViewSet(viewsets.ModelViewSet):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer

class CurrencyViewSet(viewsets.ModelViewSet):
    queryset = Currency.objects.all()
    serializer_class = CurrencySerializer

class PurposeViewSet(viewsets.ModelViewSet):
    queryset = Account.objects.filter(isAccount=False)
    serializer_class = PurposeSerializer

class OperationViewSet(viewsets.ModelViewSet):
    queryset = Operation.objects.all().order_by('-date')
    serializer_class = OperationSerializer