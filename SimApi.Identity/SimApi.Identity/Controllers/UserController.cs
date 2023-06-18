using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Identity;

[Route("simapi/identity/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;

    public UserController(IUserService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ApplicationUserResponse>>> GetAll()
    {
        var list = await service.GetAll();
        return list;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<ApplicationUserResponse>> GetById(string id)
    {
        var model = await service.GetById(id);
        return model;
    }

    [HttpGet("GetUser")]
    public async Task<ApiResponse<ApplicationUserResponse>> GetUser()
    {
        var response = await service.GetUser(HttpContext.User);
        return response;
    }

    [HttpGet("GetUserId")]
    public async Task<ApiResponse<string>> GetUserId()
    {
        var response = await service.GetUserId(HttpContext.User);
        return response;
    }

    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] ApplicationUserRequest request)
    {
        var response = await service.Insert(request);
        return response;
    }

    [HttpPut]
    public async Task<ApiResponse> Put([FromBody] ApplicationUserRequest request)
    {
        var response = await service.Update(request);
        return response;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(string id)
    {
        var response = await service.Delete(id);
        return response;
    }
}