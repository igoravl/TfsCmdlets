$global:TfsPrivateAssemblies = @{}

# InitializePrivateAssemblyPath

$binDir = (Join-Path $PSScriptRoot 'lib')
$assemblies = Get-ChildItem $binDir

foreach($a in $assemblies)
{
    $global:TfsPrivateAssemblies.Add($a.BaseName, $a.FullName)
}

# InitializeAssemblyResolver

$OnAssemblyResolve = [System.ResolveEventHandler] {
    param($sender, $e)
    try {      
        if ($global:TfsPrivateAssemblies.ContainsKey($e.Name))
        {
            return [System.Reflection.Assembly]::LoadFrom($global:TfsPrivateAssemblies[$e.Name])
        }
    }
    catch {}

    return $null
}

[System.AppDomain]::CurrentDomain.add_AssemblyResolve($OnAssemblyResolve)

# InitializeShell

if ($Host.UI.RawUI.WindowTitle -eq "Team Foundation Server Shell")
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

    @'
Function Prompt
{
    Process
    {
        if (Test-Path variable:global:TfsTpcConnection)
        {
            $tfsConnectionText = "[TFS@$($Global:TfsTpcConnection.Uri.Host)/$($Global:TfsTpcConnection.Uri.Segments[$Global:TfsTpcConnection.Uri.Segments.Length-1])]`r`n"
        }
        "${tfsConnectionText}$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
    }
}
'@ | iex

}

Add-Type -AssemblyName 'Microsoft.TeamFoundation.Common'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.WorkItemTracking.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Build.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Git.Client'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.VersionControl.Client'
