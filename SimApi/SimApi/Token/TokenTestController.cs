using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using System.Security.Claims;

namespace SimApi.Service;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
[NonController]
public class TokenTestController : ControllerBase
{
    public TokenTestController()
    {
    }


    [HttpGet("NoToken")]
    public string NoToken()
    {
        return "NoToken";
    }

    [HttpGet("Authorize")]
    [Authorize]
    public string Authorize()
    {
        return "Authorize";
    }

    [HttpGet("Admin")]
    [Authorize(Roles = Role.Admin)]
    public string Admin()
    {
        return "Admin";
    }

    [HttpGet("Viewer")]
    [Authorize(Roles = Role.Viewer)]
    public string Viewer()
    {
        return "Viewer";
    }

    [HttpGet("AdminEditor")]
    [Authorize(Roles = $"{Role.Editor},{Role.Admin}")]
    public string AdminEditor()
    {
        return "AdminEditor";
    }

    [HttpGet("ViewerEditor")]
    [Authorize(Roles = $"{Role.Viewer},{Role.Editor}")]
    public string ViewerEditor()
    {
        return "ViewerEditor";
    }

    [HttpGet("GetUserId")]
    public string GetUserId()
    {
        var userIdClaims = User.Claims.Where(x=> x.Type == "UserId").FirstOrDefault();
        var userId = userIdClaims.Value;

        var url  = HttpContext.Request.GetDisplayUrl();

        var userId2 = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;

        return userId2.ToString();
    }


}
