# TASK 4 C# — To'liq Texnik Topshiriq (TZ) va Roadmap

User Management Web Application — ASP.NET Core + React + Bootstrap

---

## 1. LOYIHA MAQSADI

Ro'yxatdan o'tish va autentifikatsiyaga ega bo'lgan Web ilova yaratish. Autentifikatsiyadan o'tgan va blocklanmagan userlar boshqa userlarni boshqarish (admin panel) imkoniyatiga ega bo'lishi kerak: ko'rish, block/unblock, delete. Barcha userlar admin huquqiga ega.

---

## 2. ASOSIY TALABLAR

1. Databaseda **UNIQUE INDEX** yaratish kerak (Email ustuni uchun)
2. Jadval `<table>` ko'rinishida, toolbar jadval ustida alohida bo'lishi kerak
3. Jadval **sorted** bo'lishi kerak (Last Login time bo'yicha DESC)
4. Bir nechta tanlash **checkboxlar** orqali (select-all ham checkbox)
5. Har qanday requestdan oldin (register/login dan tashqari) **server user borligini va blocklanmaganligini tekshirishi kerak**; user blocked yoki deleted bo'lsa — login page ga redirect

### Qo'shimcha talablar
- Non-auth userlar faqat login/register ga kira oladi
- Leftmost column — labelsiz checkbox
- Header da ham labelsiz checkbox (select/deselect all)
- Toolbar: `Block` (text button), `Unblock` (icon), `Delete` (icon), `Delete unverified` (icon)
- Tugmalar yo'qolmaydi, faqat disable/enable holatda bo'ladi
- Har qanday **non-empty password** (hatto 1 ta belgi ham) qabul qilinadi
- User email tasdiqlanmagan bo'lsa ham login qila oladi va boshqa userlarni boshqara oladi
- Deleted userlar fizik o'chiriladi (hard delete), "marked" emas
- Ro'yxatdan o'tish **darhol** amalga oshadi, confirmation email **asynchronously** yuboriladi
- Email linkini bosganda: `Unverified` → `Active` (agar `Blocked` bo'lsa — `Blocked` qoladi)
- Blocklangan user login qila olmaydi, deletelangan user qayta ro'yxatdan o'ta oladi
- O'zini block/delete qila oladi (hamma admin)
- Bootstrap yoki boshqa CSS framework majburiy
- Toastr (yoki shunga o'xshash) xabarnomalar majburiy
- Jadval + toolbar uchun **primary key** kodini yozing (standard library qidirmang)

---

## 3. TEXNOLOGIYALAR STACKI

### Backend
- **ASP.NET Core 8** Web API
- **Entity Framework Core 8**
- **SQL Server** yoki **PostgreSQL** (Neon/Supabase uchun PostgreSQL tavsiya)
- **JWT Bearer Authentication**
- **BCrypt.Net-Next** — password hashing
- **MailKit** — email yuborish (SMTP)
- **Serilog** — logging
- **FluentValidation** (ixtiyoriy) — request validation

### Frontend
- **React 18 + Vite** (yoki Next.js)
- **Bootstrap 5** + **bootstrap-icons**
- **Axios** — HTTP client
- **React Router DOM v6**
- **React Hook Form** + **Yup**
- **react-toastify** — notifications
- **date-fns** yoki **dayjs** — relative time format

### Deploy
- Backend: Render / Railway / Azure App Service
- Frontend: Vercel / Netlify
- Database: Neon.tech (PostgreSQL, bepul) / Supabase

---

## 4. DATABASE SXEMASI

### Users jadvali

| Field | Type | Constraints | Izoh |
|-------|------|-------------|------|
| Id | Guid / uniqueidentifier | PK | Primary key |
| Name | nvarchar(100) | NOT NULL | User ismi |
| Email | nvarchar(256) | NOT NULL, **UNIQUE INDEX** | 1-talab |
| PasswordHash | nvarchar(max) | NOT NULL | BCrypt hash |
| Status | int | NOT NULL, DEFAULT 0 | Enum: 0=Unverified, 1=Active, 2=Blocked |
| RegistrationTime | datetime2 | NOT NULL | Ro'yxatdan o'tgan vaqt |
| LastLoginTime | datetime2 | NULL | Oxirgi kirish |
| EmailConfirmationToken | nvarchar(500) | NULL | Email tasdiqlash tokeni |
| TokenExpiresAt | datetime2 | NULL | Token tugash vaqti |
| CreatedDate | datetime2 | NOT NULL | Audit |
| UpdatedDate | datetime2 | NOT NULL | Audit |

### UserStatus Enum
```
Unverified = 0   ← Ro'yxatdan o'tgan, email tasdiqlanmagan
Active = 1       ← Email tasdiqlangan yoki shunchaki login qila oladigan
Blocked = 2      ← Bloklangan
```

### Migration SQL
```sql
CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
```

EF Core Configurationda:
```
builder.HasIndex(u => u.Email).IsUnique();
```

---

## 5. BACKEND PROJEKT STRUKTURASI (The Standard)

```
UserManagement.sln
│
├── UserManagement.Api/                        ← Presentation layer
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── UsersController.cs
│   ├── Middlewares/
│   │   ├── UserStatusCheckMiddleware.cs      ← 5-talab
│   │   └── ExceptionHandlerMiddleware.cs
│   ├── Models/
│   │   ├── Requests/
│   │   │   ├── RegisterRequest.cs
│   │   │   ├── LoginRequest.cs
│   │   │   └── BulkUserIdsRequest.cs
│   │   └── Responses/
│   │       ├── AuthResponse.cs
│   │       ├── UserResponse.cs
│   │       └── ApiResponse.cs
│   ├── Extensions/
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── ApplicationBuilderExtensions.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── UserManagement.Core/                       ← Business logic
│   ├── Models/
│   │   ├── Users/
│   │   │   ├── User.cs
│   │   │   ├── UserStatus.cs                 (enum)
│   │   │   └── Exceptions/
│   │   │       ├── UserValidationException.cs
│   │   │       ├── UserNotFoundException.cs
│   │   │       ├── UserAlreadyExistsException.cs
│   │   │       ├── UserBlockedException.cs
│   │   │       ├── InvalidCredentialsException.cs
│   │   │       ├── UserDependencyException.cs
│   │   │       └── UserServiceException.cs
│   │   └── Auth/
│   │       ├── AuthCredentials.cs
│   │       ├── RegistrationData.cs
│   │       └── TokenResult.cs
│   ├── Services/
│   │   ├── Foundations/                      ← CRUD + Validation
│   │   │   ├── Users/
│   │   │   │   ├── IUserService.cs
│   │   │   │   ├── UserService.cs
│   │   │   │   ├── UserService.Validations.cs
│   │   │   │   └── UserService.Exceptions.cs
│   │   │   └── Emails/
│   │   │       ├── IEmailService.cs
│   │   │       └── EmailService.cs
│   │   ├── Processings/                      ← Higher-order
│   │   │   └── Users/
│   │   │       ├── IUserProcessingService.cs
│   │   │       └── UserProcessingService.cs
│   │   └── Orchestrations/                   ← Multi-service flows
│   │       └── Auth/
│   │           ├── IAuthOrchestrationService.cs
│   │           └── AuthOrchestrationService.cs
│   └── Brokers/                              ← Abstractions
│       ├── Storages/
│       │   ├── IStorageBroker.cs
│       │   └── IStorageBroker.Users.cs
│       ├── Emails/IEmailBroker.cs
│       ├── DateTimes/IDateTimeBroker.cs
│       ├── Securities/ISecurityBroker.cs     (password hashing, JWT)
│       └── Loggings/ILoggingBroker.cs
│
├── UserManagement.Infrastructure/             ← External dependencies
│   ├── Brokers/
│   │   ├── Storages/
│   │   │   ├── StorageBroker.cs
│   │   │   ├── StorageBroker.Users.cs
│   │   │   └── AppDbContext.cs
│   │   ├── Emails/EmailBroker.cs             (MailKit/SMTP)
│   │   ├── DateTimes/DateTimeBroker.cs
│   │   ├── Securities/
│   │   │   ├── PasswordBroker.cs             (BCrypt)
│   │   │   └── TokenBroker.cs                (JWT)
│   │   └── Loggings/LoggingBroker.cs
│   └── Persistence/
│       ├── Configurations/
│       │   └── UserConfiguration.cs           (unique index)
│       └── Migrations/
│
└── UserManagement.Tests.Unit/                 ← Optional
    └── Services/Foundations/Users/
        ├── UserServiceTests.Logic.cs
        ├── UserServiceTests.Validations.cs
        └── UserServiceTests.Exceptions.cs
```

---

## 6. BROKERS (I/O wrappers)

### IStorageBroker.Users
Database CRUD amallari:
| Method | Vazifasi |
|--------|----------|
| `InsertUserAsync(User user)` | Yangi user qo'shish |
| `SelectAllUsers()` | IQueryable<User> qaytaradi (sorting/filtering uchun) |
| `SelectUserByIdAsync(Guid id)` | ID bo'yicha topish |
| `SelectUserByEmailAsync(string email)` | Email bo'yicha topish |
| `UpdateUserAsync(User user)` | Yangilash |
| `DeleteUserAsync(User user)` | Hard delete |
| `DeleteUsersRangeAsync(IEnumerable<User>)` | Bulk delete |

### IEmailBroker
| Method | Vazifasi |
|--------|----------|
| `SendEmailAsync(string to, string subject, string htmlBody)` | SMTP orqali email yuborish |

### IDateTimeBroker
| Method | Vazifasi |
|--------|----------|
| `GetCurrentDateTimeOffset()` | `DateTimeOffset.UtcNow` qaytaradi (testability uchun) |

### ISecurityBroker (Passwords)
| Method | Vazifasi |
|--------|----------|
| `HashPassword(string plainPassword)` | BCrypt hash |
| `VerifyPassword(string plain, string hash)` | Tekshirish |
| `GenerateConfirmationToken()` | Random 32-byte base64 token |

### ISecurityBroker (Tokens)
| Method | Vazifasi |
|--------|----------|
| `GenerateJwtToken(User user)` | JWT tuzadi |
| `ValidateJwtToken(string token)` | Validate qiladi |

### ILoggingBroker
| Method | Vazifasi |
|--------|----------|
| `LogInformation(string)` / `LogError(Exception)` / `LogWarning(...)` | Serilog wrapper |

---

## 7. FOUNDATION SERVICES

### IUserService — User entity CRUD + validation

| Method | Parametrlar | Qaytaradi | Vazifasi |
|--------|-------------|-----------|----------|
| `AddUserAsync` | `User user` | `User` | Validate qiladi va DBga yozadi |
| `RetrieveUserByIdAsync` | `Guid id` | `User` | Topilmasa NotFoundException |
| `RetrieveUserByEmailAsync` | `string email` | `User` | Login uchun |
| `RetrieveAllUsers` | — | `IQueryable<User>` | Sorted by LastLoginTime DESC |
| `ModifyUserAsync` | `User user` | `User` | Yangilash + UpdatedDate set |
| `RemoveUserByIdAsync` | `Guid id` | `User` | Hard delete |

### UserService.Validations.cs — private validation metodlari
- `ValidateUserOnAdd(User user)` — Name/Email/Password tekshirish
- `ValidateUserOnModify(User user)` — update uchun
- `ValidateUserId(Guid id)` — empty emasligini
- `ValidateUserExists(User user)` — null emasligini

### UserService.Exceptions.cs — try/catch wrapper metodlari
- Har bir metod `TryCatch` pattern orqali o'raladi:
  - `UserValidationException` — validation xato
  - `UserDependencyException` — DB connection xato
  - `UserServiceException` — boshqa xatolar
  - `UserDependencyValidationException` — unique constraint violation (duplicate email)

### IEmailService

| Method | Parametrlar | Vazifasi |
|--------|-------------|----------|
| `SendConfirmationEmailAsync` | `string toEmail, string token` | HTML email jo'natadi (confirmation link bilan) |

---

## 8. PROCESSING SERVICES

### IUserProcessingService — bulk amallar

| Method | Parametrlar | Vazifasi |
|--------|-------------|----------|
| `BlockUsersAsync` | `IEnumerable<Guid> ids` | Hammaning Statusini Blocked qiladi |
| `UnblockUsersAsync` | `IEnumerable<Guid> ids` | Blocked bo'lganlarini Active qiladi (Unverified — Unverified qoladi, lekin talab bo'yicha soddalashtirib Active qilsak ham bo'ladi) |
| `DeleteUsersAsync` | `IEnumerable<Guid> ids` | Hard delete |
| `DeleteUnverifiedUsersAsync` | `IEnumerable<Guid> ids` | Faqat Status=Unverified bo'lganlarini o'chiradi |
| `GetAllUsersSorted` | — | LastLoginTime DESC sorted list |
| `UpdateLastLoginAsync` | `Guid userId` | LastLoginTime = now |
| `ConfirmEmailAsync` | `string token` | Tokenga mos userni topadi; Blocked bo'lmasa Active qiladi |

---

## 9. ORCHESTRATION SERVICES

### IAuthOrchestrationService — multi-service flow

| Method | Parametrlar | Vazifasi |
|--------|-------------|----------|
| `RegisterAsync` | `RegistrationData data` | 1) Password hash 2) User create 3) Token generate 4) DB save 5) Email async yuborish (fire-and-forget) 6) Darhol javob qaytaradi |
| `LoginAsync` | `AuthCredentials creds` | 1) Email bo'yicha topish 2) Password verify 3) Status tekshirish (Blocked → exception) 4) LastLoginTime update 5) JWT qaytaradi |
| `ConfirmEmailAsync` | `string token` | UserProcessingService.ConfirmEmailAsync ga delegate qiladi |
| `LogoutAsync` | `Guid userId` | Serverda minimal ish (JWT stateless), faqat audit log |

