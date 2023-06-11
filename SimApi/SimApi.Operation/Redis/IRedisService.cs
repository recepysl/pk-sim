using Newtonsoft.Json;

namespace SimApi.Operation;

public interface IRedisService
{
    public Task<int> Ping();
    public Task<string> Get(string key);
    public Task<bool> Set(string key, string value);
    public Task<bool> Exist(string key);
    public Task<bool> Delete(string key);
    public void Flush();
    public bool SetDynamic<T>(string key, T value);
    public T GetDynamic<T>(string key);

}
