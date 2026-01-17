# Unit Testing and TDD

## Testing Frameworks

### xUnit (Modern, Recommended)

```powershell
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq
```

## Writing Tests

### Arrange-Act-Assert Pattern

```csharp
public class CalculatorTests
{
    [Fact]
    public void Add_WithTwoNumbers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act
        var result = calculator.Add(2, 3);
        
        // Assert
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Subtract_WithNegativeResult_ReturnsNegativeNumber()
    {
        var calculator = new Calculator();
        var result = calculator.Subtract(2, 5);
        Assert.Equal(-3, result);
    }
}
```

### Test Multiple Cases

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
public void Add_WithVariousInputs_ReturnsExpected(int a, int b, int expected)
{
    var calculator = new Calculator();
    var result = calculator.Add(a, b);
    Assert.Equal(expected, result);
}
```

## Mocking Dependencies

```csharp
public class UserServiceTests
{
    [Fact]
    public async Task CreateUser_WithValidData_CallsRepository()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var service = new UserService(mockRepository.Object);
        
        var dto = new CreateUserDto 
        { 
            Name = "John", 
            Email = "john@example.com" 
        };
        
        // Act
        await service.CreateUserAsync(dto);
        
        // Assert - Verify repository was called
        mockRepository.Verify(
            r => r.AddAsync(It.IsAny<User>()), 
            Times.Once
        );
    }
}
```

### Mock Setup

```csharp
// Mock return value
var mockRepository = new Mock<IUserRepository>();
mockRepository
    .Setup(r => r.GetByIdAsync(1))
    .ReturnsAsync(new User { Id = 1, Name = "John" });

// Mock throws exception
mockRepository
    .Setup(r => r.DeleteAsync(It.IsAny<int>()))
    .ThrowsAsync(new Exception("DB error"));

// Use in test
var user = await mockRepository.Object.GetByIdAsync(1);
Assert.NotNull(user);
```

## Testing Service Layer

```csharp
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly ProductService _service;
    
    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepository.Object);
    }
    
    [Fact]
    public async Task GetById_WithValidId_ReturnsProduct()
    {
        // Arrange
        var expectedProduct = new Product { Id = 1, Name = "Laptop", Price = 999 };
        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(expectedProduct);
        
        // Act
        var result = await _service.GetByIdAsync(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Laptop", result.Name);
    }
    
    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product)null);
        
        // Act
        var result = await _service.GetByIdAsync(999);
        
        // Assert
        Assert.Null(result);
    }
}
```

## Testing Controllers

```csharp
public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductsController _controller;
    
    public ProductsControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductsController(_mockService.Object);
    }
    
    [Fact]
    public async Task GetById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var product = new ProductDto { Id = 1, Name = "Laptop" };
        _mockService
            .Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(product);
        
        // Act
        var result = await _controller.GetById(1);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
    }
}
```

## Test Naming Convention

```csharp
// Pattern: MethodName_Scenario_ExpectedBehavior
[Fact]
public void Withdraw_WithSufficientBalance_DeductsMoney() { }

[Fact]
public void Withdraw_WithInsufficientBalance_ThrowsException() { }

[Fact]
public void Withdraw_WithNegativeAmount_ThrowsArgumentException() { }
```

## Code Coverage

```powershell
# Install coverage tool
dotnet add package coverlet.collector

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover

# View coverage
# Look for coverage.opencover.xml
```

## TDD (Test-Driven Development)

1. **Red** - Write failing test
2. **Green** - Write minimal code to pass test
3. **Refactor** - Improve code while tests pass

```csharp
// Step 1: Write test (Red - will fail)
[Fact]
public void StringCalculator_WithEmptyString_ReturnsZero()
{
    var calculator = new StringCalculator();
    var result = calculator.Add("");
    Assert.Equal(0, result);
}

// Step 2: Write code to pass (Green)
public class StringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
            return 0;
        
        // ... more implementation
    }
}

// Step 3: Refactor while keeping tests passing
```

## Best Practices

1. ✅ **One assertion per test** (or related assertions)
2. ✅ **Clear test names** - Know what's being tested
3. ✅ **Mock external dependencies** - Database, HTTP, etc.
4. ✅ **Test edge cases** - Null, empty, negative values
5. ✅ **Keep tests fast** - No real DB calls
6. ❌ **Don't test everything** - Focus on logic
7. ❌ **Don't test private methods** - Test public interface

## Running Tests

```powershell
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "MethodName"

# Run with verbose output
dotnet test --verbosity normal

# Run and generate report
dotnet test --logger "console;verbosity=detailed"
```

## Next Steps

Learn **Design Patterns** to write better code!
