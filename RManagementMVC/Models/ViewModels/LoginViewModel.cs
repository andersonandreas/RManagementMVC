using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models.ViewModels;

public class LoginViewModel
{
	[Required(ErrorMessage = "Account name is required")]
	[Display(Name = "Account Name")]
	public string Username { get; set; } = default!;

	[Required(ErrorMessage = "Password is required")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = default!;
}