---

## 10. CONTROLLERS

### AuthController — `/api/auth`

| HTTP | Route | Action | Body / Query |
|------|-------|--------|--------------|
| POST | `/register` | Register | `{ name, email, password }` |
| POST | `/login` | Login | `{ email, password }` |
| GET | `/confirm-email` | ConfirmEmail | `?token=xxx` |
| POST | `/logout` | Logout | — (Auth required) |
| GET | `/me` | GetCurrentUser | — (Auth required) |

### UsersController — `/api/users` (barcha endpointlar Auth required)

| HTTP | Route | Action | Vazifasi |
|------|-------|--------|----------|
| GET | `/` | GetAll | LastLoginTime DESC sorted barcha userlar |
| POST | `/block` | BlockUsers | Body: `{ ids: [guid] }` |
| POST | `/unblock` | UnblockUsers | Body: `{ ids: [guid] }` |
| DELETE | `/` | DeleteUsers | Body: `{ ids: [guid] }` |
| DELETE | `/unverified` | DeleteUnverified | Body: `{ ids: [guid] }` |

---

## 11. MIDDLEWARE

### UserStatusCheckMiddleware (5-talab)

**Ishlash tartibi:**
1. Request path `/api/auth/register`, `/api/auth/login`, yoki `/api/auth/confirm-email` bo'lsa → skip qiladi
2. JWT dan `userId` claim oladi
3. DB da shu userni qidiradi
4. User topilmasa (deleted) yoki Status=Blocked bo'lsa:
   - Response: 401 Unauthorized
   - JSON: `{ "redirect": "/login", "reason": "blocked_or_deleted" }`
