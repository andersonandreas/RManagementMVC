using RManagementMVC.DTOs;
using RManagementMVC.Models.ViewModels;

namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ApiLoginResult> LoginAsync(LoginViewModel loginViewModel);
        Task LogoutAsync();
    }
}