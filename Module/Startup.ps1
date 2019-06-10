# Initialize Shell

if ($TfsCmdletsDebugStartup)
{
    # For some reason, setting VerbosePreference here breaks the script. Using Set-Alias to work around it
    
    Write-Host "Entering TfsCmdlets startup debug mode" -ForegroundColor DarkYellow
    Set-Alias Write-Verbose Write-Host -Option Private
}

if ($Host.UI.RawUI.WindowTitle -match "(Team Foundation Server Shell*)|(Azure DevOps Shell*)")
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

    Write-Verbose "Enumerating assemblies from $libPath"

    foreach($f in (Get-ChildItem $libPath -Filter '*.dll'))
    {
        Write-Verbose "Adding $f to list of private assemblies"
        $assemblies.Add($f.BaseName, $f.FullName)
    }

    Write-Verbose 'Calling AssemblyResolver.Register()'
    [TfsCmdlets.AssemblyResolver]::Register($assemblies, [bool]($TfsCmdletsDebugStartup))
}
else
{
    Write-Verbose 'Custom Assembly Resolver already registered; skipping'
}