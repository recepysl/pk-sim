using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;


[Route("simapi/v1/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private ITokenService tokenService;

    public TokenController(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }


    [HttpPost]
    public BaseResponse<TokenResponse> Post([FromBody] TokenRequest request)
    {       
       return tokenService.GetToken(request);
    }


}
