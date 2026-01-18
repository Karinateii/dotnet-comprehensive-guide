using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetGuide.Async
{
    /// <summary>
    /// Async/Await and Threading Examples
    /// Demonstrates asynchronous patterns, Task parallelization, and cancellation
    /// </summary>

    public class AsyncAwaitExamples
    {
        // Simulate asynchronous operation
        private static async Task<string> FetchDataAsync(int delayMs)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting fetch...");
            await Task.Delay(delayMs);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Fetch completed");
            return "Data fetched successfully";
        }

        // Basic async/await
        public static async Task BasicAsyncExample()
        {
            Console.WriteLine("=== Basic Async/Await ===");
            string result = await FetchDataAsync(2000);
            Console.WriteLine($"Result: {result}\n");
        }

        // Multiple async operations
        public static async Task MultipleAsyncExample()
        {
            Console.WriteLine("=== Sequential Async Operations ===");
            var result1 = await FetchDataAsync(1000);
            var result2 = await FetchDataAsync(1000);
            Console.WriteLine($"Both operations completed\n");
        }

        // Parallel async operations with Task.WhenAll
        public static async Task ParallelAsyncExample()
        {
            Console.WriteLine("=== Parallel Async Operations ===");
            var task1 = FetchDataAsync(2000);
            var task2 = FetchDataAsync(1500);
            var task3 = FetchDataAsync(1000);

            await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("All tasks completed in parallel\n");
        }

        // Task with return value
        public static async Task<int> CalculateAsync(int a, int b)
        {
            await Task.Delay(1000);
            return a + b;
        }

        // Cancellation token example
        public static async Task CancellationExample()
        {
            Console.WriteLine("=== Cancellation Token ===");
            using (var cts = new CancellationTokenSource())
            {
                // Cancel after 3 seconds
                cts.CancelAfter(TimeSpan.FromSeconds(3));

                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        Console.WriteLine($"Processing item {i}...");
                        await Task.Delay(1000, cts.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation was cancelled\n");
                }
            }
        }

        // Exception handling in async operations
        public static async Task ExceptionHandlingExample()
        {
            Console.WriteLine("=== Exception Handling in Async ===");
            try
            {
                var task = Task.Run(() =>
                {
                    throw new InvalidOperationException("Something went wrong!");
                });
                await task;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}\n");
            }
        }

        // Task.WhenAny - Return when first task completes
        public static async Task WhenAnyExample()
        {
            Console.WriteLine("=== Task.WhenAny ===");
            var task1 = FetchDataAsync(3000);
            var task2 = FetchDataAsync(1000);
            var task3 = FetchDataAsync(2000);

            var completedTask = await Task.WhenAny(task1, task2, task3);
            Console.WriteLine($"First task completed: {await completedTask}\n");
        }

        // Async IEnumerable (C# 8+)
        public static async IAsyncEnumerable<int> GenerateNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }

        public static async Task AsyncEnumerableExample()
        {
            Console.WriteLine("=== Async IEnumerable ===");
            await foreach (var number in GenerateNumbersAsync())
            {
                Console.WriteLine($"Number: {number}");
            }
            Console.WriteLine();
        }

        // Fire and forget pattern (use with caution)
        public static void FireAndForgetExample()
        {
            Console.WriteLine("=== Fire and Forget Pattern ===");
            
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Background task completed\n");
            });

            Console.WriteLine("Main thread continues without waiting");
        }

        // ValueTask for synchronous completions
        public static async ValueTask<int> FastOperationAsync(bool useSync)
        {
            if (useSync)
            {
                return 42; // Synchronous completion (no allocation)
            }
            else
            {
                await Task.Delay(1000);
                return 42; // Asynchronous completion
            }
        }

        public static async Task Main()
        {
            await BasicAsyncExample();
            await MultipleAsyncExample();
            await ParallelAsyncExample();
            await CancellationExample();
            await ExceptionHandlingExample();
            await WhenAnyExample();
            await AsyncEnumerableExample();
            
            FireAndForgetExample();
            await Task.Delay(3000); // Give time for fire-and-forget to complete
        }
    }
}
