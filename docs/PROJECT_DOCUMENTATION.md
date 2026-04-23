# Project Documentation / Loyiha Hujjatlari

---

## 1) English Documentation

## 1.1 Overview
This project is a full-stack user management platform built with:
- **ASP.NET Core Web API** backend
- **React + Vite** frontend
- **PostgreSQL** as the main database
- **JWT authentication** for protected API access
- **Email verification workflow** for account activation

It supports both single-user flows (register/login) and admin-like bulk user operations (block/unblock/delete).

## 1.2 Functional Scope

### Authentication
- Register user (`name`, `email`, `password`)
- Generate email confirmation token
- Send verification email through SMTP
- Confirm email using token
- Login and receive JWT token

### User Management
- Create/read/update/delete user records
- List all users sorted by registration date descending
- Bulk block users
- Bulk unblock users
- Bulk delete users
- Delete only unverified users

### Security / Access Behavior
- Passwords are hashed using BCrypt.
- JWT is validated on protected endpoints.
- Middleware checks if authenticated user is blocked/deleted and returns `401` if invalid.

## 1.3 Architecture
The codebase follows layered separation:

- **Controllers**: API entry points
- **Services.Orchestrations**: cross-domain workflow coordination (auth)
- **Services.Processings**: domain workflows (bulk user actions)
- **Services.Foundations**: core domain services (users/email/security)
- **Brokers**: external integrations and infrastructural abstractions
- **Models**: entities, DTOs, exceptions
- **Middlewares**: pipeline checks and request interception

This style improves maintainability and testability by reducing direct coupling between transport logic and storage/infrastructure.

## 1.4 Backend Technical Details

### Runtime & Libraries
- Target framework: `net10.0`
- EF Core + Npgsql provider for PostgreSQL
- Swashbuckle + OpenAPI for Swagger docs
- JWT Bearer auth
- MailKit for SMTP email
- RESTFulSense and Xeption for structured API exception behavior

### Dependency Injection Registrations
`Program.cs` wires:
- Brokers (`ILoggingBroker`, `IEmailBroker`, etc.)
- Foundation services (`IUserService`, `IEmailService`, `ITokenService`, `IPasswordHashService`)
- Orchestration/process services (`IAuthService`, `IUserProcessingService`)
- DbContext (`StorageBroker`) + interface abstraction (`IStorageBroker`)

### CORS
A permissive policy (`AllowAnyOrigin/AllowAnyHeader/AllowAnyMethod`) is active under policy name `AllowFrontend`.

### Automatic Migration
Application startup executes `context.Database.Migrate();`, so pending migrations are automatically applied on boot.

## 1.5 Frontend Technical Details

### Stack
- React 19
- Vite 8
- Axios for API communication
- React Router for route handling
- Bootstrap UI + Bootstrap Icons
- React Toastify for user feedback

### Auth State Handling
- Token and user are stored in `localStorage`.
- Request interceptor attaches `Authorization: Bearer <token>`.
- Response interceptor handles `401` by clearing local storage and redirecting to `/login`.

## 1.6 Configuration Guide
Create backend `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=usermanagementdb;Username=postgres;Password=your_password"
  },
  "JwtSettings": {
    "Issuer": "UserManagement.Api",
    "Audience": "UserManagement.Client",
    "SecretKey": "your-super-long-secret-key-here",
    "ExpirationInMinutes": "60"
  },
  "EmailSettings": {
    "From": "your-email@gmail.com",
    "AppPassword": "your-gmail-app-password"
  }
}
```

Notes:
- `EmailSettings` assumes Gmail SMTP (`smtp.gmail.com:587`, STARTTLS).
- Prefer storing secrets via environment variables or secret manager in production.

## 1.7 API Reference

Base path: `/api`

### Auth Endpoints

