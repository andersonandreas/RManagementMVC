namespace RManagementMVC.Services.Auth;

public class AdminStateService
{

	private bool _isAdminLoggedIn = false;



	public void SetAdminLoggedIn(bool isAdmin)
	{
		_isAdminLoggedIn = isAdmin;
	}


	public bool IsAdminLoggedIn()
	{
		return _isAdminLoggedIn;
	}

}