5. Aks holda `next(context)` chaqiradi

### ExceptionHandlerMiddleware

Custom exceptionlarni HTTP status codega maplaydi:
- `UserValidationException` → 400
- `UserNotFoundException` → 404
- `UserAlreadyExistsException` → 409
- `UserBlockedException` → 403
- `InvalidCredentialsException` → 401
- Default → 500

---

## 12. FRONTEND PROJEKT STRUKTURASI

```
frontend/
├── public/
│   └── favicon.ico
├── src/
│   ├── api/
│   │   ├── axiosClient.js          ← baseURL, interceptors
│   │   ├── authApi.js              ← register/login/confirm
│   │   └── userApi.js              ← CRUD calls
│   ├── components/
│   │   ├── common/
│   │   │   ├── Navbar.jsx
│   │   │   ├── ProtectedRoute.jsx
│   │   │   ├── Loading.jsx
│   │   │   └── StatusBadge.jsx
│   │   ├── auth/
│   │   │   ├── LoginForm.jsx
│   │   │   └── RegisterForm.jsx
│   │   └── users/
│   │       ├── UserToolbar.jsx
│   │       ├── UserTable.jsx
│   │       ├── UserRow.jsx
│   │       └── SelectAllCheckbox.jsx
│   ├── pages/
│   │   ├── LoginPage.jsx
│   │   ├── RegisterPage.jsx
│   │   ├── ConfirmEmailPage.jsx
│   │   └── UsersPage.jsx
│   ├── context/
│   │   └── AuthContext.jsx
│   ├── hooks/
│   │   ├── useAuth.js
│   │   ├── useUsers.js
│   │   └── useSelection.js
│   ├── utils/
│   │   ├── formatDate.js
│   │   ├── storage.js              ← localStorage wrapper
│   │   └── validators.js
│   ├── App.jsx
│   ├── main.jsx
│   └── index.css
├── .env
├── index.html
├── package.json
└── vite.config.js
```

