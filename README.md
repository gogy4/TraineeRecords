Это приложение для записи стажеров. 

Насчет фронта я не стал сильно заморачиваться. Приложил также файл в dump.sql для таблиц mysql. В нем не содержится данных, а лишь структура таблиц.
dump.sql удалил, потому что вызывалась ошибка, из за неверного дампа при миграциях бд. теперь миграции создаются сами.
добавил docker обертку

нужно писать в консоль

docker-compose build

docker-compose up -d (-d для того, чтобы можно было пользоваться консолью)
