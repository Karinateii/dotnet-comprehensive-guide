# Common Design Patterns

## Creational Patterns

### Singleton

One instance, globally accessible:

```csharp
public class Database
{
    private static Database _instance;
    
    private Database() { }
    
    public static Database GetInstance()
    {
        if (_instance == null)
            _instance = new Database();
        return _instance;
    }
}

// Thread-safe version
public sealed class Database
{
    private static readonly Lazy<Database> _instance =
        new Lazy<Database>(() => new Database());
    
    public static Database Instance => _instance.Value;
    
    private Database() { }
}
```

### Factory

Creates objects without specifying exact classes:

```csharp
public interface IEmailService
{
    void SendEmail(string to, string message);
}

public class EmailServiceFactory
{
    public static IEmailService Create(string providerType)
    {
        return providerType switch
        {
            "gmail" => new GmailService(),
            "sendgrid" => new SendGridService(),
            "smtp" => new SmtpService(),
            _ => throw new ArgumentException("Unknown provider")
        };
    }
}

// Usage
var service = EmailServiceFactory.Create("gmail");
service.SendEmail("user@example.com", "Hello");
```

### Builder

Constructing complex objects step by step:

```csharp
public class QueryBuilder
{
    private string _select = "*";
    private string _from = "";
    private string _where = "";
    
    public QueryBuilder Select(string columns)
    {
        _select = columns;
        return this;
    }
    
    public QueryBuilder From(string table)
    {
        _from = table;
        return this;
    }
    
    public QueryBuilder Where(string condition)
    {
        _where = condition;
        return this;
    }
    
    public string Build()
    {
        return $"SELECT {_select} FROM {_from} WHERE {_where}";
    }
}

// Usage
string query = new QueryBuilder()
    .Select("Id, Name, Email")
    .From("Users")
    .Where("Age > 18")
    .Build();
```

## Structural Patterns

### Decorator

Adding functionality without modifying original:

```csharp
public interface IDataSource
{
    string ReadData();
}

public class FileDataSource : IDataSource
{
    public string ReadData() => "Data from file";
}

public class EncryptionDecorator : IDataSource
{
    private readonly IDataSource _source;
    
    public EncryptionDecorator(IDataSource source)
    {
        _source = source;
    }
    
    public string ReadData()
    {
        string data = _source.ReadData();
        return Encrypt(data);
    }
    
    private string Encrypt(string data) => Convert.ToBase64String(
        System.Text.Encoding.UTF8.GetBytes(data)
    );
}

// Usage
IDataSource source = new FileDataSource();
source = new EncryptionDecorator(source);
string encrypted = source.ReadData();  // Encrypted!
```

### Adapter

Making incompatible interfaces work together:

```csharp
// Old interface
public interface IOldLogger
{
    void LogMessage(string msg);
}

// New interface
public interface ILogger
{
    void Log(string message);
}

// Adapter
public class LoggerAdapter : ILogger
{
    private readonly IOldLogger _oldLogger;
    
    public LoggerAdapter(IOldLogger oldLogger)
    {
        _oldLogger = oldLogger;
    }
    
    public void Log(string message)
    {
        _oldLogger.LogMessage(message);  // Call old method
    }
}
```

## Behavioral Patterns

### Strategy

Selecting algorithm at runtime:

```csharp
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"Credit card: {amount}");
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"PayPal: {amount}");
}

public class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;
    
    public void SetPaymentMethod(IPaymentStrategy strategy)
    {
        _paymentStrategy = strategy;
    }
    
    public void Checkout(decimal total)
    {
        _paymentStrategy.Pay(total);
    }
}

// Usage
var cart = new ShoppingCart();
cart.SetPaymentMethod(new CreditCardPayment());
cart.Checkout(100);  // Credit card payment

cart.SetPaymentMethod(new PayPalPayment());
cart.Checkout(50);   // PayPal payment
```

### Observer

Notify multiple objects of state changes:

```csharp
public interface IObserver
{
    void Update(string message);
}

public class EventPublisher
{
    private List<IObserver> _observers = new();
    
    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }
    
    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }
    
    public void PublishEvent(string message)
    {
        foreach (var observer in _observers)
            observer.Update(message);
    }
}

public class EmailSubscriber : IObserver
{
    public void Update(string message) =>
        Console.WriteLine($"Email notification: {message}");
}

// Usage
var publisher = new EventPublisher();
publisher.Subscribe(new EmailSubscriber());
publisher.PublishEvent("New product available!");
```

## When to Use Each Pattern

| Pattern | Use When |
|---------|----------|
| Singleton | Need one global instance |
| Factory | Creating different objects |
| Builder | Complex object construction |
| Decorator | Adding functionality |
| Adapter | Making incompatible code work |
| Strategy | Switching algorithms |
| Observer | Broadcasting changes |

## SOLID Principles Review

```csharp
// S - Single Responsibility
public class EmailService        // Only sends emails
{
    public void SendEmail() { }
}

// O - Open/Closed
public interface ILogger { }     // Open for extension
public class FileLogger : ILogger { }  // Closed for modification

// L - Liskov Substitution
// Derived classes should be substitutable for base

// I - Interface Segregation
// Many specific interfaces > one bloated interface

// D - Dependency Inversion
// Depend on abstractions, not concrete types
public class Service
{
    public Service(IRepository repo) { }  // Interface, not concrete
}
```

## Next Steps

Learn **Security** to protect your applications!
