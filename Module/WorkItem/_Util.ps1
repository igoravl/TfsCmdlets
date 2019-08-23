Function _FixAreaIterationValues([hashtable] $Fields, $ProjectName)
{
	if ($Fields.ContainsKey('System.AreaPath') -and ($Fields['System.AreaPath'] -notmatch "'\\\\?$ProjectName\\\\.+'"))
	{
		$Fields['System.AreaPath'] = ("$ProjectName\\$($Fields['System.AreaPath'])" -replace '\\\\', '\\')
	}

	if ($Fields.ContainsKey('System.IterationPath') -and ($Fields['System.IterationPath'] -notmatch "'\\\\?$ProjectName\\\\.+'"))
	{
		$Fields['System.IterationPath'] = ("$ProjectName\\$($Fields['System.IterationPath'])" -replace '\\\\', '\\')
	}
	
	return $Fields
}

Function _GetEscapedFieldName([string] $fieldName)
{
	$fieldName = $fieldName.Trim()

	if(-not $fieldName.StartsWith('['))
	{
		$fieldName = '[' + $fieldName
	}

	if(-not $fieldName.EndsWith(']'))
	{
		$fieldName += ']'
	}

	return $fieldName
}

Function _GetEncodedFieldName([string] $fieldName)
{
	return $fieldName.Trim(' ', '[', ']') -replace '[/W]', '_'
}
