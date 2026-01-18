using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetGuide.LINQ
{
    /// <summary>
    /// Advanced LINQ Query Examples
    /// Demonstrates complex queries, joins, grouping, and performance considerations
    /// </summary>
    
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public List<int> CourseIds { get; set; } = new();
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Instructor { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AdvancedLinqExamples
    {
        private static List<Student> GetStudents()
        {
            return new()
            {
                new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 20, DepartmentId = 1, CourseIds = new() { 1, 2, 3 } },
                new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Age = 21, DepartmentId = 1, CourseIds = new() { 1, 4 } },
                new Student { Id = 3, Name = "Charlie", Email = "charlie@example.com", Age = 19, DepartmentId = 2, CourseIds = new() { 2, 3, 4 } },
                new Student { Id = 4, Name = "Diana", Email = "diana@example.com", Age = 22, DepartmentId = 2, CourseIds = new() { 4 } }
            };
        }

        private static List<Course> GetCourses()
        {
            return new()
            {
                new Course { Id = 1, Title = "C# Fundamentals", Credits = 3, Instructor = "Dr. Smith" },
                new Course { Id = 2, Title = "Web Development", Credits = 4, Instructor = "Dr. Jones" },
                new Course { Id = 3, Title = "Database Design", Credits = 3, Instructor = "Dr. Brown" },
                new Course { Id = 4, Title = "Cloud Computing", Credits = 4, Instructor = "Dr. Lee" }
            };
        }

        // LINQ Select - Transform data
        public static void SelectExample()
        {
            var students = GetStudents();
            
            var studentNames = students
                .Select(s => s.Name)
                .ToList();

            Console.WriteLine("Student Names:");
            foreach (var name in studentNames)
            {
                Console.WriteLine($"  - {name}");
            }
        }

        // LINQ Where - Filter data
        public static void WhereExample()
        {
            var students = GetStudents();
            
            var adultsFromDept1 = students
                .Where(s => s.Age >= 21 && s.DepartmentId == 1)
                .Select(s => $"{s.Name} ({s.Age})")
                .ToList();

            Console.WriteLine("Adults from Department 1:");
            foreach (var student in adultsFromDept1)
            {
                Console.WriteLine($"  - {student}");
            }
        }

        // LINQ Join - Combine data from multiple sources
        public static void JoinExample()
        {
            var students = GetStudents();
            var courses = GetCourses();

            var studentCourses = students
                .SelectMany(s => s.CourseIds, (s, courseId) => new { s.Name, CourseId = courseId })
                .Join(courses, 
                    sc => sc.CourseId, 
                    c => c.Id, 
                    (sc, c) => $"{sc.Name} enrolled in {c.Title}")
                .ToList();

            Console.WriteLine("Student-Course Enrollments:");
            foreach (var enrollment in studentCourses)
            {
                Console.WriteLine($"  - {enrollment}");
            }
        }

        // LINQ GroupBy - Group data
        public static void GroupByExample()
        {
            var students = GetStudents();

            var studentsByDept = students
                .GroupBy(s => s.DepartmentId)
                .Select(g => new 
                { 
                    DepartmentId = g.Key, 
                    Count = g.Count(),
                    Students = string.Join(", ", g.Select(s => s.Name))
                })
                .ToList();

            Console.WriteLine("Students by Department:");
            foreach (var dept in studentsByDept)
            {
                Console.WriteLine($"  Department {dept.DepartmentId}: {dept.Count} students - {dept.Students}");
            }
        }

        // LINQ OrderBy - Sort data
        public static void OrderByExample()
        {
            var students = GetStudents();

            var sortedStudents = students
                .OrderByDescending(s => s.Age)
                .ThenBy(s => s.Name)
                .Select(s => $"{s.Name} - Age {s.Age}")
                .ToList();

            Console.WriteLine("Students sorted by Age (desc) then Name:");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"  - {student}");
            }
        }

        // LINQ Aggregate - Calculate values
        public static void AggregateExample()
        {
            var students = GetStudents();

            var avgAge = students.Average(s => s.Age);
            var maxAge = students.Max(s => s.Age);
            var minAge = students.Min(s => s.Age);
            var total = students.Count();

            Console.WriteLine("Student Age Statistics:");
            Console.WriteLine($"  Average Age: {avgAge:F2}");
            Console.WriteLine($"  Max Age: {maxAge}");
            Console.WriteLine($"  Min Age: {minAge}");
            Console.WriteLine($"  Total Students: {total}");
        }

        // LINQ Distinct - Remove duplicates
        public static void DistinctExample()
        {
            var numbers = new[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4 };
            
            var unique = numbers
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            Console.WriteLine("Unique Numbers:");
            Console.WriteLine($"  {string.Join(", ", unique)}");
        }

        // LINQ Any/All - Conditional checks
        public static void ConditionalExample()
        {
            var students = GetStudents();

            bool hasAdults = students.Any(s => s.Age >= 21);
            bool allAdults = students.All(s => s.Age >= 18);

            Console.WriteLine("Conditional Checks:");
            Console.WriteLine($"  Has students 21+: {hasAdults}");
            Console.WriteLine($"  All students 18+: {allAdults}");
        }

        public static void Main()
        {
            Console.WriteLine("=== Advanced LINQ Examples ===\n");

            SelectExample();
            Console.WriteLine();

            WhereExample();
            Console.WriteLine();

            JoinExample();
            Console.WriteLine();

            GroupByExample();
            Console.WriteLine();

            OrderByExample();
            Console.WriteLine();

            AggregateExample();
            Console.WriteLine();

            DistinctExample();
            Console.WriteLine();

            ConditionalExample();
        }
    }
}
