using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class ReservationsController : Controller
{

	private const string _createString = "create";
	private const string _editString = "edit";


	public IActionResult Index()
	{
		var reservations = ReservationService.GetAll();
		return View(reservations);
	}


	public IActionResult Edit(string id)
	{
		ViewBag.Action = _editString;

		var reservation = ReservationService.GetByID(Guid.Parse(id));
		return View(reservation);
	}


	[HttpPost]
	public IActionResult Edit(Reservation reservation)
	{

		if (ModelState.IsValid)
		{
			ReservationService.UpdateReservation(reservation);
			return RedirectToAction(nameof(Index));
		}

		return View(reservation);
	}


	public IActionResult Create()
	{
		ViewBag.Action = _createString;

		return View();
	}


	[HttpPost]
	public IActionResult Create(Reservation reservation)
	{
		ViewBag.Action = _createString;

		if (ModelState.IsValid)
		{
			ReservationService.Create(reservation);
			return RedirectToAction(nameof(Index));
		}

		return View(reservation);
	}


	public IActionResult Delete(string id)
	{
		ReservationService.Delete(Guid.Parse(id));
		return RedirectToAction(nameof(Index));
	}


}















