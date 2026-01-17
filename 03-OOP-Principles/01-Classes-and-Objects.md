# Classes and Objects

## What Are Classes and Objects?

### Analogy
Think of a **class** as a blueprint and an **object** as the actual house built from that blueprint.

```
Class (Blueprint)      →  Objects (Actual Houses)
Car (blueprint)        →  My Tesla, Your Honda, Their BMW
Person (blueprint)     →  John, Alice, Bob
```

## Creating a Class

```csharp
// Class definition
public class Person
{
    // Fields (data)
    public string Name;
    public int Age;
    
    // Method
    public void Greet()
    {
        Console.WriteLine($"Hello, my name is {Name}");
    }
}

// Create an object (instance of the class)
Person person1 = new Person();
person1.Name = "John";
person1.Age = 30;
person1.Greet();  // Output: Hello, my name is John

// Create another object
Person person2 = new Person();
person2.Name = "Alice";
person2.Age = 25;
person2.Greet();  // Output: Hello, my name is Alice
```

## Constructors

Special method that runs when creating an object. Used to initialize values.

```csharp
public class Car
{
    public string Brand;
    public string Model;
    public int Year;
    
    // Constructor (same name as class)
    public Car(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"{Year} {Brand} {Model}");
    }
}

// Create objects using constructor
Car car1 = new Car("Toyota", "Camry", 2023);
car1.DisplayInfo();  // 2023 Toyota Camry

Car car2 = new Car("Honda", "Civic", 2022);
car2.DisplayInfo();  // 2022 Honda Civic
```

### Default Constructor

If you don't define a constructor, C# provides one automatically:

```csharp
public class Animal
{
    // No constructor defined
    public string Name;
}

Animal dog = new Animal();  // Uses default constructor
dog.Name = "Buddy";
```

### Multiple Constructors

```csharp
public class Rectangle
{
    public double Width;
    public double Height;
    
    // Constructor 1: Both parameters
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    // Constructor 2: Square (width = height)
    public Rectangle(double side)
    {
        Width = side;
        Height = side;
    }
    
    public double GetArea()
    {
        return Width * Height;
    }
}

var rect = new Rectangle(5, 10);      // Rectangle: area = 50
var square = new Rectangle(5);        // Square: area = 25
```

## Properties

Modern way to access and modify fields with get/set.

```csharp
public class Student
{
    private string _name;  // Private field (can't access from outside)
    
    // Property (public, controls access)
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    public int Age { get; set; }  // Auto-property (shorthand)
}

Student student = new Student();
student.Name = "Bob";      // Uses set
Console.WriteLine(student.Name);  // Uses get
```

### Auto-Properties (Simpler)

```csharp
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

var person = new Person();
person.FirstName = "John";
person.LastName = "Doe";
person.Age = 30;
```

### Property Initialization

```csharp
public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; } = "General";  // Default value
    public double Salary { get; set; }
}

var emp = new Employee { Name = "Alice", Salary = 50000 };
// Department defaults to "General"
```

## Access Modifiers

Control who can access class members.

```csharp
public class BankAccount
{
    public string AccountNumber { get; set; }      // Anyone can access
    private decimal _balance;                      // Only this class
    protected string AccountHolder { get; set; }   // This class + derived
    internal string BankName { get; set; }         // Same project
    
    // Private method - only usable inside this class
    private void LogTransaction(decimal amount)
    {
        Console.WriteLine($"Transaction: {amount}");
    }
}
```

### Access Modifier Rules

| Modifier | Accessible From |
|----------|-----------------|
| `public` | Everywhere |
| `private` | Only this class |
| `protected` | This class + derived classes |
| `internal` | Same project |
| `protected internal` | Same project + derived classes |

**Default if not specified**: `private`

## Methods

Functions that belong to a class.

```csharp
public class Calculator
{
    // Method with parameters and return value
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    // Method with no return value (void)
    public void PrintResult(int result)
    {
        Console.WriteLine($"Result: {result}");
    }
    
    // Method with no parameters
    public string GetVersion()
    {
        return "1.0";
    }
}

Calculator calc = new Calculator();
int result = calc.Add(5, 3);      // 8
calc.PrintResult(result);          // Prints: Result: 8
string version = calc.GetVersion(); // "1.0"
```

