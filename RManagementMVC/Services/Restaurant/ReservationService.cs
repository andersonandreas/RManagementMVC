using Microsoft.Extensions.Options;
using RManagementMVC.Models;
using RManagementMVC.Options;
using RManagementMVC.Services.Restaurant.Interfaces;
using System.Text.Json;

namespace RManagementMVC.Services.Restaurant;

public class ReservationService(
    IHttpClientFactory IHttpClientFactory,
    IConfiguration configuration,
    IOptions<RestaurantOptions> options) : IReservationService
{


    private readonly IHttpClientFactory _httpClientFactory = IHttpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly RestaurantOptions _options = options.Value;


    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {

        var restaurantId = _options.RestaurantId;

        var client = _httpClientFactory.CreateClient("RestaurantApiClient");
        var response = await client.GetAsync($"api/restaurants/{restaurantId}/reservations/all");

        if (response.IsSuccessStatusCode)
        {

            var content = await response.Content.ReadAsStringAsync();
            var reservations = JsonSerializer.Deserialize<List<Reservation>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return reservations ?? [];
        }

        throw new HttpRequestException($"Error fetching reservations: {response.StatusCode}");
    }


    public async Task<bool> CreateAsync(Reservation reservation)
    {
        var restaurantId = _options.RestaurantId;

        var client = _httpClientFactory.CreateClient("RestaurantApiClient");
        var response = await client.PostAsJsonAsync($"api/restaurants/{restaurantId}/reservations", reservation);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }


    public async Task<bool> UpdateAsync(Reservation reservation)
    {
        var restaurantId = _options.RestaurantId;
        var client = _httpClientFactory.CreateClient("RestaurantApiClient");
        var response = await client.PatchAsJsonAsync($"api/restaurants/{restaurantId}/reservations/{reservation.Id}", reservation);

        // i need better response here 
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }


    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        var restaurantId = _options.RestaurantId;

        var client = _httpClientFactory.CreateClient("RestaurantApiClient");
        var response = await client.GetAsync($"api/restaurants/{restaurantId}/reservations/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var dish = JsonSerializer.Deserialize<Reservation>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return dish;
        }

        return null;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var restaurantId = _options.RestaurantId;

        var client = _httpClientFactory.CreateClient("RestaurantApiClient");
        var response = await client.DeleteAsync($"api/restaurants/{restaurantId}/reservations/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }




}
