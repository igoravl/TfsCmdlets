# Initialize Shell

if (-not ($Host.UI.RawUI.WindowTitle -match "(Azure DevOps Shell*)|(Team Foundation Server Shell*)"))
{
    return
}

Clear-Host

# Show banner

$module = Test-ModuleManifest -Path (Join-Path $PSScriptRoot 'TfsCmdlets.psd1')
Write-Host "TfsCmdlets: $($module.Description)"
Write-Host "Version $($module.PrivateData.Build)"
Write-Host ""

# Laad prompt

. (Join-Path $PSScriptRoot 'Prompt.ps1')
