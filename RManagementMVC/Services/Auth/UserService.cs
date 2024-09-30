using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Services.Auth;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{

	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


	public bool IsAdminLoggedIn()
	{
		var user = _httpContextAccessor.HttpContext?.User;

		return user?.Identity?.IsAuthenticated == true && user.IsInRole("Admin");
	}
}
