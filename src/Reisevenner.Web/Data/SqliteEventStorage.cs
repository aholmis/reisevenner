using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Reisevenner.Web.Data;

public class SqliteEventStorage : IEventStorage
{
    private readonly EventDbContext _context;
    private readonly ILogger<SqliteEventStorage> _logger;

    public SqliteEventStorage(EventDbContext context, ILogger<SqliteEventStorage> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Set(string eventCode, EventModel eventData)
    {
        try
        {
            // Check if event already exists
            var existingEvent = _context.Events
                .Include(e => e.Drivers)
                .FirstOrDefault(e => e.Code == eventCode);

            if (existingEvent != null)
            {
                // Update existing event
                UpdateExistingEvent(existingEvent, eventData);
            }
            else
            {
                // Create new event
                CreateNewEvent(eventCode, eventData);
            }

            _context.SaveChanges();
            _logger.LogInformation("Successfully saved event {EventCode}", eventCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving event {EventCode}", eventCode);
            throw;
        }
    }

    public bool TryGetValue(string eventCode, out EventModel eventData)
    {
        try
        {
            var eventEntity = _context.Events
                .Include(e => e.Drivers)
                .FirstOrDefault(e => e.Code == eventCode);

            if (eventEntity != null)
            {
                eventData = MapToEventModel(eventEntity);
                return true;
            }

            eventData = default!;
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving event {EventCode}", eventCode);
            eventData = default!;
            return false;
        }
    }

    private void CreateNewEvent(string eventCode, EventModel eventData)
    {
        var eventEntity = new EventEntity
        {
            Code = eventCode,
            Name = eventData.Name,
            When = eventData.When,
            Where = eventData.Where,
            Drivers = eventData.Drivers.Select(MapToDriverEntity).ToList()
        };

        // Set the EventCode for each driver
        foreach (var driver in eventEntity.Drivers)
        {
            driver.EventCode = eventCode;
        }

        _context.Events.Add(eventEntity);
    }

    private void UpdateExistingEvent(EventEntity existingEvent, EventModel eventData)
    {
        // Update event properties
        existingEvent.Name = eventData.Name;
        existingEvent.When = eventData.When;
        existingEvent.Where = eventData.Where;

        // Remove existing drivers
        _context.Drivers.RemoveRange(existingEvent.Drivers);

        // Add new drivers
        existingEvent.Drivers = eventData.Drivers.Select(d =>
        {
            var driverEntity = MapToDriverEntity(d);
            driverEntity.EventCode = existingEvent.Code;
            return driverEntity;
        }).ToList();
    }

    private DriverEntity MapToDriverEntity(DriverModel driver)
    {
        return new DriverEntity
        {
            Name = driver.DriverName,
            PhoneNumber = driver.Phone,
            AvailableSeats = driver.AvailableSpace,
            Comment = string.Empty, // DriverModel doesn't have Comment field
            PassengersJson = JsonSerializer.Serialize(driver.Passengers)
        };
    }

    private EventModel MapToEventModel(EventEntity eventEntity)
    {
        var drivers = eventEntity.Drivers.Select(MapToDriverModel).ToList();
        return new EventModel(eventEntity.Code, eventEntity.Name, eventEntity.When, eventEntity.Where, drivers);
    }

    private DriverModel MapToDriverModel(DriverEntity driverEntity)
    {
        var passengers = string.IsNullOrEmpty(driverEntity.PassengersJson) 
            ? new List<PassengerModel>()
            : JsonSerializer.Deserialize<List<PassengerModel>>(driverEntity.PassengersJson) ?? new List<PassengerModel>();

        return new DriverModel
        {
            DriverName = driverEntity.Name,
            Phone = driverEntity.PhoneNumber,
            AvailableSpace = driverEntity.AvailableSeats,
            Passengers = passengers
        };
    }
}