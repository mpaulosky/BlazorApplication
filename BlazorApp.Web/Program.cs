using Auth0.AspNetCore.Authentication;
using BlazorApp.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Configure Auth0
builder.Services
	.AddAuth0WebAppAuthentication(options =>
	{
		options.Domain = builder.Configuration["auth0-domain"] ?? throw new ArgumentNullException("auth0-domain", "Auth0 domain is required");
		options.ClientId = builder.Configuration["auth0-client-id"] ?? throw new ArgumentNullException("auth0-client-id", "Auth0 client ID is required");
		options.Scope = "openid profile email";
	});

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();

// Add authorization
builder.Services.AddAuthorization();

// Add controllers for auth endpoints
builder.Services.AddControllers();

builder.Services.AddOutputCache();

// Add Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
