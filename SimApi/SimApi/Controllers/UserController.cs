using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    public UserController(IUserService service)
    {
        this.service = service;
    }


    [HttpGet]
    [Authorize]
    public ApiResponse<List<UserResponse>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id}")]
    [Authorize]
    public ApiResponse<UserResponse> GetById(int id)
    {
        return service.GetById(id);
    }

    [HttpPost]
    [Authorize]
    public ApiResponse Post([FromBody] UserRequest request)
    {
        var response = service.Insert(request);
        return response;       
    }

    [HttpPut("{id}")]
    [Authorize]
    public ApiResponse Put(int id, [FromBody] UserRequest request)
    {
        var response = service.Update(id,request);
        return response;
    }


    [HttpDelete("{id}")]
    [Authorize]
    public ApiResponse Delete(int id)
    {
        var response = service.Delete(id);
        return response; ;
    }

}
