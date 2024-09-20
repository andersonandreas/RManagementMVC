using RManagementMVC.Models;

namespace RManagementMVC.Services.Restaurant.Interfaces
{
	public interface IDishesService
	{
		Task<IEnumerable<Dish>> GetAllAsync();
		Task<Dish?> GetByIdAsync(int id);
		Task UpdateAsync(Dish dish);
	}
}