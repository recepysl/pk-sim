using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimApi.Data;

namespace SimApi.Operation;

public class UserIdentityService : IUserIdentityService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager; 

    public UserIdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager; 
        this.signInManager = signInManager; 
    }

    public IActionResult ChangePassword()
    {
        throw new NotImplementedException();
    }

    public IActionResult Login()
    {
        throw new NotImplementedException();
    }

    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }

    public IActionResult Register()
    {
        throw new NotImplementedException();
    }
}
