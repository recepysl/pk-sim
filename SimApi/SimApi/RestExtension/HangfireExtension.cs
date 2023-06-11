using Hangfire;
using Hangfire.PostgreSql;

namespace SimApi.Service.RestExtension;

public static class HangfireExtension
{
    public static void AddHangfireExtension(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("HangfireConnection");

        services.AddHangfire(configuration => configuration
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UsePostgreSqlStorage(connection, new PostgreSqlStorageOptions
           {
               TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
               InvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.FromMinutes(10),
               AllowUnsafeValues = true
           }));


        services.AddHangfireServer();
    }
}
