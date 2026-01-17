# LINQ and Collections

## Collections

### Lists

```csharp
using System.Collections.Generic;

// Create a list
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Add items
numbers.Add(6);
numbers.AddRange(new[] { 7, 8, 9 });

// Access
int first = numbers[0];  // 1

// Iterate
foreach (int num in numbers)
{
    Console.WriteLine(num);
}

// Methods
numbers.Count;           // Total items
numbers.Contains(5);     // true
numbers.IndexOf(3);      // 2
numbers.Remove(5);       // Remove item
numbers.RemoveAt(0);     // Remove by index
```

### Dictionaries

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "John", 30 },
    { "Alice", 25 },
    { "Bob", 35 }
};

// Add
ages["Charlie"] = 28;

// Access
int johnAge = ages["John"];  // 30

// Check existence
if (ages.ContainsKey("John"))
{
    Console.WriteLine("Found John");
}

// Iterate
foreach (var kvp in ages)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
```

### Arrays

```csharp
// Declare
int[] numbers = new int[5];

// Initialize
int[] values = { 1, 2, 3, 4, 5 };

// Length
int length = values.Length;

// Index
int first = values[0];
values[0] = 10;
```

## LINQ (Language Integrated Query)

Query and transform collections.

### Basic LINQ

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Filter with Where
var evenNumbers = numbers.Where(n => n % 2 == 0);  // 2, 4, 6, 8, 10

// Transform with Select
var doubled = numbers.Select(n => n * 2);  // 2, 4, 6, 8, ...

// Combine
var result = numbers
    .Where(n => n > 5)      // 6, 7, 8, 9, 10
    .Select(n => n * 2);    // 12, 14, 16, 18, 20

// Convert to list
var list = result.ToList();
```

### LINQ Methods

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Filtering
numbers.Where(n => n > 2);           // 3, 4, 5
numbers.First();                     // 1
numbers.Last();                      // 5
numbers.FirstOrDefault();            // 1
numbers.Single(n => n == 3);         // 3

// Transformation
numbers.Select(n => n * 2);          // 2, 4, 6, 8, 10
numbers.SelectMany(n => new[] { n, n });  // Flatten

// Ordering
numbers.OrderBy(n => n);             // Ascending
numbers.OrderByDescending(n => n);   // Descending

// Aggregation
numbers.Count();                     // 5
numbers.Sum();                       // 15
numbers.Average();                   // 3
numbers.Max();                       // 5
numbers.Min();                       // 1

// Other
numbers.Distinct();                  // Remove duplicates
numbers.Skip(2);                     // Skip first 2
numbers.Take(3);                     // Take first 3
numbers.Any(n => n > 3);             // true (exists)
numbers.All(n => n > 0);             // true (all match)
```

### LINQ with Objects

```csharp
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double GPA { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Name = "John", Age = 20, GPA = 3.8 },
    new Student { Name = "Alice", Age = 21, GPA = 3.9 },
    new Student { Name = "Bob", Age = 20, GPA = 3.5 }
};

// Find students with GPA > 3.7
var topStudents = students.Where(s => s.GPA > 3.7);

// Get names of students aged 20
var names = students
    .Where(s => s.Age == 20)
    .Select(s => s.Name);

// Find highest GPA
var bestStudent = students.OrderByDescending(s => s.GPA).First();

// Group by age
var grouped = students.GroupBy(s => s.Age);
```

### LINQ Query Syntax (Alternative)

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Method syntax (what we used above)
var result1 = numbers.Where(n => n > 2).Select(n => n * 2);

// Query syntax (SQL-like)
var result2 = from n in numbers
              where n > 2
              select n * 2;

// Both produce the same result
```

## Common Mistakes

❌ **Forgetting to convert to list**
```csharp
List<int> numbers = GetNumbers();
var filtered = numbers.Where(n => n > 5);  // Still IEnumerable
```

✅ **Convert when needed**
```csharp
var filtered = numbers.Where(n => n > 5).ToList();  // Now List<int>
```

## Quick Reference

| Collection | Use When |
|-----------|----------|
| `List<T>` | Need ordered items, frequent add/remove |
| `Dictionary<K,V>` | Need key-value pairs |
| `Array` | Fixed size, simple collections |
| `HashSet<T>` | Need unique items, fast lookup |

## Next Steps

You now understand collections and basic queries. Continue with **Async/Await** for responsive applications!
