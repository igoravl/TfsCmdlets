#=================================
# Team Project cmdlets
#=================================

Function Get-TeamProjectInformation
{
    param
    (
        [Parameter(Mandatory=$true)] [string] 
        $CollectionUrl,
    
        [Parameter(Mandatory=$true)] [string] 
        $Name,
    
        [Parameter()] [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] [ValidateNotNull()] [System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
        $Credential = [System.Management.Automation.PSCredential]::Empty
    )

    Process
    {
        if ((!$UseDefaultCredentials.IsPresent) -and ($Credential -eq [System.Management.Automation.PSCredential]::Empty)) { $Credential = Get-Credential }

        $apiUrl = _GetUrl $CollectionUrl "_apis/projects/${Name}?api-version=1.0&includeCapabilities=true"
    
        if ($UseDefaultCredentials.IsPresent)
        {
            $json = Invoke-RestMethod -Uri $apiUrl -UseDefaultCredentials -Method "GET"
        }
        else
        {
            $json = Invoke-RestMethod -Uri $apiUrl -Credential $Credential -Method "GET"
        }

        return $json
    }
}

