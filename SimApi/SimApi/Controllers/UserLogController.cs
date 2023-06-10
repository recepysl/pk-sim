using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class UserLogController : ControllerBase
{
    private readonly IUserLogService service;
    public UserLogController(IUserLogService service)
    {
        this.service = service;
    }


    [HttpGet]
    public ApiResponse<List<UserLogResponse>> GetAll()
    {
        var entityList = service.GetAll();
        return entityList;
    }


}
