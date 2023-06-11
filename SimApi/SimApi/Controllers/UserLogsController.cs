using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;
using System.Text;

namespace SimApi.Service.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class UserLogsController : ControllerBase
{
    private readonly IUserLogService service;
    private readonly IDistributedCache distributedCache;
    public UserLogsController(IUserLogService service, IDistributedCache distributedCache)
    {
        this.service = service;
        this.distributedCache = distributedCache;
    }


    [HttpGet]
    public ApiResponse<List<UserLogResponse>> GetAll([FromQuery] string username)
    {
        var checkMemory = distributedCache.Get(username);
        if (checkMemory is not null)
        {
            string json = Encoding.UTF8.GetString(checkMemory);
            List<UserLogResponse> list = JsonConvert.DeserializeObject<List<UserLogResponse>>(json);
            return new ApiResponse<List<UserLogResponse>>(list);
        }

        var entityList = SetCache(username);
        return entityList;
    }

    [HttpPut("InvalidateCache")]
    public ApiResponse InvalidateCache([FromQuery] string username)
    {
        distributedCache.Remove(username);
        SetCache(username);
        return new ApiResponse();
    }

    [HttpDelete("DeleteCache")]
    public ApiResponse DeleteCache([FromQuery] string username)
    {
        distributedCache.Remove(username);
        return new ApiResponse();
    }

    private ApiResponse<List<UserLogResponse>> SetCache(string username)
    {
        var entityList = service.GetByUserName(username);
        if (entityList.Success && entityList.Response.Any())
        {
            string json = JsonConvert.SerializeObject(entityList.Response);
            byte[] bytes  = Encoding.UTF8.GetBytes(json);

            var CacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };

            distributedCache.Set(username, bytes, CacheOptions);
        }

        return entityList;
    }
}
