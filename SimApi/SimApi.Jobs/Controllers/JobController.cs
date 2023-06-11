using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimApi.Jobs;

namespace SimApi.Service.Controllers;


[Route("simapi/job/v1/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    public JobController()
    {
    }


    [HttpGet("FireAndForget")]
    public string Fire()
    {
        var jobId = BackgroundJob.Enqueue(() => new JobFireAndForget().Run());
        return jobId;
    }

    [HttpGet("Schedule")]
    public string Schedule()
    {
        var jobId = BackgroundJob.Schedule(() => new JobSchedule().Run(), TimeSpan.FromMinutes(7));
        return jobId;
    }

    [HttpGet("RecurringJob")]
    public string Recurring()
    {
        string jobId = "recrjob2";
        RecurringJob.AddOrUpdate(jobId, () => JobRecurring.Run(), Cron.MinuteInterval(1));
        return jobId;
    }

    [HttpGet("ContinueJobWith")]
    public string ContinueJobWith()
    {
        var jobId = BackgroundJob.Schedule(() => new JobSchedule().Run(), TimeSpan.FromMinutes(1));
        var newJobId = BackgroundJob.ContinueJobWith(jobId, () => new JobFireAndForget().Run());
        return newJobId;
    }

}
