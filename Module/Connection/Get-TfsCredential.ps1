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
        _Requires 'Microsoft.VisualStudio.Services.Common', 'Microsoft.VisualStudio.Services.Client.Interactive', 'Microsoft.TeamFoundation.Core.WebApi'
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
                return [Microsoft.VisualStudio.Services.Client.VssClientCredentials]::new($true)
            }

            'User name and password' {
                $netCred = [System.Net.NetworkCredential]::new($UserName, $Password)
                $fedCred = [Microsoft.VisualStudio.Services.Common.VssBasicCredential]::new($netCred)
                $winCred = [Microsoft.VisualStudio.Services.Common.WindowsCredential]::new($netCred)
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

                $fedCred = [Microsoft.VisualStudio.Services.Common.VssBasicCredential]::new($netCred)
                $winCred = [Microsoft.VisualStudio.Services.Common.WindowsCredential]::new($netCred)
            }

            'Personal Access Token' {
                $netCred = [System.Net.NetworkCredential]::new('dummy-pat-user', $PersonalAccessToken)
                $fedCred = [Microsoft.VisualStudio.Services.Common.VssBasicCredential]::new($netCred)
                $winCred = [Microsoft.VisualStudio.Services.Common.WindowsCredential]::new($netCred)
            }

            'Prompt for credential' {

                if($PSEdition -eq 'Core')
                {
                    Throw 'Interactive logons are currently not supported in PowerShell Core. Use personal access tokens instead.'
                }

                $fedCred = [Microsoft.VisualStudio.Services.Client.VssFederatedCredential]::new($false)
                $winCred = [Microsoft.VisualStudio.Services.Common.WindowsCredential]::new($false)
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

        return [Microsoft.VisualStudio.Services.Client.VssClientCredentials]::new($winCred, $fedCred, $promptType)
    }
}