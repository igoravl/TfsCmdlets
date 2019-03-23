# Initialize Shell

if ($Host.UI.RawUI.WindowTitle -like "Team Foundation Server Shell*")
{
    # SetConsoleColors
    $Host.UI.RawUI.BackgroundColor = "DarkMagenta"
    $Host.UI.RawUI.ForegroundColor = "White"
    Clear-Host

    # ShowBanner
    $module = Test-ModuleManifest -Path (Join-Path $PSScriptRoot 'TfsCmdlets.psd1')
    Write-Host "TfsCmdlets: $($module.Description)"
    Write-Host "Version $($module.PrivateData.Build)"
    Write-Host ""

    . (Join-Path $PSScriptRoot 'Prompt.ps1')
}

# Configure assembly resolver

Write-Verbose 'Registering custom Assembly Resolver'

if (-not [type]::GetType('TfsCmdlets.AssemblyResolver'))
{
    Write-Verbose "Compiling $PSEdition version of the assembly resolver"

    $sourcePath = (Join-Path $PSScriptRoot "_cs/$($PSEdition)AssemblyResolver.cs")
    $sourceText = (Get-Content $sourcePath -Raw)

    Add-Type -TypeDefinition $sourceText -Language CSharp

    $libPath = (Join-Path $PSScriptRoot 'Lib')
    $assemblies = [System.Collections.Generic.Dictionary[string,string]]::new()

    Write-Verbose "Enumeration assemblies from $libPath"

    foreach($f in (Get-ChildItem $libPath -Filter '*.dll'))
    {
        Write-Verbose "Adding $f to list of private assemblies"
        $assemblies.Add($f.BaseName, $f.FullPath)
    }

    Write-Verbose 'Calling AssemblyResolver.Register()'
    [TfsCmdlets.AssemblyResolver]::Register($assemblies, ($VerbosePreference -eq 'Continue'))
}

# Load basic TFS client assemblies
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Common.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Client.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.WorkItemTracking.Client.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Build.Client.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Git.Client.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.VersionControl.Client.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Core.WebApi.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.SourceControl.WebApi.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.ProjectManagement.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.VisualStudio.Services.WebApi.dll')
# Add-Type -Path (Join-Path $BinDir 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.dll')
