# Скрипт для создания релиза и публикации NuGet пакета
# Использование: .\create-release.ps1 [version]

param(
  [Parameter(Mandatory = $false)]
  [string]$Version
)

# Получаем текущую версию из проекта, если не указана
if (-not $Version) {
  $csprojPath = "MarchSeven/MarchSeven.csproj"
  if (Test-Path $csprojPath) {
    $content = Get-Content $csprojPath -Raw
    if ($content -match '<AssemblyVersion>([^<]+)</AssemblyVersion>') {
      $Version = $matches[1]
      Write-Host "Найдена версия в проекте: $Version" -ForegroundColor Green
    }
  }
    
  if (-not $Version) {
    Write-Host "Не удалось определить версию. Укажите версию вручную." -ForegroundColor Red
    Write-Host "Пример: .\create-release.ps1 1.3.1" -ForegroundColor Yellow
    exit 1
  }
}

# Проверяем формат версии
if ($Version -notmatch '^\d+\.\d+\.\d+$') {
  Write-Host "Неверный формат версии. Используйте формат: X.Y.Z" -ForegroundColor Red
  Write-Host "Пример: 1.3.1" -ForegroundColor Yellow
  exit 1
}

Write-Host "Создание релиза версии: $Version" -ForegroundColor Cyan

# Проверяем статус git
$gitStatus = git status --porcelain
if ($gitStatus) {
  Write-Host "Внимание: Есть несохраненные изменения в репозитории!" -ForegroundColor Yellow
  Write-Host "Рекомендуется сначала закоммитить изменения." -ForegroundColor Yellow
  $continue = Read-Host "Продолжить? (y/N)"
  if ($continue -ne 'y' -and $continue -ne 'Y') {
    Write-Host "Операция отменена." -ForegroundColor Red
    exit 0
  }
}

# Проверяем, что мы на ветке main
$currentBranch = git branch --show-current
if ($currentBranch -ne "main") {
  Write-Host "Внимание: Вы не на ветке main (текущая ветка: $currentBranch)" -ForegroundColor Yellow
  $continue = Read-Host "Продолжить? (y/N)"
  if ($continue -ne 'y' -and $continue -ne 'Y') {
    Write-Host "Операция отменена." -ForegroundColor Red
    exit 0
  }
}

# Создаем тег
$tagName = "v$Version"
Write-Host "Создание тега: $tagName" -ForegroundColor Green

try {
  # Создаем аннотированный тег
  git tag -a $tagName -m "Release $Version"
  Write-Host "Тег создан локально" -ForegroundColor Green
    
  # Пушим тег
  Write-Host "Отправка тега на GitHub..." -ForegroundColor Green
  git push origin $tagName
    
  Write-Host "✅ Тег $tagName успешно создан и отправлен!" -ForegroundColor Green
  Write-Host "🚀 GitHub Actions автоматически запустит workflow для публикации NuGet пакета" -ForegroundColor Cyan
  Write-Host "📦 Пакет будет опубликован после успешного выполнения всех тестов" -ForegroundColor Cyan
    
  # Открываем GitHub Actions в браузере
  $repoUrl = git remote get-url origin
  if ($repoUrl -match 'github\.com[:/]([^/]+/[^/]+)') {
    $repoPath = $matches[1]
    $actionsUrl = "https://github.com/$repoPath/actions"
    Write-Host "🔗 GitHub Actions: $actionsUrl" -ForegroundColor Blue
        
    $openBrowser = Read-Host "Открыть GitHub Actions в браузере? (Y/n)"
    if ($openBrowser -ne 'n' -and $openBrowser -ne 'N') {
      Start-Process $actionsUrl
    }
  }
    
}
catch {
  Write-Host "❌ Ошибка при создании тега: $_" -ForegroundColor Red
  exit 1
}

Write-Host "`n📋 Следующие шаги:" -ForegroundColor Cyan
Write-Host "1. Проверьте статус workflow в GitHub Actions" -ForegroundColor White
Write-Host "2. Убедитесь, что все тесты прошли успешно" -ForegroundColor White
Write-Host "3. NuGet пакет будет опубликован автоматически" -ForegroundColor White
Write-Host "4. GitHub Release будет создан автоматически" -ForegroundColor White
