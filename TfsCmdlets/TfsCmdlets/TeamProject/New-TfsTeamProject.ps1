<#
.SYNOPSIS
    Creates a new team project. 

#>
Function New-TfsTeamProject
{
    [CmdletBinding(DefaultParameterSetName='Get by project')]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
    Param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [string] 
        $Project,

        [Parameter(ValueFromPipeline=$true, Position=1)]
        [object]
        $Collection,

        [string]
        $Description,

        [string]
        [ValidateSet('Git, TFVC')]
        $SourceControl,

        [object]
        $ProcessTemplate
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $template = Get-TfsProcessTemplate -Collection $tpc -Name $ProcessTemplate
        $client = Get-TfsHttpClient 'Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient' -Collection $tpc

        $tpInfo = New-Object 'Microsoft.TeamFoundation.Core.WebApi.TeamProject'
        $tpInfo.Name = $Project
        $tpInfo.Description = $Description
        $tpInfo.Capabilities = New-Object 'System.Collections.Generic.Dictionary[[string],System.Collections.Generic.Dictionary[[string],[string]]]'

        $tpInfo.Capabilities.Add('versioncontrol', (New-Object 'System.Collections.Generic.Dictionary[[string],[string]]'))
        $tpInfo.Capabilities['versioncontrol'].Add('sourceControlType', $SourceControl)

        $tpInfo.Capabilities.Add('processTemplate', (New-Object 'System.Collections.Generic.Dictionary[[string],[string]]'))
        $tpInfo.Capabilities['processTemplate'].Add('templateTypeId', ([xml]$template.Metadata).metadata.version.type)


        # Trigger the project creation

        $token = $client.QueueCreateProject($tpInfo).Result

        if (-not $token)
        {
            throw "Error queueing team project creation: $($client.LastResponseContext.Exception.Message)"
        }

        # Wait for the operation to complete

        $operationsClient = Get-TfsHttpClient 'Microsoft.VisualStudio.Services.Operations.OperationsHttpClient' -Collection $tpc

        $opsToken = $operationsClient.GetOperation($token.Id).Result

        while (
            ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded) -and
            ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Failed) -and 
            ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Cancelled))
        {
            Start-Sleep -Seconds 2
            $opsToken = $operationsClient.GetOperation($token.Id).Result
        }

        if ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded)
        {
            throw "Error creating team project $Project"
        }

        return Get-TfsTeamProject -Project $Project -Collection $Collection
    }
}