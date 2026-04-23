# User Management API

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Database](https://img.shields.io/badge/database-PostgreSQL-336791?logo=postgresql&logoColor=white)](#texnologiyalar)
[![Frontend](https://img.shields.io/badge/frontend-React%2019-61DAFB?logo=react&logoColor=black)](#texnologiyalar)

Email tasdiqlash, JWT autentifikatsiya va admin ommaviy amallari bilan ishlab chiqilgan full-stack foydalanuvchi boshqaruv tizimi.

## 🌐 Til
- 🇺🇿 **O'zbekcha** (joriy)
- 🇬🇧 **English** → [README.md](README.md)

## Mundarija
- [Umumiy ma'lumot](#umumiy-malumot)
- [Imkoniyatlar](#imkoniyatlar)
- [Texnologiyalar](#texnologiyalar)
- [Arxitektura](#arxitektura)
- [Loyiha tuzilmasi](#loyiha-tuzilmasi)
- [Ishga tushirish](#ishga-tushirish)
- [Tezkor start](#tezkor-start)
- [Hujjatlar](#hujjatlar)
- [Hissa qo'shish](#hissa-qoshish)

## Umumiy ma'lumot
Tizim quyidagi oqimlarni qo‘llab-quvvatlaydi:
- Email tasdiqlash tokeni bilan ro‘yxatdan o‘tish
- Token orqali emailni tasdiqlash
- JWT bilan tizimga kirish
- Userlarni ro‘yxatlash va ommaviy boshqarish
- Bloklangan/o‘chirilgan akkauntlar uchun middleware tekshiruvi

## Imkoniyatlar
- `register`, `confirm-email`, `login` endpointlari
- User CRUD + bulk amallar
- PostgreSQL + EF Core migratsiyalar
- Controllerlarda exception mapping
- Developmentda Swagger/OpenAPI
- React admin UI (protected route + toast xabarnoma)

## Texnologiyalar
### Backend
- .NET `net10.0`
- ASP.NET Core Web API
- Entity Framework Core `10.0.6`
- Npgsql EF Core Provider `10.0.1`
- JWT Bearer `10.0.7`
- MailKit `4.16.0`
- BCrypt.Net-Next `4.1.0`
- RESTFulSense `3.2.0`
- Xeption `2.8.0`

### Frontend
- React `19.2.5`
- React Router DOM `7.14.2`
- Vite `8.0.10`
- Axios `1.15.2`
- Bootstrap `5.3.8`
- React Toastify `11.1.0`

## Arxitektura
- **Controllers**: HTTP boundary va status mapping
- **Services**: biznes mantiq (Foundations/Processings/Orchestrations)
- **Brokers**: storage/email/hash/random/logging abstraksiyasi
- **Middlewares**: pipeline tekshiruvlari
- **Models**: entity, DTO, exceptionlar

Batafsil: [docs/architecture.md](docs/architecture.md).

## Loyiha tuzilmasi
```text
.
├── README.md
├── README.uz.md
├── docs/
├── Controllers/
├── Services/
├── Brokers/
├── Models/
├── Migrations/
└── frontend/
```

## Ishga tushirish
1. Repositoryni clone qiling
2. Rootda `appsettings.json` yarating
3. Backendni ishga tushiring
4. Frontendni ishga tushiring
5. Swagger/UI ni oching

## Tezkor start
```bash
git clone <REPO_URL>
cd task4-user-managment-

# Backend
# .NET o'rnatilgan bo'lsa
dotnet restore
dotnet run

# Frontend
cd frontend
npm install
npm run dev
```

Swagger: `https://localhost:<port>/swagger`

## Hujjatlar
- Arxitektura → [docs/architecture.md](docs/architecture.md)
- API hujjati → [docs/api.md](docs/api.md)
- Diagrammalar → [docs/diagrams.md](docs/diagrams.md)
- Testlash qo‘llanmasi → [docs/testing.md](docs/testing.md)
- Deploy qo‘llanmasi → [docs/deployment.md](docs/deployment.md)
- Muammolarni yechish → [docs/troubleshooting.md](docs/troubleshooting.md)

## Hissa qo'shish
1. `feature/<name>` branch oching
2. Kichik va aniq PR yuboring
3. Behavior o'zgarsa docs ni yangilang
4. Imkon bo'lsa test qo'shing
