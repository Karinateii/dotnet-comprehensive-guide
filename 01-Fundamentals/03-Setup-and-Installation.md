# Setup and Installation

## Prerequisites

Before installing .NET, make sure you have:
- ✅ A supported operating system (Windows 10+, Ubuntu 20.04+, macOS 11+)
- ✅ Administrator access to your computer
- ✅ Internet connection (for downloading)
- ✅ ~2 GB free disk space

## Installation Steps

### Windows

#### Option 1: Official Installer (Easiest) ⭐

1. Go to https://dotnet.microsoft.com/download
2. Click "Download .NET 8 SDK" (or latest LTS)
3. Run the installer
4. Follow the installation wizard
5. **Restart your computer** (important!)
6. Verify installation:
```powershell
dotnet --version
```

#### Option 2: Windows Package Manager

```powershell
# Using winget (built-in Windows 10/11)
winget install Microsoft.DotNet.SDK.8

# Or using Chocolatey
choco install dotnet-sdk-8.0
```

#### Option 3: Scoop Package Manager

```powershell
scoop bucket add versions
scoop install dotnet-sdk-8.0
```

### macOS

#### Option 1: Official Installer

1. Visit https://dotnet.microsoft.com/download
2. Select macOS
3. Choose Intel or Apple Silicon (check your Mac)
4. Download and run installer
5. Verify:
```bash
dotnet --version
```

#### Option 2: Homebrew (Recommended for Mac)

```bash
# Install Homebrew if you don't have it
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install .NET SDK
brew install dotnet-sdk

# Or specific version
brew install dotnet-sdk@8
```

#### Determining Your Mac's CPU

```bash
# Apple Silicon (M1, M2, M3, etc.)
uname -m
# Output: arm64 → Download Apple Silicon version

# Intel
uname -m
# Output: x86_64 → Download Intel version
```

### Linux (Ubuntu/Debian)

```bash
# Update package lists
sudo apt-get update

# Install .NET SDK 8
sudo apt-get install -y dotnet-sdk-8.0

# Verify installation
dotnet --version
```

### Linux (Red Hat/CentOS/RHEL)

```bash
# Using dnf (Red Hat 8+)
sudo dnf install dotnet-sdk-8.0

# Using yum (older versions)
sudo yum install dotnet-sdk-8.0

# Verify
dotnet --version
```

### Docker (Containerized)

If you prefer a containerized setup:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app
COPY . .

RUN dotnet restore
RUN dotnet build

CMD ["dotnet", "run"]
```

## What Gets Installed?

### .NET SDK (What you need for development)
- **SDK** - Compiler, build tools, dotnet CLI
- **Runtime** - Necessary to run .NET applications
- **Tools** - NuGet, debuggers, analyzers

**Size**: ~500 MB - 2 GB depending on platform

### Key Files Location

| OS | Location |
|----|----------|
| Windows | `C:\Program Files\dotnet\` |
| macOS | `/usr/local/share/dotnet/` |
| Linux | `/usr/share/dotnet/` |

## Verify Installation

### Check Version
```powershell
dotnet --version
# Output: 8.0.x or later
```

### Check Runtime
```powershell
dotnet --info
# Shows SDK version, Runtime version, and environment info
```

### List Installed SDKs
```powershell
dotnet --list-sdks
# Lists all installed .NET SDKs
```

### List Runtimes
```powershell
dotnet --list-runtimes
# Lists all installed runtimes
```

## IDE/Editor Installation

### Visual Studio Code (Lightweight) ⭐
```
1. Download from https://code.visualstudio.com
2. Install
3. Install C# Dev Kit extension (ms-dotnettools.csharp)
4. Open folder and start coding
```

**Recommended extensions**:
- C# Dev Kit
- C# Extensions
- NuGet Package Manager
- Thunder Client (API testing)

### Visual Studio 2022 (Full IDE - Professional)
```
1. Download Community Edition (FREE): https://visualstudio.microsoft.com
2. Run installer
3. Select "ASP.NET and web development" workload
4. Also select ".NET desktop development" if needed
5. Complete installation (~15-20 GB)
```

**Community Edition is FREE** - Use for commercial projects if revenue < $1M

### JetBrains Rider (Paid but excellent)
```
Download from https://www.jetbrains.com/rider/
```

### Terminal/Command Line Only

You can develop with just .NET CLI:
```powershell
dotnet new console -n MyApp
cd MyApp
dotnet run
```

## Updating .NET

### Check for Updates
```powershell
dotnet --version
```

### Update SDK
```powershell
# Windows
winget upgrade Microsoft.DotNet.SDK.8

# macOS
brew upgrade dotnet-sdk

# Linux
sudo apt-get update
sudo apt-get install dotnet-sdk-8.0
```

## Troubleshooting

### Problem: Command not found / dotnet not recognized

**Solution**:
1. Restart terminal/command prompt
2. Restart your computer
3. Check installation path in environment variables
4. Reinstall .NET SDK

### Problem: Wrong version installed

```powershell
# Uninstall and reinstall
dotnet --uninstall --all-previews  # Remove preview versions
```

### Problem: Permission denied (Linux/macOS)

```bash
# Ensure correct ownership
sudo chown -R $USER /usr/local/share/dotnet

# Or use sudo for commands
sudo dotnet --version
```

### Problem: Out of disk space

- Clean NuGet cache: `dotnet nuget locals all --clear`
- Remove old SDK versions: Uninstall old .NET versions
- Remove Docker images: `docker image prune`

## Environment Variables (Advanced)

### DOTNET_ROOT
Points to .NET installation directory. Usually set automatically.

```powershell
# Check current path
$env:DOTNET_ROOT

# Set manually if needed
$env:DOTNET_ROOT = "C:\Program Files\dotnet"
```

### DOTNET_CLI_TELEMETRY_OPTOUT
Disable telemetry (Microsoft collects basic usage data):

```powershell
# Opt out of telemetry
$env:DOTNET_CLI_TELEMETRY_OPTOUT = "1"
```

## System Requirements Summary

| Requirement | Minimum | Recommended |
|------------|---------|-------------|
| RAM | 512 MB | 8+ GB |
| Disk Space | 500 MB | 2+ GB (20+ with IDE) |
| CPU | 1 GHz | Multi-core |
| Network | Download | Persistent |

## Next Steps

✅ Installation complete!

- [04-Your-First-Program.md](./04-Your-First-Program.md) - Create your first .NET app
- Then proceed to [C# Basics](../02-CSharp-Basics/)
