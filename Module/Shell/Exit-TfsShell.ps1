Function Exit-TfsShell
{
    [CmdletBinding()]
    Param
    (
    )

    if($script:PrevShellTitle)
    {
        $Host.UI.RawUI.WindowTitle = $script:PrevShellTitle
    }

    if($script:PrevPrompt)
    {
        Set-Content function:\prompt $script:PrevPrompt
    }

    Clear-Host
}