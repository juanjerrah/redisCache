using ToDoRedis.Models;

namespace ToDoRedis.Infra.CachingRepository;

public interface ICachingRepository
{
    Task SetAsync(string key, string value);
    Task<string> GetAsync(string key);

    Task<IEnumerable<T>?> GetCollection<T>(string collectionKey);
    Task SetCollection<T>(string collectionKey, IEnumerable<T> collection);
}