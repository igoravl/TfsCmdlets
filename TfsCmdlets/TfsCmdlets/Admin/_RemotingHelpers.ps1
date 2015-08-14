Function New-ScriptBlock($EntryPoint, [string[]]$Dependency)
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

Function Invoke-ScriptBlock($ScriptBlock, $Computer, $Credentials, $ArgumentList)
{
	if (-not $Computer)
	{
		return Invoke-Command -ScriptBlock $scriptBlock -ArgumentList $ArgumentList
	}
	elseif ($Computer -is [System.Management.Automation.Runspaces.PSSession])
	{
		return Invoke-Command -ScriptBlock $scriptBlock -Session $Computer -ArgumentList $ArgumentList
	}

	return Invoke-Command -ScriptBlock $scriptBlock -ComputerName $Computer -Credential $Credential -ArgumentList $ArgumentList
}
