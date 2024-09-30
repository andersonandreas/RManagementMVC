using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class AdminController(
	IAuthService authService,
	IDishesService dishesService,
	IReservationService IReservationService,
	IUserService userService) : Controller
{

	private readonly IDishesService _dishesService = dishesService;
	private readonly IAuthService _authService = authService;
	private readonly IReservationService _reservationService = IReservationService;


	public async Task<IActionResult> Panel()
	{

		await _dishesService.GetAllAsync();

		if (userService.IsAdminLoggedIn())
		{
			var viewModel = new AdminPanelViewModel
			{
				Dishes = await _dishesService.GetAllAsync(),
				Reservations = await _reservationService.GetAllAsync()
			};

			return View(viewModel);
		}

		return RedirectToAction("Login", "Account");
	}


}

