Function Enter-TfsShell
{
    [CmdletBinding()]
    Param
    (
    )

    # Persist current values for later restoring

    $script:PrevShellTitle = $Host.UI.RawUI.WindowTitle

    if(Test-Path function:\prompt)
    {
        $script:PrevPrompt = Get-Content function:\prompt
    }

    # Replace title and prompt

    $Host.UI.RawUI.WindowTitle = 'Azure DevOps Shell'
    Set-Content function:\prompt (Get-Content function:\_TfsCmdletsPrompt)

    # Show banner

    Clear-Host
    $module = Test-ModuleManifest -Path (Join-Path $PSScriptRoot '../TfsCmdlets.psd1')
    Write-Host "TfsCmdlets: $($module.Description)"
    Write-Host "Version $($module.PrivateData.Build)"
    Write-Host "Azure DevOps Client Library v$($module.PrivateData.TfsClientVersion)"
    Write-Host ""
}