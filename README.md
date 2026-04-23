# User Management System / Foydalanuvchi Boshqaruv Tizimi

## English

A full-stack **User Management** application with email verification, JWT authentication, admin-style user operations (block/unblock/delete), and a modern React dashboard.

### Key Features
- User registration with email confirmation token.
- Login with JWT-based authentication.
- Protected routes and automatic logout on `401`.
- User listing with bulk actions:
  - Block users
  - Unblock users
  - Delete selected users
  - Delete unverified users
- Middleware-based access check for blocked/deleted accounts.
- PostgreSQL + Entity Framework Core migrations.

### Tech Stack & Versions

#### Backend (.NET)
- .NET SDK / Target Framework: **.NET 10 (`net10.0`)**
- ASP.NET Core Web API
- Entity Framework Core **10.0.6**
- Npgsql EF Core Provider **10.0.1**
- JWT Bearer Auth **10.0.7**
- Swagger (Swashbuckle) **10.1.7**
- MailKit **4.16.0**
- BCrypt.Net-Next **4.1.0**
- RESTFulSense **3.2.0**
- Xeption **2.8.0**

#### Frontend (React)
- React **19.2.5**
- React DOM **19.2.5**
- React Router DOM **7.14.2**
- Vite **8.0.10**
- Axios **1.15.2**
- Bootstrap **5.3.8**
- Bootstrap Icons **1.13.1**
- React Toastify **11.1.0**
- date-fns **4.1.0**
- ESLint **10.2.1**

### Project Structure
```text
.
├── Brokers/              # External concerns (email, logging, hashing, random, storage)
├── Controllers/          # API endpoints
├── Middlewares/          # User status verification middleware
├── Models/               # Entities, requests, responses, custom exceptions
├── Services/
│   ├── Foundations/      # Core services (users, security, email)
│   ├── Processings/      # Business workflows for bulk user operations
│   └── Orchestrations/   # Auth orchestration
├── Migrations/           # EF Core migrations
└── frontend/             # React + Vite client app
```

### Quick Start

#### 1) Clone
```bash
git clone <your-repo-url>
cd task4-user-managment-
```

#### 2) Configure backend settings
Create `appsettings.json` in the backend root:

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
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

#### 3) Run backend
```bash
dotnet restore
dotnet run
```

#### 4) Run frontend
```bash
cd frontend
npm install
npm run dev
```

### API Summary

#### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/confirm-email?token=...`

#### User
- `GET /api/user`
- `GET /api/user/{userId}`
- `POST /api/user`
- `PUT /api/user`
- `DELETE /api/user/{userId}`
- `PATCH /api/user/block`
- `PATCH /api/user/unblock`
- `DELETE /api/user/bulk`
- `DELETE /api/user/unverified`

For complete API schemas, architecture notes, deployment guidance, and troubleshooting, see **[docs/PROJECT_DOCUMENTATION.md](docs/PROJECT_DOCUMENTATION.md)**.

---

## O'zbekcha

Email tasdiqlash, JWT autentifikatsiya, admin panel uslubidagi foydalanuvchi amallari (bloklash/blokdan chiqarish/o‘chirish) va React interfeysiga ega to‘liq **Foydalanuvchi Boshqaruv** tizimi.

### Asosiy imkoniyatlar
- Email tasdiqlash tokeni bilan ro‘yxatdan o‘tish.
- JWT orqali tizimga kirish.
- Himoyalangan sahifalar va `401` holatda avtomatik chiqish.
- Foydalanuvchilar ro‘yxati va ommaviy amallar:
  - Foydalanuvchilarni bloklash
  - Blokdan chiqarish
  - Tanlanganlarni o‘chirish
  - Tasdiqlanmaganlarni o‘chirish
- Bloklangan/o‘chirilgan akkauntlarni middleware orqali tekshirish.
- PostgreSQL + Entity Framework Core migratsiyalar.

### Texnologiyalar va versiyalar

#### Backend (.NET)
- .NET SDK / Target Framework: **.NET 10 (`net10.0`)**
- ASP.NET Core Web API
- Entity Framework Core **10.0.6**
- Npgsql EF Core Provider **10.0.1**
- JWT Bearer Auth **10.0.7**
- Swagger (Swashbuckle) **10.1.7**
- MailKit **4.16.0**
- BCrypt.Net-Next **4.1.0**
- RESTFulSense **3.2.0**
- Xeption **2.8.0**

#### Frontend (React)
- React **19.2.5**
- React DOM **19.2.5**
- React Router DOM **7.14.2**
- Vite **8.0.10**
- Axios **1.15.2**
- Bootstrap **5.3.8**
- Bootstrap Icons **1.13.1**
- React Toastify **11.1.0**
- date-fns **4.1.0**
- ESLint **10.2.1**

### Loyihani ishga tushirish (tezkor)

#### 1) Clone qiling
```bash
git clone <repo-url>
cd task4-user-managment-
```

#### 2) Backend sozlamalarini kiriting
Root papkada `appsettings.json` yarating (yuqoridagi JSON namunadan foydalaning).

#### 3) Backend ishga tushiring
```bash
dotnet restore
dotnet run
```

#### 4) Frontend ishga tushiring
```bash
cd frontend
npm install
npm run dev
```

### API qisqacha

#### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/confirm-email?token=...`

#### User
- `GET /api/user`
- `GET /api/user/{userId}`
- `POST /api/user`
- `PUT /api/user`
- `DELETE /api/user/{userId}`
- `PATCH /api/user/block`
- `PATCH /api/user/unblock`
- `DELETE /api/user/bulk`
- `DELETE /api/user/unverified`

To‘liq hujjatlar (arxitektura, API request/response misollari, deployment va troubleshooting) bu yerda: **[docs/PROJECT_DOCUMENTATION.md](docs/PROJECT_DOCUMENTATION.md)**.
