using RManagementMVC.Models;

namespace RManagementMVC.Services.Interfaces;

public static class ReservationService
{

	private static IList<Reservation> _reservations = [

	new() { Id = Guid.NewGuid(), At = "2024-09-15T19:30:00", Name = "John Smith", Email = "john.smith@example.com", Quantity = 2 },
	new() { Id = Guid.NewGuid(), At = "2024-09-16T18:00:00", Name = "Emma Johnson", Email = "emma.j@example.com", Quantity = 4 },
	new() { Id = Guid.NewGuid(), At = "2024-09-16T20:15:00", Name = "Michael Brown", Email = null, Quantity = 1 },
	new() { Id = Guid.NewGuid(), At = "2024-09-17T19:00:00", Name = "Sophia Davis", Email = "sophia.davis@example.com", Quantity = 3 },
	new() { Id = Guid.NewGuid(), At = "2024-09-18T18:45:00", Name = "William Wilson", Email = "will.wilson@example.com", Quantity = 2 },
	new() { Id = Guid.NewGuid(), At = "2024-09-19T20:30:00", Name = "Olivia Taylor", Email = null, Quantity = 5 },
	new() { Id = Guid.NewGuid(), At = "2024-09-20T19:15:00", Name = "James Anderson", Email = "j.anderson@example.com", Quantity = 2 },
	new() { Id = Guid.NewGuid(), At = "2024-09-21T18:30:00", Name = "Ava Martinez", Email = "ava.m@example.com", Quantity = 6 },
	new() { Id = Guid.NewGuid(), At = "2024-09-22T20:00:00", Name = "Liam Garcia", Email = null, Quantity = 1 },
	new() { Id = Guid.NewGuid(), At = "2024-09-23T19:45:00", Name = "Isabella Robinson", Email = "bella.robinson@example.com", Quantity = 3 },
	new() { Id = Guid.NewGuid(), At = "2024-09-24T18:15:00", Name = "Noah Lee", Email = "noah.lee@example.com", Quantity = 2 }

	];



	public static IReadOnlyList<Reservation> GetAll()
	{
		return _reservations.ToList();
	}


	public static Reservation? GetByID(Guid id)
	{
		var reservation = _reservations.FirstOrDefault(d => d.Id == id);

		return reservation;
	}


	public static void UpdateReservation(Reservation reservation)
	{
		var UpdateReservation = GetByID(reservation.Id);

		if (UpdateReservation != null)
		{
			UpdateReservation.At = reservation.At;
			UpdateReservation.Name = reservation.Name;
			UpdateReservation.Email = reservation.Email;
			UpdateReservation.Quantity = reservation.Quantity;
		}
	}


	public static Guid Create(Reservation reservation)
	{
		var id = Guid.NewGuid();

		var createdReservation = new Reservation
		{
			Id = id,
			At = reservation.At,
			Name = reservation.Name,
			Email = reservation.Email,
			Quantity = reservation.Quantity,
		};

		_reservations.Add(createdReservation);

		return id;
	}


	public static void Delete(Guid id)
	{
		var reservation = _reservations.FirstOrDefault(d => d.Id == id);

		if (reservation != null)
		{
			_reservations.Remove(reservation);
		}

		return;
	}


}


