using RManagementMVC.Models.ViewModels;

namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        bool EnsureValidToken();
        bool IsAdmin();
        bool IsAuthenticated();
        Task<bool> LoginAsync(LoginViewModel loginViewModel);
        void Logout();
    }
}