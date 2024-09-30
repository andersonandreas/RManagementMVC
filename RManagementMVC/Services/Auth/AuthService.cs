using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RManagementMVC.Services.Auth;

public class AuthService(
	IHttpClientFactory httpClientFactory,
	IHttpContextAccessor httpContextAccessor,
	ICookieService cookieService) : IAuthService
{

	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private readonly ICookieService _cookieService = cookieService;




	public async Task<ApiLoginResult> LoginAsync(LoginViewModel loginViewModel)
	{

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.PostAsJsonAsync("/api/account/login", loginViewModel);

		if (!response.IsSuccessStatusCode)
		{
			return new ApiLoginResult { Message = "Failed to log in", Success = false };
		}

		var result = await response.Content.ReadFromJsonAsync<ApiLoginResult>();

		if (result?.Success != true || result.Token == null)
		{
			return new ApiLoginResult { Message = "Invalid login response", Success = false };
		}

		await SignInAsync(result.Token);

		return result;
	}


	public async Task LogoutAsync()
	{
		if (_httpContextAccessor.HttpContext != null)
		{
			await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			_cookieService.RemoveTokenCookie();
		}
	}


	private async Task SignInAsync(string token)
	{
		var jwtToken = ParseToken(token);
		var claimsPrincipal = CreateClaimsPrincipal(jwtToken);

		EnsureHttpContext();

		await SignInWithCookie(claimsPrincipal, jwtToken.ValidTo);
		_cookieService.SetTokenCookie(token, jwtToken.ValidTo);
	}


	private static JwtSecurityToken ParseToken(string token)
	{
		var handler = new JwtSecurityTokenHandler();

		return handler.ReadJwtToken(token);
	}


	private static ClaimsPrincipal CreateClaimsPrincipal(JwtSecurityToken jwtToken)
	{
		var claims = jwtToken.Claims.ToList();
		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		return new ClaimsPrincipal(claimsIdentity);
	}


	private void EnsureHttpContext()
	{
		if (_httpContextAccessor.HttpContext == null)
		{
			throw new InvalidOperationException("HttpContext is not available");
		}
	}


	private async Task SignInWithCookie(ClaimsPrincipal claimsPrincipal, DateTime tokenExpirs)
	{
		await _httpContextAccessor.HttpContext!.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			claimsPrincipal,
			new AuthenticationProperties
			{
				IsPersistent = true,
				ExpiresUtc = tokenExpirs
			});
	}
}

