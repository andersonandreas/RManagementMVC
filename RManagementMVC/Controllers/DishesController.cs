using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class DishesController(IDishesService dishesService) : Controller
{

	private readonly IDishesService _dishesService = dishesService;

	private const string _createString = "create";
	private const string _editString = "edit";



	public async Task<IActionResult> Index()
	{
		var dishes = await _dishesService.GetAllAsync();

		return View(dishes);
	}


	public async Task<IActionResult> Edit(int id)
	{
		ViewBag.Action = _editString;

		var dish = await _dishesService.GetByIdAsync(id);

		return View(dish);
	}


	[HttpPost]
	public IActionResult Edit(Dish dish)
	{
		if (ModelState.IsValid)
		{
			_dishesService.UpdateAsync(dish);
			return RedirectToAction(nameof(Index));
		}

		return View(dish);
	}




	//public IActionResult Create()
	//{
	//	ViewBag.Action = _createString;

	//	return View();
	//}


	//[HttpPost]
	//public IActionResult Create(Dish dish)
	//{

	//	if (ModelState.IsValid)
	//	{
	//		DishesService.Create(dish);
	//		return RedirectToAction(nameof(Index));
	//	}

	//	return View(dish);
	//}


	//public IActionResult Delete(int id)
	//{
	//	DishesService.Delete(id);
	//	return RedirectToAction("Index", "Home");
	//}


}
