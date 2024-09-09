using Microsoft.AspNetCore.Mvc;

namespace RManagementMVC.Controllers;

public class RestaurantController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
