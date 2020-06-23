from django.conf.urls import url, include
from rest_framework import routers
from operations import views
from rest_framework_jwt.views import obtain_jwt_token, refresh_jwt_token

router = routers.DefaultRouter()
router.register(r'accounts', views.AccountViewSet)
router.register(r'categories', views.CategoryViewSet)
router.register(r'currencies', views.CurrencyViewSet)
router.register(r'purposes', views.PurposeViewSet)
router.register(r'operations', views.OperationViewSet)

urlpatterns = [
    url(r'^', include(router.urls)),
    url(r'^api-auth/', include('rest_framework.urls', namespace='rest_framework')),
    url(r'api-token-auth/', obtain_jwt_token),
    url(r'api-token-refresh/', refresh_jwt_token),
]
