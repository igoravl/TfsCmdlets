$InstallPath = Join-Path $($env:ChocolateyInstall) 'lib\\TfsCmdlets'
$ToolsDir = Join-Path $InstallPath 'Tools'

if ($env:PSModulePath -notlike "*$ToolsDir*")
{
    Write-Output "TfsCmdlets: Adding installation directory to PSModulePath environment variable"
    SETX @('PSModulePath', "$env:PSModulePath;$ToolsDir", '/M')
}

$ShortcutTargetDir = "$Env:ProgramData\\Microsoft\\Windows\\Start Menu\\Programs"
$ShortcutName = 'Azure DevOps Shell'
$ShortcutFilePath = "$ShortcutTargetDir\\$ShortcutName.lnk"
$ShortcutExecutable = "$Env:SystemRoot\\System32\\WindowsPowerShell\\v1.0\\powershell.exe"
$ShortcutArguments = '-noexit -command "Import-Module TfsCmdlets; Enter-TfsShell"'
$ShortcutIconLocation = "$ToolsDir\\TfsCmdlets\\TfsCmdletsShell.ico"

if (-not (Test-Path $ShortcutFilePath))
{
    Write-Output "TfsCmdlets: Adding Start Menu shortcut file"
	Install-ChocolateyShortcut -ShortcutFilePath $ShortcutFilePath -TargetPath $ShortcutExecutable -Arguments $ShortcutArguments -IconLocation $ShortcutIconLocation
}