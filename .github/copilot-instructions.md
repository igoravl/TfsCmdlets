# TfsCmdlets - PowerShell Cmdlets for Azure DevOps and Team Foundation Server

Welcome to the TfsCmdlets repository! This document provides instructions for GitHub Copilot Coding Agent to understand this project and how to work with it effectively.

## Project Overview

TfsCmdlets is a PowerShell module offering cmdlets to work with Azure DevOps (formerly known as VSTS) and Team Foundation Server. The project is primarily written in C# and PowerShell.

## Repository Structure

- `PS/`: Contains PowerShell module files, formats, types, and tests
- `CSharp/`: Contains C# source code for the project
- `Docs/`: Documentation files, including release notes
- `Setup/`: Installation setup files
- `BuildTools/`: Tools used during the build process
- `Assets/`: Project assets like icons and images

## Build Instructions

### Prerequisites

- PowerShell 5.1 or later
- .NET SDK 6.0 or later

### How to Build the Project

To build the project, run the following command:

```powershell
./Build.ps1 -Targets Package -Verbose
```

This command builds the project and creates the package with verbose output.

Other common build targets include:

- `Clean`: Cleans the build output directories
- `Compile`: Compiles the code without packaging
- `Test`: Runs the tests
- `Package`: Creates the package for distribution

### Development Flow

1. Make your changes to the codebase
2. Run the build script to verify everything compiles: `./Build.ps1 -Targets Compile -Verbose`
3. Run tests to ensure functionality: `./Build.ps1 -Targets Test -Verbose`
4. Create the package: `./Build.ps1 -Targets Package -Verbose`

## Code Standards

- Follow C# best practices and PowerShell best practices
- Maintain consistent formatting and naming conventions
- Write meaningful XML documentation for public APIs
- Update release notes in the Docs/ReleaseNotes directory when appropriate

## Testing

Tests are located in:
- PowerShell tests: `PS/_Tests/`
- C# tests: `CSharp/TfsCmdlets.Tests.UnitTests/`

Run tests using: `./Build.ps1 -Targets Test -Verbose`

## Documentation

Update documentation when adding new features or making changes:
- For new cmdlets, ensure proper XML documentation is added
- Update release notes for significant changes in `Docs/ReleaseNotes/`

## Common Issues and Solutions

- If the build fails with missing NuGet packages, ensure NuGet package restore is working properly
- If PowerShell execution is restricted, you may need to use `Set-ExecutionPolicy` to allow the build script to run
- Make sure all prerequisites are installed before attempting to build

When working on this project, please ensure your changes align with the existing architecture and follow the established patterns.
