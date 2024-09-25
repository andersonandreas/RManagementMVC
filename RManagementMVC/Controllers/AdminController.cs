using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class AdminController(
    IAuthService authService,
    IAuthApiClient authApiClient,
    IDishesService dishesService,
    IReservationService IReservationService) : Controller
{

    private readonly IDishesService _dishesService = dishesService;
    private readonly IAuthService _authService = authService;
    private readonly IAuthApiClient _authApiClient = authApiClient;
    private readonly IReservationService _reservationService = IReservationService;


    public async Task<IActionResult> Panel()
    {

        await _dishesService.GetAllAsync();

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
            Dishes = await _dishesService.GetAllAsync(),
            Reservations = await _reservationService.GetAllAsync()
        };

        return View(viewModel);
    }


}