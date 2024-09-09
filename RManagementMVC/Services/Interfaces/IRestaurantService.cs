using RManagementMVC.Models.ViewModels;

namespace RManagementMVC.Services.Interfaces;

public interface IRestaurantService
{
	//Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
	//Task<RestaurantDto?> GetById(int id);


	public RestaurantViewModel GetAll();
}



