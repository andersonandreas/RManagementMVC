using RManagementMVC.Models;

namespace RManagementMVC.Services.Restaurant.Interfaces
{
	public interface IDishesService
	{
		IEnumerable<Dish> GetAll();
		Dish? GetByID(int id);
		//void UpdateDish(DishDto dishDto);
		//int Create(DishDto dishDto);

		void UpdateDish(Dish dish);
		int Create(Dish dish);

	}
}