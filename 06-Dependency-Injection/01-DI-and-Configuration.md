# Dependency Injection and Configuration

## What is Dependency Injection?

Providing objects their dependencies instead of having them create them.

### Without DI (Tightly Coupled)

```csharp
public class EmailService
{
    public void SendEmail(string to, string message)
    {
        Console.WriteLine($"Sending email to {to}: {message}");
    }
}

public class UserService
{
    private EmailService _emailService;  // Hard dependency
    
    public UserService()
    {
        _emailService = new EmailService();  // Creates dependency
    }
    
    public void RegisterUser(string email)
    {
        // ... register user ...
        _emailService.SendEmail(email, "Welcome!");
    }
}

// Problem: Hard to test, EmailService tightly coupled
```

### With DI (Loosely Coupled) ⭐

```csharp
// Define interface
public interface IEmailService
{
    void SendEmail(string to, string message);
}

// Implementation
public class EmailService : IEmailService
{
    public void SendEmail(string to, string message)
    {
        Console.WriteLine($"Sending email to {to}: {message}");
    }
}

// Inject dependency
public class UserService
{
    private readonly IEmailService _emailService;
    
    // Dependency injected through constructor
    public UserService(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public void RegisterUser(string email)
    {
        // ... register user ...
        _emailService.SendEmail(email, "Welcome!");
    }
}

// Usage
IEmailService emailService = new EmailService();
UserService userService = new UserService(emailService);
userService.RegisterUser("john@example.com");
```

## Built-in DI Container (ASP.NET Core)

```csharp
// In Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();

// Or with implementation
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Services are automatically injected
app.MapPost("/users/{email}", async (UserService userService, string email) =>
{
    userService.RegisterUser(email);
    return Results.Ok();
});
```

## Service Lifetimes

```csharp
// Transient: New instance each time
services.AddTransient<IEmailService, EmailService>();

// Scoped: New per request/scope
services.AddScoped<IRepository, Repository>();

// Singleton: One instance forever
services.AddSingleton<IConfigService, ConfigService>();
```

### Visual

```
Transient:
Request 1 → EmailService instance A
Request 2 → EmailService instance B
Request 3 → EmailService instance C

Scoped:
Request 1 → EmailService instance A (reused within request)
Request 2 → EmailService instance B (reused within request)
Request 3 → EmailService instance A (new request, new instance)

Singleton:
Request 1 → EmailService instance A
Request 2 → EmailService instance A (same)
Request 3 → EmailService instance A (same)
```

## Configuration

```csharp
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyDb"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ApiKeys": {
    "SendGrid": "sk-..."
  }
}

// In Program.cs
var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
string connectionString = config.GetConnectionString("DefaultConnection");
string apiKey = config["ApiKeys:SendGrid"];

// Use configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);
```

## Best Practices

1. ✅ **Program against interfaces**, not concrete types
2. ✅ **Inject dependencies** through constructor
3. ✅ **Register in DI container** all required services
4. ✅ **Use appropriate lifetime** (Transient/Scoped/Singleton)
5. ❌ **Avoid service locator pattern** (requesting from container)
6. ❌ **Don't create dependencies manually** in services

## Next Steps

Continue with **Entity Framework** to work with databases!
