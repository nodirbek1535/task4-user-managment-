# Testing

## Available checks
### Frontend
```bash
cd frontend
npm run build
npm run lint
```

### Backend
```bash
dotnet build
```

> In this environment, `dotnet` CLI may be unavailable. Run backend checks locally or in CI with .NET SDK installed.

## Recommended additions
- Unit tests for services and validations
- Integration tests for controllers
- Contract tests for frontend API usage
