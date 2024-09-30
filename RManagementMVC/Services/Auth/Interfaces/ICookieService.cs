namespace RManagementMVC.Services.Auth.Interfaces
{
    public interface ICookieService
    {
        string? GetTokenFromCookie();
        void RemoveTokenCookie();
        void SetTokenCookie(string token, DateTime expires);
        bool TryGetTokenFromCookie(out string token);
    }
}