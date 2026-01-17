// Variables and Data Types Examples
// Location: 15-Code-Examples/01-Fundamentals/

// Integer types
byte smallNumber = 255;
short mediumNumber = 32000;
int largeNumber = 2_000_000;  // Underscores for readability
long veryLargeNumber = 9223372036854775807L;

Console.WriteLine($"Byte: {smallNumber}");
Console.WriteLine($"Short: {mediumNumber}");
Console.WriteLine($"Int: {largeNumber}");
Console.WriteLine($"Long: {veryLargeNumber}");

// Floating point
float singlePrecision = 3.14f;
double doublePrecision = 3.14159265359;
decimal moneyAmount = 19.99m;

Console.WriteLine($"Float: {singlePrecision}");
Console.WriteLine($"Double: {doublePrecision}");
Console.WriteLine($"Decimal: {moneyAmount}");

// String operations
string firstName = "John";
string lastName = "Doe";
string fullName = $"{firstName} {lastName}";

Console.WriteLine($"Full Name: {fullName}");
Console.WriteLine($"Name Length: {fullName.Length}");
Console.WriteLine($"Uppercase: {fullName.ToUpper()}");
Console.WriteLine($"Lowercase: {fullName.ToLower()}");

// Boolean
bool isActive = true;
bool isDeleted = false;

Console.WriteLine($"Active: {isActive}");
Console.WriteLine($"Deleted: {isDeleted}");

// DateTime
DateTime now = DateTime.Now;
DateTime specificDate = new DateTime(2025, 1, 17);

Console.WriteLine($"Current: {now}");
Console.WriteLine($"Specific: {specificDate}");
Console.WriteLine($"Date: {now:yyyy-MM-dd}");
Console.WriteLine($"Time: {now:HH:mm:ss}");

// Type inference with var
var count = 42;           // int
var message = "Hello";    // string
var price = 9.99;        // double

Console.WriteLine($"Count type: {count.GetType()}");
Console.WriteLine($"Message type: {message.GetType()}");
Console.WriteLine($"Price type: {price.GetType()}");
