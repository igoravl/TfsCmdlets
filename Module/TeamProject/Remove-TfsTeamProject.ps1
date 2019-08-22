<#
.SYNOPSIS
Deletes one or more team projects. 

.DESCRIPTION

.PARAMETER Project
Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.NOTES
As with most cmdlets in the TfsCmdlets module, this cmdlet requires a TfsTeamProjectCollection object to be provided via the -Collection argument. If absent, it will default to the connection opened by Connect-TfsTeamProjectCollection.

#>
Function Remove-TfsTeamProject
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='High')]
    Param
    (
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [object] 
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Hard,

        [Parameter()]
        [switch]
        $Force
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
    }

    Process
    {
        $tps = Get-TfsTeamProject -Project $Project -Collection $Collection

        if(-not $tps)
        {
            return
        }

        foreach($tp in $tps)
        {
            $tpc = $tp.TeamProjectCollection
            GET_CLIENT('Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient')

            if($PSCmdlet.ShouldProcess($tp.Name, 'Delete team project'))
            {
                if((-not $Hard.IsPresent) -or ($Force.IsPresent -or ($PSCmdlet.ShouldContinue('The team project deletion is IRREVERSIBLE and may cause DATA LOSS. Are you sure you want to proceed?'))))
                {
                    $method = (&{if($Hard.IsPresent) {'Hard'} else {'Soft'}})

                    _Log "$method-deleting team project $($tp.Name)"

                    $token = $client.QueueDeleteProject($tp.Guid, $Hard.IsPresent).Result

                    if (-not $token)
                    {
                        throw "Error queueing team project deletion: $($client.LastResponseContext.Exception.Message)"
                    }
        
                    # Wait for the operation to complete
        
                    $operationsClient = _GetRestClient 'Microsoft.VisualStudio.Services.Operations.OperationsHttpClient' -Collection $tpc
        
                    $opsToken = $operationsClient.GetOperation($token.Id).Result
        
                    while (
                        ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded) -and
                        ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Failed) -and 
                        ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Cancelled))
                    {
                        _Log "Waiting for the queued operation to finish (current status: $($opsToken.Status))"

                        Start-Sleep -Seconds 1
                        $opsToken = $operationsClient.GetOperation($token.Id).Result
                    }
        
                    if ($opsToken.Status -ne [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded)
                    {
                        _Log "Queued operation finished with status $($opsToken.Status)"

                        throw "Error deleting team project ${Project}: $($opsToken.DetailedMessage)"
                    }
                }
            }
        }
    }
}

Function _GetAllProjects
{
    param ($tpc)

    $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

    return $css.ListAllProjects() | Where-Object Status -eq WellFormed
}