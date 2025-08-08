# BlazorApp Copilot Instructions

**ALWAYS follow these instructions first and only fallback to additional search and context gathering if the information here is incomplete or found to be in error.**

BlazorApp is a .NET 9 Blazor Server application with .NET Aspire orchestration, MongoDB, Redis, and Auth0 authentication. The solution demonstrates modern cloud-native development practices including CQRS, Vertical Slice Architecture, and comprehensive testing.

## Working Effectively

### Prerequisites and Setup
**Install .NET 9.0.301 SDK (REQUIRED):**
```bash
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 9.0.301
export PATH="$HOME/.dotnet:$PATH"
export DOTNET_ROOT="$HOME/.dotnet"
```
- Installation takes 2-3 minutes. NEVER CANCEL.
- Verify with: `dotnet --version` (should show 9.0.301)

**Verify Docker availability:**
```bash
docker --version
```
- Required for Aspire orchestration (MongoDB, Redis containers)

### Build and Test Commands

**Restore dependencies:**
```bash
dotnet restore BlazorApp.sln
```
- Takes ~60 seconds. NEVER CANCEL. Set timeout to 120+ seconds.
- Downloads all NuGet packages and sets up project dependencies

**Build the solution:**
```bash
dotnet build BlazorApp.sln --no-restore
```
- Takes ~15 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- Compiles all 5 projects in dependency order

**Run tests (LIMITED SUCCESS - see Validation section):**
```bash
dotnet test BlazorApp.sln --no-build --no-restore
```
- Integration tests WILL FAIL due to Aspire infrastructure requirements
- Takes ~25 seconds before timeout. NEVER CANCEL. Set timeout to 120+ seconds.
- Unit tests would pass but integration tests require MongoDB/Redis containers

### Running the Application

**Run Web application directly (RECOMMENDED for development):**
```bash
dotnet run --project BlazorApp.Web
```
- Starts in ~2 seconds
- Runs on http://localhost:5229 (check console output for exact port)
- This bypasses Aspire orchestration but runs the core Blazor app

**Run with Aspire orchestration (INFRASTRUCTURE DEPENDENT):**
```bash
dotnet run --project BlazorApp.AppHost
```
- Requires DCP (Distributed application Control Plane) and container runtime
- WILL FAIL in constrained environments without proper Docker setup
- Takes several minutes and may timeout. NEVER CANCEL. Set timeout to 300+ seconds.
- Only attempt if full Docker/container environment is confirmed working

## Validation

**ALWAYS manually validate any changes with these steps:**

1. **Build validation:**
   ```bash
   dotnet build BlazorApp.sln --no-restore
   ```
   - Must complete without errors

2. **Web application functionality:**
   ```bash
   # Start the app (in background or separate terminal)
   dotnet run --project BlazorApp.Web &
   
   # Wait 5 seconds for startup
   sleep 5
   
   # Test HTTP response
   curl -s -o /dev/null -w "%{http_code}" http://localhost:5229/
   ```
   - Should return `200`
   - Application should serve Blazor HTML content

3. **Verify application content:**
   ```bash
   curl -s http://localhost:5229/ | head -5
   ```
   - Should return HTML with DOCTYPE, Blazor components, and CSS references

## Repository Structure

**File structure overview:**
```
BlazorApp.sln                    -- Main solution file
BlazorApp.Web/                   -- Blazor Server UI (main application)
BlazorApp.AppHost/               -- Aspire orchestration host  
BlazorApp.ServiceDefaults/       -- Shared service configurations
BlazorApp.Shared/                -- Shared contracts and constants
BlazorApp.Tests/                 -- Unit and integration tests
global.json                      -- .NET SDK version specification (9.0.300)
.github/workflows/dotnet.yml     -- CI/CD pipeline
```

**Key project purposes:**
- **BlazorApp.Web**: Main Blazor Server application with interactive components
- **BlazorApp.AppHost**: Aspire host for orchestrating MongoDB, Redis, and Web app
- **BlazorApp.ServiceDefaults**: OpenTelemetry, health checks, and DI configuration
- **BlazorApp.Shared**: Service names, constants, and shared contracts
- **BlazorApp.Tests**: xUnit tests including Aspire integration tests

