from django.contrib.auth.models import User, Group
from rest_framework import serializers
from .models import Operation, Account, Category, Currency
import datetime

class UserSerializer(serializers.ModelSerializer):
    url = serializers.HyperlinkedIdentityField(view_name="operations:user-detail")

    class Meta:
        model = User
        fields = ('id', 'username', 'first_name', 'last_name')

class AccountSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Account
        fields = ('id', 'id2', 'title', 'balance', 'plannedSum', 'totalSum', 'isAccount', 'updateDate', 'user')
    def create(self, validated_data):
        return Account.objects.create(**validated_data)
    def update(self, instance, validated_data):
        instance.title = validated_data.get('title', instance.title)
        instance.balance = validated_data.get('balance', instance.balance)
        instance.totalSum = validated_data.get('totalSum', instance.totalSum)
        instance.updateDate = datetime.datetime.now()
        instance.save()
        return instance
    def delete(self, validated_data):
        return Account.objects.delete(**validated_data)

class CategorySerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Category
        fields = ('id', 'id2', 'title', 'totalSum', 'updateDate', 'user')
    def create(self, validated_data):
        return Category.objects.create(**validated_data)
    def update(self, instance, validated_data):
        instance.title = validated_data.get('title', instance.title)
        instance.totalSum = validated_data.get('totalSum', instance.totalSum)
        instance.updateDate = datetime.datetime.now()
        instance.save()
        return instance
    def delete(self, validated_data):
        return Category.objects.delete(**validated_data)

class CurrencySerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Currency
        fields = ('id', 'key', 'title', 'coefficient', 'totalSum')
    def create(self, validated_data):
        return Currency.objects.create(**validated_data)
    def update(self, instance, validated_data):
        instance.title = validated_data.get('title', instance.title)
        instance.coefficient = validated_data.get('coefficient', instance.coefficient)
        instance.save()
        return instance
    def delete(self, validated_data):
        return Currency.objects.delete(**validated_data)

class PurposeSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Account
        fields = ('id', 'id2', 'title', 'balance', 'plannedSum', 'totalSum', 'isAccount', 'updateDate', 'user')
    def create(self, validated_data):
        return Account.objects.create(**validated_data)
    def update(self, instance, validated_data):
        instance.title = validated_data.get('title', instance.title)
        instance.balance = validated_data.get('balance', instance.balance)
        instance.totalSum = validated_data.get('totalSum', instance.totalSum)
        instance.updateDate = datetime.datetime.now()
        instance.save()
        return instance
    def delete(self, validated_data):
        return Account.objects.delete(**validated_data)

class OperationSerializer(serializers.HyperlinkedModelSerializer):
    account = AccountSerializer()
    category = CategorySerializer()
    currency = CurrencySerializer()
    purpose = AccountSerializer()
    """ user = UserSerializer() """
    class Meta:
        model = Operation
        fields = ('id', 'id2', 'date', 'account', 'category', 'currency', 'purpose', 'user', 'type', 'summ', 'description', 'performed', 'updateDate')
    def create(self, validated_data):
        account_data = validated_data.pop('account', None)
        if account_data:
            account = Account.objects.get(**account_data)
            account.balance = account.balance - validated_data.get('summ', None)
            account.totalSum = account.totalSum + validated_data.get('summ', None)
            account.save()
            validated_data['account'] = account
        
        category_data = validated_data.pop('category', None)
        if category_data:
            category = Category.objects.get(**category_data)
            category.totalSum = category.totalSum + validated_data.get('summ', None)
            category.save()
            validated_data['category'] = category
        
        currency_data = validated_data.pop('currency', None)
        if currency_data:
            currency = Currency.objects.get(**currency_data)
            validated_data['currency'] = currency

        purpose_data = validated_data.pop('purpose', None)
        if purpose_data:
            purpose = Account.objects.get(**purpose_data)
            purpose.totalSum = purpose.totalSum + validated_data.get('summ', None)
            purpose.save()
            validated_data['purpose'] = purpose
        return Operation.objects.create(**validated_data)

    def update(self, instance, validated_data):
        #region Account
        account_data = validated_data.pop('account', None)
        if account_data:
            account = Account.objects.get(**account_data)
            validated_data['account'] = account
        instance.account = validated_data.get('account', instance.account)
        #endregion
        #region Category
        category_data = validated_data.pop('category', None)
        if category_data:
            category = Category.objects.get(**category_data)
            validated_data['category'] = category
        instance.category = validated_data.get('category', instance.category)
        #endregion
        #region Currency
        currency_data = validated_data.pop('currency', None)
        if currency_data:
            currency = Currency.objects.get(**currency_data)
            validated_data['currency'] = currency
        instance.currency = validated_data.get('currency', instance.currency)
        #endregion 
        #region Purpose
        purpose_data = validated_data.pop('purpose', None)
        if purpose_data:
            purpose = Purpose.objects.get(**purpose_data)
            validated_data['purpose'] = purpose
        instance.purpose = validated_data.get('purpose', instance.purpose)
        #endregion
        instance.date = validated_data.get('date', instance.date)
        instance.summ = validated_data.get('summ', instance.summ)
        instance.updateDate = datetime.datetime.now()
        instance.description = validated_data.get('description', instance.description)
        instance.save()
        return instance

    def delete(self, validated_data):
        account_data = validated_data.pop('account', None)
        if account_data:
            account = Account.objects.get(**account_data)[0]
            account.balance = account.balance + validated_data.get('summ', None)
            account.save()
            validated_data['account'] = account
        category_data = validated_data.pop('category', None)
        if category_data:
            category = Category.objects.get(**category_data)[0]
            category.totalSum = category.totalSum - validated_data.get('summ', None)
            category.save()
            validated_data['category'] = category
        currency_data = validated_data.pop('currency', None)
        if currency_data:
            currency = Currency.objects.get(**currency_data)[0]
            validated_data['currency'] = currency
        purpose_data = validated_data.pop('purpose', None)
        if purpose_data:
            purpose = Purpose.objects.get(**purpose_data)[0]
            purpose = Purpose.objects.get(**purpose_data)
            purpose.totalSum = purpose.totalSum - validated_data.get('summ', None)
            validated_data['purpose'] = purpose
        return Operation.objects.delete(**validated_data)