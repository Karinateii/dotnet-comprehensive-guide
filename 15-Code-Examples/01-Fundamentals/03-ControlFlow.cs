// Control Flow Examples
// Location: 15-Code-Examples/01-Fundamentals/

// IF STATEMENTS
Console.WriteLine("=== IF Statements ===");

int age = 25;
if (age >= 18)
{
    Console.WriteLine("You are an adult");
}
else
{
    Console.WriteLine("You are a minor");
}

// IF-ELSE IF-ELSE
int score = 85;
if (score >= 90)
    Console.WriteLine("Grade: A");
else if (score >= 80)
    Console.WriteLine("Grade: B");
else if (score >= 70)
    Console.WriteLine("Grade: C");
else
    Console.WriteLine("Grade: F");

// TERNARY OPERATOR
string ageGroup = age < 13 ? "Child" : age < 18 ? "Teen" : "Adult";
Console.WriteLine($"Age Group: {ageGroup}");

// SWITCH STATEMENT
Console.WriteLine("\n=== Switch Statement ===");

int dayOfWeek = 3;
string dayName = dayOfWeek switch
{
    1 => "Monday",
    2 => "Tuesday",
    3 => "Wednesday",
    4 => "Thursday",
    5 => "Friday",
    _ => "Weekend"
};
Console.WriteLine($"Day: {dayName}");

// FOR LOOP
Console.WriteLine("\n=== For Loop ===");
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Count: {i}");
}

// WHILE LOOP
Console.WriteLine("\n=== While Loop ===");
int counter = 0;
while (counter < 3)
{
    Console.WriteLine($"Loop: {counter}");
    counter++;
}

// DO-WHILE LOOP
Console.WriteLine("\n=== Do-While Loop ===");
int num = 0;
do
{
    Console.WriteLine($"Do-While: {num}");
    num++;
} while (num < 3);

// FOREACH LOOP
Console.WriteLine("\n=== Foreach Loop ===");
int[] numbers = { 10, 20, 30, 40, 50 };
foreach (int number in numbers)
{
    Console.WriteLine($"Number: {number}");
}

// NESTED LOOPS
Console.WriteLine("\n=== Nested Loops ===");
for (int row = 1; row <= 3; row++)
{
    for (int col = 1; col <= 3; col++)
    {
        Console.Write($"({row},{col}) ");
    }
    Console.WriteLine();
}

// BREAK AND CONTINUE
Console.WriteLine("\n=== Break and Continue ===");
for (int i = 0; i < 10; i++)
{
    if (i == 3)
        continue;  // Skip 3
    if (i == 7)
        break;     // Stop at 7
    Console.Write($"{i} ");
}
Console.WriteLine();

// TIMES TABLE
Console.WriteLine("\n=== Times Table ===");
int tableNumber = 5;
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{tableNumber} × {i} = {tableNumber * i}");
}
