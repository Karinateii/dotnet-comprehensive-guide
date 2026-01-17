# .NET Versions and Platforms

## Understanding .NET Versions

### Current .NET Versions (2024-2025)

#### **.NET 8 LTS** (Long Term Support) ⭐ **RECOMMENDED**
- Released: November 2023
- Support Until: November 2026
- **For Production**: Stable, well-tested, great for enterprises
- Features: AOT compilation, performance improvements, AI/ML features
- **Best for**: New production applications

#### **.NET 9** (Latest)
- Released: November 2024
- Support Until: May 2026
- Latest features and improvements
- More experimental features
- **Best for**: Learning, new features, cutting-edge applications

#### **.NET 7 & Earlier**
- ❌ No longer supported
- Don't use for new projects

### Legacy: .NET Framework 4.8
- Windows-only
- Only for maintaining old applications
- No new features will be added
- End of life: January 2026

## Version Selection Guide

```
What should I use?

For NEW projects?
→ Use .NET 8 LTS (stable, long-term support)

For LEARNING?
→ Use .NET 8 LTS (stable) or .NET 9 (latest)

For EXISTING projects?
→ Keep what you have unless there's a reason to upgrade

For PRODUCTION?
→ Use .NET 8 LTS for maximum stability
```

## Release Schedule

```
November 2022 ← .NET 7 (End of Life: May 2024)
November 2023 ← .NET 8 LTS (Supported until Nov 2026)
November 2024 ← .NET 9 (Supported until May 2026)
November 2025 ← .NET 10 (Expected)
```

**LTS releases**: Every 2 years (5, 6, 7, 8, 9...)  
**Regular releases**: Every year (alternating with LTS)

## Supported Platforms

### Windows
| OS | Supported | Notes |
|----|-----------|-------|
| Windows 11 | ✅ Yes | Recommended |
| Windows 10 | ✅ Yes | Still supported |
| Windows Server 2022 | ✅ Yes | Latest LTS |
| Windows Server 2019 | ✅ Yes | Still supported |

**Minimum for .NET 8**: Windows 7 SP1 (for older systems)

### Linux
| Distribution | Supported | Notes |
|--------------|-----------|-------|
| Ubuntu 22.04 LTS | ✅ Yes | Recommended |
| Ubuntu 20.04 LTS | ✅ Yes | Still supported |
| Debian 11+ | ✅ Yes | Good support |
| Red Hat RHEL 8+ | ✅ Yes | Enterprise support |
| Alpine Linux | ✅ Yes | Lightweight, used in containers |
| CentOS | ⚠️ Limited | Deprecated, use RHEL/Rocky |

### macOS
| Version | Supported | Notes |
|---------|-----------|-------|
| macOS 12+ | ✅ Yes | Recommended |
| macOS 11 | ⚠️ Partial | Some features limited |
| Intel & Apple Silicon | ✅ Both | Native support for both |

## Hardware Requirements

### Minimum
- CPU: 1 GHz processor
- RAM: 512 MB
- Disk: 500 MB for installation

### Recommended
- CPU: Multi-core processor
- RAM: 4+ GB
- SSD: 2+ GB free space

### For Development
- RAM: 8+ GB (for IDE, Docker, debugging)
- SSD: 50+ GB (for projects, dependencies, Docker images)

## Different .NET Implementations

### .NET (Modern) - PRIMARY
```
Used for:
- New web applications
- New APIs
- Cross-platform apps
- Cloud applications
- Enterprise systems
```

### .NET Framework - LEGACY
```
Used for:
- Maintaining Windows-only apps
- Legacy systems
- Applications that require Windows features
```

### What's the Difference?

| Aspect | .NET | .NET Framework |
|--------|------|-----------------|
| **Platforms** | Windows, Linux, macOS | Windows only |
| **Development** | Active | No new features |
| **Performance** | Excellent | Good |
| **Target Use** | New projects | Legacy only |
| **Container Ready** | Yes | Limited |
| **Cloud Native** | Yes | No |
| **Open Source** | Yes | No |

## Version Compatibility

### Forward Compatibility
- Code written for .NET 6 usually runs on .NET 7, 8, 9 without changes
- Libraries need to be compatible

### Backward Compatibility
- .NET 9 can run .NET 8 code
- .NET 8 can run .NET 6 code
- **But not guaranteed** - test your code

### Breaking Changes
Each version documents breaking changes:
- Usually minimal for patch versions (8.0 → 8.1)
- May occur in minor versions (8 → 9)
- Always documented in release notes

## Side-by-Side Installation

You can have **multiple .NET versions installed** on one machine:
- .NET 6, 8, 9 can coexist
- Each application specifies which version it uses
- Specified in project file or global.json

Example:
```
dotnet --info   # Shows all installed versions
```

## LTS vs Standard Release

### Long Term Support (LTS) ⭐
- Released every 2 years
- Supported for 3 years
- More stable
- For production applications

**LTS Versions**: .NET 6, 8, (10), (12)...

### Standard Release
- Released every year
- Supported for 18 months
- Latest features
- For learning or rapid projects

**Non-LTS Versions**: .NET 5, 7, 9...

## Support Status Visualization

```
.NET 6 LTS:     ████████████████ (Ends Nov 2024)
.NET 7:         ████ (Ended May 2024)
.NET 8 LTS:     ████████████████████████████████ (Until Nov 2026) ← CURRENT
.NET 9:         ████████████ (Until May 2026)
.NET 10:        (Expected Nov 2025)

████ = 6 months support
```

## Migration Path

### From .NET Framework to .NET
```
Step 1: Identify breaking changes
Step 2: Understand third-party library compatibility
Step 3: Plan migration (may take months/years)
Step 4: Update dependencies in NuGet
Step 5: Fix compilation errors
Step 6: Test thoroughly
Step 7: Deploy to production
```

⚠️ Migration can be complex - plan accordingly!

## Checking Your Current .NET Version

```powershell
# See all installed versions
dotnet --list-sdks

# See runtime versions
dotnet --list-runtimes

# Check current version
dotnet --version
```

## Downloading and Installing

**Official Source**: https://dotnet.microsoft.com/download

**Command Line Installation**:
```powershell
# Windows with chocolatey
choco install dotnet-sdk

# Linux (Ubuntu)
sudo apt-get install dotnet-sdk-8.0

# macOS with Homebrew
brew install dotnet-sdk
```

## Best Practices

1. ✅ Always use **LTS versions** for production
2. ✅ **Test thoroughly** before upgrading
3. ✅ Keep your version **reasonably current** (security patches)
4. ✅ Use **global.json** to lock version for team projects
5. ✅ **Document** which version your project uses
6. ❌ Don't skip major versions without testing
7. ❌ Don't use unsupported versions in production

## Next Steps

- [03-Setup-and-Installation.md](./03-Setup-and-Installation.md) - Install .NET SDK
- [04-Your-First-Program.md](./04-Your-First-Program.md) - Create your first program
