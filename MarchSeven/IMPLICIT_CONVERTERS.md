# 🔄 Implicit Converters для BaseGameUser

Этот документ описывает implicit convert'оры, доступные для классов, наследующихся от `BaseGameUser`.

## 📋 Поддерживаемые классы

- `GenshinUser` - пользователь Genshin Impact
- `StarRailUser` - пользователь Honkai Star Rail  
- `ZenlessUser` - пользователь Zenless Zone Zero

## 🔄 Доступные преобразования

### 1. Int ↔ User

```csharp
// Автоматическое создание пользователя из UID
GenshinUser user = 123456789;
StarRailUser starRailUser = 800000000;
ZenlessUser zenlessUser = 900000000;

// Автоматическое извлечение UID из пользователя
int uid = user; // 123456789
int starRailUid = starRailUser; // 800000000
int zenlessUid = zenlessUser; // 900000000
```

### 2. String ↔ User

```csharp
// Автоматическое создание пользователя из строки UID
GenshinUser user = "123456789";
StarRailUser starRailUser = "800000000";

// Автоматическое преобразование пользователя в строку UID
string uidString = user; // "123456789"
string starRailUidString = starRailUser; // "800000000"
```

## 💡 Примеры использования

### В методах

```csharp
public void ProcessUser(GenshinUser user)
{
    // Метод принимает GenshinUser
}

// Можно передавать int напрямую
ProcessUser(123456789);

// Или string
ProcessUser("123456789");
```

### В коллекциях

```csharp
// Создание списка пользователей
var users = new List<GenshinUser> { 123456789, 234567890, 345678901 };

// Извлечение UIDs
var uids = users.Select(u => (int)u).ToList();
```

### В сравнениях

```csharp
var user1 = new GenshinUser(123456789);
var user2 = new GenshinUser(123456789);

// Сравнение UIDs
if ((int)user1 == (int)user2)
{
    Console.WriteLine("Same user");
}
```

## 🎯 Практические примеры

### Создание клиентов

```csharp
var cookie = TestData.CreateTestCookie();
var client = MarchSevenClient.Create(cookie);

// Использование реального UID пользователя
var realUser = TestData.RealStarRailUid; // 700000000
var dailyNote = await client.StarRail.FetchDailyNoteAsync(realUser);
```

### Работа с несколькими пользователями

```csharp
var users = new List<StarRailUser> 
{ 
    TestData.RealStarRailUid,  // 700000000 (Europe)
    800000000,                  // 800000000 (Asia)
    600000000                   // 600000000 (America)
};

foreach (var user in users)
{
    Console.WriteLine($"User {user.Uid} on server {user.Server}");
}
```

## ⚠️ Важные замечания

1. **Безопасность**: Implicit convert'оры выполняют автоматическое преобразование, что может скрыть потенциальные ошибки
2. **Производительность**: Создание объектов происходит при каждом преобразовании
3. **Валидация**: UID не проверяется на корректность при преобразовании

## 🔧 Настройка

Implicit convert'оры автоматически доступны для всех классов, наследующихся от `BaseGameUser`. Для добавления новых convert'оров:

```csharp
public class CustomUser : BaseGameUser
{
    public CustomUser(int uid) : base(uid) { }

    // Добавьте свои implicit convert'оры
    public static implicit operator CustomUser(int uid) => new(uid);
    public static implicit operator int(CustomUser user) => user.Uid;
}
```

## 📚 Связанные файлы

- `BaseGameUser.cs` - базовый класс
- `GenshinUser.cs` - реализация для Genshin Impact
- `StarRailUser.cs` - реализация для Honkai Star Rail
- `ZenlessUser.cs` - реализация для Zenless Zone Zero
- `RealUserTests.cs` - тесты с реальными данными
- `SimpleTests.cs` - тесты implicit convert'оров
