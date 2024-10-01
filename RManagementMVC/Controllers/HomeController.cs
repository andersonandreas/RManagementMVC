using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class HomeController(ILogger<HomeController> logger, IRestaurantService restaurantService, IDishesService dishesService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;




    public async Task<IActionResult> Index()
    {
        var topDishes = await dishesService.GetAllAsync();
        return View(topDishes.ToList());
    }


    public IActionResult AboutUs()
    {
        return View();

    }

}