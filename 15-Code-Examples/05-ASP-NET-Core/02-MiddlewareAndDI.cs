using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetGuide.AspNetCore
{
    /// <summary>
    /// ASP.NET Core Middleware and Dependency Injection Examples
    /// Demonstrates custom middleware, DI container, and request pipeline
    /// </summary>

    // Simple service interface
    public interface IGreetingService
    {
        string GetGreeting(string name);
    }

    // Implementation
    public class GreetingService : IGreetingService
    {
        public string GetGreeting(string name)
        {
            return $"Hello, {name}! Welcome to ASP.NET Core.";
        }
    }

    // Repository pattern example
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Mouse", Price = 29.99m },
            new Product { Id = 3, Name = "Keyboard", Price = 79.99m }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }

        public Product GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }

    // Custom middleware for logging
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Request: {context.Request.Method} {context.Request.Path}");
            
            await _next(context);
            
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Response: {context.Response.StatusCode}");
        }
    }

    // Custom middleware for exception handling
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An internal server error occurred");
            }
        }
    }

    // Example Startup class demonstrating DI configuration
    public class StartupExample
    {
        // This method is called by the runtime to configure services
        public void ConfigureServices(IServiceCollection services)
        {
            // Register services with different lifetimes
            
            // Transient: New instance every time
            services.AddTransient<IGreetingService, GreetingService>();
            
            // Scoped: One per HTTP request
            services.AddScoped<IProductRepository, ProductRepository>();
            
            // Singleton: One instance for entire application
            services.AddSingleton<Random>();

            services.AddControllers();
        }

        // This method is called by the runtime to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app)
        {
            // Add custom middleware
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("ASP.NET Core is running!");
                });

                endpoints.MapGet("/greet/{name}", async context =>
                {
                    var greetingService = context.RequestServices.GetRequiredService<IGreetingService>();
                    var name = (string)context.Request.RouteValues["name"];
                    var greeting = greetingService.GetGreeting(name);
                    await context.Response.WriteAsync(greeting);
                });

                endpoints.MapGet("/products", async context =>
                {
                    var repository = context.RequestServices.GetRequiredService<IProductRepository>();
                    var products = repository.GetAllProducts();
                    
                    var json = System.Text.Json.JsonSerializer.Serialize(products);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });

                endpoints.MapGet("/products/{id}", async context =>
                {
                    var repository = context.RequestServices.GetRequiredService<IProductRepository>();
                    var id = int.Parse((string)context.Request.RouteValues["id"]);
                    var product = repository.GetProductById(id);
                    
                    if (product == null)
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync("Product not found");
                        return;
                    }

                    var json = System.Text.Json.JsonSerializer.Serialize(product);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });
            });
        }
    }

    public class AspNetCoreExample
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This is an example of ASP.NET Core configuration.");
            Console.WriteLine("To run this in a real application:");
            Console.WriteLine("  1. Create an ASP.NET Core project");
            Console.WriteLine("  2. Copy this configuration to Startup.cs or Program.cs");
            Console.WriteLine("  3. Run 'dotnet run' from the project directory");
        }
    }
}
