using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SimApi.Operation;

public class RedisService : IRedisService
{
    private readonly IServer server;
    private readonly IDatabase database;

    public RedisService(IConfiguration configuration)
    {
        var configOptoins = new ConfigurationOptions();
        configOptoins.EndPoints.Add(configuration["Redis:Host"], Convert.ToInt32(configuration["Redis:Port"]));
        configOptoins.DefaultDatabase = Convert.ToInt32(configuration["Redis:DefaultDatabase"]);

        var redisConenction = ConnectionMultiplexer.Connect(configOptoins);
        this.server = redisConenction.GetServer(configuration["Redis:Host"], Convert.ToInt32(configuration["Redis:Port"]));
        this.database = redisConenction.GetDatabase();
    }


    public async Task<int> Ping()
    {
        var ping = await database.PingAsync();
        return ping.Milliseconds;
    }

    public async Task<string> Get(string key)
    {
        var val = await database.StringGetAsync(key);
        return val.ToString();
    }

    public async Task<bool> Set(string key, string value)
    {
        var val = await database.StringSetAsync(key, value);
        return val;
    }
    public async Task<bool> Exist(string key)
    {
        var val = await database.KeyExistsAsync(key);
        return val;
    }
    public async Task<bool> Delete(string key)
    {
        var val = await database.KeyDeleteAsync(key);
        return val;
    }
    public void Flush()
    {
        server.FlushAllDatabases();
    }
    public bool SetDynamic<T>(string key, T value)
    {
        string json = JsonConvert.SerializeObject(value);
        var val = database.StringSet(key, json);
        return val;
    }
    public T GetDynamic<T>(string key)
    {
        string json = database.StringGet(key);
        var val = JsonConvert.DeserializeObject<T>(json);
        return val;
    }
}
