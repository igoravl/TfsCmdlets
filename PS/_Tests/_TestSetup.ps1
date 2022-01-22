$scriptRoot = $PSScriptRoot
$solutionDir = Join-Path $scriptRoot '../..' -Resolve
$outDir = Join-Path $solutionDir 'out' -Resolve
$modulePath = Join-Path $outDir Module
$manifestPath = Join-Path $modulePath 'TfsCmdlets.psd1'
$hasBuild = Test-Path $modulePath

$global:tfsAccessToken = $env:TFSCMDLETS_ACCESS_TOKEN
$global:tfsCollectionUrl = $env:TFSCMDLETS_COLLECTION_URL
$global:tfsProject = 'TestProject'

if (-not $hasBuild)
{
    throw "Module TfsCmdlets not found at $modulePath. Build project prior to running tests."
}

Get-Module TfsCmdlets | Remove-Module
Import-Module $manifestPath -Force
