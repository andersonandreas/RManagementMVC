using RManagementMVC.Models;

namespace RManagementMVC.Services.Interfaces
{
	public interface IDishesService
	{
		IReadOnlyList<Dish> GetAll();
		Dish? GetByID(int id);
		//void UpdateDish(DishDto dishDto);
		//int Create(DishDto dishDto);

		void UpdateDish(Dish dish);
		int Create(Dish dish);

	}
}