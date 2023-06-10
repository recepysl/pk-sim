using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    private readonly IMemoryCache memoryCache;
    public UserLogController(IUserLogService service, IMemoryCache memoryCache)
    {
        this.service = service;
        this.memoryCache = memoryCache;
    }


    [HttpGet]
    public ApiResponse<List<UserLogResponse>> GetAll([FromQuery] string username)
    {
        var checkMemory = memoryCache.TryGetValue(username, out List<UserLogResponse> response);
        if (checkMemory)
        {
            return new ApiResponse<List<UserLogResponse>>(response);
        }

        var entityList = SetCache(username);

        return entityList;

    }

    [HttpPut("InvalidateCache")]
    public ApiResponse InvalidateCache([FromQuery] string username)
    {
        memoryCache.Remove(username);
        SetCache(username);
        return new ApiResponse();
    }

    [HttpDelete("DeleteCache")]
    public ApiResponse DeleteCache([FromQuery] string username)
    {
        memoryCache.Remove(username);
        return new ApiResponse();
    }

    private ApiResponse<List<UserLogResponse>> SetCache(string username)
    {
        var entityList = service.GetByUserName(username);
        if (entityList.Success && entityList.Response.Any())
        {
            var CacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1),
                Priority = CacheItemPriority.High
            };
            memoryCache.Set(username, entityList.Response, CacheOptions);
        }

        return entityList;
    }
}
