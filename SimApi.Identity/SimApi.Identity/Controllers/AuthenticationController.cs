using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Identity;

[Route("simapi/identity/v1/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService service;

    public AuthenticationController(IAuthenticationService service)
    {
        this.service = service;
    }

    [HttpPost("SignIn")]
    public async Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request)
    {
        var list = await service.SignIn(request);
        return list;
    }

    [HttpPost("SignOut")]
    public async Task<ApiResponse> SignOut()
    {
        var model = await service.SignOut();
        return model;
    }

    [HttpPost("ChangePassword")]
    public async Task<ApiResponse> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var model = await service.ChangePassword(HttpContext.User,request);
        return model;
    }    
}