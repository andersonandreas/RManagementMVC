
using RManagementMVC.Services.Auth.Interfaces;
using System.Net.Http.Headers;

namespace RManagementMVC.Services.TokenHandler;

public class BearerTokenHandler(ICookieService cookieService) : DelegatingHandler
{

	private readonly ICookieService _cookieService = cookieService;
	private const string _tokenCookieName = "AuthToken";



	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{

		if (_cookieService.TryGetTokenFromCookie(out var token))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		return base.SendAsync(request, cancellationToken);
	}


}
