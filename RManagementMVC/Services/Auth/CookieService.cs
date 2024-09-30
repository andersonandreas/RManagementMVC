using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class CookieService(IHttpContextAccessor httpContextAccessor) : ICookieService
{

	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private const string _tokenCookieName = "AuthToken";
	private const string _messageException = "HttpContext is not available";


	public void SetTokenCookie(string token, DateTime expires)
	{
		var httpContext = _httpContextAccessor.HttpContext
			?? throw new InvalidOperationException(_messageException);

		httpContext.Response.Cookies.Append(
			_tokenCookieName,
			token,
			new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = expires
			});
	}


	public string? GetTokenFromCookie()
	{
		var httpContext = _httpContextAccessor.HttpContext
			?? throw new InvalidOperationException(_messageException);

		return httpContext.Request.Cookies
				.TryGetValue(_tokenCookieName, out var token) ? token : null;
	}


	public bool TryGetTokenFromCookie(out string token)
	{

		if (_httpContextAccessor.HttpContext?.Request.Cookies[_tokenCookieName] is string cookieValue)
		{
			token = cookieValue;

			return true;
		}

		token = string.Empty;

		return false;
	}


	public void RemoveTokenCookie()
	{
		var httpContext = _httpContextAccessor.HttpContext
			?? throw new InvalidOperationException(_messageException);

		httpContext.Response.Cookies.Delete(_tokenCookieName);
	}

}
