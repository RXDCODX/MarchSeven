#!/bin/bash

# Скрипт для создания релиза и публикации NuGet пакета
# Использование: ./create-release.sh [version]

set -e

VERSION="$1"

# Получаем текущую версию из проекта, если не указана
if [ -z "$VERSION" ]; then
    CSPROJ_PATH="MarchSeven/MarchSeven.csproj"
    if [ -f "$CSPROJ_PATH" ]; then
        VERSION=$(grep -oP '<AssemblyVersion>\K[^<]+' "$CSPROJ_PATH" || echo "")
        if [ -n "$VERSION" ]; then
            echo "✅ Найдена версия в проекте: $VERSION"
        fi
    fi
    
    if [ -z "$VERSION" ]; then
        echo "❌ Не удалось определить версию. Укажите версию вручную."
        echo "Пример: ./create-release.sh 1.3.1"
        exit 1
    fi
fi

# Проверяем формат версии
if [[ ! "$VERSION" =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    echo "❌ Неверный формат версии. Используйте формат: X.Y.Z"
    echo "Пример: 1.3.1"
    exit 1
fi

echo "🚀 Создание релиза версии: $VERSION"

# Проверяем статус git
if [ -n "$(git status --porcelain)" ]; then
    echo "⚠️  Внимание: Есть несохраненные изменения в репозитории!"
    echo "Рекомендуется сначала закоммитить изменения."
    read -p "Продолжить? (y/N): " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        echo "Операция отменена."
        exit 0
    fi
fi

# Проверяем, что мы на ветке main
CURRENT_BRANCH=$(git branch --show-current)
if [ "$CURRENT_BRANCH" != "main" ]; then
    echo "⚠️  Внимание: Вы не на ветке main (текущая ветка: $CURRENT_BRANCH)"
    read -p "Продолжить? (y/N): " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        echo "Операция отменена."
        exit 0
    fi
fi

# Создаем тег
TAG_NAME="v$VERSION"
echo "🏷️  Создание тега: $TAG_NAME"

# Создаем аннотированный тег
git tag -a "$TAG_NAME" -m "Release $VERSION"
echo "✅ Тег создан локально"

# Пушим тег
echo "📤 Отправка тега на GitHub..."
git push origin "$TAG_NAME"

echo "✅ Тег $TAG_NAME успешно создан и отправлен!"
echo "🚀 GitHub Actions автоматически запустит workflow для публикации NuGet пакета"
echo "📦 Пакет будет опубликован после успешного выполнения всех тестов"

# Открываем GitHub Actions в браузере
REPO_URL=$(git remote get-url origin)
if [[ "$REPO_URL" =~ github\.com[:/]([^/]+/[^/]+) ]]; then
    REPO_PATH="${BASH_REMATCH[1]}"
    ACTIONS_URL="https://github.com/$REPO_PATH/actions"
    echo "🔗 GitHub Actions: $ACTIONS_URL"
    
    read -p "Открыть GitHub Actions в браузере? (Y/n): " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Nn]$ ]]; then
        if command -v xdg-open >/dev/null 2>&1; then
            xdg-open "$ACTIONS_URL"  # Linux
        elif command -v open >/dev/null 2>&1; then
            open "$ACTIONS_URL"       # macOS
        else
            echo "Не удалось открыть браузер автоматически. Откройте: $ACTIONS_URL"
        fi
    fi
fi

echo ""
echo "📋 Следующие шаги:"
echo "1. Проверьте статус workflow в GitHub Actions"
echo "2. Убедитесь, что все тесты прошли успешно"
echo "3. NuGet пакет будет опубликован автоматически"
echo "4. GitHub Release будет создан автоматически"
