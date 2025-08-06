// AppHost.cs: Configures distributed application resources for BlazorApp.
// Resource names are provided via global usings from BlazorApp.Shared.Services.

using AppHost;

var builder = DistributedApplication.CreateBuilder(args);

// Define constants for resources
var auth0Domain = builder.AddParameter("auth0-domain", secret: true)
		.WithDescription("The Auth0 domain for authentication.");

var auth0Client = builder.AddParameter("auth0-client-id", secret: true)
		.WithDescription("The Auth0 client ID for authentication.");

var database = builder.AddMongoDbServices();

var cache = builder.AddRedis(CACHE)
		.WithClearCommand()
		.WithLifetime(ContainerLifetime.Persistent);

// Add a composite command that coordinates multiple operations
var api = builder.AddProject<Projects.BlazorApp_Web>(WEBSITE)
		.WithExternalHttpEndpoints()
		.WithHttpHealthCheck("/health")
		.WithHttpHealthCheck("/redis-health")
		.WithReference(database).WaitFor(database)
		.WithReference(cache).WaitFor(cache)
		.WithEnvironment("auth0-domain", auth0Domain)
		.WithEnvironment("auth0-client-id", auth0Client);

builder.Build().Run();