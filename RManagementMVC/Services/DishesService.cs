using RManagementMVC.Models;

namespace RManagementMVC.Services;

public static class DishesService /*: IDishesService*/
{

	private static IList<Dish> _dishes =
		[
			new Dish { Id = 1, Name = "Spaghetti Carbonara", Description = "Classic Italian pasta dish with eggs, cheese, and bacon", Price = 12.99m, IsAvailable = false },
			new Dish { Id = 2, Name = "Chicken Tikka Masala", Description = "Tender chicken in a creamy tomato sauce", Price = 14.99m, IsAvailable = true },
			new Dish { Id = 3, Name = "Caesar Salad", Description = "Crisp romaine lettuce with Caesar dressing and croutons", Price = 8.99m, IsAvailable = true },
			new Dish { Id = 4, Name = "Margherita Pizza", Description = "Traditional pizza with tomato, mozzarella, and basil", Price = 11.99m, IsAvailable = true },
			new Dish { Id = 5, Name = "Beef Burger", Description = "Juicy beef patty with lettuce, tomato, and cheese", Price = 10.99m, IsAvailable = false },
			new Dish { Id = 6, Name = "Sushi Platter", Description = "Assortment of fresh sushi rolls", Price = 18.99m, IsAvailable = true },
			new Dish { Id = 7, Name = "Greek Salad", Description = "Mixed greens with feta cheese, olives, and vinaigrette", Price = 9.99m, IsAvailable = true },
			new Dish { Id = 8, Name = "Pad Thai", Description = "Stir-fried rice noodles with shrimp, tofu, and peanuts", Price = 13.99m, IsAvailable = true },
			new Dish { Id = 9, Name = "Grilled Salmon", Description = "Fresh salmon fillet with lemon butter sauce", Price = 16.99m, IsAvailable = true },
			new Dish { Id = 10, Name = "Mushroom Risotto", Description = "Creamy Italian rice dish with assorted mushrooms", Price = 12.99m, IsAvailable = true },
			new Dish { Id = 11, Name = "Beef Tacos", Description = "Soft tortillas filled with seasoned ground beef and toppings", Price = 10.99m, IsAvailable = false },
			new Dish { Id = 12, Name = "Vegetable Stir Fry", Description = "Mixed vegetables in a savory sauce with rice", Price = 11.99m, IsAvailable = true },
			new Dish { Id = 13, Name = "French Onion Soup", Description = "Rich beef broth with caramelized onions and melted cheese", Price = 7.99m, IsAvailable = true },
			new Dish { Id = 14, Name = "Eggplant Parmesan", Description = "Breaded eggplant slices with tomato sauce and cheese", Price = 13.99m, IsAvailable = true },
			new Dish { Id = 15, Name = "Chocolate Lava Cake", Description = "Warm chocolate cake with a gooey center", Price = 6.99m, IsAvailable = false }
		];





	public static IReadOnlyList<Dish> GetAll()
	{
		return _dishes.ToList();
	}


	public static Dish? GetByID(int id)
	{
		var dish = _dishes.FirstOrDefault(d => d.Id == id);
		return dish;
	}


	public static void UpdateDish(Dish dish)
	{
		var currentDish = GetByID(dish.Id);

		if (currentDish != null)
		{
			currentDish.Name = dish.Name;
			currentDish.Description = dish.Description;
			currentDish.Price = dish.Price;
			currentDish.IsAvailable = dish.IsAvailable;
		}
	}


	public static int Create(Dish dish)
	{
		var id = _dishes.Count() + 1;

		var createdDish = new Dish
		{
			Id = id,
			Name = dish.Name,
			Description = dish.Description,
			IsAvailable = dish.IsAvailable
		};

		_dishes.Add(createdDish);

		return id;
	}


}