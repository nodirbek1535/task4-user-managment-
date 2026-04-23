# Deployment Guide

## Backend
1. Provide connection string and JWT/email secrets via env vars or secret manager.
2. Ensure PostgreSQL is reachable.
3. Start API process (container or app service).
4. Verify migrations complete successfully at startup.

## Frontend
1. Build static assets:
   ```bash
   cd frontend
   npm run build
   ```
2. Serve `frontend/dist` using static hosting.
3. Point API base URL to deployed backend.

## Production hardening checklist
- Restrict CORS to known frontend domains
- Rotate JWT secret regularly
- Move hardcoded URLs to configuration
- Enable observability (structured logs + metrics)