---

## 13. FRONTEND — COMPONENT VAZIFALARI

### `axiosClient.js`
- `baseURL`, `withCredentials` set
- **Request interceptor**: JWT ni `Authorization: Bearer ...` ga qo'shadi
- **Response interceptor**: 401 kelsa → `localStorage` tozalaydi → `/login` ga redirect

### `AuthContext.jsx`
| Method | Vazifasi |
|--------|----------|
| `login(credentials)` | API call, token saqlash, user set |
| `register(data)` | API call |
| `logout()` | Token o'chirish, state reset |
| `checkAuth()` | App yuklanganda tokenni tekshirish |

### `ProtectedRoute.jsx`
- Agar auth emas → `/login` ga redirect
- Auth bor → children render qiladi

### `LoginForm.jsx`
- Email + Password
- **HAR QANDAY non-empty password qabul qilinadi** (hatto "1" ham)
- Submit → AuthContext.login

### `RegisterForm.jsx`
- Name + Email + Password
- Pareldin keyin darhol login page ga redirect + toastr "Check your email"

### `UserToolbar.jsx`
Props: `selectedIds[]`, `onBlock`, `onUnblock`, `onDelete`, `onDeleteUnverified`
- `Block` — text button (Bootstrap primary)
- `Unblock` — icon button (bi-unlock)
- `Delete` — icon button (bi-trash)
- `Delete unverified` — icon button (bi-person-x)
- Hech narsa tanlanmagan bo'lsa → barchasi **disabled**

