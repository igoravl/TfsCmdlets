$binDir = (Join-Path $PSScriptRoot 'lib')
$assemblyList = ''

foreach($a in Get-ChildItem $binDir)
{
     $assemblyList += "{""$($a.BaseName)"", @""$($a.FullName)""},`r`n"
}

if (-not ([System.Management.Automation.PSTypeName]'TfsCmdlets.AssemblyResolver').Type)
{
    Add-Type -ErrorAction SilentlyContinue -Language CSharp -TypeDefinition @"
${File:CSharp\AssemblyResolver.cs}
"@
}

[TfsCmdlets.AssemblyResolver]::Register()

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

# Load basic TFS client assemblies
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Common.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.WorkItemTracking.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Build.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Git.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.VersionControl.Client.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.Core.WebApi.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.SourceControl.WebApi.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.TeamFoundation.ProjectManagement.dll')
Add-Type -Path (Join-Path $BinDir 'Microsoft.VisualStudio.Services.WebApi.dll')
