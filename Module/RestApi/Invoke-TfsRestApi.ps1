<#
.SYNOPSIS
    Short description
.DESCRIPTION
    Long description
.EXAMPLE
    PS C:\> <example usage>
    Explanation of what the example does
.INPUTS
    Inputs (if any)
.OUTPUTS
    Output (if any)
.NOTES
    General notes
#>
Function Invoke-TfsRestApi
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ParameterSetName="Library call")]
        [Alias("Name")]
        [Alias("API")]
        [string]
        $Operation,

        [Parameter(ParameterSetName="Library call")]
        [Alias("Client")]
        [Alias("Type")]
        [string]
        $ClientType,

        [Parameter(ParameterSetName="Library call")]
        [object[]]
        $ArgumentList,

        [Parameter(Mandatory=$true, ParameterSetName="URL call")]
        [string]
        $Path,

        [Parameter(ParameterSetName="URL call")]
        [string]
        $Method = 'GET',

        [Parameter(ParameterSetName="URL call")]
        [Alias('Content')]
        [string]
        $Body,

        [Parameter(ParameterSetName="URL call")]
        [string]
        $RequestContentType = 'application/json',

        [Parameter(ParameterSetName="URL call")]
        [string]
        $ResponseContentType = 'application/json',

        [Parameter(ParameterSetName="URL call")]
        [hashtable]
        $AdditionalHeaders,

        [Parameter(ParameterSetName="URL call")]
        [hashtable]
        $QueryParameters,

        [Parameter(ParameterSetName="URL call")]
        [string]
        $ApiVersion = '4.1',

        [Parameter(ParameterSetName="URL call")]
        [object]
        $Team,

        [Parameter(ParameterSetName="URL call")]
        [object]
        $Project,

        [Parameter(ParameterSetName="URL call")]
        [string]
        $UseHost,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Raw,

        [Parameter()]
        [switch]
        $AsTask
    )

    End
    {
        GET_COLLECTION($tpc)

        if($PSCmdlet.ParameterSetName -eq 'Library call')
        {
            _Log "Using library call method"

            GET_CLIENT($ClientType)
            $task = $client.$Operation.Invoke($ArgumentList)
        }
        else
        {
            _Log "Using URL call method"

            $Path = $Path.TrimStart('/')
            
            if($UseHost)
            {
                if($UseHost -notlike '*.dev.azure.com')
                {
                    _Log "Converting service prefix $UseHost to $UseHost.dev.azure.com"

                    $UseHost += '.dev.azure.com'
                }

                _Log "Using service host $UseHost"
                [TfsCmdlets.GenericHttpClient]::UseHost($UseHost)
            }

            GET_CLIENT("TfsCmdlets.GenericHttpClient")

            if($Path -like '*{project}*')
            {
                GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$Team.Project)
                $Path = $Path.Replace('{project}', $tp.Guid)

                _Log "Replace token {project} in URL with '$($tp.Guid)'"
            }

            if($Path -like '*{team}*')
            {
                GET_TEAM($t,$tp,$tpc)
                $Path = $Path.Replace('{team}', $t.Id)

                _Log "Replace token {team} in URL with '$($t.Id)'"
            }

            _Log "Calling API '$Path', version '$ApiVersion', via $Method"
            
            $task = $client.InvokeAsync($Method, $Path, $Body, $RequestContentType, $ResponseContentType, $AdditionalHeaders, $QueryParameters, $ApiVersion)

            _Log "URI called: $($client.Uri)"
        }

        if ($AsTask)
        {
            return $task
        }

        CHECK_ASYNC($task,$result,$Message)

        if($PSCmdlet.ParameterSetName -eq 'URL call')
        {
            $json = $result.Content.ReadAsStringAsync().GetAwaiter().GetResult()
            $obj = ($json | ConvertFrom-Json)

            if($Raw.IsPresent)
            {
                Add-Member -InputObject $result -Name 'ResponseString' -MemberType NoteProperty -Value $json

                if($ResponseContentType -eq 'application/json')
                {
                    Add-Member -InputObject $result -Name 'ResponseObject' -MemberType NoteProperty -Value $obj
                }
            }
            else
            {
                $result = $obj
            }
        }
        
        return $result
    }
}