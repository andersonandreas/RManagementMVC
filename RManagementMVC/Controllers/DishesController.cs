﻿using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models;
using RManagementMVC.Services;

namespace RManagementMVC.Controllers;

public class DishesController/*(IDishesService dishesService)*/ : Controller
{

	public IActionResult Index()
	{
		var dishes = DishesService.GetAll();
		return View(dishes);
	}


	public IActionResult Edit(int id)
	{
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

		return View();
	}


	[HttpPost]
	public IActionResult Create(Dish dish)
	{
		if (ModelState.IsValid)
		{
			DishesService.Create(dish);
			return RedirectToAction(nameof(Index));
		}

		return View(dish);
	}

}