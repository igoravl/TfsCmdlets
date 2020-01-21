# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop
$global:TfsCmdletsLoadSw = [System.Diagnostics.Stopwatch]::StartNew()

Write-Host "Loading module TfsCmdlets from $PSScriptRoot"

# Configure assembly resolver

if ($TfsCmdletsDebugStartup)
{
    Write-Host "Entering TfsCmdlets startup debug mode" -ForegroundColor DarkYellow

    # For some reason, setting VerbosePreference here breaks the script. Using Set-Alias to work around it
    Set-Alias Write-Verbose Write-Host -Option Private
}

Write-Verbose 'Registering custom Assembly Resolver'

if (-not ([System.Management.Automation.PSTypeName]'TfsCmdlets.AssemblyResolver').Type)
{
    $resolverSw = [System.Diagnostics.Stopwatch]::StartNew()

    Write-Verbose "Compiling $PSEdition version of the assembly resolver"

    $sourcePath = (Join-Path $PSScriptRoot "_cs/$($PSEdition)AssemblyResolver.cs" -Resolve)
    $sourceText = (Get-Content $sourcePath -Raw)

    Add-Type -TypeDefinition $sourceText -Language CSharp

    $libPath = (Join-Path $PSScriptRoot 'Lib' -Resolve)
    $assemblies = [System.Collections.Generic.Dictionary[string,string]]::new()

    Write-Verbose "Enumerating assemblies from $libPath"

    foreach($f in (Get-ChildItem $libPath -Filter '*.dll'))
    {
        Write-Verbose "Adding $f to list of private assemblies"
        $assemblies.Add($f.BaseName, $f.FullName)
    }

    Write-Verbose 'Calling AssemblyResolver.Register()'
    [TfsCmdlets.AssemblyResolver]::Register($assemblies, [bool]($TfsCmdletsDebugStartup))

    Write-Verbose "Assembly resolver loading took $($resolverSw.ElapsedMilliseconds)ms."
}
else
{
    Write-Verbose 'Custom Assembly Resolver already registered; skipping'
}

# Pre-load assemblies in the background

$libPath = (Join-Path $PSScriptRoot "lib" -Resolve)
$assemblies = (Get-ChildItem "$libPath/*.dll" -Exclude 'Microsoft.WitDataStore*.*','TfsCmdletsLib.dll').BaseName
$assemblies += (Get-ChildItem "$libPath/TfsCmdletsLib.dll").BaseName

# $global:TfsCmdletsAssemblyLoadjob = Start-Job -ScriptBlock {
    foreach($asm in $assemblies)
    {
        Write-Verbose "Loading assembly $asm from folder $libPath"

        try
        {
            Add-Type -Path (Join-Path $libPath "$asm.dll")
        }
        catch
        {
            Write-Warning "Error loading assembly '$asm': $($_.Exception.Message)"
        }
    }
# }
