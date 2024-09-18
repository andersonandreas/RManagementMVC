using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using System.Net.Http.Headers;

namespace RManagementMVC.Services.Auth;

public class AuthApiClient(HttpClient httpClient, ITokenService tokenService) : IAuthApiClient
{

	private readonly HttpClient _httpClient = httpClient;
	private readonly ITokenService _tokenService = tokenService;



	public async Task<LoginResult> LoginAsync(LoginViewModel loginViewModel)
	{
		var response = await _httpClient.PostAsJsonAsync("api/identity/login", loginViewModel);

		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadFromJsonAsync<LoginResult>();

			if (result != null)
			{
				result.Success = true;
				return result;
			}
		}

		return new LoginResult { Success = false };
	}


	public async Task<bool> ValidateAdminTokenAsync(string token)
	{
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		var response = await _httpClient.GetAsync("api/identity/validateAdmin");

		return response.IsSuccessStatusCode;
	}



}


