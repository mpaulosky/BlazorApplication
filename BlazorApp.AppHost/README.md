# BlazorApp.AppHost

This project is the Aspire App Host for orchestrating the distributed application. It configures and wires up all resources, environment variables, and service dependencies for local development and cloud-native scenarios.

## Key Features
- Orchestrates MongoDB, Redis, Auth0, and the Blazor Web frontend
- Sets up environment config and resource references
- Provides health checks and readiness endpoints
- Entry point for running the full solution locally

## Usage
```
dotnet run --project BlazorApp.AppHost
```

---

See the root README.md for solution-wide details.