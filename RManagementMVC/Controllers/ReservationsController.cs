using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RManagementMVC.Models;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;

public class ReservationsController(IReservationService reservationService) : Controller
{


	private const string _createString = "create";
	private const string _editString = "edit";



	// if i have time take the opoens times from the restaurant isntead with an api call.... 
	private static readonly List<string> _timeSlots =
	[
		"17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00", "21:30", "22:00"
	];


	public IActionResult Index()
	{
		ViewBag.TimeSlots = _timeSlots
			.Select(t => new SelectListItem { Value = t, Text = t })
			.ToList();

		return View(new CreateReservation { At = DateTime.Today.ToString() });
	}


	public async Task<IActionResult> Edit(Guid id)
	{
		ViewBag.Action = _editString;

		var reservation = await reservationService.GetByIdAsync(id);

		return View(reservation);
	}


	[HttpPost]
	public async Task<IActionResult> Edit(Reservation reservation)
	{
		if (ModelState.IsValid)
		{
			await reservationService.UpdateAsync(reservation);
			return RedirectToAction("Panel", "Admin");
		}

		return View(reservation);
	}


	public IActionResult Delete(Guid id)
	{
		reservationService.DeleteAsync(id);
		return RedirectToAction("Panel", "Admin");
	}


	public IActionResult Create()
	{
		ViewBag.Action = _createString;

		return View();
	}


	[HttpPost]
	public async Task<IActionResult> Create(CreateReservation model)
	{
		if (ModelState.IsValid)
		{
			if (_timeSlots.Contains(model.TimeSlot))
			{

				var at = model.FormattedDateTime;
				model.At = at;

				var result = await reservationService.CreateAsync(model);

				if (result)
				{
					return View("Success", model);
				}


			}
			else
			{
				ModelState.AddModelError("TimeSlot", "Invalid time slot selected");
			}
		}

		ViewBag.TimeSlots = _timeSlots
			.Select(t => new SelectListItem { Value = t, Text = t })
			.ToList();


		return View("Index", model);
	}
























	//private const string _createString = "create";
	//private const string _editString = "edit";


	//public IActionResult Index()
	//{
	//	var reservations = ReservationService.GetAll();
	//	return View(reservations);
	//}


	//public IActionResult Edit(string id)
	//{
	//	ViewBag.Action = _editString;

	//	var reservation = ReservationService.GetByID(Guid.Parse(id));
	//	return View(reservation);
	//}


	//[HttpPost]
	//public IActionResult Edit(Reservation reservation)
	//{

	//	if (ModelState.IsValid)
	//	{
	//		ReservationService.UpdateReservation(reservation);
	//		return RedirectToAction(nameof(Index));
	//	}

	//	return View(reservation);
	//}


	//public IActionResult Create()
	//{
	//	ViewBag.Action = _createString;

	//	return View();
	//}


	//[HttpPost]
	//public IActionResult Create(Reservation reservation)
	//{
	//	ViewBag.Action = _createString;

	//	if (ModelState.IsValid)
	//	{
	//		ReservationService.Create(reservation);
	//		return RedirectToAction(nameof(Index));
	//	}

	//	return View(reservation);
	//}


	//public IActionResult Delete(string id)
	//{
	//	ReservationService.Delete(Guid.Parse(id));
	//	return RedirectToAction(nameof(Index));
	//}


}















