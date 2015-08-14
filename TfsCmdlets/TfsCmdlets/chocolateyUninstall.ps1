$InstallPath = Join-Path $($env:ChocolateyInstall) 'lib\TfsCmdlets'
$ToolsDir = Join-Path $InstallPath 'Tools'

if ($env:PSModulePath -contains $ToolsDir)
{
    [Environment]::SetEnvironmentVariable("PSModulePath", ($env:PSModulePath -replace ";$ToolDir",''), 'Machine')
}
