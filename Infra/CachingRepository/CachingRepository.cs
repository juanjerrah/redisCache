
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ToDoRedis.Infra.CachingRepository;

public class CachingRepository : ICachingRepository
{
    private readonly IDistributedCache _cache;

    public CachingRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value);
    }

    public async Task<string> GetAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }

    public async Task<IEnumerable<T>?> GetCollection<T>(string collectionKey)
    {
        var result = await _cache.GetStringAsync(collectionKey);

        return result == null ? null : JsonConvert.DeserializeObject<IEnumerable<T>>(result);
    }

    public async Task SetCollection<T>(string collectionKey, IEnumerable<T> collection)
    {
        var serializedCollection = JsonConvert.SerializeObject(collection);
        await _cache.SetStringAsync(collectionKey, serializedCollection);
    }
}