using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class AuthApiClient(HttpClient httpClient, IConfiguration configuration) : IAuthApiClient
{

	private readonly HttpClient _httpClient = httpClient;

	private string _baseUrl = configuration["ApiSettings:BaseUrl"]
		?? throw new ApplicationException("Check baseUrl in appsettings");



	public async Task<LoginResult> LoginAsync(LoginViewModel loginViewModel)
	{
		var response = await _httpClient.PatchAsJsonAsync(
			$"{_baseUrl}/api/identity/login", loginViewModel);

		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadFromJsonAsync<LoginResult>();

			if (result != null)
			{
				result.Succes = true;
				return result;
			}
		}

		return new LoginResult
		{
			Succes = false,
			Message = $"Login failed with status code: {response.StatusCode}"
		};
	}


	public async Task<LoginResult> RefreshTokenAsync(string refreshToken)
	{

		var response = await _httpClient.PostAsJsonAsync(
			$"{_baseUrl}/api/identity/refresh", new { RefreshToken = refreshToken });

		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadFromJsonAsync<LoginResult>();
			if (result != null)
			{
				result.Succes = true;
				return result;
			}
		}

		return new LoginResult
		{
			Succes = false,
			Message = $"Token refresh failed with status code: {response.StatusCode}"
		};
	}


}


