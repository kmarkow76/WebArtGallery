Учитывая текущее время (09:41 AM EEST, 17 июня 2025 года) и весь проделанный нами путь, давайте создадим файл README.md для вашего репозитория. Я проанализирую всё, что мы разработали: модели (Painter, Rental, Counterparty, PaintingMovement), репозитории, сервисы, контроллеры, а также настройки Program.cs и общую архитектуру проекта. Этот README будет содержать обзор проекта, инструкции по установке, использование, а также информацию для разработчиков.
Art Gallery API
Добро пожаловать в репозиторий Art Gallery API — это RESTful API, разработанное для управления галереей искусства. Проект реализует CRUD-операции и дополнительные функциональные возможности для работы с художниками, картинами, арендой, контрагентами и движением картин. API построено с использованием .NET 9.0, Entity Framework Core и PostgreSQL.
О проекте
API предоставляет функционал для:
Управления данными о художниках (Painters).
Управления арендой картин (Rentals) с отслеживанием их статуса в таблице PaintingMovements.
Управления контрагентами (Counterparties) с анализом доходов и стоимости рент.
Поддержки дополнительных сущностей, таких как Paintings, MoneyExpenses, MoneyIncomes и другие.
Проект использует архитектуру с разделением на слои: модели данных, репозитории, сервисы и контроллеры. Swagger интегрирован для удобного тестирования API.
Установленные зависимости
.NET 9.0: Основная платформа.
Entity Framework Core: ORM для работы с базой данных.
Npgsql: Провайдер для подключения к PostgreSQL.
Swashbuckle.AspNetCore: Для генерации Swagger-документации.
Структура проекта
ArtGallery.Data.Models: Содержит сущности базы данных (например, Painter, Rental, Counterparty, PaintingMovement).
ArtGallery.Interfaces.Repositories: Определяет интерфейсы репозиториев.
ArtGallery.Repositories.Repositories: Реализации репозиториев для доступа к данным.
ArtGallery.Interfaces.ServicesInterfaces: Интерфейсы сервисов бизнес-логики.
ArtGallery.Services.Services: Реализации сервисов.
ArtGallery.Controllers: Контроллеры API.
ArtGallery.DTO: DTO (Data Transfer Objects) для передачи данных между слоями.
Program.cs: Настройка приложения и DI-контейнера.
Установка и запуск
Предварительные требования
Установлен .NET 9.0 SDK.
Установлен PostgreSQL.
Git (для клонирования репозитория).
Шаги установки
Клонируйте репозиторий:
bash
git clone https://github.com/ваше_имя/ArtGallery.git
cd ArtGallery
Настройте строку подключения:
Откройте appsettings.json или appsettings.Development.json.
Укажите строку подключения к вашей базе данных:
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ArtGallery;Username=your_user;Password=your_password"
  }
}
Восстановите зависимости:
bash
dotnet restore
Создайте и примените миграции:
Убедитесь, что база данных существует или будет создана.
Выполните миграции:
bash
dotnet ef migrations add InitialCreate --project ArtGallery.Core
dotnet ef database update --project ArtGallery.Core
Запустите приложение:
bash
dotnet run --project ArtGallery.Core
Доступ к API:
Откройте браузер и перейдите по адресу https://localhost:<порт>/swagger (порт указан в выводе консоли).
Используйте Swagger UI для тестирования эндпоинтов.
Использование
Основные эндпоинты
Художники (/api/painters):
POST /api/painters: Создание художника.
GET /api/painters/{id}: Получение данных художника.
GET /api/painters/by-full-name: Поиск по имени и фамилии.
PUT /api/painters/{id}: Обновление.
DELETE /api/painters/{id}: Удаление.
Аренда (/api/rentals):
POST /api/rentals: Создание аренды (с записью в PaintingMovements как "Rented").
GET /api/rentals/{id}: Получение аренды.
GET /api/rentals/by-painting-title: Поиск аренд по названию картины.
DELETE /api/rentals/{id}: Удаление аренды (с записью в PaintingMovements как "Available").
Контрагенты (/api/counterparties):
POST /api/counterparties: Создание контрагента.
GET /api/counterparties/{id}: Получение данных контрагента.
GET /api/counterparties/with-total-income-above: Контрагенты с доходом выше порога.
GET /api/counterparties/with-total-rental-cost-above: Контрагенты с общей стоимостью рент выше порога.
PUT /api/counterparties/{id}: Обновление.
DELETE /api/counterparties/{id}: Удаление.
Примеры запросов
Создание аренды:
json
POST /api/rentals
{
  "counterpartyId": 1,
  "paintingId": 1,
  "startDate": "2025-06-17T12:00:00Z",
  "endDate": "2025-06-21T12:00:00Z",
  "price": 500.00
}
Получение контрагентов с доходом выше 1000:
GET /api/counterparties/with-total-income-above?threshold=1000
Функциональные особенности
Атомарность операций: Использование транзакций для создания и удаления аренд с обновлением PaintingMovements.
История движений: Каждая аренда и её завершение фиксируются в PaintingMovements с типами "Rented" и "Available".
Анализ данных: Необычные методы для контрагентов позволяют анализировать доходы и стоимость рент.
Разработка и вклад
Требования: .NET 9.0, PostgreSQL.
Сборка: Используйте dotnet build для проверки кода.
Тестирование: Тестируйте эндпоинты через Swagger или инструменты вроде Postman.
Вклад: Пишите код в соответствии с существующей структурой, добавляйте миграции для новых сущностей.
Лицензия
[Укажите лицензию, например MIT или другая] — по умолчанию проект не имеет лицензии, добавьте её в корень репозитория.
Контакты
Автор: [Ваше имя или никнейм]
Email: [Ваш email]
Примечания
Модели: Мы разработали Painter, Rental, Counterparty, PaintingMovement с соответствующими связями.
Сервисы и контроллеры: Реализованы CRUD и кастомные методы (например, поиск аренд по названию картины, анализ доходов контрагентов).
Ошибки: Учтены случаи вроде отсутствия миграций или некорректных данных (например, проверка PaintingId).
Расширения: Вы можете добавить новые сущности (MoneyExpense, MoneyIncome) или методы, следуя той же архитектуре.
Если у вас есть дополнительные пожелания (например, добавить секцию с примерами кода или инструкции по деплою), дайте знать, и я обновлю README!
