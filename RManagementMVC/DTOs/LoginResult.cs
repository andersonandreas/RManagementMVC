namespace RManagementMVC.DTOs;

public record LoginResult
{
	public bool Success { get; set; }
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;

}
