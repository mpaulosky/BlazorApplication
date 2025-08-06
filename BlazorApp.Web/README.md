# BlazorApp.Web

This project is the main Blazor Server UI for the solution. It provides interactive server-side rendering, state management, and secure endpoints for the application.

## Key Features
- Blazor Server with interactive and stream rendering
- Auth0 authentication and authorization
- Antiforgery, HTTPS, CORS, and secure headers
- Output caching and distributed cache integration
- Health checks and error boundaries
- Component-based architecture (Component/Page suffixes)
- Follows CQRS and Vertical Slice patterns

## Entry Point
- `Program.cs` configures all middleware, services, and endpoints

## Usage
Run as part of the Aspire AppHost or directly for development:
```
dotnet run --project BlazorApp.Web
```

---

See the root README.md for solution-wide details.