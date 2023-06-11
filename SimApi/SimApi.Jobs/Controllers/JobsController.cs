using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimApi.Service.Controllers;


[Route("simapi/job/v1/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    public JobsController()
    {
    }


    [HttpGet("FireAndForget")]
    public string FireAndForget()
    {
        var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("FireAndForget!"));
        return jobId;
    }

    [HttpGet("Schedule")]
    public string Schedule()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromDays(7));
        return jobId;
    }

    [HttpGet("RecurringJob")]
    public string Recurring()
    {
        string jobId = "recrjob1";
        RecurringJob.AddOrUpdate(jobId, () => Console.WriteLine("Recurring!"), "0 22 * * 1-5");
        return jobId;
    }

    [HttpGet("ContinueJobWith")]
    public string ContinueJobWith()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromMinutes(1));
        var newJobId = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation!"));
        var newJobId2 = BackgroundJob.ContinueJobWith(newJobId, () => Console.WriteLine("Continuation!"));
        return newJobId2;
    }

}
