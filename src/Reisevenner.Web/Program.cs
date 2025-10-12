using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Reisevenner.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure storage option from appsettings
var useDatabase = builder.Configuration.GetValue<bool>("UseDatabase", false);

if (useDatabase)
{
    // Configure SQLite database
    builder.Services.AddDbContext<EventDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
            ?? "Data Source=events.db"));
    
    builder.Services.AddScoped<IEventStorage, SqliteEventStorage>();
}
else
{
    // Use in-memory cache
    builder.Services.AddMemoryCache();
    builder.Services.AddSingleton<IEventStorage, MemoryEventStorage>();
}

var app = builder.Build();

// Initialize database if using SQLite storage
if (useDatabase)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<EventDbContext>();
        context.Database.EnsureCreated();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
