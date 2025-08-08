using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Web.Controllers;

/// <summary>
/// Controller to handle authentication actions.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	/// <summary>
	/// Initiates the Auth0 login process.
	/// </summary>
	/// <param name="returnUrl">The URL to return to after login.</param>
	/// <returns>A challenge result that redirects to Auth0.</returns>
	[HttpPost("login")]
	public IActionResult Login(string? returnUrl = null)
	{
		var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
			.WithRedirectUri(returnUrl ?? "/")
			.Build();

		return Challenge(authenticationProperties, Auth0Constants.AuthenticationScheme);
	}

	/// <summary>
	/// Handles logout and redirects to the home page.
	/// </summary>
	/// <returns>A sign out result that clears the session and redirects.</returns>
	[Authorize]
	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
			.WithRedirectUri("/")
			.Build();

		await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		await HttpContext.SignOutAsync();

		return Redirect("/");
	}
}