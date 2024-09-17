namespace RManagementMVC.Models.ViewModels;

public class AdminPanelViewModel
{
	public IEnumerable<Reservation> Reservations { get; set; } = [];
	public IEnumerable<Dish> Dishes { get; set; } = [];
}
