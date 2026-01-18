using System;
using System.Collections.Generic;

namespace DotNetGuide.Fundamentals
{
    /// <summary>
    /// Exception Handling Examples
    /// Demonstrates try-catch-finally, custom exceptions, and best practices
    /// </summary>
    public class ExceptionHandlingExamples
    {
        // Basic try-catch-finally
        public static void BasicExceptionHandling()
        {
            try
            {
                int[] numbers = { 1, 2, 3 };
                Console.WriteLine(numbers[10]); // IndexOutOfRangeException
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Error: Array index out of range - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Cleanup code always executes");
            }
        }

        // Custom exception class
        public class InvalidAgeException : Exception
        {
            public int ProvidedAge { get; set; }

            public InvalidAgeException(int age) 
                : base($"Age {age} is invalid")
            {
                ProvidedAge = age;
            }
        }

        // Throwing custom exceptions
        public static void ValidateAge(int age)
        {
            if (age < 0 || age > 150)
            {
                throw new InvalidAgeException(age);
            }
            Console.WriteLine($"Age {age} is valid");
        }

        // Using statements for resource cleanup
        public static void FileProcessingExample()
        {
            using (var file = System.IO.File.OpenRead("data.txt"))
            {
                // File operations here
                // Automatically disposed when using block exits
            } // File is automatically closed and disposed
        }

        // Exception filtering (C# 6.0+)
        public static void ExceptionFiltering(int value)
        {
            try
            {
                if (value < 0)
                    throw new ArgumentException("Value cannot be negative");
                if (value == 0)
                    throw new DivideByZeroException();
            }
            catch (ArgumentException ex) when (ex.Message.Contains("negative"))
            {
                Console.WriteLine("Argument was negative");
            }
            catch (DivideByZeroException ex) when (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Console.WriteLine("Division by zero on Friday!");
            }
        }

        // Null-coalescing and null-conditional operators
        public static void NullHandling(string? input)
        {
            // Null-coalescing operator (??)
            string result = input ?? "Default Value";

            // Null-conditional operator (?.)
            int? length = input?.Length;

            // Null-coalescing assignment (??=)
            input ??= "Assigned if null";

            Console.WriteLine($"Result: {result}, Length: {length}");
        }

        public static void Main()
        {
            Console.WriteLine("=== Exception Handling Examples ===\n");

            Console.WriteLine("1. Basic Exception Handling:");
            BasicExceptionHandling();

            Console.WriteLine("\n2. Custom Exception:");
            try
            {
                ValidateAge(25);
                ValidateAge(200);
            }
            catch (InvalidAgeException ex)
            {
                Console.WriteLine($"Caught custom exception: {ex.Message}");
            }

            Console.WriteLine("\n3. Exception Filtering:");
            ExceptionFiltering(-5);

            Console.WriteLine("\n4. Null Handling:");
            NullHandling(null);
        }
    }
}
