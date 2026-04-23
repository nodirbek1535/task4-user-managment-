# Architecture

## Overview
The solution follows layered boundaries to isolate transport, domain logic, and infrastructure.

## Layers
- **Controllers** (`Controllers/`)
  - Expose HTTP endpoints and map exceptions to status codes.
- **Orchestrations** (`Services/Orchestrations/`)
  - Coordinate multi-step workflows (e.g., auth).
- **Processings** (`Services/Processings/`)
  - Business workflows like bulk user operations.
- **Foundations** (`Services/Foundations/`)
  - Core domain services: users, security, email.
- **Brokers** (`Brokers/`)
  - External integrations: DB, SMTP, hashing, random, logging.
- **Models** (`Models/`)
  - Entity models, request/response contracts, domain exceptions.
- **Middlewares** (`Middlewares/`)
  - Cross-cutting request checks (`UserStatusCheckMiddleware`).

## Request flow example
1. `AuthController.RegisterAsync` receives request.
2. `AuthService` validates + creates user.
3. `UserService` writes via `StorageBroker`.
4. `EmailService` sends confirmation via `EmailBroker`.
5. HTTP response returned.

## Notes
- Startup applies EF migrations automatically (`context.Database.Migrate()`).
- CORS policy is open by default and should be restricted in production.
