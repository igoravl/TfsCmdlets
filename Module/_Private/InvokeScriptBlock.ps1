Function _InvokeScriptBlock($ScriptBlock, $Computer, $Credentials, $ArgumentList)
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
