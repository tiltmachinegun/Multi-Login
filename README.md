# Multi-Login (Автоматизированное рабочее место для учёта и реализации радиоэлементов)
СУБД LocalDB, БД Mssqllocaldb(.mdf)
WPF+CS
Работа с БД реализована через прямые запросы в Async подключении к БД.
Реализовано: 
Авторизация
Регистрация
Управление складами- вывод содержимого склада, создание нового склада для пользователя
Списание товара со склада(полное удаление позиции)
Перемещение товара с одного доступного пользователю склада на другой
Добавление товара
NEXT UPD:
списание/добавление единиц товара, а не новой позиции
редактирование datatable и отгрузка данных обратно в Бд
права доступа уровня user/admin
личные кабинеты для пользователей и администраторов для редактирования собственных/чужих учётных записей
