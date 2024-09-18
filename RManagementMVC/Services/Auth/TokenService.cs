using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
{

	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private const string _accessTokenKey = "AccessToken";
	private const string _refreshTokenKey = "RefreshToken";


	public string? GetAccessToken() => _httpContextAccessor.HttpContext?.Request.Cookies[_accessTokenKey];
	public string? GetRefreshToken() => _httpContextAccessor.HttpContext?.Request.Cookies[_refreshTokenKey];


	public void SetTokens(string accessToken, string refreshToken)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			SameSite = SameSiteMode.Strict
		};

		_httpContextAccessor.HttpContext?.Response.Cookies.Append(_accessTokenKey, accessToken, cookieOptions);
		_httpContextAccessor.HttpContext?.Response.Cookies.Append(_refreshTokenKey, refreshToken, cookieOptions);
	}


	public void ClearTokens()
	{
		_httpContextAccessor.HttpContext?.Response.Cookies.Delete(_accessTokenKey);
		_httpContextAccessor.HttpContext?.Response.Cookies.Delete(_refreshTokenKey);
	}


}
