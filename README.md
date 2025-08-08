# BlazorApp Solution

## Overview

BlazorApp is a modern, secure, and scalable .NET 9 solution built with Blazor Server, .NET Aspire, and MongoDB. It demonstrates best practices in architecture, testing, and cloud-native development, including CQRS, Vertical Slice, and strong security defaults (Auth0, HTTPS, CORS, Antiforgery, secure headers).

---

## Solution Structure

```
BlazorApp.Web           -- Blazor Server UI (main entrypoint, interactive server rendering)
BlazorApp.AppHost       -- Aspire App Host (orchestration, resource wiring, environment config)
BlazorApp.ServiceDefaults -- Shared service defaults (OpenTelemetry, health checks, DI, resilience)
BlazorApp.Shared        -- Shared contracts, constants, and service/resource names
BlazorApp.Tests         -- Unit, integration, and architecture tests (xUnit, bUnit, Playwright)
```

---

## Key Technologies & Features

- **.NET 9** & **.NET Aspire** (cloud-native orchestration)
- **Blazor Server** (interactive, stream rendering, error boundaries)
- **MongoDB** (NoSQL data, async access)
- **Auth0** (authentication/authorization)
- **CQRS & Vertical Slice Architecture**
- **Dependency Injection** everywhere
- **OpenAPI/Swagger** for APIs
- **OpenTelemetry & Application Insights**
- **Distributed Caching** (Redis)
- **Output Caching**
- **Health Checks**
- **FluentValidation** for model validation
- **Unit, Integration, and Architecture Tests** (xUnit, bUnit, Playwright, TestContainers)

---

## Getting Started

### Auth0 Setup

This application uses Auth0 for authentication. To configure Auth0:

1. **Create an Auth0 Application**
   - Go to [Auth0 Dashboard](https://manage.auth0.com/)
   - Create a new "Regular Web Application"
   - Note your Domain and Client ID

2. **Configure Application Settings**
   - **Allowed Callback URLs**: `https://localhost:7039/callback`
   - **Allowed Logout URLs**: `https://localhost:7039/`
   - **Allowed Web Origins**: `https://localhost:7039`

3. **Set User Secrets**
   ```bash
   cd BlazorApp.AppHost
   dotnet user-secrets set "Parameters:auth0-domain" "your-domain.auth0.com"
   dotnet user-secrets set "Parameters:auth0-client-id" "your-client-id"
   ```

### Running the Application

1. **Requirements:** .NET 9 SDK, Docker (for MongoDB/Redis/TestContainers), Node.js (for Playwright tests)
2. **Run the App:**
   - `dotnet run --project BlazorApp.AppHost` (or use Visual Studio/Rider launch)
3. **Browse:** Navigate to the provided endpoint (see console output)
4. **Tests:**
   - `dotnet test` (runs all unit/integration/architecture tests)

---

## Contribution & Documentation

- [Code of Conduct](./CODE_OF_CONDUCT.md)
- [Contributing Guide](./docs/CONTRIBUTING.md)
- [Architecture & Usage Docs](./docs/README.md)

---

## Software References

- .NET 9, .NET Aspire
- Blazor Server, C#, TailwindCSS
- MongoDB, Redis
- Auth0

---

## License

See [LICENSE](./LICENSE)