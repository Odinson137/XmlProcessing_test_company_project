Файл базы данных выглядит следующим образом:

![image](https://github.com/Odinson137/XmlProcessing_test_company_project/assets/87028237/75b80819-226a-42ca-ba8e-1957fee860fe)

(Это насколько я понял задание, не судите строго)

В проекте используется всё, что было сказано в листе с заданием, включая логирование, многопоточность, пример SQLite, все сервисы, настройка проекта через конфигурационные файлы, чтоб не пришлось в код вносить изменения

Для запуска проекта необходимдо сделать следующие шаги:
- Установить Docker
- Запусить Docker
- Скачать данный репозиторий
- В консоле ввести команду:
  ```
  docker-compose up
  ```
- Дождаться, пока сервис RabbitMq запуститься
- Включить FileParser сервис
- Включить DataProcessor
- Любоваться работой и выводом результатов в консоли

