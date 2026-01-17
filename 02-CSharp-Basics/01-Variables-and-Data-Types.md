# C# Basics - Variables, Data Types, and Operators

## Variables

A variable is a named container for storing data.

### Declaration and Initialization

```csharp
// Declare and initialize
int age = 25;
string name = "Alice";
double salary = 50000.50;

// Declare without initializing (must initialize before use)
int count;
count = 10;  // Initialize later

// Multiple variables
int x = 1, y = 2, z = 3;
```

### Naming Conventions

```csharp
// ✅ Good names
int customerAge = 25;
string productName = "Laptop";
bool isActive = true;

// ❌ Bad names
int a = 25;           // Too vague
string x = "Laptop";  // Meaningless
bool b = true;        // What does it mean?
```

**Rules**:
- Start with letter or underscore
- Use camelCase for variables (firstName, notFirstName)
- Use PascalCase for classes/properties (ClassName)
- Be descriptive

## Data Types

### Value Types (Primitive)

Stored directly in memory, fast access.

```csharp
// Integer types
byte smallNum = 255;              // 0-255
short mediumNum = 32000;          // -32,768 to 32,767
int wholeNumber = 1000;           // -2.1B to 2.1B (default integer)
long bigNumber = 9223372036854775807;  // Very large numbers

// Floating point
float decimal1 = 3.14f;           // 7 decimal places (f suffix)
double decimal2 = 3.14159;        // 15 decimal places (default)
decimal price = 19.99m;           // 28 decimal places, financial (m suffix)

// Boolean
bool isReady = true;
bool isComplete = false;

// Character
char letter = 'A';                // Single character

// DateTime
DateTime now = DateTime.Now;      // Current date/time
DateTime birthday = new DateTime(1995, 5, 15);
```

### Reference Types

Stored by reference, more flexible.

```csharp
// String (most common reference type)
string greeting = "Hello, World!";
string emptyString = "";
string nullString = null;         // No value

// Arrays
int[] numbers = { 1, 2, 3, 4, 5 };
string[] names = new string[3];   // Array of 3 strings

// Objects
object anything = 42;             // Can hold any type
```

### Type Comparison Chart

| Type | Size | Range | Default | Use Case |
|------|------|-------|---------|----------|
| `byte` | 1 byte | 0-255 | 0 | Small counts |
| `int` | 4 bytes | -2.1B to 2.1B | 0 | Most common |
| `long` | 8 bytes | Very large | 0L | Big numbers |
| `float` | 4 bytes | Decimals | 0f | Precise decimals (7) |
| `double` | 8 bytes | Decimals | 0d | More precise (15) |
| `decimal` | 16 bytes | Financial | 0m | Money (28 places) |
| `bool` | 1 byte | true/false | false | Conditions |
| `char` | 2 bytes | Single char | '\0' | Individual characters |
| `string` | Variable | Any text | null | Text |
| `DateTime` | 8 bytes | Dates/times | 0 | Dates |

## Type Inference (var keyword)

```csharp
// Let compiler figure out the type
var age = 25;           // int (inferred)
var name = "Bob";       // string (inferred)
var score = 95.5;       // double (inferred)

// var is convenient but explicit types are clearer
// Use var when type is obvious:
var users = new List<User>();  // Clear what type is

// Don't use var when type is unclear
var result = CalculateValue();  // What type is this?
```

## String Basics

```csharp
// String concatenation (combining)
string firstName = "John";
string lastName = "Doe";
string fullName = firstName + " " + lastName;  // "John Doe"

// String interpolation (cleaner way) ⭐
string greeting = $"Hello, {firstName} {lastName}!";

// Escape sequences
string withTab = "Name\tAge";        // Tab character
string withNewLine = "Line1\nLine2"; // New line
string withQuote = "He said \"Hi\""; // Escaped quote

// String methods
string text = "Hello";
text.Length;           // 5
text.ToUpper();        // "HELLO"
text.ToLower();        // "hello"
text.Contains("ell");  // true
text.Replace("l", "L"); // "HeLLo"
text.Substring(1, 3);  // "ell" (start at 1, take 3 chars)
```

## Operators

### Arithmetic Operators

```csharp
int a = 10, b = 3;

int sum = a + b;       // 13
int difference = a - b; // 7
int product = a * b;   // 30
int quotient = a / b;  // 3 (integer division)
int remainder = a % b; // 1
int power = (int)Math.Pow(a, 2); // 100

// Increment/Decrement
a++;  // a = 11 (post-increment)
b--   // b = 2 (post-decrement)
++a;  // Pre-increment (slightly different)
--b;  // Pre-decrement
```

### Comparison Operators

