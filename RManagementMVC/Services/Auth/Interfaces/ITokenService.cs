namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        string? GetRefreshToken();
        string? GetToken();
        void RemoveRefreshToken();
        void RemoveToken();
        void SetRefreshToken(string refreshToken);
        void SetToken(string token);
    }
}