Function _ImportRequiredAssembly($assemblyName)
{
    if (_TestLoadedAssembly $assemblyName)
    {
        return
    }

    _Log "Loading assembly $assemblyName"

    Add-Type -Path (Join-Path $PSScriptRoot "../lib/$($assemblyName).dll" -Resolve)
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
