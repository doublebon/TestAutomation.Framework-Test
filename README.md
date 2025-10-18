# TestAutomation Framework - Playwright .NET

Масштабируемый фреймворк для UI автоматизации на Playwright .NET с поддержкой fluent chaining и навигации между страницами.

## Структура проекта

```
TestAutomation.sln
├── src/
│   └── TestAutomation.Framework/           # Основной фреймворк
│       ├── Core/                            # Ядро (Fixtures, Configuration)
│       ├── Extensions/                      # Extension methods для fluent API
│       ├── PageObjects/                     # Page Object Model
│       │   ├── Base/                        # Базовые классы
│       │   ├── Pages/                       # Страницы приложения
│       │   └── Components/                  # Переиспользуемые компоненты
│       └── TestAutomation.Framework.csproj
├── tests/
│   └── TestAutomation.Tests/               # Тестовые сценарии
│       ├── Tests/                           # Тесты
│       ├── BaseTest.cs                      # Базовый класс для тестов
│       ├── appsettings.json                 # Конфигурация
│       └── TestAutomation.Tests.csproj
└── README.md
```

## Быстрый старт

### 1. Установка зависимостей

```bash
# Восстановить NuGet пакеты
dotnet restore

# Установить браузеры Playwright (важно!)
pwsh tests/TestAutomation.Tests/bin/Debug/net8.0/playwright.ps1 install
```

Или на Linux/macOS:
```bash
dotnet restore
playwright install
```

### 2. Сборка проекта

```bash
dotnet build
```

### 3. Запуск тестов

```bash
# Запустить все тесты
dotnet test

# Запустить только Smoke тесты
dotnet test --filter Category=Smoke

# Запустить с подробным выводом
dotnet test --verbosity detailed

# Запустить в headless режиме (через env variable)
TEST_ENV=CI dotnet test
```

### 4. Настройка конфигурации

Отредактируйте `tests/TestAutomation.Tests/appsettings.json`:

```json
{
  "TestSettings": {
    "BaseUrl": "https://your-app.com",
    "Browser": "chromium",      // chromium, firefox, webkit
    "Headless": false,           // true для CI/CD
    "SlowMo": 100,              // Замедление для отладки (мс)
    "DefaultTimeout": 30000,     // Timeout по умолчанию (мс)
    "ViewportWidth": 1920,
    "ViewportHeight": 1080,
    "RecordVideo": false        // Запись видео тестов
  }
}
```

## Ключевые возможности

### ✅ Fluent API с навигацией между страницами

```csharp
var userManagementPage = await new LoginPage(Page, Context)
    .LoginAsync("admin", "admin123")                    // DashboardPage
    .Then(page => page.GoToUserManagementAsync())       // UserManagementPage
    .Then(page => page.ClickAddUserAsync())             // CreateUserPage
    .Then(page => page.EnterFirstNameAsync("John")
        .Then(p => p.EnterLastNameAsync("Doe"))
        .Then(p => p.SaveAsync()));                     // UserManagementPage
```

### ✅ Thread-safe параллельное выполнение

```csharp
[Test]
[Parallelizable(ParallelScope.All)]
public async Task ParallelTest_1() { ... }

[Test]
[Parallelizable(ParallelScope.All)]
public async Task ParallelTest_2() { ... }
```

### ✅ Auto-waiting и Web-First Assertions

```csharp
// Playwright автоматически ждет элементы
await Page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

// Assertions с auto-retry
await Expect(Page).ToHaveURLAsync(new Regex(".*dashboard.*"));
await Expect(element).ToBeVisibleAsync();
```

### ✅ Browser Context Isolation

Каждый тест получает изолированный BrowserContext (инкогнито режим).

### ✅ Автоматические screenshots и traces при падении

Артефакты сохраняются в `screenshots/` и `traces/`.

## Примеры тестов

Смотрите файлы в `tests/TestAutomation.Tests/Tests/`:
- `LoginTests.cs` - Тесты авторизации
- `NavigationTests.cs` - Примеры навигации между страницами
- `UserManagementTests.cs` - Комплексные сценарии

## CI/CD

Пример GitHub Actions workflow:

```yaml
- name: Install Playwright Browsers
  run: pwsh tests/TestAutomation.Tests/bin/Debug/net8.0/playwright.ps1 install

- name: Run tests
  run: dotnet test --verbosity normal
  env:
    TEST_ENV: CI
```

## Debug

### Просмотр traces:

```bash
playwright show-trace traces/test-name.zip
```

### Headed режим для отладки:

В `appsettings.json` установите:
```json
"Headless": false,
"SlowMo": 500
```

## Архитектурные паттерны

- **Page Object Model** - инкапсуляция страниц
- **Factory Pattern** - создание Page Objects
- **Fluent Interface** - method chaining
- **Extension Methods** - async chaining через Task<T>.Then()
- **Dependency Injection** - управление конфигурацией

## Требования

- .NET 8.0 SDK
- PowerShell (для установки браузеров на Windows)

## Автор

Senior AQA Engineer специализирующийся на C# и .NET экосистеме.
