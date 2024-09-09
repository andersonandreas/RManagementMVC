using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Interfaces;

namespace RManagementMVC.Services
{
	public class RestaurantService : IRestaurantService
	{
		public RestaurantViewModel GetAll()
		{
			return new RestaurantViewModel("Andreas Restaurant", "Burgers", "Fast Food", true);
		}
	}
}
