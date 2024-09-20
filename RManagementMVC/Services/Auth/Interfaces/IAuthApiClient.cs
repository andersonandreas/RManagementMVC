using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;

namespace RManagementMVC.Services.Auth.Interfaces;

public interface IAuthApiClient
{
	Task<LoginResult> LoginAsync(LoginViewModel loginViewModel);
	Task<bool> ValidateAdminTokenAsync(string token);
}