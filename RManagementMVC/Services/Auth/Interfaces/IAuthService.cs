namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<bool> EnsureValidTokenAsync();
        bool IsAdmin();
        bool IsAuthenticated();
        void Logout();
    }
}