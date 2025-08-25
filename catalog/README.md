# Catalog Service

Product catalog service for **MiniCommerce**.  
Provides CRUD operations and product queries, following CQRS and Clean Architecture patterns.

## 📚 Stack

-   .NET (Minimal APIs)
-   EF Core + PostgreSQL

## ⚙️ Configuration

The service requires a connection string.  
It can be provided via `dotnet user-secrets`:

```bash
dotnet user-secrets set "CONNECTIONSTRINGS:DEFAULTCONNECTION" "Host=localhost;Database=catalog;Username=postgres;Password=postgres"
dotnet user-secrets set "JWT:SECRET" "super_secret_key"
```

## 🐳 Running with Docker

```bash
docker compose up -d --build catalog
```

The service automatically applies EF Core migrations at startup.

## 🔍 Health & Observability (suggested)

-   `GET /health` (with Postgres check)
-   Structured logging (Serilog)
-   Correlation ID via `X-Correlation-ID`

## 🧪 Testing

Test projects are organized by type:

-   `MiniCommerce.Catalog.Application.UnitTests`
-   `MiniCommerce.Catalog.Domain.UnitTests`
-   `MiniCommerce.Catalog.Infrastructure.IntegrationTests`
-   `MiniCommerce.Catalog.Web.AcceptanceTests`

```bash
dotnet test
```

## 🧩 Endpoints (MVP)

| Method   | Route            | Auth | Description                        |
| -------- | ---------------- | ---- | ---------------------------------- |
| `POST`   | `/products`      | ✅   | Create a new product               |
| `GET`    | `/products`      | ✅   | List products (paged)              |
| `GET`    | `/products/{id}` | ✅   | Get product by Id                  |
| `PUT`    | `/products/{id}` | ✅   | Update product (name, price, etc.) |
| `DELETE` | `/products/{id}` | ✅   | Delete product                     |

---

## 🪪 License

This project is licensed under the MIT License – see [LICENSE](../LICENSE) for details.
