#===========================
# Connection cmdlets
#===========================

Function Get-TeamProjectCollection
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
        }

        $tpc = New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection ([Uri] $CollectionUrl), $cred
        [void] $tpc.EnsureAuthenticated()

        return $tpc
    }
}

Function Connect-TeamProjectCollection
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
        $Global:TfsConnection = Get-TeamProjectCollection @PSBoundParameters
        $Global:TfsConnectionUrl = $CollectionUrl
        $Global:TfsConnectionCredential = $cred
        $Global:TfsConnectionUseDefaultCredentials = $UseDefaultCredentials.IsPresent
    }
}

Function Disconnect-TeamProjectCollection
{
    Process
    {
        Remove-Variable -Name TfsConnection -Scope Global
        Remove-Variable -Name TfsConnectionUrl -Scope Global
        Remove-Variable -Name TfsConnectionCredential -Scope Global
        Remove-Variable -Name TfsConnectionUseDefaultCredentials -Scope Global
    }
}

