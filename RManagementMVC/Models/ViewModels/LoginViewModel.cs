using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models.ViewModels;

public class LoginViewModel
{
	[Required(ErrorMessage = "Email required")]
	public string Email { get; set; } = default!;

	[Required(ErrorMessage = "Password is required")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = default!;
}
