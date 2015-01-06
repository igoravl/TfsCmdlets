#=============================
# Git cmdlets
#=============================

Function New-GitRepository
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [string] 
        $Name
    )

	Begin
	{
        $connection = _GetRestConnection
	}

    Process
    {
        $project = Get-TeamProjectInformation -Name $ProjectName
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

