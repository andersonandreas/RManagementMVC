using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;

namespace RManagementMVC.Controllers;

public class AccountController(IAuthService authService) : Controller
{

	private readonly IAuthService _authService = authService;




	public IActionResult Login()
	{
		return View();
	}


	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{

		if (!ModelState.IsValid)
		{
			return View(loginViewModel);
		}

		var result = await _authService.LoginAsync(loginViewModel);

		if (result.Success)
		{
			return RedirectToAction("Panel", "Admin");
		}

		ModelState.AddModelError(string.Empty, result.Message ?? "Invalid login attempt");

		return View(loginViewModel);
	}


	public async Task<IActionResult> Logout()
	{
		await _authService.LogoutAsync();

		return View(nameof(Login));
	}


}