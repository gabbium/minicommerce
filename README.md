# MiniCommerce

![GitHub last commit](https://img.shields.io/github/last-commit/gabbium/minicommerce)
![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gabbium_minicommerce?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gabbium_minicommerce?server=https%3A%2F%2Fsonarcloud.io)

A modular **.NET 9** solution for e-commerce, featuring Clean Architecture, CQRS, and robust identity management.

---

## ✨ Features

-   ✅ Modular architecture (domain, application, infrastructure, web)
-   ✅ Identity & authentication (OpenID Connect, JWT)
-   ✅ CQRS patterns and behaviors
-   ✅ Automated CI/CD with semantic-release
-   ✅ Extensible and maintainable codebase

---

## 🧱 Tech Stack

| Layer   | Stack                                |
| ------- | ------------------------------------ |
| Runtime | .NET 9                               |
| Infra   | Docker + Docker Compose + PostgreSQL |
| CI/CD   | GitHub Actions + semantic-release    |

---

## 📦 Services

| Service  | Description                    | README                                   |
| -------- | ------------------------------ | ---------------------------------------- |
| Identity | Authentication & user context  | [identity/README.md](identity/README.md) |
| Catalog  | Product catalog CRUD & queries | [catalog/README.md](catalog/README.md)   |

---

## 📁 Structure

```
├─ services/
│  └─ identity/
│     ├─ src/
│     ├─ tests/
│     └─ README.md
│  └─ catalog/
│     ├─ src/
│     ├─ tests/
│     └─ README.md
├─ docker-compose.yml
└─ README.md
```

---

## ⚙️ Configuration

Global .env.example is provided at the root.
Each service may also use dotnet user-secrets when running locally in IDE.

---

## 📦 Getting Started

```bash
git clone https://github.com/gabbium/minicommerce.git
cd minicommerce
dotnet build
```

Start with Docker Compose:

```bash
docker compose up -d --build
```

---

## 🪪 License

This project is licensed under the MIT License – see [LICENSE](LICENSE) for details.
