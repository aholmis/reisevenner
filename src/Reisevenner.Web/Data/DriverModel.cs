namespace Reisevenner.Web.Data;

public record DriverModel
{
    public string DriverName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int AvailableSpace { get; set; }
    public List<PassengerModel> Passengers { get; set; } = new();
}

public record PassengerModel
{
    public string Name { get; set; } = string.Empty;
    public string PhoneOrComment { get; set; } = string.Empty;
}

public record EventModel(string Code, string Name, string When, string Where, List<DriverModel> Drivers);