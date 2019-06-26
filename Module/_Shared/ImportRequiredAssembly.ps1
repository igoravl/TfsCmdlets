Function _ImportRequiredAssembly($assemblyName)
{
    _Log "Trying to load assembly $assemblyName"

    if (Test-LoadedAssembly $assemblyName)
    {
        _Log "Assembly $assemblyName already loaded; skipping"
        return
    }

    Add-Type -Path (Join-Path $PSScriptRoot "lib/$($assemblyName).dll")

    _Log "Loaded assembly $assemblyName"
}
