using Microsoft.AspNetCore.Mvc;

namespace SimApi.Operation;

public interface IUserIdentityService
{
    public IActionResult Login();
    public IActionResult Logout();
    public IActionResult ChangePassword();
    public IActionResult Register();

}