### `UserTable.jsx`
- Bootstrap `<table className="table table-hover">`
- Header: checkbox (select-all) | Name | Email | Last Login | Status | Registration
- `<tbody>`: UserRow componentlari
- Default sort: LastLoginTime DESC

### `SelectAllCheckbox.jsx`
- 3 holat: none / some / all
- `some` holatda `indeterminate` property true

### `UserRow.jsx`
- Checkbox + ma'lumotlar
- Last login — relative ("5 minutes ago") — `formatDistanceToNow`
- Status — `StatusBadge` (active=success, blocked=danger, unverified=warning)

### `useUsers.js` (custom hook)
| Function | Vazifasi |
|----------|----------|
| `fetchUsers()` | GET /api/users |
| `blockUsers(ids)` | POST /api/users/block + refresh |
| `unblockUsers(ids)` | POST /api/users/unblock + refresh |
| `deleteUsers(ids)` | DELETE /api/users + refresh |
| `deleteUnverified(ids)` | DELETE /api/users/unverified + refresh |

### `useSelection.js` (custom hook)
| Function | Vazifasi |
|----------|----------|
| `toggle(id)` | Bitta toggle |
| `toggleAll(allIds)` | Hammasini toggle |
| `clear()` | Reset |
| `isSelected(id)` | Tekshirish |

