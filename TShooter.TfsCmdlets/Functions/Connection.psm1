#===========================
# Connection cmdlets
#===========================

Function Get-TfsConfigurationServer
{
    param
    (
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
        [Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
        [string]
        $ServerUrl,
    
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential,
    
        [Parameter(ParameterSetName="Current Connection", Position=0, Mandatory=$true)] 
        [switch] 
        $Current
    )

    Process
    {
        switch($PSCmdlet.ParameterSetName)
        {
            "Windows Integrated Credential"
            {
                $cred = [System.Net.CredentialCache]::DefaultNetworkCredentials
            }
            "Custom Credential"
            {
                $cred = $Credential.GetNetworkCredential()
            }
            "Current Connection"
            {
                if ($Global:TfsServerConnection -and $Global:TfsServerConnection -ne $null)
                {
                    return $Global:TfsServerConnection
                }
                throw "No TFS connection information available. Use Connect-TfsConfigurationServer prior to invoking this cmdlet."
            }
        }

        $configServer = New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer ([Uri] $ServerUrl), $cred
        [void] $configServer.EnsureAuthenticated()

        return $configServer
    }
}

Function Connect-TfsConfigurationServer
{
    param
    (
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
        [Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
        [string]
        $ServerUrl,
    
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential
    )

    Process
    {
        $Global:TfsServerConnection = Get-TfsConfigurationServer @PSBoundParameters
        $Global:TfsServerConnectionUrl = $ServerUrl
        $Global:TfsServerConnectionCredential = $cred
        $Global:TfsServerConnectionUseDefaultCredentials = $UseDefaultCredentials.IsPresent
    }
}

Function Disconnect-TfsConfigurationServer
{
    Process
    {
        Remove-Variable -Name TfsServerConnection -Scope Global
        Remove-Variable -Name TfsServerConnectionUrl -Scope Global
        Remove-Variable -Name TfsServerConnectionCredential -Scope Global
        Remove-Variable -Name TfsServerConnectionUseDefaultCredentials -Scope Global
    }
}

Function Get-TfsTeamProjectCollection
{
    param
    (
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
        [Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
        [string]
        $CollectionUrl,
    
        [Parameter(ParameterSetName="Current TfsConfigurationServer Connection", Position=0, Mandatory=$true)]  
        [string]
        $CollectionName,

        [Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter(ParameterSetName="Current Connection", Position=0, Mandatory=$true)] 
        [switch] 
        $Current
    )

    Process
    {
        switch($PSCmdlet.ParameterSetName)
        {
            "Windows Integrated Credential"
            {
                $cred = [System.Net.CredentialCache]::DefaultNetworkCredentials
            }
            "Custom Credential"
            {
                $cred = $Credential.GetNetworkCredential()
            }
            "Current TfsConfigurationServer Connection"
            {
                if ($Global:TfsServerConnection -and $Global:TfsServerConnection -ne $null)
                {
                    return _GetCollectionByName $CollectionName
                }
                throw "No TFS connection information available. Use Connect-TfsConfiguration prior to invoking this cmdlet."
            }
            "Current Connection"
            {
                if ($Global:TfsTpcConnection -and $Global:TfsTpcConnection -ne $null)
                {
                    return $Global:TfsTpcConnection
                }
                throw "No TFS connection information available. Use Connect-TfsTeamProjectCollection prior to invoking this cmdlet."
            }
        }

        $tpc = New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection ([Uri] $CollectionUrl), $cred
        [void] $tpc.EnsureAuthenticated()

        return $tpc
    }
}

Function Connect-TfsTeamProjectCollection
{
    param
    (
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
        [Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
        [string]
        $CollectionUrl,
    
        [Parameter(ParameterSetName="Current TfsConfigurationServer Connection", Position=0, Mandatory=$true)]  
        [string]
        $CollectionName,

        [Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential
    )

    Process
    {
        $Global:TfsTpcConnection = Get-TfsTeamProjectCollection @PSBoundParameters
        $Global:TfsTpcConnectionUrl = $CollectionUrl
        $Global:TfsTpcConnectionCredential = $cred
        $Global:TfsTpcConnectionUseDefaultCredentials = $UseDefaultCredentials.IsPresent
    }
}

Function Disconnect-TfsTeamProjectCollection
{
    Process
    {
        Remove-Variable -Name TfsTpcConnection -Scope Global
        Remove-Variable -Name TfsTpcConnectionUrl -Scope Global
        Remove-Variable -Name TfsTpcConnectionCredential -Scope Global
        Remove-Variable -Name TfsTpcConnectionUseDefaultCredentials -Scope Global
    }
}

# =================
# Helper Functions
# =================

Function _GetCollectionByName
{
    Param
    (
        $CollectionName
    )
    Process
    {
        $configServer = Get-TfsConfigurationServer -Current
        $filter = New-Object 'System.Collections.Generic.List[System.Guid]'
        [void] $filter.Add([Microsoft.TeamFoundation.Framework.Common.CatalogResourceTypes]::ProjectCollection)
        
        $collections = $configServer.CatalogNode.QueryChildren($filter, $false, [Microsoft.TeamFoundation.Framework.Common.CatalogQueryOptions]::IncludeParents) 
        $collection = $collections | Select -ExpandProperty Resource | ? {$_.DisplayName -eq $CollectionName}

        if (-not $collection)
        {
            throw "Invalid or non-existent Team Project Collection: $CollectionName"
        }

        $collectionId = $collection.Properties["InstanceId"]

        return $configServer.GetTeamProjectCollection($collectionId)
    }
}

