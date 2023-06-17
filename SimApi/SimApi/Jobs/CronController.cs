using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;

namespace SimApi.Service;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class CronController : ControllerBase
{
    private readonly INotificationService notificationService;
    private readonly ICurrencyService currencyService;

    public CronController(INotificationService notificationService, ICurrencyService currencyService)
    {
        this.notificationService = notificationService;
        this.currencyService = currencyService;
    }

    [HttpPut("SetRecurringEmailJob")]
    public ApiResponse SetRecurringEmailJob()
    {
        RecurringJob.AddOrUpdate(RecurringCrons.EmailQueue, () => notificationService.ConsumeEmail(), "*/10 * * * *");
        return new ApiResponse();
    }


    [HttpPut("SetCasheCurrencyJob")]
    public ApiResponse SetCasheCurrencyJob()
    {
        RecurringJob.AddOrUpdate(RecurringCrons.CurrencyCache, () => currencyService.Cache(), "10 09 * * *");

        return new ApiResponse();
    }

}
