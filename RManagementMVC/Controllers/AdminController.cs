using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant;

namespace RManagementMVC.Controllers;

public class AdminController(IAuthService authService, IAuthApiClient authApiClient) : Controller
{

	private readonly IAuthService _authService = authService;
	private readonly IAuthApiClient _authApiClient = authApiClient;


	public IActionResult Panel()
	{
		if (!_authService.IsAuthenticated() || !_authService.IsAdmin())
		{
			return RedirectToAction("Login", "Account");
		}

		if (!_authService.EnsureValidToken())
		{
			return RedirectToAction("Login", "Account");
		}

		var viewModel = new AdminPanelViewModel
		{
			//Dishes = await _authApiClient.GetDishesAsync(),
			//Reservations = await _authApiClient.GetReservationsAsync() 
			Dishes = DishesService.GetAll(),
			Reservations = []
		};

		return View(viewModel);
	}











}