# UserManager
User management console app using web api calls to work with db. (non-commercial, made as tech approaches usage example)

Консольное приложение позволяет запрашивать и изменять данные о пользователях, используя 
запросы к веб апи. 

Для всех запросов, изменяющих базу данных программа запрашивает авторизацию.

При запросе информации о пользователе (user info) информация берётся из списка объектов в 
памяти. Этот список актуализируется запросом к базе данных раз в 10 минут.
       
*** Список доступных команд и их описание:  ***

create user - добавить пользователя в базу данных
remove user - удалить пользователя из базы данных
set status - изменить статус пользователя и сохранить изменение в базе данных
user info - вывести информацию о пользователе
sign in - авторизоваться
sign out - разлогиниться
exit - завершить работу приложения
help - вызвать справку"
