# MarchSeven

Библиотека-обертка для работы с HoyoLab API для получения данных об играх Genshin Impact, Honkai Star Rail и Zenless Zone Zero.

## Описание

MarchSeven - это .NET библиотека, которая предоставляет удобный интерфейс для работы с официальными API HoyoLab. Библиотека поддерживает три основные игры:

- **Genshin Impact** - получение статистики персонажей, ежедневных заметок, экспедиций
- **Honkai Star Rail** - статистика аватаров, ежедневные заметки, экспедиции
- **Zenless Zone Zero** - статистика и данные игры

## Основные возможности

### 🎮 Мульти-игровая поддержка

- Единый клиент для всех поддерживаемых игр
- Автоматическое управление HTTP-соединениями
- Поддержка различных регионов и серверов

### 🔐 Аутентификация

- Поддержка различных типов cookies (Cookie, CookieV2, RawCookie)
- Автоматическое управление сессиями
- Безопасная работа с токенами

### 📊 Получение данных

- Статистика пользователя и аккаунта
- Игровые роли и персонажи
- Ежедневные заметки и награды
- Информация об экспедициях и трансформерах

## Установка

```bash
dotnet add package MarchSeven
```

## Быстрый старт

### Создание клиента

```csharp
using MarchSeven;
using MarchSeven.Models.Core.Cookie;

// Создаем cookie из строки
var cookie = Cookie.Create("your_cookie_string_here");

// Создаем основной клиент
var client = MarchSevenClient.Create(cookie);
```

### Получение базовой информации

```csharp
// Получаем статистику пользователя
var userStats = await client.FetchUserStats();

// Получаем информацию об аккаунте
var accountInfo = await client.GetUserAccountInfoByLToken();

// Получаем игровые роли
var gameRoles = await client.GetGameRoles();
```

## Примеры использования

### Genshin Impact

```csharp
// Работа с Genshin Impact
var genshinClient = client.Genshin;

// Получаем статистику персонажей
var stats = await genshinClient.FetchGenshinStatsAsync(uid);

// Получаем ежедневную заметку
var dailyNote = await genshinClient.FetchDailyNoteAsync(uid);

// Получаем информацию об экспедициях
var expeditions = dailyNote.Data.Expeditions;

// Проверяем трансформер
var transformer = dailyNote.Data.Transformer;
```

### Honkai Star Rail

```csharp
// Работа с Honkai Star Rail
var starRailClient = client.StarRail;

// Получаем статистику аватаров
var stats = await starRailClient.FetchStarRailStatsAsync(uid);

// Получаем ежедневную заметку
var dailyNote = await starRailClient.FetchDailyNoteAsync(uid);

// Получаем информацию об экспедициях
var expeditions = dailyNote.Data.Expeditions;
```

### Zenless Zone Zero

```csharp
// Работа с Zenless Zone Zero
var zenlessClient = client.Zenless;

// Получаем статистику
var stats = await zenlessClient.FetchZenlessStatsAsync(uid);
```

## Структура проекта

```
MarchSeven/
├── Models/
│   ├── Abstractions/          # Базовые интерфейсы
│   ├── Core/                  # Основные модели и утилиты
│   ├── GenshinImpact/         # Модели для Genshin Impact
│   ├── HonkaiStarRail/        # Модели для Honkai Star Rail
│   ├── HoYoLab/               # Общие модели HoyoLab
│   └── ZenlessZoneZero/       # Модели для Zenless Zone Zero
├── Util/                      # Утилиты и вспомогательные классы
└── MarchSevenClient.cs        # Основной клиент
```

## Обработка ошибок

Библиотека предоставляет специализированные исключения для различных типов ошибок:

```csharp
try
{
    var stats = await client.FetchUserStats();
}
catch (AccountNotFoundException)
{
    Console.WriteLine("Аккаунт не найден");
}
catch (LoginExpiredException)
{
    Console.WriteLine("Сессия истекла, требуется повторная авторизация");
}
catch (HoyoLabApiBadRequestException ex)
{
    Console.WriteLine($"Ошибка API: {ex.Message}");
}
catch (DailyRewardAlreadyReceivedException)
{
    Console.WriteLine("Ежедневная награда уже получена");
}
```

## Требования

- .NET 9.0 или выше
- Подключение к интернету для работы с HoyoLab API
- Валидные cookies от HoyoLab

## Лицензия

MIT License - см. файл [LICENSE.txt](LICENSE.txt)

## Поддержка

Если у вас есть вопросы или предложения, создайте Issue в репозитории проекта.

## Вклад в проект

Мы приветствуем вклад в развитие проекта! Пожалуйста, создайте Pull Request или обсудите изменения в Issues.
