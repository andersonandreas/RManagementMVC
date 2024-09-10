namespace RManagementMVC.Models;

public class Reservation
{
	public Guid Id { get; set; }
	public string At { get; set; } = default!;
	public string Name { get; set; } = default!;
	public string? Email { get; set; }
	public int Quantity { get; set; }
}
