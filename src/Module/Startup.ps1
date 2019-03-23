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
