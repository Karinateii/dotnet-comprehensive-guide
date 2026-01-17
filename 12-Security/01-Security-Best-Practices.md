# Security Best Practices

## Authentication & Authorization

### JWT (JSON Web Tokens)

```csharp
// In Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

builder.Services.AddAuthorization();

app.UseAuthentication();
app.UseAuthorization();
```

### Creating JWT Tokens

```csharp
public class AuthService
{
    private readonly IConfiguration _config;
    
    public AuthService(IConfiguration config)
    {
        _config = config;
    }
    
    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

### Protected Endpoints

```csharp
[Authorize]  // Requires authentication
public class ProductsController : ControllerBase
{
    [AllowAnonymous]  // Allow without auth
    [HttpGet]
    public async Task<IActionResult> GetAll() { }
    
    [Authorize(Roles = "Admin")]  // Requires Admin role
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto) { }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) { }
}
```

## Password Security

### Hashing Passwords

```csharp
// Using BCrypt
using BCrypt.Net;

public class PasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}

// Usage
var passwordService = new PasswordService();
string hashedPassword = passwordService.HashPassword("MyPassword123!");

// When user logs in:
bool isCorrect = passwordService.VerifyPassword("MyPassword123!", hashedPassword);
```

### Secure Password Requirements

```csharp
public class PasswordValidator
{
    public static bool IsValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return false;
        
        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));
        
        return hasUpper && hasLower && hasDigit && hasSpecial;
    }
}

// Requirements:
// ✓ At least 8 characters
// ✓ Uppercase letter
// ✓ Lowercase letter
// ✓ Number
// ✓ Special character
```

## Data Protection

### Encryption

```csharp
using System.Security.Cryptography;

public class EncryptionService
{
    public string Encrypt(string plainText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (var aes = Aes.Create())
        {
            aes.Key = keyBytes;
            
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);
                
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
```

## HTTPS and SSL/TLS

```csharp
// In Program.cs
app.UseHttpsRedirection();  // Redirect HTTP to HTTPS

// appsettings.json
{
  "Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://localhost:5001",
        "Certificate": {
          "Path": "cert.pfx",
          "Password": "password"
        }
      }
    }
  }
}
```

## CSRF Protection

```csharp
// In Program.cs
builder.Services.AddAntiforgery();

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteUser(int id)
{
    // Protected from CSRF attacks
}
```

## SQL Injection Prevention

```csharp
// ❌ Bad - Vulnerable to SQL injection
string query = $"SELECT * FROM Users WHERE Email = '{email}'";

// ✅ Good - Use parameterized queries with EF Core
var user = await context.Users
    .FromSqlInterpolated($"SELECT * FROM Users WHERE Email = {email}")
    .FirstOrDefaultAsync();

// ✅ Also good - LINQ (built-in protection)
var user = await context.Users
    .Where(u => u.Email == email)
    .FirstOrDefaultAsync();
```

## Input Validation

```csharp
public class CreateUserDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must have uppercase, lowercase, digit, special char")]
    public string Password { get; set; }
}

[HttpPost]
public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    
    // Safe to use validated data
}
```

## OWASP Top 10

1. **Broken Authentication** - Use proper auth (JWT, OAuth)
2. **Sensitive Data Exposure** - Encrypt data, use HTTPS
3. **Injection** - Use parameterized queries
4. **Broken Access Control** - Validate authorization
5. **Security Misconfiguration** - Keep dependencies updated
6. **XSS** - Encode output, validate input
7. **Insecure Deserialization** - Validate input, use safe libraries
8. **Using Components with Known Vulnerabilities** - Keep dependencies updated
9. **Insufficient Logging/Monitoring** - Log security events
10. **Broken API** - Validate input, use authentication

## Security Checklist

- ✅ Use HTTPS everywhere
- ✅ Hash passwords with BCrypt
- ✅ Use strong authentication (JWT, OAuth)
- ✅ Validate all input
- ✅ Use parameterized queries
- ✅ Keep dependencies updated
- ✅ Don't expose sensitive data in errors
- ✅ Log security events
- ✅ Use secret management (Azure Key Vault)
- ✅ Regular security audits

## Next Steps

Learn **Performance** to optimize your applications!
