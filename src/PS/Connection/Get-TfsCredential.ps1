<#
.SYNOPSIS
    Provides credentials to use when you connect to a Team Foundation Server or Visual Studio Team Services account.

.DESCRIPTION

.NOTES

.INPUTS
   
#>
Function Get-TfsCredential
{
    [CmdletBinding(DefaultParameterSetName="Prompt for credential")]
    [OutputType([Microsoft.TeamFoundation.Client.TfsClientCredentials])]
    Param
    (
        [Parameter(ParameterSetName="Cached Credential", Mandatory=$true)]
        [switch]
        $Cached,

        [Parameter(ParameterSetName="User name and password", Mandatory=$true, Position=1)]
        [string]
        $UserName,

        [Parameter(ParameterSetName="User name and password", Position=2)]
        [securestring]
        $Password,

        [Parameter(ParameterSetName="Credential object")]
        [object]
        $Credential,

        [Parameter(ParameterSetName="Personal Access Token")]
        [Alias('Pat')]
        $PersonalAccessToken,

        [Parameter(ParameterSetName="Prompt for credential")]
        [switch]
        $Interactive
    )

    Process
    {
        $parameterSetName = $PSCmdlet.ParameterSetName
        
        if (($parameterSetName -eq 'Credential object') -and (-not $Credential))
        {
            Write-Verbose "Parameter Credential was specified but no value was provided. Defaulting to cached credentials"
            $parameterSetName = 'Cached Credential'
        }

        $allowInteractive = $false

        switch($parameterSetName)
        {
            'Cached Credential' {
                Write-Verbose "Returning cached credentials"
                $fedCred = New-Object 'Microsoft.TeamFoundation.Client.CookieCredential' -ArgumentList $true
                $winCred = New-Object 'Microsoft.TeamFoundation.Client.WindowsCredential' -ArgumentList $true
            }

            'User name and password' {
                Write-Verbose "Returning user name / password credentials from arguments UserName and Password"
                $netCred = New-Object 'System.Net.NetworkCredential' -ArgumentList $UserName, $Password
                $fedCred = New-Object 'Microsoft.TeamFoundation.Client.BasicAuthCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.TeamFoundation.Client.WindowsCredential' -ArgumentList $netCred
            }

            'Credential object' {
                if ($Credential -is [Microsoft.TeamFoundation.Client.TfsClientCredentials])
                {
                    Write-Verbose "Credentials argument is of type TfsClientCredentials. Returning that object unmodified."
                    return $Credential
                }

                if($Credential -is [pscredential])
                {
                    Write-Verbose "Credentials argument is of type PSCredential. Deriving a new user name/password credential from it."
                    $netCred = $Credential.GetNetworkCredential()
                }
                elseif ($Credential -is [System.Net.NetworkCredential])
                {
                    Write-Verbose "Credentials argument is of type NetworkCredential. Deriving a new user name/password credential from it."
                    $netCred = $Credential
                }
                else
                {
                    throw "Invalid argument Credential. Supply either a PowerShell credential (PSCredential object) or a System.Net.NetworkCredential object."    
                }

                $fedCred = New-Object 'Microsoft.TeamFoundation.Client.BasicAuthCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.TeamFoundation.Client.WindowsCredential' -ArgumentList $netCred
            }

            'Personal Access Token' {
                Write-Verbose "Returning PAT-based credentials from PersonalAccessToken argument."
                $netCred = New-Object 'System.Net.NetworkCredential' -ArgumentList 'dummy-pat-user', $PersonalAccessToken
                $fedCred = New-Object 'Microsoft.TeamFoundation.Client.BasicAuthCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.TeamFoundation.Client.WindowsCredential' -ArgumentList $netCred
            }

            'Prompt for credential' {
                Write-Verbose "Returning an empty, interactive login credential."
                $fedCred = New-Object 'Microsoft.TeamFoundation.Client.CookieCredential' -ArgumentList $false
                $winCred = New-Object 'Microsoft.TeamFoundation.Client.WindowsCredential' -ArgumentList $false
                $allowInteractive = $true
            }

            else {
                throw "Invalid parameter set $($PSCmdlet.ParameterSetName)"
            }
        }

        return New-Object 'Microsoft.TeamFoundation.Client.TfsClientCredentials' -ArgumentList $winCred, $fedCred, $allowInteractive
    }
}