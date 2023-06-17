using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace SimApi.Rest;

public static class RedisExtension
{
    public static void AddRedisExtension(this IServiceCollection services,IConfiguration configuration)
    {
        var configOptoins = new ConfigurationOptions();
        configOptoins.EndPoints.Add(configuration["Redis:Host"], Convert.ToInt32(configuration["Redis:Port"]));
        configOptoins.DefaultDatabase = Convert.ToInt32(configuration["Redis:DefaultDatabase"]);

        services.AddStackExchangeRedisCache(option =>
        {
            option.ConfigurationOptions = configOptoins;
            option.InstanceName = configuration["Redis:InstanceName"];
        });
    }
}
