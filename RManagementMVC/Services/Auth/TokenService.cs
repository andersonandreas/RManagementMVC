using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
{

	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	private const string _tokenKey = "token";
	private const string _refreshToken = "refreshToken";


	public string? GetToken()
	{
		return _httpContextAccessor.HttpContext?.Request.Cookies[_tokenKey];
	}


	public string? GetRefreshToken()
	{
		return _httpContextAccessor?.HttpContext?.Request.Cookies[_refreshToken];
	}


	public void SetToken(string token)
	{
		var cookieOpts = new CookieOptions
		{
			HttpOnly = true,
			Expires = DateTime.UtcNow.AddDays(2),
			Secure = true,
			SameSite = SameSiteMode.Strict
		};

		_httpContextAccessor?.HttpContext?.Response.Cookies.Append(_tokenKey, token, cookieOpts);
	}


	public void SetRefreshToken(string refreshToken)
	{
		var cookieOpts = new CookieOptions
		{
			HttpOnly = true,
			Expires = DateTime.UtcNow.AddDays(10),
			Secure = true,
			SameSite = SameSiteMode.Strict
		};

		_httpContextAccessor?.HttpContext?.Response.Cookies.Append(_refreshToken, refreshToken, cookieOpts);
	}


	public void RemoveToken()
	{
		_httpContextAccessor?.HttpContext?.Response.Cookies.Delete(_tokenKey);
	}


	public void RemoveRefreshToken()
	{
		_httpContextAccessor?.HttpContext?.Response.Cookies.Delete(_refreshToken);
	}


}
