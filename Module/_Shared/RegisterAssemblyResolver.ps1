Function _RegisterAssemblyResolver
{
    if ($TfsCmdletsDebugStartup)
    {
        Write-Host "Entering TfsCmdlets startup debug mode" -ForegroundColor DarkYellow
    
        # For some reason, setting VerbosePreference here breaks the script. Using Set-Alias to work around it
        Set-Alias Write-Verbose Write-Host -Option Private
    }
    
    Write-Verbose 'Registering custom Assembly Resolver'

    if (-not [type]::GetType('TfsCmdlets.AssemblyResolver'))
    {
        Write-Verbose "Compiling $PSEdition version of the assembly resolver"

        $sourcePath = (Join-Path $PSScriptRoot "../_cs/$($PSEdition)AssemblyResolver.cs" -Resolve)
        $sourceText = (Get-Content $sourcePath -Raw)

        Add-Type -TypeDefinition $sourceText -Language CSharp

        $libPath = (Join-Path $PSScriptRoot '../Lib' -Resolve)
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
}
