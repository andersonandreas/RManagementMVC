using Microsoft.AspNetCore.Authentication.Cookies;
using RManagementMVC.Options;
using RManagementMVC.Services.Auth;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant;
using RManagementMVC.Services.Restaurant.Interfaces;
using RManagementMVC.Services.TokenHandler;

namespace RManagementMVC.Extensions;

public static class WebApplicationBuilderExtensions
{

	public static void ConfigureRestaurantServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddControllersWithViews();
		builder.Services.AddHttpContextAccessor();

		builder.Services.AddSession(options =>
		{
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/api/account/login";
				options.LogoutPath = "/api/account/logout";
				options.Cookie.Name = "AuthCookie";
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.SameSite = SameSiteMode.Strict;
			});

		builder.Services.AddAuthorization();

		builder.Services.Configure<RestaurantOptions>(
			builder.Configuration.GetSection(RestaurantOptions.SectionName));


		builder.Services.AddHttpClient("RestaurantApiClient", client =>
		{
			var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ??
				throw new ApplicationException("Missing API settings in appsettings");

			client.BaseAddress = new Uri(baseUrl);

		})
			.AddHttpMessageHandler<BearerTokenHandler>();

		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<ICookieService, CookieService>();
		builder.Services.AddTransient<BearerTokenHandler>();
		builder.Services.AddScoped<IRestaurantService, RestaurantService>();
		builder.Services.AddScoped<IDishesService, DishesService>();
		builder.Services.AddScoped<IReservationService, ReservationService>();
	}
}
