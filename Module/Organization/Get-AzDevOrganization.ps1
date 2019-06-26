Function Get-AzDevOrganization
{
	[CmdletBinding(DefaultParameterSetName='Get by collection')]
	[OutputType('Microsoft.TeamFoundation.Client.TfsTeamProjectCollection')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	Param
	(
		[Parameter(Position=0, ParameterSetName="Get by organization")]
		[object] 
		$Organization,
	
		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(ParameterSetName="Get by collection")]
		[object]
		$Credential
	)

	Begin
	{
		_ImportRequiredAssembly 'Microsoft.VisualStudio.Services.WebApi'
	}

	Process
	{
        if ($Current)
        {
            return $script:AzDevOrganizationConnection
        }
    }
}