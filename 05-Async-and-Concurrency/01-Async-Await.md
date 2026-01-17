# Async/Await and Concurrency

## Asynchronous Programming

Making your app responsive by not blocking on long operations.

### Basic Async/Await

```csharp
// Async method (can be awaited)
public async Task<string> FetchDataAsync()
{
    // Simulate long operation
    await Task.Delay(2000);  // Wait 2 seconds
    return "Data loaded";
}

// Using async method
async Task Main()
{
    string result = await FetchDataAsync();
    Console.WriteLine(result);
}
```

### Task and Task<T>

```csharp
// Task - async operation with no return
public async Task ProcessDataAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("Processing complete");
}

// Task<T> - async operation returning a value
public async Task<int> CalculateAsync()
{
    await Task.Delay(1000);
    return 42;
}

// Usage
await ProcessDataAsync();
int result = await CalculateAsync();
```

### Multiple Async Operations

```csharp
// Sequential (waits for each)
async Task SequentialExample()
{
    var data1 = await FetchData1Async();  // Wait 2s
    var data2 = await FetchData2Async();  // Wait 2s
    // Total: 4 seconds
}

// Parallel (runs simultaneously)
async Task ParallelExample()
{
    var task1 = FetchData1Async();  // Start both
    var task2 = FetchData2Async();
    
    var data1 = await task1;  // Wait for both
    var data2 = await task2;
    // Total: ~2 seconds
}

// Best practice
async Task BestPractice()
{
    var results = await Task.WhenAll(
        FetchData1Async(),
        FetchData2Async(),
        FetchData3Async()
    );
}
```

## Real World Example: Web Requests

```csharp
using System.Net.Http;

public class DataService
{
    private readonly HttpClient _httpClient;
    
    public DataService()
    {
        _httpClient = new HttpClient();
    }
    
    // Don't block UI - use async
    public async Task<string> FetchJsonAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}

// Usage
var service = new DataService();
string json = await service.FetchJsonAsync("https://api.example.com/data");
```

## Concurrent Vs Asynchronous

```csharp
// Concurrent: Multiple tasks in progress (may on same thread)
// Asynchronous: Operations don't block thread

// Synchronous (Blocking)
string data = FetchData();      // App freezes until done
DoSomething(data);

// Asynchronous (Non-blocking)
string data = await FetchDataAsync();  // App responsive
DoSomething(data);
```

## Threading (Lower level)

```csharp
// Create new thread
Thread thread = new Thread(() =>
{
    Console.WriteLine("Running on different thread");
});

thread.Start();
thread.Join();  // Wait for completion
```

## Parallel Processing

```csharp
// Process items in parallel
var items = new List<int> { 1, 2, 3, 4, 5 };

Parallel.ForEach(items, item =>
{
    ProcessItem(item);  // Runs on multiple threads
});

// Map function across items in parallel
var results = items.AsParallel()
    .Select(i => ExpensiveCalculation(i))
    .ToList();
```

## Common Mistakes

❌ **Forgetting await**
```csharp
async Task Example()
{
    var result = FetchDataAsync();  // ❌ Returns Task, not data
    Console.WriteLine(result);
}
```

✅ **Use await**
```csharp
async Task Example()
{
    var result = await FetchDataAsync();  // ✅ Gets actual data
    Console.WriteLine(result);
}
```

❌ **Blocking async code**
```csharp
async Task Example()
{
    var result = FetchDataAsync().Result;  // ❌ Blocks thread
}
```

✅ **Use await instead**
```csharp
async Task Example()
{
    var result = await FetchDataAsync();  // ✅ Doesn't block
}
```

## Quick Reference

| Concept | Use |
|---------|-----|
| `async/await` | Non-blocking operations |
| `Task` | Async operation |
| `Task<T>` | Async operation returning T |
| `await` | Wait for async operation |
| `Task.Delay()` | Async pause |
| `Task.WhenAll()` | Wait for all tasks |
| `ConfigureAwait(false)` | Don't capture context (libraries) |

## Next Steps

Continue with **Dependency Injection** to build scalable applications!
