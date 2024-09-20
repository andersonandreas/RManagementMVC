using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using System.Net.Http.Headers;

namespace RManagementMVC.Services.Auth;

public class AuthApiClient(IHttpClientFactory httpClientFactory, ITokenService tokenService) : IAuthApiClient
{

	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
	private readonly ITokenService _tokenService = tokenService;



	public async Task<LoginResult> LoginAsync(LoginViewModel loginViewModel)
	{

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.PostAsJsonAsync("api/identity/login", loginViewModel);

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
		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		var response = await client.GetAsync("api/identity/validateAdmin");

		return response.IsSuccessStatusCode;
	}



}


