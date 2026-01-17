# Advanced Topics

## Reflection

Inspecting and manipulating types at runtime.

```csharp
public class ReflectionExamples
{
    [Obsolete("Use NewMethod instead")]
    public void OldMethod() { }
    
    public void NewMethod() { }
}

// Get type information
Type type = typeof(ReflectionExamples);

// Get all methods
MethodInfo[] methods = type.GetMethods();
foreach (var method in methods)
{
    Console.WriteLine($"Method: {method.Name}");
}

// Check for attributes
var method = type.GetMethod("OldMethod");
var obsoleteAttr = method.GetCustomAttribute<ObsoleteAttribute>();
if (obsoleteAttr != null)
    Console.WriteLine(obsoleteAttr.Message);

// Create instance dynamically
object instance = Activator.CreateInstance(type);
```

## Delegates and Events

### Delegates

```csharp
// Define delegate
public delegate void ProcessDataDelegate(string data);

// Use delegate
ProcessDataDelegate handler = Console.WriteLine;
handler("Hello");  // Executes Console.WriteLine

// Multiple subscribers
handler += (data) => Console.WriteLine($"Also: {data}");
handler("World");  // Prints twice
```

### Events

```csharp
public class Button
{
    public event EventHandler Clicked;
    
    public void Click()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }
}

var button = new Button();
button.Clicked += (sender, e) => Console.WriteLine("Button clicked");
button.Click();  // Prints: Button clicked
```

## Extension Methods

```csharp
// Extend existing types
public static class StringExtensions
{
    public static string Reverse(this string str)
    {
        char[] chars = str.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    
    public static bool IsPalindrome(this string str)
    {
        return str == str.Reverse();
    }
}

// Usage
string text = "hello";
string reversed = text.Reverse();           // "olleh"
bool isPalin = "racecar".IsPalindrome();    // true
```

## Generic Types

Creating flexible, type-safe code:

```csharp
// Generic class
public class Repository<T> where T : class
{
    private List<T> _items = new();
    
    public void Add(T item) => _items.Add(item);
    public T GetById(int id) => _items.FirstOrDefault();
    public List<T> GetAll() => _items;
}

// Usage
var userRepo = new Repository<User>();
userRepo.Add(new User { Name = "John" });

var productRepo = new Repository<Product>();
productRepo.Add(new Product { Name = "Laptop" });

// Generic method
public T GetRandom<T>(List<T> items)
{
    return items[new Random().Next(items.Count)];
}

var randomUser = GetRandom(users);
var randomProduct = GetRandom(products);
```

## Nullable Reference Types

```csharp
#nullable enable

public class Person
{
    // Can't be null
    public string Name { get; set; }
    
    // Can be null (explicit)
    public string? NickName { get; set; }
    
    // Parameter can't be null
    public void SetName(string name) { }
    
    // Parameter can be null
    public void SetNickName(string? nickName) { }
}

var person = new Person();
person.Name = null;      // ⚠️ Warning
person.NickName = null;  // ✓ OK
```

## Attributes

Metadata about code:

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class LogExecutionAttribute : Attribute
{
    public string Message { get; set; }
}

public class UserService
{
    [LogExecution(Message = "Getting user")]
    public User GetUser(int id) { ... }
}

// Use reflection to check attributes
var method = typeof(UserService).GetMethod("GetUser");
var attr = method.GetCustomAttribute<LogExecutionAttribute>();
if (attr != null)
    Console.WriteLine(attr.Message);
```

## LINQ Advanced

### GroupBy and Aggregate

```csharp
var students = new List<Student>
{
    new Student { Name = "John", Grade = 'A', Score = 95 },
    new Student { Name = "Alice", Grade = 'A', Score = 92 },
    new Student { Name = "Bob", Grade = 'B', Score = 85 },
};

// Group by grade
var grouped = students.GroupBy(s => s.Grade);
foreach (var group in grouped)
{
    Console.WriteLine($"Grade {group.Key}: {group.Count()} students");
}

// Complex aggregation
var stats = students
    .GroupBy(s => s.Grade)
    .Select(g => new
    {
        Grade = g.Key,
        Count = g.Count(),
        Average = g.Average(s => s.Score)
    });
```

### Join

```csharp
var users = new List<User> { ... };
var orders = new List<Order> { ... };

// Inner join
var userOrders = users.Join(
    orders,
    u => u.Id,
    o => o.UserId,
    (u, o) => new { u.Name, o.Total }
);

// Group join
var userOrderGroups = users.GroupJoin(
    orders,
    u => u.Id,
    o => o.UserId,
    (u, os) => new
    {
        User = u.Name,
        Orders = os.Count()
    }
);
```

## Expression Trees

Creating code at runtime:

```csharp
// Create lambda: x => x > 5
Expression<Func<int, bool>> expr = x => x > 5;

// Compile and execute
var compiled = expr.Compile();
bool result = compiled(10);  // true
bool result2 = compiled(3);  // false

// Use for dynamic queries
public Expression<Func<User, bool>> GetFilterExpression(string role)
{
    return u => u.Role == role;
}

var filter = GetFilterExpression("Admin");
var admins = context.Users.Where(filter).ToList();
```

## Async Streams

```csharp
public async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(100);  // Simulate async work
        yield return i;
    }
}

// Usage
await foreach (var number in GetNumbersAsync())
{
    Console.WriteLine(number);
}
```

## Channels

For advanced async communication:

```csharp
using System.Threading.Channels;

public class DataProcessor
{
    private readonly Channel<Data> _channel = Channel.CreateUnbounded<Data>();
    
    public async Task ProduceAsync()
    {
        for (int i = 0; i < 100; i++)
        {
            await _channel.Writer.WriteAsync(new Data { Value = i });
        }
        _channel.Writer.Complete();
    }
    
    public async Task ConsumeAsync()
    {
        await foreach (var data in _channel.Reader.ReadAllAsync())
        {
            Console.WriteLine(data.Value);
        }
    }
}
```

## Tuple Deconstruction

```csharp
public class Calculator
{
    public (int Sum, int Product) Calculate(int a, int b)
    {
        return (a + b, a * b);
    }
}

var calc = new Calculator();

// Deconstruct tuple
(int sum, int product) = calc.Calculate(5, 3);
Console.WriteLine($"Sum: {sum}, Product: {product}");

// Or with var
var (s, p) = calc.Calculate(5, 3);

// Deconstruct in switch
string result = (sum, product) switch
{
    (0, 0) => "Both zero",
    (> 0, > 0) => "Both positive",
    _ => "Mixed"
};
```

## Source Generators (.NET 5+)

Creating code at compile time:

```csharp
[Generator]
public class ClassGeneratorSyntaxReceiver : ISyntaxReceiver
{
    // Generates code during compilation
}

// Automatically generated classes are available at compile time
// Useful for: DTOs, serializers, API clients, etc.
```

## Next Steps

You've reached expert level! Continue:
- Reading source code of popular projects
- Contributing to open source
- Building production applications
- Specializing in specific domains (Cloud, Desktop, Gaming, etc.)

## Learning Resources

- Official Microsoft Docs
- GitHub repositories
- Open source projects
- Tech blogs and articles
- Podcasts and webinars
