# Building RESTful Web APIs

## REST Principles

- **R**epresentational **S**tate **T**ransfer
- Use HTTP methods meaningfully
- Resources identified by URLs
- Stateless communication

## REST Best Practices

### Naming Conventions

```csharp
// ✅ Good - Use nouns for resources
GET    /api/users
POST   /api/users
GET    /api/users/{id}
PUT    /api/users/{id}
DELETE /api/users/{id}

// ✅ Good - Plural nouns
GET /api/products
GET /api/customers

// ❌ Bad - Verbs in URL
GET /api/getUser
POST /api/createUser
GET /api/deleteUser/{id}
```

### Status Codes

```csharp
// 2xx Success
200 OK              // Request succeeded
201 Created         // Resource created
204 No Content      // Success, no content to return

// 4xx Client Error
400 Bad Request     // Invalid input
401 Unauthorized    // Authentication required
403 Forbidden       // Access denied
404 Not Found       // Resource doesn't exist
409 Conflict        // Request conflicts with state

// 5xx Server Error
500 Internal Error  // Server error
503 Service Unavailable
```

### Controller Example

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductsController(IProductService service)
    {
        _service = service;
    }
    
    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }
    
    // GET /api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound($"Product {id} not found");
        
        return Ok(product);
    }
    
    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var product = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    
    // PUT /api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var success = await _service.UpdateAsync(id, dto);
        if (!success)
            return NotFound();
        
        return NoContent();
    }
    
    // DELETE /api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound();
        
        return NoContent();
    }
}
```

## DTOs (Data Transfer Objects)

Entities vs DTOs:

```csharp
// Entity (Database)
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }  // Don't expose!
    public DateTime CreatedAt { get; set; }
    public ICollection<Order> Orders { get; set; }
}

// DTO (API Response)
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    // ✓ No PasswordHash
    // ✓ No Orders (to avoid cycles)
}

// Request DTO
public class CreateUserDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
```

## Error Handling

```csharp
public class ApiException : Exception
{
    public int StatusCode { get; set; }
    
    public ApiException(string message, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
    }
}

// Global exception handler
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        var response = new
        {
            message = exception?.Message ?? "An error occurred",
            statusCode = context.Response.StatusCode
        };
        
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    });
});
```

## Pagination

```csharp
public class PaginationParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

[HttpGet]
public async Task<ActionResult> GetUsers([FromQuery] PaginationParams paging)
{
    var products = await _service.GetPagedAsync(paging.Page, paging.PageSize);
    return Ok(products);
}

// Client usage:
// GET /api/users?page=2&pagesize=20
```

## API Versioning

```csharp
// Route-based versioning
[ApiController]
[Route("api/v1/[controller]")]
public class UsersV1Controller : ControllerBase { }

[ApiController]
[Route("api/v2/[controller]")]
public class UsersV2Controller : ControllerBase { }

// Header-based versioning
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase { }

// Usage: X-API-Version: 2.0
```

## Testing APIs

```powershell
# Using dotnet CLI
dotnet test

# Using cURL
curl -X GET https://api.example.com/api/users
curl -X POST https://api.example.com/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"John","email":"john@example.com"}'

# Or use Postman/Swagger/Thunder Client (GUI tools)
```

## Documentation

### Swagger/OpenAPI

```csharp
// Add in Program.cs
builder.Services.AddSwaggerGen();

app.UseSwagger();
app.UseSwaggerUI();

// Access at: https://localhost:5001/swagger
```

### XML Documentation

```csharp
/// <summary>
/// Gets a product by ID
/// </summary>
/// <param name="id">Product ID</param>
/// <returns>Product details</returns>
/// <response code="200">Product found</response>
/// <response code="404">Product not found</response>
[HttpGet("{id}")]
public async Task<ActionResult<ProductDto>> GetById(int id)
{
    // ...
}
```

## Next Steps

Learn **Testing** to ensure API reliability!
