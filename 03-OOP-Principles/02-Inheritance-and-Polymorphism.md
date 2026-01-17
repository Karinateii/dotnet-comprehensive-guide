# Inheritance and Polymorphism

## Inheritance

Allowing a class to inherit from another class.

### Basic Inheritance

```csharp
// Base class (parent)
public class Animal
{
    public string Name { get; set; }
    
    public virtual void MakeSound()
    {
        Console.WriteLine("Some sound");
    }
}

// Derived class (child)
public class Dog : Animal
{
    public void Fetch()
    {
        Console.WriteLine($"{Name} is fetching");
    }
}

// Usage
Dog dog = new Dog();
dog.Name = "Buddy";
dog.MakeSound();   // Inherited method
dog.Fetch();       // Own method
```

### Constructor Inheritance

```csharp
public class Animal
{
    public string Name { get; set; }
    
    public Animal(string name)
    {
        Name = name;
    }
}

public class Dog : Animal
{
    public string Breed { get; set; }
    
    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
    }
}

Dog dog = new Dog("Max", "Labrador");
```

## Polymorphism

Objects responding differently to the same method call.

### Method Overriding

```csharp
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Meow!");
    }
}

// Polymorphism in action
Animal dog = new Dog();
Animal cat = new Cat();

dog.MakeSound();  // "Woof!"
cat.MakeSound();  // "Meow!"
```

### Virtual vs Abstract

```csharp
// Virtual - has implementation, can be overridden
public class Vehicle
{
    public virtual void Start()
    {
        Console.WriteLine("Starting vehicle");
    }
}

// Abstract - no implementation, must be overridden
public abstract class Shape
{
    public abstract double GetArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}
```

## Interfaces

Contracts that define what methods a class must implement.

```csharp
public interface IVehicle
{
    void Start();
    void Stop();
    void Drive();
}

public class Car : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Car starting");
    }
    
    public void Stop()
    {
        Console.WriteLine("Car stopping");
    }
    
    public void Drive()
    {
        Console.WriteLine("Car driving");
    }
}

// Usage
IVehicle car = new Car();
car.Start();
car.Drive();
car.Stop();
```

## Key Principles

1. **DRY** - Don't Repeat Yourself
2. **SOLID** - Five principles for good design
3. **Single Responsibility** - One job per class
4. **Open/Closed** - Open for extension, closed for modification
5. **Liskov Substitution** - Derived classes should be substitutable
6. **Interface Segregation** - Many small interfaces > one large
7. **Dependency Inversion** - Depend on abstractions, not concrete

## Next Steps

Continue learning advanced OOP concepts and design patterns!
