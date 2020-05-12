Function Get-TfsRestClient
{
    [CmdletBinding(DefaultParameterSetName="Get by collection")]
    [OutputType('Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [string]
        $Type,

        [Parameter(ParameterSetName="Get by collection", Mandatory=$true)]
        [object] 
        $Collection,

        [Parameter(ParameterSetName="Get by server", Mandatory=$true)]
        [object] 
        $Server
    )

    Process
    {
        if($PSCmdlet.ParameterSetName -eq 'Get by collection')
        {
            $provider =  (Get-TfsTeamProjectCollection -Collection $Collection)
        }
        else
        {
            $provider =  (Get-TfsConfigurationServer -Server $Server)
        }

        return _InvokeGenericMethod -InputObject $provider -MethodName 'GetClient' -GenericType $Type
    }
}
