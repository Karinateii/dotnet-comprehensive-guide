# ASP.NET Core Fundamentals

## What is ASP.NET Core?

Framework for building web applications, APIs, and services.

### Creating an ASP.NET Core App

```powershell
# Web API
dotnet new webapi -n MyApi

# Web MVC
dotnet new mvc -n MyWebApp

# Minimal API
dotnet new web -n MinimalApi
```

## Program.cs Structure

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

// Configure HTTP pipeline (middleware)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## Middleware

Pipeline that processes requests.

```
Request
  ↓
[Error Handling Middleware]
  ↓
[Authentication Middleware]
  ↓
[Authorization Middleware]
  ↓
[Your Code]
  ↓
Response
```

### Custom Middleware

```csharp
app.Use(async (context, next) =>
{
    // Before
    Console.WriteLine($"Request: {context.Request.Path}");
    
    await next();  // Call next middleware
    
    // After
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});
```

## Controllers

Handle HTTP requests.

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        await _userService.UpdateUserAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
```

## HTTP Methods

```csharp
[HttpGet]      // Retrieve data
[HttpPost]     // Create data
[HttpPut]      // Update entire resource
[HttpPatch]    // Update partial resource
[HttpDelete]   // Delete data
```

## Routing

```csharp
// Attribute routing
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id) { ... }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts(string query) { ... }
}

// Routes generated:
// GET  /api/products/{id}
// GET  /api/products/search?query=...
```

## Model Binding

Automatically maps request data to parameters.

```csharp
[HttpPost]
public async Task<IActionResult> CreateUser(
    [FromBody] CreateUserDto dto,      // From request body
    [FromQuery] string? notifications, // From query string
    [FromRoute] int id                 // From URL
)
{
    // dto, notifications, id are populated automatically
    return Ok();
}
```

## Validation

```csharp
public class CreateUserDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Range(18, 120)]
    public int Age { get; set; }
}

[HttpPost]
public async Task<IActionResult> CreateUser(CreateUserDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    
    // Process...
}

// Or automatic validation
[HttpPost]
public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
{
    // Validation happens automatically
    // Invalid requests get 400 BadRequest
}
```

## Environment Configuration

```csharp
if (app.Environment.IsDevelopment())
{
    // Development only
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    // Production only
    app.UseHsts();
}

// Check environment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
```

## Minimal APIs (.NET 6+)

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/users/{id}", async (int id, IUserService service) =>
    await service.GetUserByIdAsync(id))
   .WithName("GetUser")
   .WithOpenApi();

app.MapPost("/users", async (CreateUserDto dto, IUserService service) =>
    await service.CreateUserAsync(dto))
   .WithName("CreateUser");

app.Run();
```

Simpler than controllers for small APIs!

## Next Steps

Learn **Web APIs** for building RESTful services!
