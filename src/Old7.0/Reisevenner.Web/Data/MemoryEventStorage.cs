using Microsoft.Extensions.Caching.Memory;

namespace Reisevenner.Web.Data;

public class MemoryEventStorage : IEventStorage
{
    private readonly IMemoryCache _cache;

    public MemoryEventStorage(IMemoryCache cache)
    {
        _cache = cache;
    }
    public void Set(string eventCode, EventModel eventData)
    {
        _cache.Set(eventCode, eventData);
    }

    public bool TryGetValue(string eventCode, out EventModel eventData)
    {
        return _cache.TryGetValue(eventCode, out eventData!);
    }
}