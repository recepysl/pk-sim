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
public class CasheController : ControllerBase
{
    private readonly IUserLogService userLogService;
    private readonly IRedisService redisService;
    private readonly ICurrencyService currencyService;
    public CasheController(IUserLogService service, IRedisService redisService, ICurrencyService currencyService)
    {
        this.userLogService = service;
        this.redisService = redisService;
        this.currencyService = currencyService;
    }

    [HttpPut("Flush")]
    public ApiResponse Flush()
    {
        redisService.Flush();
        return new ApiResponse();
    }


    [HttpPut("CurrencyCashe")]
    public ApiResponse<bool> CurrencyCashe()
    {
        redisService.Delete(RedisKeys.CurrencyList);
        var response = currencyService.GetAll();
        if (!response.Success)
        {
            return new ApiResponse<bool>(response.Message);
        }
        bool status = redisService.SetDynamic(RedisKeys.CurrencyList, response.Response);
        return new ApiResponse<bool>(status);
    }

    [HttpPut("UserLogCashe")]
    public ApiResponse<bool> UserLogCashe()
    {
        redisService.Delete(RedisKeys.UserLogList);
        var response = userLogService.GetAll();
        if (!response.Success)
        {
            return new ApiResponse<bool>(response.Message);
        }
        bool status = redisService.SetDynamic(RedisKeys.UserLogList, response.Response);
        return new ApiResponse<bool>(status);
    }
}
