using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class AuthService(
	ITokenService tokenService,
	IJwtService jwtService,
	IAuthApiClient authApiClient) : IAuthService
{

	private const int _tokenRefreshMinutes = 5;


	public bool IsAuthenticated()
	{
		return !string.IsNullOrEmpty(tokenService.GetToken());
	}


	public void Logout()
	{
		tokenService.RemoveToken();
		tokenService.RemoveRefreshToken();
	}


	public bool IsAdmin()
	{
		return jwtService.IsAdmin(tokenService.GetToken());
	}


	public async Task<bool> EnsureValidTokenAsync()
	{
		var token = tokenService.GetToken();

		if (string.IsNullOrEmpty(token))
		{
			return false;
		}

		if (jwtService.IsTokenExpiring(token, _tokenRefreshMinutes))
		{
			return true;
		}


		return await RefreshTokenAsync();
	}


	private async Task<bool> RefreshTokenAsync()
	{

		var refreshToken = tokenService.GetRefreshToken();

		if (string.IsNullOrEmpty(refreshToken))
		{
			return false;
		}

		var result = await authApiClient.RefreshTokenAsync(refreshToken);

		if (!result.Succes || string.IsNullOrEmpty(result.Token))
		{
			return false;
		}

		tokenService.SetToken(result.Token);
		tokenService.SetRefreshToken(refreshToken);

		return true;
	}












}