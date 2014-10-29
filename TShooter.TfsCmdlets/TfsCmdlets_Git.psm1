#=============================
# Git cmdlets
#=============================

Function New-GitRepository
{
    param
    (
        [Parameter(ParameterSetName="Not connected",Mandatory=$true)]
        [string] 
        $CollectionUrl = $null,
    
        [Parameter(ParameterSetName="Not connected",Mandatory=$true)]
        [Parameter(ParameterSetName="Connected",Mandatory=$true)]
        [string] 
        $ProjectName,
    
        [Parameter(ParameterSetName="Not connected",Mandatory=$true, ValueFromPipeline=$true)]
        [Parameter(ParameterSetName="Connected",Mandatory=$true, ValueFromPipeline=$true)]
        [string] 
        $Name,
    
        [Parameter(ParameterSetName="Not connected")]
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Not connected")]
        [System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
        $Credential = [System.Management.Automation.PSCredential]::Empty
    )

    Process
    {
        $connection = _GetRestConnection  -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential -ParameterSetName $PSCmdlet.ParameterSetName
        $project = Get-TeamProjectInformation -CollectionUrl $connection.Url -Name $ProjectName -UseDefaultCredentials:$connection.UseDefaultCredentials -Credential $connection.Credential

        $id = $project.id
        $apiUrl = _GetUrl $CollectionUrl "_apis/git/repositories?api-version=1.0"
        $body = "{`"name`": `"${Name}`", `"project`": { `"id`": `"${id}`" } }"
    
        if ($connection.UseDefaultCredentials)
        {
            Invoke-RestMethod -Uri $apiUrl -UseDefaultCredentials -Method "POST" -Body $body -ContentType "application/json"
        }
        else
        {
            Invoke-RestMethod -Uri $apiUrl -Credential $Credential -Method "POST" -Body $body -ContentType "application/json"
        }
    }
}

