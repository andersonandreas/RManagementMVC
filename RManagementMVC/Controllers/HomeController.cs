using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class HomeController(ILogger<HomeController> logger, IRestaurantService restaurantService) : Controller
{
	private readonly ILogger<HomeController> _logger = logger;

	public IActionResult Index()
	{
		var restaurant = restaurantService.GetAll();
		return View(restaurant);
	}


}