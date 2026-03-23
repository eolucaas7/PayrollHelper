# 💰 PayrollHelper

<p align="center">
  <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet&logoColor=white" alt=".NET"></a>
  <a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white" alt="C#"></a>
  <a href="https://www.postgresql.org/"><img src="https://img.shields.io/badge/PostgreSQL-18-4169E1?logo=postgresql&logoColor=white" alt="PostgreSQL"></a>
  <a href="https://docs.microsoft.com/en-us/ef/core/"><img src="https://img.shields.io/badge/EF%20Core-6.0-512BD4?logo=dotnet&logoColor=white" alt="EF Core"></a>
  <a href="https://github.com/dotnet/winforms"><img src="https://img.shields.io/badge/WinForms-.NET%206.0-512BD4" alt="WinForms"></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/License-MIT-green.svg" alt="License"></a>
</p>

> Бухгалтерское приложение для управления сотрудниками, выплатами и налогами.

---

## 📋 Оглавление
- [🚀 Возможности](#features)
- [🛠️ Технологии и требования](#tech)
- [⚙️ Установка](#install)
- [🗄️ Настройка базы данных](#db-setup)
- [🏃 Запуск приложения](#run)
- [👥 Пользователи по умолчанию](#users)
- [📸 Скриншоты](#screenshots)
- [📄 Лицензия](#license)
- [⭐ Поддержка](#support)

---

<h2 id="features">🚀 Возможности</h2>

- 🔐 **Авторизация** — вход по логину/паролю с хэшированием SHA256
- 👥 **Управление сотрудниками** — добавление, просмотр, редактирование
- 💰 **Выплаты** — начисление зарплаты, премий, специальных сумм
- 📑 **Управление налогами** — добавление и настройка налогов
- 🧾 **Управление выплатами** — создание типов выплат с привязкой налогов
- 🪑 **Должности** — управление должностями и статусами (активна/неактивна)
- 🛠️ **Редактирование БД** — полный доступ к таблицам (только для админа)
- 📊 **Генерация отчётов** — выбор типа, периода, формата (.txt / .csv)
- ✅ **Валидация данных** — подсветка ошибок, защита от некорректного ввода

---

<h2 id="tech">🛠️ Технологии и требования</h2>

| Компонент | Технология / Требование | Версия |
|-----------|------------------------|--------|
| **Платформа** | .NET | 6.0 |
| **Язык** | C# | 12.0 |
| **Графический интерфейс** | WinForms | .NET 6.0 |
| **ORM** | Entity Framework Core | 6.0 |
| **База данных** | PostgreSQL | 18 |
| **Тестирование** | xUnit | 2.4.1 |
| **ОС** | Windows | 10 / 11 |

---

<h2 id="install">⚙️ Установка</h2>

### 1. Клонирование репозитория

```bash
git clone https://github.com/RamenOfficialGovPatsy/PayrollHelper.git
cd PayrollHelper
```

### 2. Установка .NET 6.0

Скачайте и установите [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (не только Runtime).

> ⚠️ **Важно:** Приложение требует .NET SDK версии 6.0. Если у вас установлена более новая версия SDK, проект всё равно будет собираться с версией 6.0 благодаря файлу `global.json` в корне репозитория.

### 3. Установка PostgreSQL 18

Скачайте и установите [PostgreSQL 18](https://www.postgresql.org/download/)

---

<h2 id="db-setup">🗄️ Настройка базы данных</h2>

### 1. Создание пользователя и базы данных

Подключитесь к PostgreSQL и выполните:

```sql
CREATE USER payroll_user WITH PASSWORD '123';
CREATE DATABASE payroll_db OWNER payroll_user ENCODING 'UTF8';
GRANT ALL PRIVILEGES ON DATABASE payroll_db TO payroll_user;
```

### 2. Создание таблиц

Выполните скрипты из папки `Docs/DatabaseScripts/` в следующем порядке:

| Файл | Описание |
|------|----------|
| `1_create_tables.sql` | Создание таблиц с ограничениями |
| `2_add_foreign_keys.sql` | Добавление внешних ключей |
| `3_insert_all_data.sql` | Заполнение начальными данными |

> 💡 **Примечание:** Скрипт `4_drop_all_tables.sql` предназначен для полного удаления всех таблиц (используется при пересоздании базы данных).

### 3. Проверка подключения

```bash
psql -U payroll_user -d payroll_db
```

---

<h2 id="run">🏃 Запуск приложения</h2>

### Через Visual Studio

1. Откройте `PayrollHelper.slnx`
2. Установите `PayrollHelper` как стартовый проект
3. Нажмите `F5` или `Ctrl+F5`

### Через командную строку

```bash
cd PayrollHelper
dotnet build -c Release
dotnet run --project PayrollHelper
```

---

<h2 id="users">👥 Пользователи по умолчанию</h2>

| Логин | Пароль | Роль |
|-------|--------|------|
| `admin` | `admin` | Администратор |
| `CEO` | `CEO123` | Администратор |
| `user` | `user` | Пользователь |

> 💡 **Примечание:** После первого запуска файл `users.json` создаётся автоматически в папке с приложением. Новых пользователей можно добавить через форму "Создать пользователя".

---

<h2 id="screenshots">📸 Скриншоты</h2>

### 1. Форма авторизации
![Login](Docs/ScreenShots/1.Login.png)

### 2. Создание пользователя
![Create User](Docs/ScreenShots/2.CreateUser.png)

### 3. Главное меню
![Menu](Docs/ScreenShots/3.Menu.png)

### 4. Вкладка выплат
![Payments](Docs/ScreenShots/4.Payments.png)

### 5. Вкладка добавления сотрудника
![New Employee](Docs/ScreenShots/5.NewEmployee.png)

### 6. Вкладка должностей
![Positions](Docs/ScreenShots/6.Positions.png)

### 7. Управление налогами
![Taxes](Docs/ScreenShots/7.Taxes.png)

### 8. Управление типами выплат
![Payment Types](Docs/ScreenShots/8.PaymentTypes.png)

### 9. Редактирование базы данных
![Edit Database](Docs/ScreenShots/9.EditDB.png)

### 10. Информация о сотруднике
![Employee Info](Docs/ScreenShots/10.EmployeeInfo.png)

### 11. Формирование отчётов
![Reports](Docs/ScreenShots/11.Reports.png)

---

<h2 id="license">📄 Лицензия</h2>

Проект распространяется под лицензией MIT. Подробнее в файле [LICENSE](LICENSE).

---

<h2 id="support">⭐ Поддержка</h2>

Если проект оказался полезным, поставьте звезду — это лучший способ сказать «спасибо» и помогает проекту находить новых пользователей.