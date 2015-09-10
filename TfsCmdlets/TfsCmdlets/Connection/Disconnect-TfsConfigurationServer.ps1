Function Disconnect-TfsConfigurationServer
{
	Process
	{
		if ($Global:TfsServerConnection)
        {
		    Remove-Variable -Name TfsServerConnection -Scope Global
		}

        Disconnect-TfsTeamProjectCollection
	}
}
