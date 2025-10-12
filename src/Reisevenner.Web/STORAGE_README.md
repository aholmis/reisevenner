# Event Storage Options

The Reisevenner application now supports two different storage options for events:

## 1. In-Memory Storage (MemoryEventStorage)

- **Use case**: Development, testing, temporary data
- **Configuration**: Set `"UseDatabase": false` in appsettings.json
- **Pros**: 
  - Fast access
  - No database setup required
  - Simple configuration
- **Cons**: 
  - Data is lost when application restarts
  - Not suitable for production
  - Limited to single server instance

## 2. SQLite Database Storage (SqliteEventStorage)

- **Use case**: Production, persistent data storage
- **Configuration**: Set `"UseDatabase": true` in appsettings.json
- **Database file**: `events.db` (created automatically)
- **Pros**: 
  - Persistent data storage
  - Survives application restarts
  - Better for production scenarios
  - ACID compliance
  - Can handle concurrent access
- **Cons**: 
  - Slightly slower than memory storage
  - Requires file system access

## Configuration

### appsettings.json (Production - uses SQLite database)
```json
{
  "UseDatabase": true,
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=events.db"
  }
}
```

### appsettings.Development.json (Development - uses in-memory)
```json
{
  "UseDatabase": false
}
```

## Database Schema

When using SQLite storage, the following tables are created automatically:

### Events Table
- `Code` (TEXT, Primary Key) - Event identifier
- `Name` (TEXT) - Event name
- `When` (TEXT) - Event date/time
- `Where` (TEXT) - Event location

### Drivers Table
- `Id` (INTEGER, Primary Key, Auto-increment) - Driver ID
- `EventCode` (TEXT, Foreign Key) - Reference to Events.Code
- `Name` (TEXT) - Driver name
- `PhoneNumber` (TEXT) - Driver phone
- `AvailableSeats` (INTEGER) - Available seats
- `Comment` (TEXT) - Additional comments
- `PassengersJson` (TEXT) - JSON serialized passenger list

## Switching Storage Types

To switch between storage types:

1. **To use in-memory storage**: Set `"UseDatabase": false`
2. **To use SQLite storage**: Set `"UseDatabase": true`

The application will automatically configure the appropriate storage implementation based on this setting.

## Database Management

### Backup
To backup your SQLite data, simply copy the `events.db` file.

### Reset Database
To reset the database, delete the `events.db` file. It will be recreated on the next application start.

### Manual Database Access
You can access the SQLite database directly using any SQLite client:
```bash
sqlite3 events.db
```

## Migration from Memory to Database Storage

When switching from memory to database storage, you'll need to recreate your events as the memory storage doesn't persist data. Consider exporting/importing data if you have important events to preserve.