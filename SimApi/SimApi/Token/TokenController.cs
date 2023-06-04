using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService tokenService;

    private readonly IUserService userService;


    public TokenController(ITokenService tokenService, IUserService userService )
    {
        this.tokenService = tokenService;
        this.userService = userService;
    }


    [HttpPost("SignIn")]
    public ApiResponse<TokenResponse> Post([FromBody] TokenRequest request)
    {       
       return tokenService.GetToken(request);
    }


    [HttpPost("SignUp")]
    public ApiResponse Post([FromBody] UserRequest request)
    {
        var response = userService.Insert(request);
        return response;
    }

}
