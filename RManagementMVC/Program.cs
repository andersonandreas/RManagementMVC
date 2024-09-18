using RManagementMVC.Services.Auth;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant;
using RManagementMVC.Services.Restaurant.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IAuthApiClient, AuthApiClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<AdminStateService>();


builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
{
	var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? null;

	if (baseUrl != null)
	{
		client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
	}

});


//builder.Services.AddScoped<IDishesService, DishesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