### Method Overloading

Same method name, different parameters.

```csharp
public class Printer
{
    // Overload 1: Print string
    public void Print(string text)
    {
        Console.WriteLine(text);
    }
    
    // Overload 2: Print int
    public void Print(int number)
    {
        Console.WriteLine($"Number: {number}");
    }
    
    // Overload 3: Print two strings
    public void Print(string text1, string text2)
    {
        Console.WriteLine($"{text1} {text2}");
    }
}

Printer printer = new Printer();
printer.Print("Hello");              // Uses overload 1
printer.Print(42);                   // Uses overload 2
printer.Print("Hello", "World");     // Uses overload 3
```

## Static Members

Belong to the class itself, not instances.

```csharp
public class Counter
{
    public static int TotalCount = 0;  // Shared by all instances
    public int IndividualCount = 0;    // Each instance has its own
    
    public Counter()
    {
        TotalCount++;
        IndividualCount++;
    }
}

Counter c1 = new Counter();
Counter c2 = new Counter();
Counter c3 = new Counter();

Console.WriteLine(Counter.TotalCount);      // 3 (class-level)
Console.WriteLine(c1.IndividualCount);      // 1 (instance-level)
```

### Static Methods

```csharp
public class MathHelper
{
    public static int Max(int a, int b)
    {
        return a > b ? a : b;
    }
}

// Call static method without creating instance
int max = MathHelper.Max(10, 20);  // 20
```

## This Keyword

Refers to current object.

```csharp
public class Student
{
    public string Name;
    public int Age;
    
    public Student(string name, int age)
    {
        this.Name = name;    // this.Name refers to field
        this.Age = age;      // this.Age refers to field
    }
    
    public void PrintInfo()
    {
        Console.WriteLine(this.Name);  // Explicit this
        Console.WriteLine(this.Age);
    }
}
```

## Object Initialization Syntax

Modern way to create and initialize objects.

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}

// Old way
Person p1 = new Person();
p1.Name = "John";
p1.Age = 30;
p1.City = "NYC";

// New way (cleaner)
Person p2 = new Person 
{ 
    Name = "Alice", 
    Age = 25, 
    City = "LA" 
};

// With constructor
Person p3 = new Person { Name = "Bob", Age = 35 };
```

## Practical Example: Bank Account

```csharp
public class BankAccount
{
    public string AccountHolder { get; set; }
    private decimal _balance;
    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }  // Private setter
    }
    
    public BankAccount(string holder, decimal initialBalance)
    {
        AccountHolder = holder;
        Balance = initialBalance;
    }
    
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive");
            return;
        }
        Balance += amount;
        Console.WriteLine($"Deposited: ${amount}. New balance: ${Balance}");
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
            return;
        }
        Balance -= amount;
        Console.WriteLine($"Withdrawn: ${amount}. New balance: ${Balance}");
    }
}

// Usage
BankAccount account = new BankAccount("John Doe", 1000);
account.Deposit(500);      // Deposited: $500. New balance: $1500
account.Withdraw(200);     // Withdrawn: $200. New balance: $1300
Console.WriteLine($"Final balance: ${account.Balance}");
```

## Quick Reference

| Concept | Example |
|---------|---------|
| Class | `public class Person { }` |
| Object | `Person p = new Person();` |
| Constructor | `public Person(string name) { }` |
| Field | `public string Name;` |
| Property | `public string Name { get; set; }` |
| Method | `public void Greet() { }` |
| Static | `public static int count = 0;` |
| Access | `public`, `private`, `protected` |

## Next Steps

You now understand:
✅ Classes and objects  
✅ Constructors and initialization  
✅ Properties and fields  
✅ Methods and overloading  

Next: **[Inheritance and Polymorphism](./02-Inheritance-and-Polymorphism.md)**
