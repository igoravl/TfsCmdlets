Function Disconnect-TfsTeamProject
{
	Process
	{
		Remove-Variable -Name TfsProjectConnection -Scope Global
	}
}
