# Authentication Microservice (AuthService) 🔐

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web-5C2D91)](https://learn.microsoft.com/aspnet/core/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-14+-336791)](https://www.postgresql.org/)
[![EF Core](https://img.shields.io/badge/EF%20Core-ORM-512BD4)](https://learn.microsoft.com/ef/)

Микросервис аутентификации и управления пользователями. Сервис предоставляет API для регистрации и логина пользователей, выпускает JWT-токены и предназначен для использования в микросервисной архитектуре.

---

## 📌 Функционал

- Регистрация пользователей
- Аутентификация (логин)
- Генерация JWT-токенов
- Хранение пользователей и ролей в PostgreSQL
- Централизованная обработка ошибок через `ErrorOr`

---

## 🧩 Технологии

- **ASP.NET Core**
- **.NET 9.0**
- **Entity Framework Core (PostgreSQL)**
- **MediatR + CQRS**
- **DDD (Domain-Driven Design)**
- **Mapster**
- **ErrorOr**
- **JWT Authentication**

---

## 🏗 Архитектура

Проект следует принципам **Clean Architecture / DDD** и разделён на слои:

- **WebAPI** — HTTP endpoints, DTO
- **Application** — CQRS (Commands / Queries), MediatR, абстракции
- **Domain** — агрегаты, value objects, интерфейсы
- **Infrastructure** — EF Core, PostgreSQL, реализации репозиториев/сервисов

Каждый слой изолирован и зависит только от нижележащих абстракций.

---

## 🔗 API — конечные точки

### Регистрация пользователя
POST /api/auth/register

### Логин пользователя
POST /api/auth/login

Обе конечные точки возвращают `AuthenticationResponse`.

---

## 📥 Модели запросов / ответов

### RegisterUserRequest
```csharp
public record RegisterUserRequest(
    string FirstName, 
    string LastName, 
    string Patronymic,
    string PhoneNumber,
    string Email,
    string Password,
    string PasswordConfirmation);
```

### LoginUserRequest
```csharp
public record LoginUserRequest(
    string Email, 
    string Password);
```

### AuthenticationResponse
```csharp
public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    string Token);
```

---

## 📤 Примеры HTTP-запросов
### Регистрация
```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "Иван",
  "lastName": "Иванов",
  "patronymic": "Иванович",
  "phoneNumber": "+79991234567",
  "email": "ivan@example.com",
  "password": "P@ssw0rd!",
  "passwordConfirmation": "P@ssw0rd!"
}
```
Успешный ответ (Register / Login):
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "Иван",
  "lastName": "Иванов",
  "patronymic": "Иванович",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```
### Логин
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "ivan@example.com",
  "password": "P@ssw0rd!"
}
```

---

## 🔐 Безопасность
- Пароли хранятся в базе только в виде хеша
- JWT не содержит чувствительных данных
- Секреты не хранятся в репозитории

Строка подключения к БД лежит в секретах вне репозитория. Дополнительно секретные данные от БД хранятся в .env файле, чтобы их не было в репозитории.

---

## Примечание
Данный микросервис содержит только базовый функционал => находится в разработке. Обновления будут описываться в этом README касательно этого сервиса.
