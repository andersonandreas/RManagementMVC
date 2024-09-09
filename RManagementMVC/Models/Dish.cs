using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models;

public class Dish
{
	public int Id { get; set; }
	[Required]
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public decimal Price { get; set; }
	public bool IsAvailable { get; set; }
}
