$InstallPath = Join-Path $($env:ChocolateyInstall) 'lib\\TfsCmdlets'
$ToolsDir = Join-Path $InstallPath 'Tools'

if ($env:PSModulePath -like "*$ToolsDir*")
{
    Write-Output "TfsCmdlets: Removing installation directory from PSModulePath environment variable"
    $NewModulePath = $Env:PSModulePath.Replace($ToolsDir, '').Replace(';;', ';')
    SETX @('PSModulePath', $NewModulePath, '/M')
}

$ShortcutTargetDir = "$Env:ProgramData\\Microsoft\\Windows\\Start Menu\\Programs"
$ShortcutName = 'Azure DevOps Shell'
$ShortcutFilePath = "$ShortcutTargetDir\\$ShortcutName.lnk"

if (Test-Path $ShortcutFilePath)
{
    Write-Output "TfsCmdlets: Removing Start Menu shortcut file"
    Remove-Item $ShortcutFilePath
}