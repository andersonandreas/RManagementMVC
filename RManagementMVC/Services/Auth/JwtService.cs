using RManagementMVC.Services.Auth.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace RManagementMVC.Services.Auth;

public class JwtService : IJwtService
{

	public bool IsAdmin(string? token)
	{

		if (string.IsNullOrEmpty(token))
		{
			return false;
		}

		var handler = new JwtSecurityTokenHandler();
		var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

		return jsonToken?.Claims
			.Any(c => c.Type == "role" && c.Value == "Admin") ?? false; ;
	}


	public bool IsTokenExpiring(string? token, int durationLimitMinutes)
	{
		if (string.IsNullOrEmpty(token))
		{
			return false;
		}

		var handler = new JwtSecurityTokenHandler();
		var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

		return jsonToken?.ValidTo <= DateTime.UtcNow.AddMinutes(durationLimitMinutes);
	}

}