#### POST `/auth/register`
Request:
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "P@ssw0rd123"
}
```

Success response:
```json
{
  "id": "guid",
  "name": "John Doe",
  "email": "john@example.com"
}
```

#### GET `/auth/confirm-email?token={token}`
- Confirms email activation token.
- On success, returns localized success message.

#### POST `/auth/login`
Request:
```json
{
  "email": "john@example.com",
  "password": "P@ssw0rd123"
}
```

Success response:
```json
{
  "token": "jwt-token",
  "expireAt": "2026-04-23T10:00:00Z",
  "user": {
    "id": "guid",
    "name": "John Doe",
    "email": "john@example.com"
  }
}
```

### User Endpoints

#### GET `/user`
Returns all users (sorted desc by registration time).

#### GET `/user/{userId}`
Returns a single user by ID.

#### POST `/user`
Creates a user entity.

#### PUT `/user`
Updates a user entity.

#### DELETE `/user/{userId}`
Deletes single user.

#### PATCH `/user/block`
Request body: array of GUIDs.

#### PATCH `/user/unblock`
Request body: array of GUIDs.

#### DELETE `/user/bulk`
Request body: array of GUIDs.

#### DELETE `/user/unverified`
Request body: array of GUIDs (only users with `Unverified` status are removed).

## 1.8 User Status Model
`UserStatus` enum:
- `Unverified = 0`
- `Active = 1`
- `Blocked = 2`

## 1.9 Error Handling Model
Controllers map custom exceptions to status codes, including:
- `400 BadRequest` for validation issues
- `404 NotFound` for missing resources
- `409 Conflict` for duplicates (e.g., email exists)
- `500 InternalServerError` for dependency/service failures

## 1.10 Deployment Notes
- Backend currently references an Azure-hosted URL for email confirmation link and frontend Axios base URL.
- For production, move host URLs to environment-based configuration values.
- Keep JWT secret long and random.
- Restrict CORS to known frontend domains.

## 1.11 Suggested Improvements
1. Move hardcoded URLs into settings/env files.
2. Add refresh token flow.
3. Add role-based authorization (admin/user roles).
4. Add automated tests (unit + integration).
5. Add Docker compose for backend + db + frontend.
6. Add request validation attributes / FluentValidation.
7. Add pagination/filtering/sorting API parameters.

---

## 2) O'zbekcha Dokumentatsiya

## 2.1 Umumiy ma'lumot
Ushbu loyiha quyidagi texnologiyalarga asoslangan to‘liq stack foydalanuvchi boshqaruv platformasi:
- **ASP.NET Core Web API** backend
- **React + Vite** frontend
- **PostgreSQL** ma’lumotlar bazasi
- Himoyalangan endpointlar uchun **JWT autentifikatsiya**
- Akkauntni faollashtirish uchun **email tasdiqlash jarayoni**

Loyiha oddiy user flow (ro‘yxatdan o‘tish/kirish) va adminga xos ommaviy amallarni (bloklash/blokdan chiqarish/o‘chirish) qo‘llab-quvvatlaydi.

## 2.2 Funksional qamrov

### Autentifikatsiya
- Foydalanuvchini ro‘yxatdan o‘tkazish (`name`, `email`, `password`)
- Email tasdiqlash tokenini yaratish
- SMTP orqali tasdiqlash xatini yuborish
- Token orqali emailni tasdiqlash
- Login qilish va JWT token olish

### Foydalanuvchi boshqaruvi
- User yozuvlarini yaratish/o‘qish/yangilash/o‘chirish
- Barcha userlarni ro‘yxatini olish (ro‘yxatdan o‘tgan sana bo‘yicha kamayish tartibida)
- Ommaviy bloklash
- Ommaviy blokdan chiqarish
- Ommaviy o‘chirish
- Faqat tasdiqlanmagan (`Unverified`) userlarni o‘chirish

### Xavfsizlik / kirish qoidalari
- Parollar BCrypt bilan hash qilinadi.
- Himoyalangan endpointlarda JWT tekshiriladi.
- Middleware orqali bloklangan/o‘chirilgan foydalanuvchi uchun `401` qaytariladi.

## 2.3 Arxitektura
Kod bazasi qatlamlarga bo‘lingan:

- **Controllers**: API endpointlar
- **Services.Orchestrations**: bir nechta qatlamlarni bog‘lovchi biznes oqimlar (auth)
- **Services.Processings**: domen workflowlari (ommaviy user amallari)
- **Services.Foundations**: asosiy servislar (users/email/security)
- **Brokers**: tashqi integratsiyalar va infratuzilma abstraksiyalari
- **Models**: entity, DTO, exceptionlar
- **Middlewares**: request pipeline tekshiruvlari

Bu yondashuv kodni qo‘llab-quvvatlash va testlashni yengillashtiradi.

## 2.4 Backend texnik tafsilotlar

### Runtime va kutubxonalar
- Target framework: `net10.0`
- PostgreSQL uchun EF Core + Npgsql
- Swagger hujjatlari uchun Swashbuckle/OpenAPI
- JWT Bearer auth
- SMTP uchun MailKit
- Xatoliklarni tartibli qaytarish uchun RESTFulSense + Xeption

### DI (Dependency Injection)
`Program.cs` ichida:
- Brokerlar (`ILoggingBroker`, `IEmailBroker`, ...)
- Foundation service lar (`IUserService`, `IEmailService`, `ITokenService`, `IPasswordHashService`)
- Orchestration/processing service lar (`IAuthService`, `IUserProcessingService`)
- `StorageBroker` DbContext

### CORS
`AllowFrontend` policy hozircha juda ochiq (`AnyOrigin/AnyHeader/AnyMethod`).

### Avtomatik migratsiya
Ilova ishga tushganda `context.Database.Migrate();` bajarilib, kutilayotgan migratsiyalar qo‘llanadi.

## 2.5 Frontend texnik tafsilotlar

### Stack
- React 19
- Vite 8
- Axios
- React Router
- Bootstrap + Bootstrap Icons
- React Toastify

### Auth state
- Token va user `localStorage`da saqlanadi.
- Har requestda `Authorization: Bearer <token>` qo‘shiladi.
- `401` bo‘lsa token o‘chiriladi va `/login`ga redirect qilinadi.

## 2.6 Sozlash yo‘riqnomasi
Backend rootida `appsettings.json` yarating:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=usermanagementdb;Username=postgres;Password=your_password"
  },
  "JwtSettings": {
    "Issuer": "UserManagement.Api",
    "Audience": "UserManagement.Client",
    "SecretKey": "your-super-long-secret-key-here",
    "ExpirationInMinutes": "60"
  },
  "EmailSettings": {
    "From": "your-email@gmail.com",
    "AppPassword": "your-gmail-app-password"
  }
}
```

