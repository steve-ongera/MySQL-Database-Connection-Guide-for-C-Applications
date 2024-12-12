# MySQL Database Connection Guide for C# Applications

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Installation Steps](#installation-steps)
3. [Connection Methods](#connection-methods)
4. [Code Examples](#code-examples)
5. [Best Practices](#best-practices)
6. [Error Handling](#error-handling)
7. [Security Considerations](#security-considerations)
8. [Performance Optimization](#performance-optimization)
9. [Troubleshooting](#troubleshooting)

## Prerequisites

### Software Requirements
- .NET Framework 4.7.2 or .NET Core 3.1+
- Visual Studio (recommended)
- MySQL Server (8.0+ recommended)

### Required NuGet Packages
Install the following packages via NuGet Package Manager:
1. `MySql.Data`
2. `MySql.Data.EntityFramework` (for Entity Framework support)

### Installation Commands
```bash
# Package Manager Console
Install-Package MySql.Data
Install-Package MySql.Data.EntityFramework
```

## Connection Methods

### 1. ADO.NET Connection
```csharp
using MySql.Data.MySqlClient;

public class MySqlConnectionManager
{
    private const string ConnectionString = 
        "Server=localhost;" +
        "Database=yourdbname;" +
        "Uid=yourusername;" +
        "Pwd=yourpassword;" +
        "CharSet=utf8;";

    public MySqlConnection GetConnection()
    {
        try 
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
        catch (MySqlException ex)
        {
            // Log error details
            Console.WriteLine($"Connection Error: {ex.Message}");
            throw;
        }
    }
}
```

### 2. Entity Framework Connection
```csharp
public class MyDbContext : DbContext
{
    public MyDbContext() : base("name=MySqlConnection")
    {
        // Configuration settings
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;Database=yourdbname;Uid=username;Pwd=password;");
    }
}
```

## Connection String Parameters

### Common Connection String Parameters
- `Server`: Database host address
- `Database`: Database name
- `Uid`: Username
- `Pwd`: Password
- `Port`: MySQL server port (default 3306)
- `CharSet`: Character encoding
- `SslMode`: SSL connection mode

### Example Advanced Connection String
```csharp
string connectionString = 
    "Server=localhost;" +
    "Database=mydatabase;" +
    "Uid=myuser;" +
    "Pwd=mypassword;" +
    "Port=3306;" +
    "CharSet=utf8;" +
    "SslMode=Required;" +
    "AllowPublicKeyRetrieval=True;" +
    "ConnectionTimeout=30;";
```

## Practical Usage Examples

### Basic Query Execution
```csharp
public List<User> GetAllUsers()
{
    List<User> users = new List<User>();
    
    using (MySqlConnection connection = new MySqlConnection(ConnectionString))
    {
        connection.Open();
        
        string query = "SELECT * FROM Users";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User 
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    });
                }
            }
        }
    }
    
    return users;
}
```

### Parameterized Query
```csharp
public User GetUserById(int userId)
{
    using (MySqlConnection connection = new MySqlConnection(ConnectionString))
    {
        connection.Open();
        
        string query = "SELECT * FROM Users WHERE id = @UserId";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@UserId", userId);
            
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User 
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                }
            }
        }
    }
    
    return null;
}
```

## Best Practices

### Connection Management
1. Always use `using` statements
2. Close connections immediately after use
3. Use connection pooling
4. Implement proper error handling

### Security Recommendations
- Store connection strings in secure configuration
- Use environment variables
- Implement least privilege database users
- Use parameterized queries to prevent SQL injection

## Error Handling

### Common MySQL Exceptions
- `MySqlException`: General MySQL errors
- `MySqlConnnectionException`: Connection-specific errors
- `MySqlCommandException`: Query execution errors

```csharp
try 
{
    // Database operation
}
catch (MySqlException ex)
{
    switch (ex.Number)
    {
        case 1045: // Access denied
            Console.WriteLine("Invalid credentials");
            break;
        case 1049: // Unknown database
            Console.WriteLine("Database not found");
            break;
        default:
            Console.WriteLine($"Unexpected error: {ex.Message}");
            break;
    }
}
```

## Performance Optimization

### Connection Pooling
- Enabled by default in MySQL Connector
- Customize pool settings in connection string
- Monitor connection pool performance

### Connection String Optimization
```csharp
"Pooling=true;" +        // Enable connection pooling
"Min Pool Size=0;" +     // Minimum connections in pool
"Max Pool Size=100;" +   // Maximum connections
"Connection Lifetime=0;" // Connections never expire
```

## Troubleshooting

### Common Issues
1. Connection timeout
2. Firewall blocking
3. Incorrect credentials
4. Network connectivity problems

### Diagnostic Steps
- Verify server accessibility
- Check network configuration
- Validate credentials
- Review MySQL server logs

## Logging

### Recommended Logging Framework
- Serilog
- NLog
- Microsoft.Extensions.Logging

## Additional Resources
- [MySQL Connector/NET Documentation](https://dev.mysql.com/doc/connector-net/en/)
- [Entity Framework Core MySQL Provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)

## Conclusion
Connecting MySQL in C# requires careful configuration and adherence to best practices. Always prioritize security, performance, and robust error handling.

---

**Disclaimer**: Adapt these examples to your specific project requirements and environment.