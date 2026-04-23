# API Documentation

Base URL: `/api`

## Auth
### POST `/auth/register`
Request:
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "P@ssw0rd123"
}
```
Response (`200`):
```json
{
  "id": "guid",
  "name": "John Doe",
  "email": "john@example.com"
}
```

### GET `/auth/confirm-email?token={token}`
Confirms registration email token.

### POST `/auth/login`
Request:
```json
{
  "email": "john@example.com",
  "password": "P@ssw0rd123"
}
```
Response (`200`):
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

## User
### GET `/user`
Returns all users.

### GET `/user/{userId}`
Returns one user.

### POST `/user`
Creates user.

### PUT `/user`
Updates user.

### DELETE `/user/{userId}`
Deletes user by ID.

### PATCH `/user/block`
Body: `string[]` GUID list.

### PATCH `/user/unblock`
Body: `string[]` GUID list.

### DELETE `/user/bulk`
Body: `string[]` GUID list.

### DELETE `/user/unverified`
Body: `string[]` GUID list.

## Error mapping
- `400` validation issues
- `404` missing resources
- `409` duplicate resource conflicts
- `500` service/dependency failures
