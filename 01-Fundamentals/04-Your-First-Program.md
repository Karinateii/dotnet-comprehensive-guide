# Your First Program

## Creating a Console Application

### Step 1: Open Terminal/Command Prompt

Navigate to where you want your project:

```powershell
cd C:\Users\YourUsername\Desktop
# or Linux/macOS
cd ~/Desktop
```

### Step 2: Create a New Project

```powershell
dotnet new console -n HelloWorld
cd HelloWorld
```

This creates a basic console application folder structure:
```
HelloWorld/
├── Program.cs          # Main code file
├── HelloWorld.csproj   # Project configuration
└── obj/               # Temporary files (ignore)
```

### Step 3: Examine the Project File (.csproj)

**HelloWorld.csproj**:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```

**What this means**:
- `OutputType=Exe` → Creates executable
- `TargetFramework=net8.0` → Uses .NET 8
- `Nullable=enable` → Enables nullable reference types
- `ImplicitUsings=enable` → Auto-imports common namespaces

### Step 4: View the Default Program

**Program.cs**:
```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

That's it! This is a complete, runnable program.

### Step 5: Run Your Program

```powershell
dotnet run
```

**Output**:
```
Hello, World!
```

Congratulations! 🎉 You've created and run your first .NET program!

## Understanding Program.cs

### Old Style (Pre .NET 6)
```csharp
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

### New Style (.NET 6+) ⭐
```csharp
Console.WriteLine("Hello, World!");
```

**Why the difference?**
- .NET 6+ uses "Top-level statements"
- Simplified for beginners
- Less boilerplate code
- Same functionality

## Your First Real Program

Let's create something more interesting:

```csharp
// Get user input
Console.Write("What is your name? ");
string name = Console.ReadLine();

Console.Write("What is your age? ");
int age = int.Parse(Console.ReadLine());

// Calculate birth year
int currentYear = DateTime.Now.Year;
int birthYear = currentYear - age;

// Display output
Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"You were born around {birthYear}");
Console.WriteLine($"That makes you {age} years old");
```

**Save this in Program.cs and run:**
```powershell
dotnet run
```

**Sample Output**:
```
What is your name? John
What is your age? 30
Hello, John!
You were born around 1994
That makes you 30 years old
```

## Project Structure Explained

After running `dotnet new console -n HelloWorld`:

```
HelloWorld/
├── bin/                    # Compiled output
│   └── Debug/
│       └── net8.0/         # Your executable lives here
├── obj/                    # Temporary build files
├── Program.cs              # Your code
├── HelloWorld.csproj       # Project configuration
└── .gitignore             # Git configuration (if using git)
```

### Key Files

| File | Purpose |
|------|---------|
| `Program.cs` | Your C# code |
| `*.csproj` | Project configuration (NuGet packages, settings) |
| `bin/` | Compiled executable |
| `obj/` | Temporary files (can be deleted) |

## Common Commands

```powershell
# Create project
dotnet new console -n ProjectName

# Run program
dotnet run

# Build program (without running)
dotnet build

# Clean temporary files
dotnet clean

# Add NuGet package
dotnet add package PackageName

# Restore dependencies
dotnet restore
```

## Project Templates Available

```powershell
# See all available templates
dotnet new --list

# Some common ones:
dotnet new console          # Console app
dotnet new webapi          # REST API
dotnet new mvc             # Web MVC app
dotnet new classlib        # Class library (reusable code)
dotnet new xunit           # Unit test project
dotnet new gitignore       # .gitignore file
```

## Running from IDE

### Visual Studio Code
1. Open folder containing your project
2. Click "Run and Debug" (Ctrl+Shift+D)
3. Select ".NET" if prompted
4. Click play button or press F5

### Visual Studio 2022
1. Open folder
2. Press F5 or click green play button
3. Program runs in debug mode

### Command Line
```powershell
dotnet run
```

## Debugging

### Run with Console Output
```powershell
dotnet run
```

### Run and Wait for Key Press
Add to end of Program.cs:
```csharp
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
```

### Debug in Visual Studio Code

1. Open Debug view (Ctrl+Shift+D)
2. Click "Create launch.json"
3. Select ".NET"
4. Click play button
5. Use breakpoints (click line number)
6. Step through code

## Building for Distribution

### Build Release Version (Optimized)
```powershell
dotnet build --configuration Release
```

### Publish as Standalone Executable
```powershell
dotnet publish -c Release -o ./publish
```

This creates a folder you can share with others (no .NET installation required on their machine).

## Next Steps

Great job! You now understand:
✅ How to create a .NET project  
✅ How to run your code  
✅ Basic console input/output  

Now learn **[C# Basics](../02-CSharp-Basics/)** to expand your programming knowledge!

## Quick Reference

| Task | Command |
|------|---------|
| New console project | `dotnet new console -n Name` |
| Run program | `dotnet run` |
| Build | `dotnet build` |
| Clean | `dotnet clean` |
| Add package | `dotnet add package Name` |
| Open in VS Code | `code .` |

---

**Happy coding!** 🚀
