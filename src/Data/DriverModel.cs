namespace BlazorApp1.Data;

public class DriverModel
{
    public string DriverName { get; set; }
    public string Phone { get; set; }
    public int AvailableSpace { get; set; }
    public List<Passenger> Passengers { get; set; }
}

public record Passenger(string Name, string Phone);

public record EventModel(string Code, string Name, string When, string Where, List<DriverModel> Drivers);