using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models;

public class Dish
{
	public int Id { get; set; }

	[Required]
	[MaxLength(75)]
	public string Name { get; set; } = default!;

	[Required]
	[MaxLength(250)]
	public string Description { get; set; } = default!;

	[Required]
	[Range(0, 1000)]
	public decimal Price { get; set; }

	[Required]
	public bool IsAvailable { get; set; }
}
