# Performance and Optimization

## Profiling and Benchmarking

### BenchmarkDotNet

```powershell
dotnet add package BenchmarkDotNet
```

```csharp
[MemoryDiagnoser]
public class PerformanceBenchmarks
{
    private readonly List<int> _numbers = Enumerable.Range(1, 10000).ToList();
    
    [Benchmark]
    public int ForEachLoop()
    {
        int sum = 0;
        foreach (int num in _numbers)
            sum += num;
        return sum;
    }
    
    [Benchmark]
    public int LinqSum()
    {
        return _numbers.Sum();
    }
}

// Run: dotnet run -c Release
// Output: Shows which is faster
```

## Caching

### In-Memory Caching

```csharp
builder.Services.AddMemoryCache();

public class ProductService
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "products";
    
    public ProductService(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public async Task<List<Product>> GetAllAsync()
    {
        if (_cache.TryGetValue(CacheKey, out List<Product> products))
            return products;  // Return cached
        
        // Fetch from database
        products = await _repository.GetAllAsync();
        
        // Cache for 1 hour
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));
        
        _cache.Set(CacheKey, products, cacheOptions);
        return products;
    }
    
    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = await _repository.AddAsync(dto);
        
        // Invalidate cache
        _cache.Remove(CacheKey);
        
        return product;
    }
}
```

### Distributed Caching (Redis)

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

public class CachedProductService
{
    private readonly IDistributedCache _cache;
    
    public CachedProductService(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    public async Task<Product> GetByIdAsync(int id)
    {
        string cacheKey = $"product_{id}";
        
        // Try cache
        string cachedValue = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedValue))
            return JsonSerializer.Deserialize<Product>(cachedValue);
        
        // Get from database
        var product = await _repository.GetByIdAsync(id);
        
        // Cache
        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(product),
            new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
        );
        
        return product;
    }
}
```

## Database Optimization

### N+1 Query Problem

```csharp
// ❌ Bad - Multiple queries (N+1)
var users = await context.Users.ToListAsync();
foreach (var user in users)
{
    var orders = await context.Orders
        .Where(o => o.UserId == user.Id)
        .ToListAsync();  // Query for each user!
}

// ✅ Good - Single query with Include
var users = await context.Users
    .Include(u => u.Orders)
    .ToListAsync();  // Load orders together
```

### Query Filtering

```csharp
// ❌ Bad - Load all, filter in memory
var users = await context.Users.ToListAsync();
var activeUsers = users.Where(u => u.IsActive).ToList();

// ✅ Good - Filter in database
var activeUsers = await context.Users
    .Where(u => u.IsActive)
    .ToListAsync();
```

### Indexing

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Create index for frequently queried column
    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
    
    // Composite index
    modelBuilder.Entity<Order>()
        .HasIndex(o => new { o.UserId, o.CreatedAt });
}
```

## Async All the Way

```csharp
// ❌ Bad - Blocking call
public ActionResult GetUser(int id)
{
    var user = _service.GetUserById(id);  // Blocks
    return Ok(user);
}

// ✅ Good - Async
public async Task<ActionResult> GetUserAsync(int id)
{
    var user = await _service.GetUserByIdAsync(id);  // Non-blocking
    return Ok(user);
}
```

## String Optimization

```csharp
// ❌ Bad - Creates many string objects
string result = "";
for (int i = 0; i < 10000; i++)
{
    result += i;  // Creates new string each time
}

// ✅ Good - Use StringBuilder
var sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
{
    sb.Append(i);
}
string result = sb.ToString();
```

## LINQ Performance

```csharp
// ❌ Bad - Multiple enumerations
var list = numbers.Where(n => n > 5);
if (list.Any())
    var total = list.Sum();

// ✅ Good - Enumerate once
var list = numbers.Where(n => n > 5).ToList();
if (list.Any())
    var total = list.Sum();

// Or use SingleOrDefault instead of Where + FirstOrDefault
var user = await context.Users
    .SingleOrDefaultAsync(u => u.Id == id);
```

## Memory Management

### Using Statement

```csharp
// Automatic disposal
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    // Use connection
} // Automatically disposed

// Or modern syntax
using var connection = new SqlConnection(connectionString);
connection.Open();
// Automatically disposed at end of scope
```

## Compression

```csharp
// Compress HTTP responses
app.UseResponseCompression();

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults
        .MimeTypes
        .Concat(new[] { "application/json" });
});
```

## Static Files and CDN

```csharp
// Serve static files with caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.CacheControl =
            "public,max-age=3600";  // Cache for 1 hour
    }
});
```

## Monitoring Performance

```csharp
public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;
    
    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        await _next(context);
        
        stopwatch.Stop();
        
        if (stopwatch.ElapsedMilliseconds > 1000)  // Log slow requests
        {
            _logger.LogWarning(
                "Slow request: {Path} took {ElapsedMilliseconds}ms",
                context.Request.Path,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}

// Register in Program.cs
app.UseMiddleware<PerformanceMiddleware>();
```

## Performance Checklist

- ✅ Use async/await consistently
- ✅ Cache frequently accessed data
- ✅ Use database indexes
- ✅ Avoid N+1 queries
- ✅ Filter data in database, not in memory
- ✅ Monitor slow requests
- ✅ Use StringBuilder for string concatenation
- ✅ Compress HTTP responses
- ✅ Implement pagination
- ✅ Lazy load related data

## Next Steps

You've covered all major topics! Continue with **Advanced Topics** for deeper knowledge.
