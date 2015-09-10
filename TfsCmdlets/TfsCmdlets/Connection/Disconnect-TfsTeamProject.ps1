Function Disconnect-TfsTeamProject
{
	Process
	{
        if ($Global:TfsProjectConnection)
        {
		    Remove-Variable -Name TfsProjectConnection -Scope Global
        }
	}
}
