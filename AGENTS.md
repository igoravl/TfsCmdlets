# AGENTS.md

## Project overview

TfsCmdlets is a PowerShell module providing cmdlets for automating Azure DevOps (Services and Server) and legacy Team Foundation Server (TFS 2010–2018). The core logic is written in C# with Roslyn source generators that produce cmdlet and controller boilerplate. The compiled DLL is loaded as a binary PowerShell module.

## Build commands

```powershell
# Full build (compile + tests + packaging)
./Build.ps1

# Debug build, skip tests
./Build.ps1 -Configuration Debug -Targets Build -SkipTests

# Run tests only
./Build.ps1 -Targets Test

# Rebuild from scratch
./Build.ps1 -Targets Rebuild

# Incremental build (don't clean output)
./Build.ps1 -Incremental
```

The build system uses **psake** (defined in `psake.ps1`) orchestrated by `Build.ps1`. It compiles the C# solution with `dotnet publish` for two target frameworks (`net471` and `netcoreapp3.1`), assembles the PowerShell module under `out/module/`, and produces NuGet, Chocolatey, MSI, winget, and ZIP packages.

## Testing instructions

- **Unit tests** (xUnit): `dotnet test CSharp/TfsCmdlets.Tests.UnitTests/TfsCmdlets.Tests.UnitTests.csproj`
- **Integration tests** (Pester): Located in `PS/_Tests/`, organized by domain. Run via `./Build.ps1 -Targets Test`.
- Always run unit tests before submitting changes. Integration tests require a live Azure DevOps organization.

## Code style

- C# language version: **11.0 (preview)** for the main project, **8.0** for `TfsCmdlets.Legacy`.
- Target frameworks: `net471` (Desktop / .NET Framework) and `netcoreapp3.1` (Core / cross-platform). Source generators target `netstandard2.0`.
- `.editorconfig` is present — respect its rules.
- Global usings are declared in `CSharp/TfsCmdlets/GlobalUsings.cs`. Use type aliases defined there (e.g., `WebApiWorkItem`, `WebApiTeamProject`) instead of fully-qualified names.
- PowerShell minimum version: **5.1**.

## Architecture and patterns

### Cmdlets

- Location: `CSharp/TfsCmdlets/Cmdlets/{Domain}/`
- Cmdlets are **partial classes** decorated with `[TfsCmdlet(scope, OutputType=...)]`.
- Naming: class `GetWorkItem` → cmdlet `Get-TfsWorkItem`. Verb must be a PowerShell Approved Verb.
- Source generators auto-complete the class with standard parameters (Credential, Collection, Project, Team, etc.) based on the `CmdletScope`.
- Do **not** manually write boilerplate that the generators handle.

### ShouldProcess / ShouldContinue

All mutation cmdlets (`New`, `Set`, `Remove`, `Enable`, `Disable`, `Update`, `Rename`, `Import`, `Move`, etc.) **must** set `SupportsShouldProcess = true` in `[TfsCmdlet]` to enable `-WhatIf` and `-Confirm`. Destructive/irreversible cmdlets (e.g. `Remove-*`) must additionally implement a `-Force` gated `ShouldContinue` barrier to prevent accidental execution when `$ConfirmPreference` is lowered or `-Confirm:$false` is passed.

See [Docs/ShouldProcess.md](Docs/ShouldProcess.md) for the full pattern, behavior matrix, and examples.

### Controllers

- Location: `CSharp/TfsCmdlets/Cmdlets/{Domain}/` — controllers are **partial classes** in the **same file** as their corresponding cmdlet class, named `{Verb}{Noun}Controller`.
- Legacy controllers for older features live in `CSharp/TfsCmdlets.Legacy/Controllers/{Domain}/`.
- Controllers implement business logic. Class name: `{Verb}{Noun}Controller`, inherits `ControllerBase`.
- Decorated with `[CmdletControllerAttribute(typeof(DataType))]`.
- Key methods: `CacheParameters()` and `Run()`.
- Services are injected via constructor (IPowerShellService, IDataManager, IParameterManager, ILogger).

#### Generated parameter properties

The source generator automatically creates a pair of properties in the controller for **each parameter declared in the corresponding cmdlet class**:

- **`{ParamName}`** — retrieves the parameter value (already cached via the auto-generated `CacheParameters()` method). Use this instead of calling `Parameters.Get<T>()`.
- **`Has_{ParamName}`** — indicates whether the caller explicitly supplied the parameter. Use this instead of calling `Parameters.HasParameter()`.

Standard scope properties (`Collection`, `Project`, `Team`) and `ParameterSetName` are also generated automatically.

For `Get` cmdlets, the **first declared parameter** (e.g. `PersonalAccessToken`, `Repository`) is generated as an `IEnumerable` property that normalizes scalar and collection inputs; iterate over it with `foreach`.

Example (see `NewGitRepository.cs`):

