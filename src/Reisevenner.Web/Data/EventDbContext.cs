using Microsoft.EntityFrameworkCore;

namespace Reisevenner.Web.Data;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    {
    }

    public DbSet<EventEntity> Events { get; set; } = null!;
    public DbSet<DriverEntity> Drivers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Event entity
        modelBuilder.Entity<EventEntity>(entity =>
        {
            entity.HasKey(e => e.Code);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.When).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Where).HasMaxLength(200).IsRequired();
            
            // Configure one-to-many relationship
            entity.HasMany(e => e.Drivers)
                  .WithOne(d => d.Event)
                  .HasForeignKey(d => d.EventCode)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Driver entity
        modelBuilder.Entity<DriverEntity>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).HasMaxLength(100).IsRequired();
            entity.Property(d => d.PhoneNumber).HasMaxLength(20).IsRequired();
            entity.Property(d => d.EventCode).HasMaxLength(50).IsRequired();
            entity.Property(d => d.AvailableSeats).IsRequired();
            entity.Property(d => d.Comment).HasMaxLength(500);
            
            // Configure JSON column for passengers
            entity.Property(d => d.PassengersJson).HasColumnType("TEXT");
        });
    }
}

// Entity classes for database storage
public class EventEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string When { get; set; } = string.Empty;
    public string Where { get; set; } = string.Empty;
    public List<DriverEntity> Drivers { get; set; } = new();
}

public class DriverEntity
{
    public int Id { get; set; }
    public string EventCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int AvailableSeats { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string PassengersJson { get; set; } = "[]"; // JSON serialized passengers
    
    // Navigation property
    public EventEntity Event { get; set; } = null!;
}