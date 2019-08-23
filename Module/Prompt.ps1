Function Prompt
{
    Process
    {
        $promptPrefix = 'AzDev'
        $tfsPrompt = ''

        if ($script:TfsServerConnection)
        {
            $tfsPrompt = $script:TfsServerConnection.Name

            if ($tfsPrompt -like '*.visualstudio.com')
            {
                $promptPrefix = 'AzDev Services'
                $tfsPrompt = $tfsPrompt.SubString(0, $tfsPrompt.IndexOf('.'))
            }
            elseif ($tfsPrompt -like 'dev.azure.com/*')
            {
                $promptPrefix = 'AzDev Services'
                $tfsPrompt = $tfsPrompt.SubString($tfsPrompt.IndexOf('/'))
            }
            else
            {
                $promptPrefix = 'AzDev Server'

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
