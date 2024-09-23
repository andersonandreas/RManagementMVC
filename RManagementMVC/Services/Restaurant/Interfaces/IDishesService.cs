using RManagementMVC.Models;

namespace RManagementMVC.Services.Restaurant.Interfaces;

public interface IDishesService
{
	Task<IEnumerable<Dish>> GetAllAsync();
	Task<Dish?> GetByIdAsync(int id);
	Task<bool> UpdateAsync(Dish dish);
	Task<bool> DeleteAsync(int id);
	Task<bool> CreateAsync(CreateDish dish);
}