### `formatDate.js`
- `formatRelativeTime(date)` → "5 minutes ago", "2 days ago"
- date-fns `formatDistanceToNow` wrapper

---

## 14. USER FLOW (oqimlar)

### Registration
1. User formani to'ldiradi → submit
2. Backend: validate → BCrypt hash → token generate → DB insert → email jo'natish **fire-and-forget** bilan boshlanadi
3. Darhol 200 OK qaytariladi
4. Frontend: toastr "Registration successful. Please check your email." → login page ga redirect

### Email Confirmation
1. User email dagi linkni bosadi: `https://frontend.com/confirm-email?token=xxx`
2. Frontend `ConfirmEmailPage` ochiladi
3. Frontend backend ga GET /api/auth/confirm-email?token=xxx yuboradi
4. Backend: tokenga mos user topadi
   - Agar Status=Unverified → Active qiladi
   - Agar Status=Blocked → Blocked qoldiradi
   - Agar token eskirgan yoki topilmadi → 400
5. Frontend natija ko'rsatadi va login page ga link beradi

### Login
1. Email + Password → backend
2. Backend: user topadi, password verify qiladi
3. Agar Status=Blocked → 403 `UserBlockedException`
4. Aks holda: LastLoginTime = now update, JWT qaytaradi
5. Frontend: token localStorage ga saqlaydi → UsersPage ga redirect

### Block oneself
1. User o'z checkboxini tanlaydi → Block bosadi
2. Backend: Status=Blocked qiladi
3. Frontend keyingi requestda 401 oladi (middleware)
4. Axios interceptor 401 ushlaydi → localStorage tozalaydi → `/login` ga redirect

### Delete all users
1. Select-all → Delete
2. Backend hammasini hard delete qiladi
3. Current user ham o'chiriladi
4. Frontend 401 oladi → login page

---

## 15. VALIDATION QOIDALARI

