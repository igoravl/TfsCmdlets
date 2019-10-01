Function Get-TfsRestClient
{
    [CmdletBinding(DefaultParameterSetName="Get by collection")]
    [OutputType('Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [string]
        $Type,

        [Parameter(ParameterSetName="Get by collection")]
        [object] 
        $Collection,

        [Parameter(ParameterSetName="Get by server", Mandatory=$true)]
        [object] 
        $Server
    )

    Process
    {
        if($Collection)
        {
            return _GetRestClient -Type $Type -Provider (Get-TfsTeamProjectCollection -Collection $Collection)
        }

        return _GetRestClient -Type $Type -Provider (Get-TfsConfigurationServer -Server $Server)
    }
}
