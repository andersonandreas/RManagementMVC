namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        void ClearTokens();
        string? GetAccessToken();
        string? GetRefreshToken();
        void SetTokens(string accessToken, string refreshToken);
    }
}