$InstallPath = Join-Path $($env:ChocolateyInstall) 'lib\TfsCmdlets'
$ToolsDir = Join-Path $InstallPath 'Tools'

if (-not ($env:PSModulePath -contains $ToolsDir))
{
    [Environment]::SetEnvironmentVariable("PSModulePath", "$env:PSModulePath;$ToolsDir", 'Machine')
}
