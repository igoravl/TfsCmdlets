Function _Log($Message)
{
    if(-not $TfsCmdletsDebugStartup)
    {
        return
    }

    Write-Host "VERBOSE: $Message" -ForegroundColor Yellow
}

foreach($s in (Get-PSCallStack))
{
    if($s.Position -match 'Import-Module.*(TfsCmdlets)+.*(-Verbose)+')
    {
        $TfsCmdletsDebugStartup = $true
        break
    }
}

if ($TfsCmdletsDebugStartup)
{
    _Log "Entering TfsCmdlets startup debug mode" -ForegroundColor DarkYellow
}

_Log "Loading module TfsCmdlets v$($PrivateData.Version) from $PSScriptRoot"

# Initialize variables

$global:TfsCmdletsLoadSw = [System.Diagnostics.Stopwatch]::StartNew()
$PrivateData = (Test-ModuleManifest (Join-Path $PSScriptRoot 'TfsCmdlets.psd1')).PrivateData

# Configure assembly resolver

$targetFramework = $PrivateData."${PSEdition}TargetFramework"
$libPath = (Join-Path $PSScriptRoot "Lib/${targetFramework}" -Resolve)
Add-Type -Path (Join-Path $libPath "TfsCmdletsLib.dll")

_Log "Registering assemblies in $libPath"

foreach($f in (Get-ChildItem (Join-Path $libPath '*.dll') -Exclude '*WITDataStore*'))
{
    try
    {
        _Log "- Found $($f.Name)"
        [TfsCmdlets.AssemblyResolver]::AddAssembly($f.BaseName, $f.FullName)
    }
    catch
    {
        _Log " - Error loading $($f.Name): $($_.Exception.Message)"
    }
}