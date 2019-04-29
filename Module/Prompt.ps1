Function Prompt
{
    Process
    {
        $tfsPrompt = ''
        $promptPrefix = ''

        if ($script:TfsServerConnection)
        {
            $tfsPrompt = $script:TfsServerConnection.Name

            if (($tfsPrompt -like '*.visualstudio.com') -or ($tfsPrompt -like 'dev.azure.com/*'))
            {
                $promptPrefix = 'AzDev'
                $tfsPrompt = $tfsPrompt.SubString(0, $tfsPrompt.IndexOf('.'))
            }
            else
            {
                $promptPrefix = 'TFS'

                if ($script:TfsTpcConnection)
                {
                    $tfsPrompt += "/$($script:TfsTpcConnection.Name)"
                }

                if ($script:TfsProjectConnection)
                {
                    $tfsPrompt += "/$($script:TfsProjectConnection.Name)"
                }

                if ($script:TfsTeamConnection)
                {
                    $tfsPrompt += "/$($script:TfsTeamConnection.Name)"
                }
            }

            $tfsPrompt = "[$tfsPrompt] "
        }

        "$promptPrefix $($tfsPrompt)$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
    }
}
