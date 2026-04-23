# User Management API

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Database](https://img.shields.io/badge/database-PostgreSQL-336791?logo=postgresql&logoColor=white)](#tech-stack)
[![Frontend](https://img.shields.io/badge/frontend-React%2019-61DAFB?logo=react&logoColor=black)](#tech-stack)
[![Build](https://img.shields.io/badge/build-local-9cf)](#quick-start)

A clean, production-oriented full-stack user management system with email verification, JWT auth, and admin bulk user operations.

## 🌐 Language
- 🇬🇧 **English** (current)
- 🇺🇿 **Uzbek** → [README.uz.md](README.uz.md)

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Architecture](#project-architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Quick Start](#quick-start)
- [Documentation](#documentation)
- [Contributing](#contributing)

## Overview
This project provides a complete user lifecycle:
- Registration with email confirmation token
- Email confirmation by token link
- JWT login/authentication
- User listing and bulk operations (block/unblock/delete)
- Middleware guard for blocked/deleted accounts

## Features
- Authentication endpoints (`register`, `confirm-email`, `login`)
- CRUD and bulk user operations
- PostgreSQL persistence with EF Core migrations
- Structured exception mapping in controllers
- Swagger/OpenAPI enabled in development
- React admin UI with protected routes and toast feedback

## Tech Stack
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

## Project Architecture
Layered structure:
- **Controllers**: HTTP boundary + status mapping
- **Services (Foundations/Processings/Orchestrations)**: business logic
- **Brokers**: infrastructure abstractions (storage/email/hash/random/logging)
- **Middlewares**: request pipeline checks
- **Models**: entities, DTOs, exceptions

For deeper details, see [docs/architecture.md](docs/architecture.md).

## Project Structure
```text
.
├── README.md
├── README.uz.md
├── docs/
│   ├── architecture.md
│   ├── api.md
│   ├── diagrams.md
│   ├── testing.md
│   ├── deployment.md
│   └── troubleshooting.md
├── Controllers/
├── Services/
├── Brokers/
├── Models/
├── Migrations/
└── frontend/
```

## Getting Started
1. Clone repository
2. Create backend `appsettings.json`
3. Run backend
4. Run frontend
5. Open Swagger and UI

## Quick Start
```bash
git clone <REPO_URL>
cd task4-user-managment-

# Backend
cat > appsettings.json <<'JSON'
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
JSON

# Start API
# (If .NET SDK is installed)
dotnet restore
dotnet run

# Frontend
cd frontend
npm install
npm run dev
```

Swagger: `https://localhost:<port>/swagger`

## Documentation
- Architecture → [docs/architecture.md](docs/architecture.md)
- API documentation → [docs/api.md](docs/api.md)
- Diagrams → [docs/diagrams.md](docs/diagrams.md)
- Testing guide → [docs/testing.md](docs/testing.md)
- Deployment guide → [docs/deployment.md](docs/deployment.md)
- Troubleshooting → [docs/troubleshooting.md](docs/troubleshooting.md)

## Contributing
1. Create a feature branch (`feature/<name>`)
2. Keep PRs small and focused
3. Update docs when behavior changes
4. Add or update tests where possible
