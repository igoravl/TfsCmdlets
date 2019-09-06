Function _ImportRequiredAssembly($assemblyName)
{
    $libPath = (Join-Path $PSScriptRoot "../lib" -Resolve)

    if($assemblyName -eq '*')
    {
        $assemblies = (Get-ChildItem "$libPath/*.dll" -Exclude 'Microsoft.WitDataStore*.*','TfsCmdletsLib.dll').BaseName
        $assemblies += (Get-ChildItem "$libPath/TfsCmdletsLib.dll").BaseName
    }
    else
    {
        $assemblies = (Get-ChildItem "$libPath/$assemblyName.dll").BaseName
    }
    
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
}

Function _TestLoadedAssembly($assemblyName)
{
    try
    {
        $asm = [System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object FullName -like "$assemblyName,*"

        return $asm -is [System.Reflection.Assembly]
    }
    catch
    {
        return $false
    }
}
