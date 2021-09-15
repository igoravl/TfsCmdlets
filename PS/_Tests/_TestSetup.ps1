$scriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent
$solutionDir = Join-Path $scriptRoot '..' -Resolve
$outDir = Join-Path $solutionDir 'out' -Resolve
$projectDir = Join-Path $solutionDir 'Module' 
$modulePath = Join-Path $outDir Module
$hasBuild = Test-Path $modulePath

if (-not $hasBuild)
{
    throw "Module TfsCmdlets not found at $modulePath. Build project prior to running tests."
}

$mod = (Get-Module TfsCmdlets)
$loadMod = (-not $mod)

if($mod -and ($mod.Path -ne (Join-Path $modulePath 'TfsCmdlets.psm1')))
{
    Write-Host "TestSetup: Removing module" -ForegroundColor Cyan
    Get-Module TfsCmdlets | Remove-Module
    $loadMod = $true
}

if($loadMod)
{
    Write-Host "- TestSetup: Importing module" -ForegroundColor Cyan
    Import-Module (Join-Path $modulePath 'TfsCmdlets.psd1') -Force
}