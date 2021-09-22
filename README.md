-------------------------------------------
-   Настройка окружения для сервера:	  -	
-------------------------------------------
py -m venv myvenv - создание окружения
myvenv\Scripts\activate - запуск окружения (Linux: source myvenv/bin/activate)
py -m pip install --upgrade pip - обновление pip
pip install django - установка Django
pip install djangorestframework - установка Django Rest Framework
django-admin startproject Parfumer - создание проекта
python manage.py startapp products - создание приложения

(pip install django-cors-headers
pip install djangorestframework-jwt)

py server/manage.py makemigrations - создать миграции
py server/manage.py migrate - провести миграции
py manage.py createsuperuser - создание суперпользователя
py server/manage.py runserver - запуск сервера

-------------------------------------------
-   Запуск сервера:		          -
-------------------------------------------
* myvenv\Scripts\activate		
* py server/manage.py runserver

---------------------------------------------------------
-   Настройка окружения и запуск клиента Angular:	-
---------------------------------------------------------
npm install -g @angular/cli
ng new client - создание проекта
(npm install --save @angular/material @angular/cdk @angular/animations
npm install --save angular/material2-builds angular/cdk-builds angular/animations-builds
npm install --save hammerjs
npm install --save hammerjs - Material Design
npm install --save ng2-charts - для диаграммы
npm install --save chart.js - для диаграммы 2
npm install --save-dev ng2-charts-schematics - для диаграммы 3
pip install django-cbrf - курс валют
pip install schedule - для таймера)

----------------------------------------
-       Screenshots	                   -
----------------------------------------
--------------------------------
-       Web	                   -
--------------------------------
![Image alt](https://github.com/Alexander201111/Grossbuch/raw/master/Results/Web_Screenshots/list_operations.png)

--------------------------------
-       Mobile	               -
--------------------------------
![Image alt](https://github.com/Alexander201111/Grossbuch/raw/branch/Results/Mobile_Screenshots/list_operations.png)
