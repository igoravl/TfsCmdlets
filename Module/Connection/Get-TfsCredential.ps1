#define ITEM_TYPE Microsoft.VisualStudio.Services.Client.VssClientCredentials
<#
.SYNOPSIS
    Provides credentials to use when you connect to a Team Foundation Server or Visual Studio Team Services account.

.DESCRIPTION

.NOTES

.INPUTS
   
#>
Function Get-TfsCredential
{
    [CmdletBinding(DefaultParameterSetName="Cached credentials")]
    [OutputType('ITEM_TYPE')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
    Param
    (
        [Parameter(ParameterSetName="Cached credentials")]
        [switch]
        $Cached,

        [Parameter(ParameterSetName="User name and password", Mandatory=$true, Position=1)]
        [string]
        $UserName,

        [Parameter(ParameterSetName="User name and password", Position=2)]
        [securestring]
        $Password,

        [Parameter(ParameterSetName="Credential object", Mandatory=$true)]
        [AllowNull()]
		[object]
        $Credential,

        [Parameter(ParameterSetName="Personal Access Token", Mandatory=$true)]
        [Alias('Pat')]
        [string]
        $PersonalAccessToken,

        [Parameter(ParameterSetName="Prompt for credential", Mandatory=$true)]
        [switch]
        $Interactive
    )

    Begin
    {
        _LogParams
    }
    
    Process
    {
        $parameterSetName = $PSCmdlet.ParameterSetName
        
        if (($parameterSetName -eq 'Credential object') -and (-not $Credential))
        {
            $parameterSetName = 'Cached Credentials'
        }

        $allowInteractive = $false

        switch($parameterSetName)
        {
            'Cached Credentials' {
                $fedCred = New-Object 'Microsoft.VisualStudio.Services.Client.VssFederatedCredential' -ArgumentList $true
                $winCred = New-Object 'Microsoft.VisualStudio.Services.Common.WindowsCredential' -ArgumentList $true
            }

            'User name and password' {
                $netCred = New-Object 'System.Net.NetworkCredential' -ArgumentList $UserName, $Password
                $fedCred = New-Object 'Microsoft.VisualStudio.Services.Common.VssBasicCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.VisualStudio.Services.Common.WindowsCredential' -ArgumentList $netCred
            }

            'Credential object' {
                if ($Credential -is [ITEM_TYPE])
                {
                    return $Credential
                }

                if($Credential -is [pscredential])
                {
                    $netCred = $Credential.GetNetworkCredential()
                }
                elseif ($Credential -is [System.Net.NetworkCredential])
                {
                    $netCred = $Credential
                }
                else
                {
                    throw "Invalid argument Credential. Supply either a PowerShell credential (PSCredential object) or a System.Net.NetworkCredential object."    
                }

                $fedCred = New-Object 'Microsoft.VisualStudio.Services.Common.VssBasicCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.VisualStudio.Services.Common.WindowsCredential' -ArgumentList $netCred
            }

            'Personal Access Token' {
                $netCred = New-Object 'System.Net.NetworkCredential' -ArgumentList 'dummy-pat-user', $PersonalAccessToken
                $fedCred = New-Object 'Microsoft.VisualStudio.Services.Common.VssBasicCredential' -ArgumentList $netCred
                $winCred = New-Object 'Microsoft.VisualStudio.Services.Common.WindowsCredential' -ArgumentList $netCred
            }

            'Prompt for credential' {
                $fedCred = New-Object 'Microsoft.VisualStudio.Services.Client.VssFederatedCredential' -ArgumentList $false
                $winCred = New-Object 'Microsoft.VisualStudio.Services.Common.WindowsCredential' -ArgumentList $false
                $allowInteractive = $true
            }

            else {
                throw "Invalid parameter set $($PSCmdlet.ParameterSetName)"
            }
        }

        if($allowInteractive)
        {
            $promptType = [Microsoft.VisualStudio.Services.Common.CredentialPromptType]::PromptIfNeeded
        }
        else
        {
            $promptType = [Microsoft.VisualStudio.Services.Common.CredentialPromptType]::DoNotPrompt
        }

        return New-Object 'Microsoft.VisualStudio.Services.Client.VssClientCredentials' -ArgumentList $winCred, $fedCred, $promptType
    }
}