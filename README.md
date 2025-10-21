# 🚗 Reisevenner - Shared Transportation Coordinator

**Reisevenner** (Norwegian for "Travel Friends") is a modern web application built with ASP.NET Core Blazor Server that helps organize shared transportation for events, celebrations, and activities. Make carpooling easy while contributing to environmental sustainability!

## 🌟 Features

### 🎉 **Event Management**
- **Create Events**: Organize transportation for birthdays, sports events, parties, and any group activities
- **Unique Event Codes**: Each event gets a shareable code for easy access
- **Event Details**: Set event name, date/time, and location
- **Instant Sharing**: Share the event URL with participants

### 🚙 **Driver Coordination**
- **Driver Registration**: Drivers can sign up and specify available seats
- **Contact Information**: Drivers provide name and phone number for coordination
- **Capacity Management**: Automatic tracking of available seats per vehicle
- **Flexible Participation**: Drivers can withdraw if plans change

### 👥 **Passenger Management**
- **Easy Sign-up**: Passengers can join available rides with one click
- **Contact Details**: Passengers provide name and phone/comments for coordination
- **Automatic Capacity Control**: System prevents overbooking of vehicles
- **Real-time Updates**: Live view of who's riding with whom

### 🔧 **Technical Features**
- **Dual Storage Options**: 
  - In-memory storage for development/testing
  - SQLite database for production persistence
- **Modern UI**: Responsive Blazor Server interface
- **Real-time Updates**: Server-side rendering with live updates
- **Environmental Focus**: Promotes carpooling to reduce carbon footprint

## 🚀 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/aholmis/reisevenner.git
   cd reisevenner
   ```

2. **Navigate to the application**
   ```bash
   cd src/Reisevenner.Web
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Open your browser**
   - Navigate to `https://localhost:5271` (HTTPS) or `http://localhost:5271` (HTTP)

## 📋 How to Use

### For Event Organizers:
1. **Create an Event**: Click "Lag en ny event" on the home page
2. **Fill Details**: Enter event name, date/time, and location
3. **Share the Code**: Send the generated event code or URL to participants
4. **Coordinate**: Monitor who's driving and riding

### For Drivers:
1. **Join Event**: Enter the event code or use the shared URL
2. **Volunteer as Driver**: Click "Legg til sjåfør" (Add Driver)
3. **Provide Details**: Enter your name, phone, and available seats
4. **Coordinate**: Connect with your passengers before the event

### For Passengers:
1. **Access Event**: Use the event code or shared URL
2. **Find a Ride**: Browse available drivers
3. **Join a Car**: Click "Jeg vil sitte på" (I want to ride along)
4. **Provide Contact**: Enter your name and phone/comments

## ⚙️ Configuration

### Storage Options

The application supports two storage modes:

- **Development**: Uses in-memory storage (data lost on restart)
- **Production**: Uses SQLite database for persistence

Configure in `appsettings.json`:
```json
{
  "UseDatabase": true,  // true for SQLite, false for in-memory
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=events.db"
  }
}
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **UI**: Blazor Server
- **Database**: SQLite with Entity Framework Core
- **Language**: C# with Razor syntax
- **Storage**: Configurable (In-memory/SQLite)

## 📁 Project Structure

```
src/Reisevenner.Web/
├── Data/                    # Data models and storage
│   ├── EventDbContext.cs    # Entity Framework context
│   ├── SqliteEventStorage.cs # SQLite implementation
│   ├── MemoryEventStorage.cs # In-memory implementation
│   └── Models/              # Data models
├── Pages/                   # Blazor pages
│   ├── Index.razor          # Home page
│   ├── CreateEvent.razor    # Event creation
│   ├── Event.razor          # Event details
│   ├── Driver.razor         # Driver component
│   └── Passenger.razor      # Passenger component
├── Shared/                  # Shared components
└── wwwroot/                 # Static files
```

## 🌍 Environmental Impact

Reisevenner promotes sustainable transportation by:
- **Reducing CO₂ emissions** through carpooling
- **Minimizing traffic congestion** by sharing rides
- **Building community** through shared travel experiences
- **Making environmental choices easy** and social

## 🤝 Contributing

We welcome contributions! Please feel free to:
- Report bugs and issues
- Suggest new features
- Submit pull requests
- Improve documentation

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**"Sammen gjør vi en innsats for miljøet"** - *Together we make an effort for the environment*