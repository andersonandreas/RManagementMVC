using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class AuthService(
	ITokenService tokenService,
	IAuthApiClient authApiClient,
	AdminStateService adminStateService) : IAuthService
{

	private readonly ITokenService _tokenService = tokenService;
	private readonly IAuthApiClient _authApiClient = authApiClient;
	private readonly AdminStateService _adminStateService = adminStateService;



	public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
	{
		var result = await _authApiClient.LoginAsync(loginViewModel);
		if (result.Success)
		{
			_tokenService.SetTokens(result.AccessToken, result.RefreshToken);

			var isAdmin = await _authApiClient.ValidateAdminTokenAsync(result.AccessToken);
			_adminStateService.SetAdminLoggedIn(isAdmin);

			return true;
		}

		return false;
	}


	public void Logout()
	{
		_tokenService.ClearTokens();
		_adminStateService.SetAdminLoggedIn(false);
	}


	public bool IsAuthenticated() => !string.IsNullOrEmpty(_tokenService.GetAccessToken());
	public bool IsAdmin() => _adminStateService.IsAdminLoggedIn();
	public bool EnsureValidToken() => !string.IsNullOrEmpty(_tokenService.GetAccessToken());

}











