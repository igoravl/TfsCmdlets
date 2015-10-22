Function Disconnect-TfsConfigurationServer
{
	Process
	{
        Disconnect-TfsTeamProjectCollection

		if ($Global:TfsServerConnection)
        {
		    Remove-Variable -Name TfsServerConnection -Scope Global
		}
	}
}
