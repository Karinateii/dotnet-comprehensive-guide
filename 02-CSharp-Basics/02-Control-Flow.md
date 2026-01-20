# Control Flow - Decisions and Loops

## If Statement

Making decisions in your code.

### Basic If
```csharp
int age = 25;

if (age >= 18)
{
    Console.WriteLine("You are an adult");
}
```

### If-Else
```csharp
int age = 15;

if (age >= 18)
{
    Console.WriteLine("You can vote");
}
else
{
    Console.WriteLine("You cannot vote yet");
}
```

### If-Else If-Else
```csharp
int score = 85;

if (score >= 90)
{
    Console.WriteLine("Grade: A");
}
else if (score >= 80)
{
    Console.WriteLine("Grade: B");
}
else if (score >= 70)
{
    Console.WriteLine("Grade: C");
}
else
{
    Console.WriteLine("Grade: F");
}
```

### Nested If
```csharp
int age = 25;
bool hasLicense = true;

if (age >= 18)
{
    if (hasLicense)
    {
        Console.WriteLine("Can drive");
    }
    else
    {
        Console.WriteLine("Need a license first");
    }
}
else
{
    Console.WriteLine("Too young to drive");
}
```

### Ternary Operator (? :)

Quick way to assign based on condition:

```csharp
int age = 25;
string status = age >= 18 ? "Adult" : "Minor";

// Equivalent to:
string status2;
if (age >= 18)
    status2 = "Adult";
else
    status2 = "Minor";

// Nested ternary (avoid - hard to read)
string grade = score >= 90 ? "A" : 
               score >= 80 ? "B" : 
               score >= 70 ? "C" : "F";
```

## Switch Statement

Better than if-else when checking many values.

### Basic Switch
```csharp
int dayOfWeek = 3;
string dayName;

switch (dayOfWeek)
{
    case 1:
        dayName = "Monday";
        break;
    case 2:
        dayName = "Tuesday";
        break;
    case 3:
        dayName = "Wednesday";
        break;
    default:
        dayName = "Invalid day";
        break;
}

Console.WriteLine(dayName);  // "Wednesday"
```

### Switch with String
```csharp
string fruit = "apple";

switch (fruit)
{
    case "apple":
        Console.WriteLine("Red fruit");
        break;
    case "banana":
        Console.WriteLine("Yellow fruit");
        break;
    case "orange":
        Console.WriteLine("Orange fruit");
        break;
    default:
        Console.WriteLine("Unknown fruit");
        break;
}
```

⚠️ **Important**: Don't forget `break;` or code will fall through!

### Modern Switch Expression (.NET 6+) ⭐
```csharp
string fruit = "apple";

string color = fruit switch
{
    "apple" => "Red",
    "banana" => "Yellow",
    "orange" => "Orange",
    _ => "Unknown"  // _ is default case
};

Console.WriteLine(color);  // "Red"
```

Much cleaner! Preferred in modern C#.

### Switch with Multiple Cases
```csharp
char grade = 'A';

switch (grade)
{
    case 'A':
    case 'B':
        Console.WriteLine("Excellent");
        break;
    case 'C':
        Console.WriteLine("Good");
        break;
    case 'D':
    case 'F':
        Console.WriteLine("Needs improvement");
        break;
}
```

## Loops

### While Loop

Repeats while condition is true:

```csharp
int count = 0;

while (count < 5)
{
    Console.WriteLine($"Count: {count}");
    count++;
}

// Output:
// Count: 0
// Count: 1
// Count: 2
// Count: 3
// Count: 4
```

### Do-While Loop

Executes at least once:

```csharp
int count = 0;

do
{
    Console.WriteLine($"Count: {count}");
    count++;
} while (count < 5);

// Even if condition was false, executes once
```

### For Loop

When you know how many times to repeat:

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Count: {i}");
}

// Parts: for (initialize; condition; increment)
//            i = 0   i < 5    i++
```

### For Loop with Arrays

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine($"Index {i}: {numbers[i]}");
}

// Output:
// Index 0: 10
// Index 1: 20
// Index 2: 30
// Index 3: 40
// Index 4: 50
```

### Foreach Loop (Cleaner for arrays/lists)

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// Output:
// 10
// 20
// 30
// 40
// 50

// With strings
string[] names = { "Alice", "Bob", "Charlie" };
foreach (string name in names)
{
    Console.WriteLine($"Hello, {name}");
}
```

**Key difference**: `foreach` gives you the value directly, not the index.

## Loop Control

### Break
Exits the loop immediately:

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5)
        break;  // Exit loop when i is 5
    Console.WriteLine(i);
}

// Output: 0 1 2 3 4
```

### Continue
Skips to next iteration:

```csharp
for (int i = 0; i < 5; i++)
{
    if (i == 2)
        continue;  // Skip when i is 2
    Console.WriteLine(i);
}

// Output: 0 1 3 4
```

## Practical Examples

### Example 1: User Input Validation
```csharp
int age = 0;
bool validInput = false;

while (!validInput)
{
    Console.Write("Enter your age: ");
    if (int.TryParse(Console.ReadLine(), out int input))
    {
        if (input >= 0 && input <= 150)
        {
            age = input;
            validInput = true;
        }
        else
        {
            Console.WriteLine("Age must be between 0 and 150");
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid number");
    }
}

Console.WriteLine($"You are {age} years old");
```

### Example 2: Times Table
```csharp
Console.Write("Enter a number: ");
int number = int.Parse(Console.ReadLine());

Console.WriteLine($"\n{number} times table:");

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}

// Output:
// 5 times table:
// 5 x 1 = 5
// 5 x 2 = 10
// ... etc
```

### Example 3: Find Highest Number
```csharp
int[] numbers = { 45, 23, 89, 12, 56, 34, 92 };
int highest = numbers[0];

foreach (int num in numbers)
{
    if (num > highest)
        highest = num;
}

Console.WriteLine($"Highest: {highest}");  // 92
```

## Comparison with Real-World Code

### Counter Example
```csharp
// Managing a loop counter
int total = 0;
for (int i = 1; i <= 100; i++)
{
    total += i;  // Add each number
}
Console.WriteLine(total);  // 5050 (sum of 1-100)
```

## Common Mistakes

❌ **Infinite loop** (forgot to increment)
```csharp
while (true)
{
    Console.WriteLine("This runs forever!");
    // No way out!
}
```

✅ **Always ensure loop ends**
```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    i++;  // Increment to eventually end
}
```

❌ **Off-by-one error**
```csharp
for (int i = 1; i <= 5; i++)  // Starts at 1, not 0
{
    array[i];  // May skip index 0, go beyond array
}
```

✅ **Use correct range**
```csharp
for (int i = 0; i < array.Length; i++)  // Correct
{
    array[i];
}
```

## Quick Reference

| Statement | Use When |
|-----------|----------|
| `if` | Single condition |
| `if-else` | Two paths |
| `if-else if-else` | Multiple paths |
| `switch` | Many specific values |
| `while` | Unknown iterations |
| `do-while` | At least one execution |
| `for` | Known count |
| `foreach` | Iterating collections |
| `break` | Exit loop |
| `continue` | Skip iteration |

## Next Steps:

You now understand:
✅ Conditional execution (if/switch)  
✅ Loops (for, while, foreach)  
✅ Loop control (break, continue)  

Next: **[OOP Principles](../../03-OOP-Principles/)** - Learn classes and objects!
