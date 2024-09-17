using RManagementMVC.Models.ViewModels;
using RManagementMVC.Services.Restaurant.Interfaces;

namespace RManagementMVC.Services.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        public RestaurantViewModel GetAll()
        {
            return new RestaurantViewModel("Andreas Restaurant", "Burgers", "Fast Food", true);
        }
    }
}
