# Initialize Shell

if ($Host.UI.RawUI.WindowTitle -match "(Azure DevOps Shell*)|(Team Foundation Server Shell*)")
{
    # SetConsoleColors
    $Host.UI.RawUI.BackgroundColor = "DarkMagenta"
    $Host.UI.RawUI.ForegroundColor = "White"
    Clear-Host

    # ShowBanner
    $module = Test-ModuleManifest -Path (Join-Path $PSScriptRoot 'TfsCmdlets.psd1')
    Write-Host "TfsCmdlets: $($module.Description)"
    Write-Host "Version $($module.PrivateData.Build)"
    Write-Host ""

    . (Join-Path $PSScriptRoot 'Prompt.ps1')
}
