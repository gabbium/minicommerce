# Identity Service

Authentication and user context service for MiniCommerce.

## 📚 Stack

-   .NET (Minimal APIs)
-   EF Core + PostgreSQL

## ⚙️ Configuration

The service requires a connection string and a JWT secret.  
They can be provided either via `dotnet user-secrets`:

```bash
dotnet user-secrets set "CONNECTIONSTRINGS:DEFAULTCONNECTION" "Host=localhost;Database=identity;Username=postgres;Password=postgres"
dotnet user-secrets set "JWT:SECRET" "super_secret_key"
```

## 🐳 Running with Docker

```bash
docker compose up -d --build identity
```

The service automatically applies EF Core migrations at startup.

## 🌱 Seeding

Admin user (admin@minicommerce.com) can be seeded automatically at startup (only if not existing).

## 🔍 Health & Observability (suggested)

-   `GET /health` (with Postgres check)
-   Structured logging (Serilog)
-   Correlation ID via `X-Correlation-ID`

## 🧪 Testing

Test projects are organized by layer:

-   `MiniCommerce.Identity.Application.UnitTests`
-   `MiniCommerce.Identity.Domain.UnitTests`
-   `MiniCommerce.Identity.Infrastructure.IntegrationTests`
-   `MiniCommerce.Identity.Web.AcceptanceTests`

```bash
dotnet test
```

## 🧩 Endpoints (MVP)

| Method | Route    | Auth | Description                                    |
| ------ | -------- | ---- | ---------------------------------------------- |
| `POST` | `/login` | ❌   | Authenticate and receive JWT                   |
| `GET`  | `/me`    | ✅   | Returns `Id` and `Email` of authenticated user |

---

## 🪪 License

This project is licensed under the MIT License – see [LICENSE](../LICENSE) for details.
