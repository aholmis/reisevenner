namespace Reisevenner.Web.Data;

public interface IEventStorage
{
    void Set(string eventCode, EventModel eventData);

    bool TryGetValue(string eventCode, out EventModel eventData);
}