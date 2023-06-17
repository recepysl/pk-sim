using AutoMapper;
using Hangfire;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;
using System.Text;

namespace SimApi.Operation;

public class CurrencyService : BaseService<Currency, CurrencyRequest, CurrencyResponse>, ICurrencyService
{

    private readonly IDistributedCache distributedCache;
    public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache) : base(unitOfWork, mapper)
    {
        this.distributedCache = distributedCache;
    }

    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public ApiResponse Cache()
    {
        var response = base.GetAll();
        if (!response.Success || !response.Response.Any())
        {
            return new ApiResponse(response.Message);
        }
        var entityList = response.Response;

        distributedCache.Remove(RedisKeys.CurrencyList);


        string json = JsonConvert.SerializeObject(entityList);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        var CacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddDays(1),
            SlidingExpiration = TimeSpan.FromHours(1)
        };

        distributedCache.Set(RedisKeys.CurrencyList, bytes, CacheOptions);

        return new ApiResponse();
    }
}
