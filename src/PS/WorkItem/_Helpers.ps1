Function _FixAreaIterationValues([hashtable] $Fields, $ProjectName)
{
	if ($Fields.ContainsKey('System.AreaPath') -and ($Fields['System.AreaPath'] -notmatch "'\\?$ProjectName\\.+'"))
	{
		$Fields['System.AreaPath'] = ("$ProjectName\$($Fields['System.AreaPath'])" -replace '\\', '\')
	}

	if ($Fields.ContainsKey('System.IterationPath') -and ($Fields['System.IterationPath'] -notmatch "'\\?$ProjectName\\.+'"))
	{
		$Fields['System.IterationPath'] = ("$ProjectName\$($Fields['System.IterationPath'])" -replace '\\', '\')
	}
	
	return $Fields
}
