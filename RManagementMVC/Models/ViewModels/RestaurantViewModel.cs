namespace RManagementMVC.Models.ViewModels;

public class RestaurantViewModel
{
	public RestaurantViewModel(string name, string description, string category, bool hasDelivery)
	{
		Name = name;
		Description = description;
		Category = category;
		HasDelivery = hasDelivery;
	}

	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Category { get; set; } = default!;
	public bool HasDelivery { get; set; }


}
