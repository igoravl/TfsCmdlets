Function Prompt
{
    Process
    {
        $tfsPrompt = ''
        $promptPrefix = 'TFS'

        if ($global:TfsServerConnection)
        {
            $tfsPrompt = $global:TfsServerConnection.Name

            if ($tfsPrompt -like '*.visualstudio.com')
            {
                $promptPrefix = 'VSTS'
                $tfsPrompt = $tfsPrompt.SubString(0, $tfsPrompt.IndexOf('.'))
            }
            else
            {
                if ($global:TfsTpcConnection)
                {
                    $tfsPrompt += "/$($global:TfsTpcConnection.Name)"
                }

                if ($global:TfsProjectConnection)
                {
                    $tfsPrompt += "/$($global:TfsProjectConnection.Name)"
                }

                if ($global:TfsTeamConnection)
                {
                    $tfsPrompt += "/$($global:TfsTeamConnection.Name)"
                }
            }

            $tfsPrompt = "[$tfsPrompt] "
        }

        "$promptPrefix $($tfsPrompt)$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
    }
}
