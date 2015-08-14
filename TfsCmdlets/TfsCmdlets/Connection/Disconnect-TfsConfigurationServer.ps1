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
