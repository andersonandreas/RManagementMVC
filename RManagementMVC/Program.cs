using RManagementMVC.Options;
using RManagementMVC.Services.Auth;
using RManagementMVC.Services.Auth.Interfaces;
using RManagementMVC.Services.Restaurant;
using RManagementMVC.Services.Restaurant.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<RestaurantOptions>(
	builder.Configuration.GetSection(RestaurantOptions.SectionName));


builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IAuthApiClient, AuthApiClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<AdminStateService>();
builder.Services.AddScoped<IDishesService, DishesService>();




builder.Services.AddHttpClient("RestaurantApiClient", client =>
{
	var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ??
		throw new ApplicationException("Missing API settings in appsettings");

	client.BaseAddress = new Uri(baseUrl);

});




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
