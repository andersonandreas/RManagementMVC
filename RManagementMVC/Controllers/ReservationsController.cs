using Microsoft.AspNetCore.Mvc;
using RManagementMVC.Models;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Controllers;


public class ReservationsController(
    IReservationService reservationService,
    IConfiguration configuration) : Controller
{

    private readonly IReservationService _reservationService = reservationService;
    private readonly IConfiguration _configuration = configuration;
    private const string _createString = "Create";
    private const string _editString = "Edit";



    public IActionResult Create()
    {
        SetupViewBag(_createString);

        return View("_Reservation", new ReservationViewModel
        {
            At = DateTime.Today.ToString("yyyy-MM-dd")
        });
    }


    [HttpPost]
    public async Task<IActionResult> Create(ReservationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var reservation = CreateReservationModel(model);
            var result = await _reservationService.CreateAsync(reservation);

            if (result)
            {
                return RedirectToAction(nameof(Success));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the reservation.");
        }

        SetupViewBag(_createString);

        return View("_Reservation", model);
    }


    public async Task<IActionResult> Edit(Guid id)
    {
        SetupViewBag(_editString);
        var reservation = await _reservationService.GetByIdAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        var model = CreateReservationViewModel(reservation);

        return View("_Reservation", model);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(ReservationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var reservation = UpdateReservationModel(model);
            var result = await _reservationService.UpdateAsync(reservation);

            if (result)
            {
                return RedirectToAction("Panel", "Admin");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the reservation.");
        }

        SetupViewBag(_editString);

        return View("_Reservation", model);
    }


    public IActionResult Delete(Guid id)
    {
        _reservationService.DeleteAsync(id);
        return RedirectToAction("Panel", "Admin");
    }


    public IActionResult Success()
    {
        return View();
    }







    private static Reservation CreateReservationModel(ReservationViewModel model)
    {
        return new Reservation
        {
            At = model.FormattedDateTime,
            Name = model.Name,
            Email = model.Email,
            Quantity = model.Quantity
        };
    }


    private static Reservation UpdateReservationModel(ReservationViewModel model)
    {
        if (model.Id == null)
        {
            throw new ArgumentNullException(nameof(model.Id), "Id can't be empty");
        }

        return new Reservation
        {
            Id = model.Id.Value,
            At = model.FormattedDateTime,
            Name = model.Name,
            Email = model.Email,
            Quantity = model.Quantity
        };
    }


    private static ReservationViewModel CreateReservationViewModel(Reservation reservation)
    {
        var reservationDateTime = DateTime.Parse(reservation.At);

        return new ReservationViewModel
        {
            Id = reservation.Id,
            At = reservationDateTime.ToString("yyyy-MM-dd"),
            TimeSlot = reservationDateTime.ToString("HH:mm"),
            Name = reservation.Name,
            Email = reservation.Email,
            Quantity = reservation.Quantity
        };
    }


    private void SetupViewBag(string action)
    {
        ViewBag.Action = action;
        ViewBag.MaxTableQuantity = _configuration.GetValue<int>("RestaurantOptions:MaxTableQuantity");
    }
}