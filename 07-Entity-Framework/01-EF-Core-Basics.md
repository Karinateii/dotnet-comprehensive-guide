# Entity Framework Core

## What is EF Core?

ORM (Object-Relational Mapper) - maps database tables to C# classes.

### Entity Definition

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }  // Navigation property
}
```

### DbContext

```csharp
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=MyDb;Trusted_Connection=true;");
    }
}
```

## Basic Operations

### Create (Add)

```csharp
using var context = new AppDbContext();

var user = new User 
{ 
    Name = "John",
    Email = "john@example.com",
    CreatedAt = DateTime.Now
};

context.Users.Add(user);
await context.SaveChangesAsync();
```

### Read (Query)

```csharp
using var context = new AppDbContext();

// Get all
var allUsers = await context.Users.ToListAsync();

// Get by ID
var user = await context.Users.FindAsync(1);

// Query with filter
var activeUsers = await context.Users
    .Where(u => u.Email.Contains("@example.com"))
    .ToListAsync();

// With related data
var postsWithUser = await context.Posts
    .Include(p => p.User)
    .ToListAsync();
```

### Update

```csharp
using var context = new AppDbContext();

var user = await context.Users.FindAsync(1);
user.Email = "newemail@example.com";

context.Users.Update(user);
await context.SaveChangesAsync();
```

### Delete

```csharp
using var context = new AppDbContext();

var user = await context.Users.FindAsync(1);
context.Users.Remove(user);
await context.SaveChangesAsync();
```

## Migrations

Creating and updating database schema.

```powershell
# Create migration
dotnet ef migrations add AddUserTable

# Update database
dotnet ef database update

# Revert last migration
dotnet ef migrations remove

# View migrations
dotnet ef migrations list
```

### Migration File Example

```csharp
public partial class AddUserTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: false),
                Email = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Users");
    }
}
```

## Relationships

### One-to-Many

```csharp
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
```

### Many-to-Many

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
```

## Configuration

### Fluent API

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasKey(u => u.Id);
    
    modelBuilder.Entity<User>()
        .Property(u => u.Name)
        .IsRequired()
        .HasMaxLength(100);
    
    modelBuilder.Entity<Post>()
        .HasOne(p => p.User)
        .WithMany(u => u.Posts)
        .HasForeignKey(p => p.UserId);
}
```

## Best Practices

1. ✅ **Use async** - `ToListAsync()`, not `ToList()`
2. ✅ **Use migrations** - Version control for schema
3. ✅ **Use Include()** - Load related data efficiently
4. ✅ **Validate data** - Check before saving
5. ❌ **Don't load everything** - Filter at database level
6. ❌ **Don't ignore change tracking** - Understand what happens

## Next Steps

Learn **ASP.NET Core** to build web applications!
