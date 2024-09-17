namespace RManagementMVC.DTOs;

public record LoginResult
{
    public bool Succes { get; set; }
    public string Message { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

}
