Function _NewScriptBlock($EntryPoint, [string[]]$Dependency)
{
	$entryPoint = (Get-Item "function:$EntryPoint").Definition.Trim()
	$paramSection = $entryPoint.Substring(0, $entryPoint.IndexOf("`n"))
	$bodySection = $entryPoint.Substring($paramSection.Length) + "`n`n"
	
	$body = $paramSection

	foreach($depFn in $Dependency)
	{
		$f = Get-Item "function:$depFn"

		$body += "Function $f `n{`n"
		$body += $f.Definition 
		$body += "`n}`n`n"
	}

	$body += $bodySection

	return [scriptblock]::Create($body)
}
