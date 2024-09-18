using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Controllers;

public class AccountController(IAuthService authService) : Controller
{

	private readonly IAuthService _authService = authService;



	public IActionResult Login()
	{
		if (_authService.IsAuthenticated() && _authService.IsAdmin())
		{
			return RedirectToAction("Panel", "Admin");
		}

		return View();
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		if (ModelState.IsValid)
		{
			var result = await _authService.LoginAsync(loginViewModel);
			if (result && _authService.IsAdmin())
			{
				return RedirectToAction("Panel", "Admin");
			}

			ModelState.AddModelError(string.Empty, "Invalid login attempt or not an admin.");
		}

		return View(loginViewModel);
	}


	public IActionResult Logout()
	{
		_authService.Logout();
		return RedirectToAction("Index", "Home");
	}


}
