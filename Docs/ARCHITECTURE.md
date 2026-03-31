# TfsCmdlets Architecture

This document describes the architecture of the TfsCmdlets PowerShell module, with emphasis on
authentication (including Personal Access Tokens and environment variables) and shell integration
(including Oh‑My‑Posh support).

---

## 1. Overview

TfsCmdlets is a PowerShell module that provides cmdlets to automate interaction with:

- Team Foundation Server (TFS) 2010–2018
- Azure DevOps Server (2019+) and Azure DevOps Services

The module targets administrators and power users who need to script and automate operations such as:

- Managing organizations, collections, and projects
- Managing work items, teams, iterations, and identities
- Managing pipelines, releases, artifacts, service hooks, extensions, and wikis

A companion **Azure DevOps Shell** experience is provided, offering a pre‑configured PowerShell
environment tailored to TfsCmdlets. Distribution is via PowerShell Gallery, NuGet, Chocolatey,
Windows Package Manager (winget), MSI installer, and portable ZIP packages.

---

## 2. High‑Level Architecture

At a high level, the module is composed of the following layers:

- **Core .NET library (C#)**  
  Implements the integration with Azure DevOps/TFS REST and client APIs, connection management,
  domain models, and shared utilities.

- **PowerShell module surface**  
  Exposes the core functionality through cmdlets, parameter binding, pipeline support, and
  user‑facing error handling.

- **Authentication and configuration layer**  
  Handles connection context, credentials (including PATs), environment‑variable configuration,
  and profile‑level defaults.

- **Shell integration**  
  Provides the Azure DevOps‑focused shell experience and integrates with prompt/customization
  tools such as Oh‑My‑Posh.

- **Build and packaging tooling**  
  Produces multi‑targeted binaries and packages for the supported distribution channels.
### **3. BUILD SYSTEM AND HOW TO BUILD**

**Primary Build Entry Point**: [Build.ps1](../Build.ps1)

**Build System Architecture:**
- Uses **PowerShell scripting** as the entry point ([Build.ps1](../Build.ps1))
- Delegates to **psake** task runner ([psake.ps1](../psake.ps1)) for actual build tasks
- Uses **.NET CLI** (dotnet) for compilation

**Build Targets (from psake.ps1):**
```
Package → Build, GenerateDocs, AllTests, RemoveEmptyFolders, ValidateReleaseNotes, 
          PackageNuget, PackageChocolatey, PackageMSI, PackageWinget, PackageDocs, PackageModule

Build → CleanOutputDir, CreateOutputDir, BuildLibrary, UnitTests, GenerateHelp, 
        CopyFiles, GenerateTypesXml, GenerateFormatXml, GenerateNestedModule, UpdateModuleManifest

Rebuild → Clean, Build
Test → UnitTests, AllTests
```

**Key Build Steps:**
1. **Install Dependencies** - NuGet packages, PowerShell modules (GitVersion, psake, PsScriptAnalyzer, VSSetup, powershell-yaml, ps1xmlgen, PlatyPS)
2. **BuildLibrary** - Publishes C# projects for both `net471` and `netcoreapp3.1` targets to `out/module/Lib/{Desktop,Core}`
3. **UnitTests** - Runs xUnit tests
4. **GenerateHelp** - Generates PowerShell help using PlatyPS
5. **Copy Files** - Assembles module structure
6. **GenerateTypesXml** & **GenerateFormatXml** - Creates PS1XML files for type extensions and formatting
7. **Package** - Creates installers for NuGet, Chocolatey, MSI, WinGet, and ZIP portable

**Build Parameters:**
- `-Configuration`: Debug or Release (default: Release)
- `-Targets`: What to build (default: Package)
- `-SkipTests`: Skip test execution
- `-SkipReleaseNotes`: Skip release notes validation
- `-Incremental`: Incremental build (don't clean output)

**Output Artifacts** (in `out/` directory):
```
out/
├── module/              # Final assembled module
├── nuget/              # NuGet package
├── chocolatey/         # Chocolatey package
├── msi/                # MSI installer
├── portable/           # ZIP portable
├── winget/             # Windows Package Manager
├── docs/               # Documentation
└── TestResults-*.xml   # Test results
```

---

### **4. TEST FRAMEWORK AND HOW TO RUN TESTS**

**Unit Testing:**
- **Framework**: xUnit v2.4.1
- **Test Project**: [CSharp/TfsCmdlets.Tests.UnitTests](CSharp/TfsCmdlets.Tests.UnitTests)
- **Target Framework**: net8.0-windows
- **Dependencies**:
  - NSubstitute v4.2.2 (mocking)
  - Microsoft.CodeAnalysis.CSharp (Roslyn)
  - Microsoft.PowerShell.SDK v7.0.10

**Run Tests:**
```bash
# Via Build.ps1
./Build.ps1 -Targets Test

# Direct with dotnet
dotnet test CSharp/TfsCmdlets.Tests.UnitTests/TfsCmdlets.Tests.UnitTests.csproj

# In CI
dotnet test (runs via psake task)
```

**Integration/Pester Tests:**
- Location: [PS/_Tests](PS/_Tests) directory with subdirectories by functionality:
  - Admin, Artifact, ExtensionManagement, Git, Identity, Organization, Pipeline, ProcessTemplate, RestApi, ServiceHook, Team, TeamProject, TeamProjectCollection, TestManagement, Wiki, WorkItem
- Framework: PowerShell Pester (invoked via build)
- Results: Saved as `TestResults-Pester*.xml` for CI reporting

**Code Coverage:**
- Tool: coverlet v3.0.2
- Collected during unit test runs
- Published in CI

---

### **5. CODE CONVENTIONS (NAMING, FORMATTING, PATTERNS)**

**Naming Conventions:**

**Cmdlets:**
- Pattern: `{Verb-TfsNoun}` (e.g., `Get-TfsWorkItem`, `New-TfsIteration`, `Set-TfsWorkItem`)
- Class naming (partial classes): `{Verb}{Noun}` (e.g., `GetWorkItem`, `NewWorkItem`)
- Verbs must follow PowerShell Approved Verbs (Get, Set, New, Remove, Test, Search, Connect, Disconnect, etc.)
- Noun represents the data type

**Controllers:**
- Pattern: `{Verb}{Noun}Controller` (e.g., `GetWorkItemController`)
- Inherit from `ControllerBase`
- Handle business logic
- Marked with `[CmdletControllerAttribute]`

**Services:**
- Pattern: `I{ServiceName}` (interface) and `{ServiceName}Impl` (implementation)
- Examples: `IRestApiService`, `IDataManager`, `IParameterManager`, `ILogger`
- Marked with `[Export(...)]` for MEF composition

**Attributes:**
- Custom attributes: ending in `Attribute` (e.g., `CmdletControllerAttribute`, `WorkItemFieldAttribute`, `TfsCmdletAttribute`)

**Formatting:**
- **EditorConfig**: `.editorconfig` present with minimal rules disabling certain IDE diagnostics (IDE0056, IDE0057)
- **C# Language Version**: LangVersion 11.0 (previewed) for main project, v8.0 for Legacy
- **Namespace Structure**: Mirrors folder structure (TfsCmdlets.Cmdlets.*, TfsCmdlets.Controllers.*, TfsCmdlets.Services.*)

**Code Patterns:**

1. **Cmdlet Pattern** (Partial Classes):
   - Base location: [CSharp/TfsCmdlets/Cmdlets/](CSharp/TfsCmdlets/Cmdlets) with subdirectories by domain (WorkItem, Team, etc.)
   - Extend `CmdletBase` (from PowerShellStandard.Library)
   - Declare parameters using `[Parameter]` attributes
   - Auto-implementation via source generators
   - Marked with `[TfsCmdlet(scope, OutputType=...)]` attribute

2. **Controller Pattern**:
   - Handle cmdlet logic
   - Implement business operations
   - Marked with `[CmdletControllerAttribute]` with DataType
   - Constructor injection of services (IPowerShellService, IDataManager, IParameterManager, ILogger)
   - Methods: `CacheParameters()` and `Run()` (abstract)

3. **Service/Interface Pattern**:
   - MEF composition using `[Import]` and `[Export]` attributes
   - Dependency injection throughout
   - Example: `RestApiServiceImpl` handles REST API invocations

4. **Global Usings** ([CSharp/TfsCmdlets/GlobalUsings.cs](CSharp/TfsCmdlets/GlobalUsings.cs)):
   - All common namespaces pre-imported globally
   - Aliases for long WebAPI type names (e.g., `WebApiWorkItem`, `WebApiTeamProject`)
   - Reduces repetitive imports

5. **Source Generator Usage**:
   - Generates cmdlet and controller boilerplate
   - Parses `[TfsCmdlet]` attributes to auto-gen parameter sets, output types, etc.
   - Generates properties and methods automatically

---

### **6. PROJECT STRUCTURE AND KEY DIRECTORIES**

```
TfsCmdlets/
├── CSharp/                          # C# source code
│   ├── TfsCmdlets/                 # Main library
│   │   ├── Cmdlets/                # Cmdlet partial classes and inline controllers by domain
│   │   │   ├── WorkItem/           # Work item operations
│   │   │   ├── Team/               # Team operations
│   │   │   ├── TeamProject/        # Team project operations
│   │   │   ├── Organization/       # Organization operations
│   │   │   ├── Pipeline/           # Build pipeline operations
│   │   │   ├── Git/                # Git repository operations
│   │   │   ├── Admin/              # Admin operations
│   │   │   ├── ControllerBase.cs   # Base class for all controllers
│   │   │   └── ... (other domains)
│   │   ├── Services/               # Service interfaces and implementations
│   │   │   └── Impl/               # Service implementations
│   │   ├── Models/                 # Data models and POCOs
│   │   ├── Extensions/             # Extension methods
│   │   ├── HttpClients/            # Http client abstractions
│   │   ├── Util/                   # Utility functions
│   │   ├── Properties/             # Assembly properties
│   │   ├── CmdletBase.cs           # Base class for all cmdlets
│   │   ├── Attributes.cs           # Custom attributes
│   │   ├── GlobalUsings.cs         # Global using directives
│   │   ├── ModuleInitializer.cs    # Module initialization
│   │   ├── TfsCmdlets.csproj       # Main project file
│   │   └── bin/, obj/              # Build outputs
│   ├── TfsCmdlets.Legacy/          # Legacy .NET Framework code (net471 only)
│   │   ├── Cmdlets/                # Legacy cmdlet classes
│   │   └── Controllers/            # Legacy controller implementations
│   ├── TfsCmdlets.SourceGenerators/  # Roslyn source generators
│   │   ├── Generators/
│   │   │   ├── Cmdlets/            # Cmdlet code generation
│   │   │   ├── Controllers/        # Controller code generation
│   │   │   ├── HttpClients/        # HttpClient generation
│   │   │   └── Models/             # Model code generation
│   │   ├── Analyzers/              # Diagnostic analyzers
│   │   └── BaseGenerator.cs, BaseTypeProcessor.cs, etc. (generator infrastructure)
│   ├── TfsCmdlets.Tests.UnitTests/   # Unit tests (xUnit)
│   │   ├── Cmdlets/                # Cmdlet tests
│   │   ├── Controllers/            # Controller tests
│   │   ├── Services/               # Service tests
│   │   ├── SourceGenerators/       # Generator tests
│   │   └── Fixtures/               # Test fixtures
│   └── TfsCmdlets.sln              # Visual Studio solution
│
├── PS/                             # PowerShell module files
│   ├── TfsCmdlets.psd1            # Module manifest
│   ├── TfsCmdlets.psm1            # Module script
│   ├── _Private/                  # Private functions
│   │   ├── Admin.ps1
│   │   ├── Aliases.ps1
│   │   └── Completers/            # Tab completion scripts
│   ├── _Tests/                    # Pester integration tests
│   │   └── {Domain}/ subdirectories
│   ├── _Formats/                  # PowerShell formatting
│   │   └── Views/, Controls/
│   ├── _Types/                    # Type extensions
│   └── _Themes/                   # Azure DevOps Shell themes
│
├── Setup/                          # WiX MSI installer and WinGet packaging
│   ├── Product.wxs                # WiX source
│   └── winget/                    # Windows Package Manager manifest
│
├── Docs/                           # Documentation
│   └── CommonHelpText.psd1        # Shared help text
│
├── BuildTools/                     # Build automation tools
│   └── XmlDoc2CmdletDoc/          # Tool for converting XML docs to cmdlet help
│
├── Assets/                         # Project assets and images
│
├── Build.ps1                       # Primary build entry point
├── BuildDoc.ps1                    # Documentation build
├── psake.ps1                       # Task runner configuration
├── gitversion.yml                  # GitVersion configuration
├── .editorconfig                   # Editor formatting rules
├── .github/workflows/main.yml      # CI/CD pipeline
├── TfsCmdlets.code-workspace       # VS Code workspace config
├── README.md, CONTRIBUTING.md, CODE_OF_CONDUCT.md, etc.
└── out/                            # Build outputs (generated)
```

---

### **7. IMPORTANT CONFIGURATION FILES**

| File | Purpose | Key Content |
|------|---------|---|
| [TfsCmdlets.csproj](CSharp/TfsCmdlets/TfsCmdlets.csproj) | Main .NET project | Multi-target (net471, netcoreapp3.1), source generators, Azure DevOps client NuGet packages, LangVersion 11 preview |
| [TfsCmdlets.psd1](PS/TfsCmdlets.psd1) | PowerShell module manifest | RootModule = Lib/Desktop/TfsCmdlets.dll, version placeholders, metadata tags, PS version requirements |
| [TfsCmdlets.psm1](PS/TfsCmdlets.psm1) | PowerShell module script | Currently empty (# Private functions) |
| [gitversion.yml](gitversion.yml) | GitVersion config | Pull request mode: ContinuousDelivery, tag override for CI |
| [.editorconfig](.editorconfig) | Code style rules | Disables IDE0056 and IDE0057 rules for .cs files |
| [.github/workflows/main.yml](.github/workflows/main.yml) | GitHub Actions CI/CD | Build on windows-latest, CodeQL analysis, runs Build.ps1, publishes packages/docs |
| [psake.ps1](psake.ps1) | Build task definitions | Defines all build tasks, output directories, versioning, multi-target framework logic |
| [Build.ps1](Build.ps1) | Build entry point | Installs dependencies, delegats to psake, validates environment |
| [TfsCmdlets.code-workspace](TfsCmdlets.code-workspace) | VS Code workspace | Workspace configuration for development |

---

### **8. SOURCE GENERATORS USAGE**

**Purpose**: Auto-generate cmdlet and controller boilerplate code

**Architecture**:
- **Framework**: Microsoft.CodeAnalysis.CSharp v4.2.0 (Roslyn)
- **Project**: [TfsCmdlets.SourceGenerators](CSharp/TfsCmdlets.SourceGenerators) (netstandard2.0, IsRoslynComponent=true)
- **Referenced As**: Analyzer with `OutputItemType="Analyzer"` and `ReferenceOutputAssembly="false"`

**Generator Components**:

1. **Cmdlet Generators** ([Generators/Cmdlets](CSharp/TfsCmdlets.SourceGenerators/Generators/Cmdlets)):
   - `Generator.cs` - Main cmdlet code generator
   - `CmdletInfo.cs` - Analyzes cmdlet attributes and metadata
   - `Filter.cs` - Filters which cmdlets to process
   - `TypeProcessor.cs` - Processes cmdlet parameter types

2. **Controller Generators** ([Generators/Controllers](CSharp/TfsCmdlets.SourceGenerators/Generators/Controllers)):
   - `Generator.cs` - Main controller code generator
   - `ControllerInfo.cs` - Analyzes controller metadata
   - `Analyzers.cs` - Diagnostic analyzers
   - `Filter.cs` - Filtering logic

3. **HttpClient Generators** ([Generators/HttpClients](CSharp/TfsCmdlets.SourceGenerators/Generators/HttpClients)):
   - Generates HTTP client abstractions

4. **Model Generators** ([Generators/Models](CSharp/TfsCmdlets.SourceGenerators/Generators/Models)):
   - Generates model classes

**Base Infrastructure** ([TfsCmdlets.SourceGenerators](CSharp/TfsCmdlets.SourceGenerators)):
- `BaseGenerator.cs` - Abstract base for all generators
- `BaseTypeProcessor.cs` - Abstract base for type processors
- `BaseFilter.cs` - Abstract base for filtering logic
- `GeneratorState.cs` - Tracks generator state
- `IGenerator.cs`, `ITypeProcessor.cs`, `IFilter.cs` - Interfaces
- `Logger.cs` - Logging for generators
- `Extensions.cs` - Reflection helper extensions
- `DiagnosticDescriptors.cs` - Analyzer diagnostic definitions
- `CmdletScope.cs` - Enum for cmdlet scopes

**Input Attributes**:
- `[TfsCmdlet(...)]` - Decorates cmdlet classes with metadata:
  - `CmdletScope` - Where the cmdlet can be used (Organization, TeamProjectCollection, TeamProject, Team, Object, Global, etc.)
  - `OutputType` - Return type
  - `DataType` - Data model type
  - `CustomVerbs/Nouns` - Override naming
  - `NoAutoPipeline`, `DesktopOnly`, `HostedOnly`, `SupportsShouldProcess`, etc.
- `[CmdletControllerAttribute(...)]` - Marks controller classes

**Output**:
- Generated C# partial classes
- In Debug configuration: Emitted to `obj/Generated` for inspection
- Enhances cmdlets with standard parameters (Credential, Collection, Project, Team, etc.)

---

### **9. POWERSHELL MODULE STRUCTURE**

**Module Manifest** ([PS/TfsCmdlets.psd1](PS/TfsCmdlets.psd1)):
```
Author: Igor Abade V. Leite
RootModule: Lib/Desktop/TfsCmdlets.dll
GUID: bd4390dc-a8ad-4bce-8d69-f53ccf8e4163
ModuleVersion: 1.0.0.0
PowerShellVersion: 5.1 (minimum)
DotNetFrameworkVersion: 4.7.1
TypesToProcess: TfsCmdlets.Types.ps1xml
FormatsToProcess: TfsCmdlets.Format.ps1xml
AliasesToExport: * (all aliases exported)
Tags: TfsCmdlets, TFS, VSTS, PowerShell, Azure, AzureDevOps, DevOps, ALM, TeamFoundationServer
```

**Module Structure** ([PS](PS)):
```
PS/
├── TfsCmdlets.psd1          # Manifest
├── TfsCmdlets.psm1          # Module script (currently just comments)
├── _Private/
│   ├── Admin.ps1            # Admin helper functions
│   ├── Aliases.ps1          # Cmdlet aliases
│   ├── Completers/          # Tab completion functions
│   └── ... other helpers
├── _Tests/                  # Integration tests (Pester)
│   ├── Admin/
│   ├── Artifact/
│   ├── WorkItem/
│   └── ... (organized by domain)
├── _Formats/                # PowerShell formatting rules
│   ├── Controls/
│   └── Views/
├── _Types/                  # PowerShell type extensions (*.ps1xml files)
│   └── (various type extension files)
├── _Themes/                 # Theming for Azure DevOps Shell
│   └── azuredevops.omp.json # OhMyPosh theme
└── Lib/                     # Compiled assemblies (generated)
    ├── Desktop/             # CLR-based (net471)
    │   └── TfsCmdlets.dll
    └── Core/                # CoreCLR-based (netcoreapp3.1)
        └── TfsCmdlets.dll
```

**Key Features**:
- **Dual-targeting**: Supports both PowerShell Desktop (5.1 with .NET Framework) and PowerShell Core (7.0+ with .NET Core)
- **Type Extensions** (ps1xml): Extends .NET types for PowerShell formatting
- **Format Views** (ps1xml): Custom formatting for cmdlet outputs
- **Tab Completion**: PowerShell completers for parameter values
- **Aliases**: Shortcuts for common cmdlets
- **Pester Tests**: Integration tests in [PS/_Tests](PS/_Tests)

**Installation Paths**:
- **Global**: `$PSHOME\Modules\TfsCmdlets`
- **User**: `$HOME\[Documents\]PowerShell\Modules\TfsCmdlets`
- **Program Files**: `C:\Program Files\WindowsPowerShell\Modules\TfsCmdlets` (MSI installer)

---

### **10. CI/CD CONFIGURATION**

**CI/CD Platform**: GitHub Actions

**Config File**: [.github/workflows/main.yml](.github/workflows/main.yml)

**Workflow: "Build"**

**Triggers**:
- Pull requests targeting `main` branch
- Concurrency: Cancels in-progress builds on same workflow/ref

**Environment Variables**:
- `Config`: Release (build configuration)
- `Debug`: false
- `SkipReleaseNotes`: true (for PR builds)
- `TFSCMDLETS_ACCESS_TOKEN`: Azure DevOps token (secret)
- `TFSCMDLETS_COLLECTION_URL`: https://dev.azure.com/tfscmdlets (Azure DevOps test org)

**Build Job (runs-on: windows-latest)**:
1. **Checkout** - v4 with full history (for versioning)
2. **CodeQL Init** - GitHub for C# security scanning
3. **Build Module** - Runs `./Build.ps1 -Targets Package`
4. **CodeQL Analyze** - Uploads security analysis
5. **Publish Test Results** - Publishes `TestResults-Pester*.xml`
6. **Upload Artifacts** (all output packages):
   - NuGet packages (`out/Nuget/*.nupkg`)
   - Chocolatey packages (`out/Chocolatey/*.nupkg`)
   - Portable ZIP (`out/Portable/*.zip`)
   - MSI installer (`out/msi/*`)
   - WinGet manifest (`out/winget/**`)
   - Documentation ZIP (`out/docs/*.zip`)
   - Release notes (`RELEASENOTES.md`)

**Staging Job (runs-on: ubuntu-latest)**:
- Depends on: Build job
- Environment: staging (requires approval)
- Downloads all artifacts
- Extracts and processes release notes
- (continues to CD steps like deployment)

**Version Management**:
- Uses **GitVersion** for semantic versioning
- Version strategy: `ContinuousDelivery` for pull requests
- **Build number**: Days since repo creation (2014-10-24)
- **Metadata**: `{yyyyMMdd}.{DaysSinceCreation}`
- **Version formats**: 
  - `ThreePartVersion`: Major.Minor.Patch
  - `FourPartVersion`: Major.Minor.Patch.Build

**Release Process**:
- Pull request → Build → Publish artifacts
- Artifacts can be manually published to package repositories (PowerShell Gallery, Chocolatey, winget, etc.)

---

### **SUMMARY TABLE: Key Metrics**

| Aspect | Details |
|--------|---------|
| **Language** | C# (main), PowerShell (module wrapper), YAML/XML (config) |
| **Target Frameworks** | net471 (Desktop), netcoreapp3.1 (Core), netstandard2.0 (Generators) |
| **Minimum PowerShell** | 5.1 |
| **Test Framework** | xUnit 2.4.1 + Pester |
| **Build Tool** | PowerShell + psake + dotnet CLI |
| **CI/CD** | GitHub Actions (main.yml) |
| **Package Distribution** | PowerShell Gallery, NuGet, Chocolatey, winget, MSI, ZIP |
| **Source Generators** | Roslyn 4.2.0 for cmdlet/controller code generation |
| **MEF Composition** | Yes (System.Composition) for services |
| **Documentation Tool** | PlatyPS (for PowerShell help) |
| **Code Analysis** | GitHub CodeQL (C#), PsScriptAnalyzer (PowerShell) |

This is a sophisticated, well-structured PowerShell module project with advanced C# features, automated code generation, comprehensive testing, and multi-platform distribution.