using RManagementMVC.Models.ViewModels;

namespace RManagementMVC.Services.Restaurant.Interfaces;

public interface IRestaurantService
{
    //Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    //Task<RestaurantDto?> GetById(int id);


    public RestaurantViewModel GetAll();
}



