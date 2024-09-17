using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant;

namespace RManagementMVC.Controllers;

public class AdminController(
	IAuthService authService,
	IAuthApiClient authApiClient) : Controller
{


	public IActionResult Login()
	{

		if (authService.IsAuthenticated() && authService.IsAdmin())
		{
			return RedirectToAction(nameof(Panel));
		}

		return View();
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		if (ModelState.IsValid)
		{
			var result = await authApiClient.LoginAsync(loginViewModel);

			if (result.Succes && authService.IsAdmin())
			{
				return RedirectToAction(nameof(Panel));
			}

			ModelState.AddModelError(string.Empty, "Invalid login attempt or not an admin.");
		}

		return View();
	}


	public async Task<IActionResult> Panel()
	{

		if (authService.IsAuthenticated() && authService.IsAdmin())
		{
			return RedirectToAction(nameof(Login));
		}

		await authService.EnsureValidTokenAsync();

		var viewModel = new AdminPanelViewModel
		{
			Dishes = DishesService.GetAll(),
			Reservations = []

		};


		return View(viewModel);
	}


	public IActionResult Logout()
	{
		authService.Logout();
		return RedirectToAction("Index", "Home");
	}

}