```csharp
// In the cmdlet class:
[Parameter(Mandatory = true)]
public string Repository { get; set; }

[Parameter()]
public object ForkFrom { get; set; }

// In the controller's Run() method — use generated properties directly:
if (Has_ForkFrom)
{
    switch (ForkFrom) { /* ... */ }
}
yield return Client.CreateRepositoryAsync(repoToCreate, Project.Name);
```

Do **not** call `Parameters.Get<T>()` or `Parameters.HasParameter()` directly — always use the generated properties.

### Services

- Location: `CSharp/TfsCmdlets/Services/` (interfaces) and `Services/Impl/` (implementations).
- Use MEF composition: `[Export(typeof(IServiceName))]` on implementations, `[Import]` for consumers.
- Interface naming: `I{ServiceName}` → implementation: `{ServiceName}Impl`.

### Extension methods

- Prefer existing extension methods from `CSharp/TfsCmdlets/Extensions/` over duplicating logic inline.
- Before introducing utility logic (string matching, object copy, JSON conversion, pipeline helpers), check `Extensions/` and reuse the corresponding method when available.

### Azure DevOps backend interaction

- Always prefer the **Azure DevOps .NET client libraries** (`Microsoft.TeamFoundationServer.Client`, `Microsoft.VisualStudio.Services.*`) over raw REST calls. Several NuGet packages for these libraries are already referenced in the project.
- Documentation: <https://learn.microsoft.com/en-us/azure/devops/integrate/concepts/dotnet-client-libraries?view=azure-devops>
- HTTP clients are injected into controllers via source generators and MEF composition — do not instantiate them manually.
- Custom HTTP client abstractions live in `CSharp/TfsCmdlets/HttpClients/`.

### Source generators

- Project: `CSharp/TfsCmdlets.SourceGenerators/`
- Generators for: Cmdlets, Controllers, HttpClients, Models.
- In Debug config, generated files are emitted to `obj/Generated/` for inspection.
- When modifying generator logic, run the full build to verify generated output.

## Project structure

```plain
CSharp/
  TfsCmdlets/             # Main library (net471 + netcoreapp3.1)
    Cmdlets/               # Cmdlet classes (and their inline controllers) by domain
    Services/              # Service interfaces and implementations
    Models/                # Data models
    Extensions/            # Extension methods
    HttpClients/           # HTTP client abstractions
    Util/                  # Utilities
  TfsCmdlets.Legacy/       # Legacy .NET Framework code (net471 only)
    Cmdlets/               # Legacy cmdlet classes
    Controllers/           # Legacy controller implementations
  TfsCmdlets.SourceGenerators/  # Roslyn source generators (netstandard2.0)
  TfsCmdlets.Tests.UnitTests/   # xUnit unit tests
PS/
  TfsCmdlets.psd1          # Module manifest
  TfsCmdlets.psm1          # Module script
  _Private/                # Private helper functions
  _Tests/                  # Pester integration tests
  _Formats/                # PS1XML formatting rules
  _Types/                  # PS1XML type extensions
Setup/                     # MSI (WiX) and winget packaging
Docs/                      # Documentation assets
out/                       # Build output (generated, not committed)
```

## Adding a new cmdlet

1. Create a partial class in `CSharp/TfsCmdlets/Cmdlets/{Domain}/` with the `[TfsCmdlet]` attribute.
2. In the **same file**, add a matching controller partial class (`{Verb}{Noun}Controller`) decorated with `[CmdletController]` and inheriting `ControllerBase`.
3. The source generator will produce parameter sets and wiring automatically.
4. For query-style cmdlets (`Get`, `Find`, `Search`, `List`, or similar), evaluate whether output needs a dedicated table/list display and, when applicable, add a format view in `PS/_Formats/Views/` (for example `{OutputType}.View.yml`) with only the most relevant columns.
5. Add Pester tests in `PS/_Tests/{Domain}/`.

## CI/CD

- **Platform**: GitHub Actions (`.github/workflows/main.yml`)
- **Triggers**: Pull requests targeting `main`
- **Steps**: Checkout → CodeQL Init → Build (`./Build.ps1 -Targets Package`) → CodeQL Analyze → Publish test results → Upload artifacts
- **Versioning**: GitVersion with `ContinuousDelivery` strategy
- **Security scanning**: GitHub CodeQL for C#, PsScriptAnalyzer for PowerShell

## Security considerations

- Never commit secrets or access tokens. The CI uses `TFSCMDLETS_ACCESS_TOKEN` from GitHub Secrets.
- Credential parameters are handled via `PSCredential` objects — never log or expose credential values.
- Azure Identity and MSAL libraries handle authentication flows; do not implement custom token handling.

## PR guidelines

- Run `./Build.ps1 -Configuration Debug -Targets Build` locally before pushing.
- Ensure all unit tests pass.
- Keep cmdlet naming consistent with PowerShell Approved Verbs.
- Update `RELEASENOTES.md` when adding user-facing changes.
