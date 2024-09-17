namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface IJwtService
    {
        bool IsAdmin(string? token);
        bool IsTokenExpiring(string? token, int durationLimitMinutes);
    }
}