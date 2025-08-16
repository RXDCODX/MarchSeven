# 🕐 Конвертация Timestamp полей в DateTime

Этот документ описывает автоматическую конвертацию Unix timestamp полей в удобные для работы типы `DateTime` и `DateTimeOffset`.

## 📋 Поддерживаемые модели

### 1. StarRailDailyNoteData

#### StaminaFullTimestamp → DateTime

```csharp
[JsonPropertyName("stamina_full_ts")]
public long StaminaFullTimestamp { get; set; }

/// <summary>
/// Время, когда stamina будет полностью восстановлена
/// </summary>
[JsonIgnore]
public DateTime StaminaFullTime => DateTimeOffset.FromUnixTimeSeconds(StaminaFullTimestamp).DateTime;

/// <summary>
/// Время, когда stamina будет полностью восстановлена (с учетом часового пояса)
/// </summary>
[JsonIgnore]
public DateTimeOffset StaminaFullTimeOffset => DateTimeOffset.FromUnixTimeSeconds(StaminaFullTimestamp);
```

#### CurrentTimestamp → DateTime

```csharp
[JsonPropertyName("current_ts")]
public long CurrentTimestamp { get; set; }

/// <summary>
/// Текущее время сервера
/// </summary>
[JsonIgnore]
public DateTime CurrentTime => DateTimeOffset.FromUnixTimeSeconds(CurrentTimestamp).DateTime;

/// <summary>
/// Текущее время сервера (с учетом часового пояса)
/// </summary>
[JsonIgnore]
public DateTimeOffset CurrentTimeOffset => DateTimeOffset.FromUnixTimeSeconds(CurrentTimestamp);
```

### 2. StarRailExpedition

#### FinishTimestamp → DateTime

```csharp
[JsonPropertyName("finish_ts")]
public long FinishTimestamp { get; set; }

/// <summary>
/// Время завершения экспедиции
/// </summary>
[JsonIgnore]
public DateTime FinishTime => DateTimeOffset.FromUnixTimeSeconds(FinishTimestamp).DateTime;

/// <summary>
/// Время завершения экспедиции (с учетом часового пояса)
/// </summary>
[JsonIgnore]
public DateTimeOffset FinishTimeOffset => DateTimeOffset.FromUnixTimeSeconds(FinishTimestamp);

/// <summary>
/// Оставшееся время до завершения экспедиции
/// </summary>
[JsonIgnore]
public TimeSpan RemainingTimeSpan => TimeSpan.FromSeconds(RemainingTime);
```

## 💡 Примеры использования

### Получение времени восстановления stamina

```csharp
var dailyNote = await starRailClient.FetchDailyNoteAsync(user);

// Использование DateTime (локальное время)
var staminaFullTime = dailyNote.Data.StaminaFullTime;
Console.WriteLine($"Stamina будет полной в: {staminaFullTime:HH:mm:ss}");

// Использование DateTimeOffset (с часовым поясом)
var staminaFullTimeOffset = dailyNote.Data.StaminaFullTimeOffset;
Console.WriteLine($"Stamina будет полной в: {staminaFullTimeOffset:HH:mm:ss} UTC");

// Проверка, полная ли stamina
var isStaminaFull = DateTime.UtcNow >= dailyNote.Data.StaminaFullTime;
```

### Работа с экспедициями

```csharp
foreach (var expedition in dailyNote.Data.Expeditions)
{
    // Время завершения экспедиции
    var finishTime = expedition.FinishTime;
    Console.WriteLine($"Экспедиция '{expedition.Name}' завершится в: {finishTime:HH:mm:ss}");
    
    // Оставшееся время
    var remainingTime = expedition.RemainingTimeSpan;
    Console.WriteLine($"Осталось времени: {remainingTime.Hours}ч {remainingTime.Minutes}м");
    
    // Проверка статуса
    if (expedition.Status == "Ongoing")
    {
        var timeUntilFinish = expedition.FinishTime - DateTime.UtcNow;
        Console.WriteLine($"До завершения: {timeUntilFinish.Hours}ч {timeUntilFinish.Minutes}м");
    }
}
```

### Текущее время сервера

```csharp
var currentServerTime = dailyNote.Data.CurrentTime;
var currentServerTimeOffset = dailyNote.Data.CurrentTimeOffset;

Console.WriteLine($"Текущее время сервера: {currentServerTime:yyyy-MM-dd HH:mm:ss}");
Console.WriteLine($"Текущее время сервера (UTC): {currentServerTimeOffset:yyyy-MM-dd HH:mm:ss} UTC");

// Проверка синхронизации времени
var timeDifference = DateTime.UtcNow - currentServerTime;
Console.WriteLine($"Разница с локальным временем: {timeDifference.TotalSeconds:F0} секунд");
```

## ⚠️ Важные замечания

### 1. Точность времени

- Unix timestamp работает с точностью до **секунд**
- Миллисекунды и микросекунды **теряются** при конвертации
- Для точных временных расчетов используйте исходные поля

### 2. Часовые пояса

- `DateTime` свойства возвращают время в **локальном часовом поясе**
- `DateTimeOffset` свойства сохраняют **UTC время** и часовой пояс
- Рекомендуется использовать `DateTimeOffset` для работы с разными часовыми поясами

### 3. JsonIgnore

- Все новые свойства помечены атрибутом `[JsonIgnore]`
- Они **не сериализуются** в JSON
- При десериализации используются только исходные timestamp поля

## 🔧 Технические детали

### Конвертация Unix timestamp

```csharp
// Unix timestamp в секундах
long timestamp = 1692187200; // 2023-08-18 12:00:00 UTC

// В DateTime (локальное время)
DateTime localTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;

// В DateTimeOffset (UTC)
DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);
```

### Обратная конвертация

```csharp
// DateTime в Unix timestamp
DateTime dateTime = DateTime.UtcNow;
long timestamp = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();

// DateTimeOffset в Unix timestamp
DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow;
long timestamp = dateTimeOffset.ToUnixTimeSeconds();
```

## 🧪 Тестирование

Для тестирования конвертации времени используйте:

```csharp
[Fact]
public void TestDateTimeConversion()
{
    var currentTime = DateTimeOffset.UtcNow;
    var dailyNoteData = new StarRailDailyNoteData
    {
        CurrentTimestamp = currentTime.ToUnixTimeSeconds(),
        StaminaFullTimestamp = currentTime.AddHours(2).ToUnixTimeSeconds()
    };

    // Сравнение с точностью до секунд
    Assert.Equal(
        currentTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), 
        dailyNoteData.CurrentTime.ToString("yyyy-MM-dd HH:mm:ss")
    );
}
```

## 📚 Связанные файлы

- `StarRailDailyNoteData.cs` - модель ежедневных заметок
- `StarRailExpedition.cs` - модель экспедиций
- `SimpleTests.cs` - тесты конвертации времени
- `IMPLICIT_CONVERTERS.md` - документация по implicit convert'орам