## Known Issues and Limitations

**Integration tests fail without infrastructure:**
- Tests in `BlazorApp.Tests/WebTests.cs` require running MongoDB and Redis containers
- Error: "The operation didn't complete within the allowed timeout of '00:00:20'"
- This is EXPECTED behavior in environments without full container orchestration

**Aspire AppHost requires container runtime:**
- `dotnet run --project BlazorApp.AppHost` fails without proper DCP setup
- Error messages about Kubernetes endpoints are NORMAL in constrained environments
- Use direct Web project execution for development and testing

**Missing frontend build process:**
- GitHub workflow references `npm install` and `npm run build:css` but no package.json exists
- TailwindCSS compilation may be missing in current repository state
- Web application still functions with existing CSS files

**Pre-commit validation commands:**
```bash
dotnet build BlazorApp.sln --no-restore
dotnet run --project BlazorApp.Web &
sleep 5
curl -s -o /dev/null -w "%{http_code}" http://localhost:5229/
pkill -f "BlazorApp.Web"
```

---

## Coding Standards and Architecture Requirements

These standards define the required coding, architecture, and project rules for all .NET code in this repository. For more details, see [CONTRIBUTING.md](../docs/CONTRIBUTING.md).

---

## C# (Required)

### Style

- **Use .editorconfig:** `true`
- **Preferred Modifier Order:** `public`, `private`, `protected`, `internal`, `static`, `readonly`, `const`
  - _Example:_
    ```csharp
    public static readonly int MY_CONST = 42;
    ```
- **Use Explicit Type:** `true` (except where `var` improves clarity)
- **Use Var:** `true` (when the type is obvious)
- **Prefer Null Check:**
  - Use `is null`: `true`
  - Use `is not null`: `true`
- **Prefer Primary Constructors:** `false`
- **Prefer Records:** `true`
- **Prefer Minimal APIs:** `true`
- **Prefer File Scoped Namespaces:** `true`
- **Use Global Usings:** `true` (see `GlobalUsings.cs` in each project)
- **Use Nullable Reference Types:** `true`
- **Use Pattern Matching:** `true`
- **Max Line Length:** `120`
- **Indent Style:** `tab`
- **Indent Size:** `2`
- **End of Line:** `lf`
- **Trim Trailing Whitespace:** `true`
- **Insert Final Newline:** `true`
- **Charset:** `utf-8`

### Naming

- **Interface Prefix:** `I` (e.g., `IService`)
- **Async Suffix:** `Async` (e.g., `GetDataAsync`)
- **Private Field Prefix:** `_` (e.g., `_myField`)
- **Constant Case:** `UPPER_CASE` (e.g., `MAX_SIZE`)
- **Component Suffix:** `Component` (for Blazor components)
- **Blazor Page Suffix:** `Page` (for Blazor pages)

### Security (Required)

- **Require HTTPS:** `true` (see `Web/Program.cs`)
- **Require Authentication:** `true` (Auth0 integration, see `README.md`)
- **Require Authorization:** `true`
- **Use Antiforgery Tokens:** `true` (see `Web/Program.cs`)
- **Use CORS:** `true`
- **Use Secure Headers:** `true`

### Architecture (Required)

- **Enforce SOLID:** `true` (see `Domain/`, `ServiceDefaults/`)
- **Enforce Dependency Injection:** `true` (see `Web/Program.cs`, `ServiceDefaults/`)
- **Enforce Async/Await:** `true` (async methods and tests)
- **Enforce Strongly Typed Config:** `true`
- **Enforce CQRS:** `true` (see `Domain/Abstractions/`, `MyMediator/`)
- **Enforce Unit Tests:** `true` (see `Tests/`)
- **Enforce Integration Tests:** `true` (see `Tests/`)
- **Enforce Architecture Tests:** `true` (see `Tests/Architecture.Tests/`)
- **Enforce Vertical Slice Architecture:** `true`
- **Enforce Aspire:** `true` (see `AppHost/`, `README.md`)
- **Centralize NuGet Package Versions:** `true` (all package versions must be managed in `Directory.Packages.props` at the repo root; do not specify versions in individual project files)

