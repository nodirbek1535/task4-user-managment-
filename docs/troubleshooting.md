# Troubleshooting

## `401 Unauthorized` on protected calls
- Token may be expired or invalid.
- User may be blocked/deleted (middleware checks status).
- Re-login and retry.

## Email not delivered
- Verify `EmailSettings:From` and `EmailSettings:AppPassword`.
- Confirm SMTP access and app-password settings.
- Check spam/junk folder.

## DB connection errors
- Validate `ConnectionStrings:DefaultConnection`.
- Ensure PostgreSQL service is running.
- Confirm host/port/network reachability.

## Frontend points to wrong API
- Update `frontend/src/api/axiosClient.js` base URL.
