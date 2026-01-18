using System;
using System.Collections.Generic;

namespace DotNetGuide.OOP
{
    /// <summary>
    /// Interfaces and Abstract Classes Examples
    /// Demonstrates contracts, polymorphism, and extensibility
    /// </summary>

    // Interface defining a contract
    public interface IVehicle
    {
        string Brand { get; }
        void Start();
        void Stop();
        void Accelerate(int speed);
    }

    // Abstract class with shared implementation
    public abstract class BaseVehicle : IVehicle
    {
        public string Brand { get; protected set; }
        protected int CurrentSpeed { get; set; }

        public BaseVehicle(string brand)
        {
            Brand = brand;
            CurrentSpeed = 0;
        }

        public virtual void Start()
        {
            Console.WriteLine($"{Brand} engine started");
        }

        public virtual void Stop()
        {
            CurrentSpeed = 0;
            Console.WriteLine($"{Brand} stopped");
        }

        // Abstract method - must be implemented by derived classes
        public abstract void Accelerate(int speed);
    }

    // Concrete implementation of abstract class
    public class Car : BaseVehicle
    {
        private int _capacity;

        public Car(string brand, int capacity) : base(brand)
        {
            _capacity = capacity;
        }

        public override void Accelerate(int speed)
        {
            CurrentSpeed = Math.Min(speed, 200); // Max speed 200 km/h
            Console.WriteLine($"{Brand} car accelerating to {CurrentSpeed} km/h");
        }

        public void OpenTrunk()
        {
            Console.WriteLine("Trunk opened");
        }
    }

    public class Motorcycle : BaseVehicle
    {
        public Motorcycle(string brand) : base(brand)
        {
        }

        public override void Accelerate(int speed)
        {
            CurrentSpeed = Math.Min(speed, 300); // Max speed 300 km/h
            Console.WriteLine($"{Brand} motorcycle accelerating to {CurrentSpeed} km/h");
        }
    }

    // Generic interface example
    public interface IRepository<T>
    {
        void Add(T item);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Delete(int id);
    }

    // Generic repository implementation
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private List<T> _items = new();
        private int _nextId = 1;

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"Added {typeof(T).Name}");
        }

        public T GetById(int id)
        {
            Console.WriteLine($"Getting {typeof(T).Name} with ID {id}");
            return _items.Count > 0 ? _items[0] : null;
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public void Delete(int id)
        {
            Console.WriteLine($"Deleted {typeof(T).Name}");
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class InterfacesDemo
    {
        public static void Main()
        {
            Console.WriteLine("=== Interfaces and Abstract Classes ===\n");

            // Polymorphism example
            List<IVehicle> vehicles = new()
            {
                new Car("Tesla", 5),
                new Motorcycle("Harley Davidson")
            };

            foreach (var vehicle in vehicles)
            {
                vehicle.Start();
                vehicle.Accelerate(100);
                vehicle.Stop();
                Console.WriteLine();
            }

            // Generic repository example
            Console.WriteLine("=== Generic Repository ===");
            var productRepo = new GenericRepository<Product>();
            productRepo.Add(new Product("Laptop", 999.99m));
            productRepo.Add(new Product("Mouse", 29.99m));

            foreach (var product in productRepo.GetAll())
            {
                Console.WriteLine($"Product: {product.Name} - ${product.Price}");
            }
        }
    }
}