Izohlar:
- `EmailSettings` Gmail SMTP (`smtp.gmail.com:587`, STARTTLS) uchun mos.
- Productionda secretlarni `.json`ga yozmasdan env yoki secret manager ishlating.

## 2.7 API ma’lumotnoma

Base path: `/api`

### Auth endpointlar
- `POST /auth/register`
- `GET /auth/confirm-email?token={token}`
- `POST /auth/login`

### User endpointlar
- `GET /user`
- `GET /user/{userId}`
- `POST /user`
- `PUT /user`
- `DELETE /user/{userId}`
- `PATCH /user/block`
- `PATCH /user/unblock`
- `DELETE /user/bulk`
- `DELETE /user/unverified`

## 2.8 User status modeli
`UserStatus` enum:
- `Unverified = 0`
- `Active = 1`
- `Blocked = 2`

## 2.9 Xatoliklar modeli
Controllerlar custom exceptionlarni HTTP statuslarga map qiladi:
- `400 BadRequest`
- `404 NotFound`
- `409 Conflict`
- `500 InternalServerError`

## 2.10 Deployment tavsiyalari
- Email confirm link va frontend API URL hozir hardcoded.
- Production uchun URLlarni `appsettings` yoki env orqali boshqaring.
- JWT secret uzun va random bo‘lsin.
- CORS ni faqat kerakli domenlarga cheklang.

## 2.11 Taklif etiladigan yaxshilashlar
1. Hardcoded URLlarni konfiguratsiyaga ko‘chirish.
2. Refresh token mexanizmi qo‘shish.
3. Role-based authorization (admin/user).
4. Unit/integration testlar qo‘shish.
5. Docker Compose qo‘shish.
6. Kuchli request validation (masalan, FluentValidation).
7. APIga pagination/filter/sort qo‘shish.