### Blazor (Required)

- **Enforce State Management:** `true` (see use of `@code` blocks and parameters)
- **Use Interactive Server Rendering:** `true` (see `Web/Program.cs`)
- **Use Stream Rendering:** `true`
- **Enforce Component Lifecycle:** `true` (see `OnInitialized`, `OnParametersSet` in components)
- **Use Cascading Parameters:** `true` (see shared layout/components)
- **Use Render Fragments:** `true` (see component parameters)
- **Use Virtualization:** `true` (see use of `Web.Virtualization`)
- **Use Error Boundaries:** `true` (see `MainLayout.razor` error UI)
- **Component Suffix:** `Component` (e.g., `FooterComponent`)
- **Page Suffix:** `Page` (e.g., `AboutPage`)

### Documentation (Required)

- **Require XML Docs:** `true` (see `<summary>` in test and code files)
- **Require Swagger:** `true` (for REST APIs)
- **Require OpenAPI:** `true` (OpenAPI/Swagger must be provided for all APIs)
- **Require Component Documentation:** `true` (see `<summary>` in Blazor tests)
- **Require README:** `true` (see `README.md`, `docs/README.md`)
- **Require CONTRIBUTING.md:** `true` (see `docs/CONTRIBUTING.md`)
- **Require LICENSE:** `true` (see `LICENSE`)
- **Require Code of Conduct:** `true` (see `CODE_OF_CONDUCT.md`)
- **Require File Copyright Headers:** `true`

### Logging (Required)

- **Require Structured Logging:** `true`
- **Require Health Checks:** `true`
- **Use OpenTelemetry:** `true`
- **Use Application Insights:** `true`

### Database (Required)

- **Use Entity Framework Core:** `true`
- **Use MongoDB:** `true` (see `Persistence.MongoDb/`)
- **Prefer Async Operations:** `true`
- **Use Migrations:** `false` (for MongoDB)
- **Use TestContainers:** `true` (for Integration testing, see `Tests/TailwindBlog.Persistence.MongoDb.Tests.Integration/`)
- **Use Change Tracking:** `true`
- **Use DbContext Pooling:** `true`
- **Use In-Memory Database:** `false`

### Versioning (Required)

- **Require API Versioning:** `true`
- **Use Semantic Versioning:** `true`

### Caching (Required)

- **Require Caching Strategy:** `true`
- **Use Distributed Cache:** `true`
- **Use Output Caching:** `true` (see `Web/Program.cs`)

### Middleware (Required)

- **Require Cross-Cutting Concerns:** `true`
- **Use Exception Handling:** `true` (see `Web/Program.cs`)
- **Use Request Logging:** `true`

### Background Services (Required)

- **Require Background Service:** `true`

### Environment (Required)

- **Require Environment Config:** `true`
- **Use User Secrets:** `true`
- **Use Key Vault:** `true`

### Validation (Required)

- **Require Model Validation:** `true`
- **Use FluentValidation:** `true`

### Testing (Required)

- **Require Unit Tests:** `true` (see `Tests/`)
- **Require Integration Tests:** `true` (see `Tests/`)
- **Require Architecture Tests:** `true` (see `Tests/Architecture.Tests/`)
- **Use xUnit:** `true` (see `Tests/`)
- **Use FluentAssertions:** `true` (see `Tests/`)
- **Use NSubstitute:** `true`
- **Use bUnit:** `true` (see `Tests/Web.Tests.Bunit/`)
- **Use Playwright:** `true` (see `README.md`)

---

**Note:** These rules are enforced via `.editorconfig`, StyleCop, and other tooling where possible. For questions or clarifications, see [CONTRIBUTING.md](../docs/CONTRIBUTING.md).
