# BlazorApp.ServiceDefaults

This project provides shared service defaults for all services in the solution. It centralizes configuration for OpenTelemetry, health checks, dependency injection, service discovery, and resilience.

## Key Features
- Adds OpenTelemetry tracing and metrics
- Configures health checks and liveness endpoints
- Sets up service discovery and HTTP client resilience
- Used as a shared reference by all service projects

## Usage
Reference this project from other projects and call `AddServiceDefaults()` in your service configuration.

---

See the root README.md for solution-wide details.