### Register/Login
- **Name**: required, max 100 char
- **Email**: required, valid format, max 256
- **Password**: required, **min 1 char** (talab bo'yicha)

### Bulk operations
- `ids` array: required, min 1 element, har bir element valid Guid

---

## 16. JWT CLAIMS

Token payload:
```
sub (UserId - Guid)
email
name
iat (issued at)
exp (expires - 24 soat)
```

**Muhim:** Har bir requestda middleware userni DB dan olib kelib, statusini tekshiradi (JWT ichidagi status bilan cheklanib qolmaydi — chunki block qilingan bo'lishi mumkin).

---

## 17. EMAIL TEMPLATE

SMTP config (`appsettings.json`):
```
"Smtp": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "Username": "you@gmail.com",
  "Password": "app-password-16-chars",
  "From": "no-reply@yourapp.com",
  "FrontendUrl": "https://your-frontend.vercel.app"
}
```

HTML template misoli:
- Subject: "Confirm your email"
- Body: "Hi {name}, click the link to verify: `{FrontendUrl}/confirm-email?token={token}`"
- Token expiration: 24 hours

---

## 18. DEPLOY KETMA-KETLIGI

### Database (Neon.tech)
1. Neon.tech da project yarating → PostgreSQL connection string oling
2. `appsettings.json` da `ConnectionStrings:DefaultConnection` ga qo'ying
3. EF Core Migration yaratib push qiling: `dotnet ef database update`

### Backend (Render.com)
1. GitHub reponi ulang
2. Build command: `dotnet publish -c Release -o out`
3. Start command: `dotnet out/UserManagement.Api.dll`
4. Environment variables:
   - `ConnectionStrings__DefaultConnection`
   - `Jwt__Secret`
   - `Smtp__*`
   - `ASPNETCORE_ENVIRONMENT=Production`
5. CORS da frontend domainini qo'shing

### Frontend (Vercel)
1. GitHub reponi import qiling
2. `VITE_API_URL` environment variable = backend URL
3. Build: `npm run build`, output: `dist`
4. Deploy

---

## 19. ISHLASHNI BOSHLASH TARTIBI (bosqichma-bosqich)

1. [ ] Solution + 4 ta project yaratish (Api, Core, Infrastructure, Tests)
2. [ ] NuGet paketlarini o'rnatish
3. [ ] User entity, UserStatus enum, UserConfiguration yozish
4. [ ] AppDbContext + Migration yaratish, unique index qo'shish
5. [ ] Brokers interfacelari + implementatsiyasi (Storage, DateTime, Email, Security, Logging)
6. [ ] UserService (Foundation) + Validations + Exceptions
7. [ ] UserProcessingService
8. [ ] AuthOrchestrationService
9. [ ] AuthController + UsersController
10. [ ] ExceptionHandlerMiddleware + UserStatusCheckMiddleware
11. [ ] JWT setup + Program.cs
12. [ ] CORS configuration
13. [ ] Postman/Swagger orqali test qilish
14. [ ] React + Vite + Bootstrap scaffold
15. [ ] axiosClient + AuthContext
16. [ ] Login/Register pages
17. [ ] UsersPage + Table + Toolbar + Selection
18. [ ] Toast notifications + validation messages
19. [ ] ConfirmEmail page
20. [ ] Email service (Gmail SMTP)
21. [ ] Local end-to-end test
22. [ ] DB deploy (Neon)
23. [ ] Backend deploy (Render)
24. [ ] Frontend deploy (Vercel)
25. [ ] Production test
26. [ ] Video yozib olish (barcha flow)

---

## 20. VIDEO TALABLARI (submission)

Video recording (narration siz) quyidagilarni ko'rsatishi kerak:
1. Registration
2. Email confirmation (link bosish)
3. Login
4. Boshqa user tanlash (non-current)
5. User blocking (status yangilanishi ko'rinishi kerak)
6. User unblocking
7. Hamma userlarni tanlash (select-all, current user ham)
8. Hamma userlarni block qilish (automatic login page redirect)
9. Databaseda yaratilgan unique index ko'rsatish (SSMS yoki pgAdmin)

---

## 21. MUHIM ESLATMALAR

- Talab: "standard library" ishlatmang — jadval va toolbar primary kodini o'zingiz yozing (Bootstrap ok)
- Talab: "don't invent anything" — 20% dan ko'p custom UI qilmang
- Talab: "NO E-MAIL UNIQUENESS INDEPENDENTLY OF HOW MANY SOURCES PUSH DATA INTO IT SIMULTANEOUSLY" → bu unique indexni DB darajasida qilishni majburlash (application code emas)
- Talab: "NO WALLPAPERS UNDER THE TABLE. NO ANIMATIONS. NO BROWSER ALERTS. NO BUTTONS IN THE DATA ROWS." — buni qat'iy rioya qiling
- Talab: "Deleted users should be deleted, not marked" → Soft delete qilmang, hard delete
- Talab: "User can use any non-empty password" → Password complexity validation qilmang
- Talab: "Users can login and manage other users even if their e-mails are unverified" → Unverified userlar ham to'liq huquqqa ega

---

## 22. KUTILGAN NATIJA

- Source code GitHub da public repo
- Deployed URL
- Video demonstratsiya
- Email: `p.lebedev@itransition.com`
- Muddat: 2026-04-15

---

## 23. FAYDALI KOMANDALAR

### EF Core Migration
```
dotnet ef migrations add Initial -p UserManagement.Infrastructure -s UserManagement.Api
dotnet ef database update -p UserManagement.Infrastructure -s UserManagement.Api
```

### Frontend setup
```
npm create vite@latest frontend -- --template react
cd frontend
npm install bootstrap bootstrap-icons axios react-router-dom react-hook-form yup @hookform/resolvers react-toastify date-fns
```

### Backend NuGet packages
```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Npgsql.EntityFrameworkCore.PostgreSQL  (yoki Microsoft.EntityFrameworkCore.SqlServer)
Microsoft.AspNetCore.Authentication.JwtBearer
BCrypt.Net-Next
MailKit
Serilog.AspNetCore
```

---

Agar biron qismini **chuqurroq ochib berishimni** xohlasangiz — masalan The Standard'dagi Foundation Service pattern, exception hierarchy, yoki concrete middleware logikasi — ayting, alohida hujjat qilib beraman.
