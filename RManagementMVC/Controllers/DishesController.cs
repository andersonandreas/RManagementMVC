using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models;
using RManagementMVC.Services;

namespace RManagementMVC.Controllers;

public class DishesController/*(IDishesService dishesService)*/ : Controller
{

	private const string _createString = "create";
	private const string _editString = "edit";


	public IActionResult Index()
	{
		var dishes = DishesService.GetAll();
		return View(dishes);
	}


	public IActionResult Edit(int id)
	{
		ViewBag.Action = _editString;

		var dish = DishesService.GetByID(id);
		return View(dish);
	}


	[HttpPost]
	public IActionResult Edit(Dish dish)
	{

		if (ModelState.IsValid)
		{
			DishesService.UpdateDish(dish);
			return RedirectToAction(nameof(Index));
		}

		return View(dish);
	}


	public IActionResult Create()
	{
		ViewBag.Action = _createString;

		return View();
	}


	[HttpPost]
	public IActionResult Create(Dish dish)
	{
		ViewBag.Action = _createString;

		if (ModelState.IsValid)
		{
			DishesService.Create(dish);
			return RedirectToAction(nameof(Index));
		}

		return View(dish);
	}


	public IActionResult Delete(int id)
	{
		DishesService.Delete(id);
		return RedirectToAction(nameof(Index));
	}


}