```csharp
int x = 5;
bool result;

result = x == 5;   // true (equal)
result = x != 5;   // false (not equal)
result = x > 3;    // true (greater than)
result = x >= 5;   // true (greater than or equal)
result = x < 10;   // true (less than)
result = x <= 5;   // true (less than or equal)
```

### Logical Operators

```csharp
bool a = true;
bool b = false;

bool result = a && b;  // false (AND - both must be true)
result = a || b;       // true (OR - at least one true)
result = !a;           // false (NOT - flips value)

// Practical example
int age = 25;
bool hasLicense = true;

if (age >= 18 && hasLicense)
{
    Console.WriteLine("Can drive");
}
```

### Assignment Operators

```csharp
int number = 10;

number += 5;   // number = number + 5 = 15
number -= 3;   // number = number - 3 = 12
number *= 2;   // number = number * 2 = 24
number /= 4;   // number = number / 4 = 6
number %= 2;   // number = number % 2 = 0
```

### Operator Precedence (Order of Operations)

```csharp
// Like math: multiply/divide before add/subtract
int result = 2 + 3 * 4;  // 14 (not 20)
int result2 = (2 + 3) * 4; // 20

// Full precedence (high to low):
// ()  [] .
// ++  --  !
// *  /  %
// +  -
// <  >  <=  >=
// ==  !=
// &&
// ||
```

## Constants

Variables that cannot be changed after initialization.

```csharp
// Must initialize when declaring
const double PI = 3.14159;
const string APP_NAME = "MyApp";

// These cause errors:
// PI = 3.14; // ❌ Error - cannot modify constant

// Good for values that never change
const int MAX_USERS = 100;
const string DATABASE_NAME = "production";
```

## Nullable Types

By default, reference types can be null, value types cannot.

```csharp
// Value type can't be null (unless nullable)
int age = null;  // ❌ Error

// Make it nullable with ?
int? age = null; // ✅ OK
int? score = 95;

// Check if it has a value
if (age.HasValue)
{
    int value = age.Value;
}

// Null coalescing operator ??
int actualAge = age ?? 0;  // Use age if not null, otherwise 0

// Reference types can be null by default
string name = null;  // ✅ OK (reference type)
```

## Type Casting and Conversion

### Implicit Casting (Safe)
```csharp
int smallNumber = 5;
long bigNumber = smallNumber;  // Automatically converts (int → long)

double decimalNumber = 3.14;
decimal financialNumber = (decimal)decimalNumber; // Explicit required
```

### Explicit Casting (Requires conversion)
```csharp
double decimal = 9.8;
int wholeNumber = (int)decimal;  // Converts to 9 (loses decimal part)

// Parse from string
string numberString = "42";
int number = int.Parse(numberString);  // "42" → 42
double decimal2 = double.Parse("3.14"); // "3.14" → 3.14
```

### Safe Conversion (Recommended) ⭐
```csharp
string numberString = "42";

// Try to convert, returns true/false
if (int.TryParse(numberString, out int number))
{
    Console.WriteLine($"Number: {number}");
}
else
{
    Console.WriteLine("Invalid number");
}

// With invalid input
if (int.TryParse("abc", out int result))
{
    // This won't execute
}
else
{
    // This executes - safely handles invalid input
    Console.WriteLine("Conversion failed");
}
```

## Quick Reference

| Concept | Example |
|---------|---------|
| Variable | `int age = 25;` |
| String interpolation | `$"Hello, {name}"` |
| Integer division | `10 / 3` = 3 |
| Modulo (remainder) | `10 % 3` = 1 |
| Logical AND | `a && b` |
| Logical OR | `a \|\| b` |
| NOT operator | `!value` |
| Nullable type | `int?` or `string?` |
| Constant | `const int MAX = 100;` |
| Parse string | `int.Parse("42")` |
| Safe parse | `int.TryParse("42", out int x)` |

## Common Mistakes

❌ **Using == for strings**
```csharp
string a = "hello";
string b = "hello";
if (a == b) // Works but be careful with null
```

✅ **Better approach**
```csharp
if (a.Equals(b, StringComparison.OrdinalIgnoreCase))
```

❌ **Integer division losing decimals**
```csharp
int result = 5 / 2;  // 2, not 2.5
```

✅ **Use decimal type**
```csharp
decimal result = 5m / 2m;  // 2.5
```

❌ **Parsing without checking**
```csharp
int num = int.Parse(userInput);  // Crashes if invalid
```

✅ **Use TryParse**
```csharp
if (int.TryParse(userInput, out int num))
{
    // Safe to use num
}
```

## Next Steps

You now understand:
✅ Variables and data types  
✅ Operators and expressions  
✅ String manipulation  
✅ Type conversion  

Next: **[Control Flow - If, Loops, Switch](../../03-OOP-Principles/01-Classes-and-Objects.md)** - Learn decision-making and repetition.
