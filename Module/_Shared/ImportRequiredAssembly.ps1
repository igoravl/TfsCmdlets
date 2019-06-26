Function _ImportRequiredAssembly($assemblyName)
{
    _Log "Trying to load assembly $assemblyName"

    if (_TestLoadedAssembly $assemblyName)
    {
        _Log "Assembly $assemblyName already loaded; skipping"
        return
    }

    Add-Type -Path (Join-Path $PSScriptRoot "lib/$($assemblyName).dll")

    _Log "Loaded assembly $assemblyName"
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
