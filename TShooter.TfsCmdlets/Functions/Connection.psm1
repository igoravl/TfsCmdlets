#===========================
# Connection cmdlets
#===========================

Function Get-TfsTeamProjectCollection
{
    param
    (
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
        [Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
        [string]
        $CollectionUrl,
    
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
                if ($Global:TfsConnection -and $Global:TfsConnection -ne $null)
                {
                    return $Global:TfsConnection
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
    
        [Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential
    )

    Process
    {
        $Global:TfsConnection = Get-TfsTeamProjectCollection @PSBoundParameters
        $Global:TfsConnectionUrl = $CollectionUrl
        $Global:TfsConnectionCredential = $cred
        $Global:TfsConnectionUseDefaultCredentials = $UseDefaultCredentials.IsPresent
    }
}

Function Disconnect-TfsTeamProjectCollection
{
    Process
    {
        Remove-Variable -Name TfsConnection -Scope Global
        Remove-Variable -Name TfsConnectionUrl -Scope Global
        Remove-Variable -Name TfsConnectionCredential -Scope Global
        Remove-Variable -Name TfsConnectionUseDefaultCredentials -Scope Global
    }
}

