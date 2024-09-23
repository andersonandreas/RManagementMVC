using Microsoft.Extensions.Options;
using RManagementMVC.Models;
using RManagementMVC.Options;
using RManagementMVC.Services.Restaurant.Interfaces;
using System.Text.Json;

namespace RManagementMVC.Services.Restaurant;

public class DishesService(
	IHttpClientFactory IHttpClientFactory,
	IConfiguration configuration,
	IOptions<RestaurantOptions> options) : IDishesService
{


	private readonly IHttpClientFactory _httpClientFactory = IHttpClientFactory;
	private readonly IConfiguration _configuration = configuration;
	private readonly RestaurantOptions _options = options.Value;


	public async Task<IEnumerable<Dish>> GetAllAsync()
	{

		var restaurantId = _options.RestaurantId;

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.GetAsync($"api/restaurants/{restaurantId}/dishes");

		if (response.IsSuccessStatusCode)
		{

			var content = await response.Content.ReadAsStringAsync();
			var dishes = JsonSerializer.Deserialize<List<Dish>>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return dishes ?? [];
		}

		throw new HttpRequestException($"Error fetching dishes: {response.StatusCode}");
	}


	public async Task<bool> UpdateAsync(Dish dish)
	{
		var restaurantId = _options.RestaurantId;
		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.PatchAsJsonAsync($"api/restaurants/{restaurantId}/dishes/{dish.Id}", dish);

		// i need better response here 
		if (response.IsSuccessStatusCode)
		{
			return true;
		}

		return false;
	}


	public async Task<Dish?> GetByIdAsync(int id)
	{
		var restaurantId = _options.RestaurantId;

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.GetAsync($"api/restaurants/{restaurantId}/dishes/{id}");

		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadAsStringAsync();
			var dish = JsonSerializer.Deserialize<Dish>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return dish;
		}

		return null;
	}


	public async Task<bool> DeleteAsync(int id)
	{
		var restaurantId = _options.RestaurantId;

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.DeleteAsync($"api/restaurants/{restaurantId}/dishes/{id}");

		if (response.IsSuccessStatusCode)
		{
			return true;
		}

		return false;
	}


	public async Task<bool> CreateAsync(CreateDish dish)
	{
		var restaurantId = _options.RestaurantId;

		var client = _httpClientFactory.CreateClient("RestaurantApiClient");
		var response = await client.PostAsJsonAsync($"api/restaurants/{restaurantId}/dishes", dish);

		if (response.IsSuccessStatusCode)
		{
			return true;
		}

		return false;
	}

}