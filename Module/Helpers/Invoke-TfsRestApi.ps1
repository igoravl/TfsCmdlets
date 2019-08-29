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

        [Parameter(ParameterSetName="Library call")]
        [string]
        $ErrorMessage,

        [Parameter(ParameterSetName="Library call")]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $AsTask
    )

    End
    {
        GET_COLLECTION($tpc)

        GET_CLIENT($ClientType)

        $task = $client.$Operation.Invoke($ArgumentList)

        if ($AsTask)
        {
            return $task
        }

        CHECK_ASYNC($task,$result,$Message)

        return $result
    }
}