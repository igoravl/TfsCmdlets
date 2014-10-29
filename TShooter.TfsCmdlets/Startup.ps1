Function Import_TfsCmdlets
{
	Process
	{
		Write-Host "Importing TfsCmdlets module... " -NoNewline 
		Import-Module "$(Get-ScriptDirectory)\TfsCmdlets.psd1"
		Write-Host "DONE."
	}
}

Function Import_TfsPowerTools
{
	Process
	{
		Write-Host "Importing TFS Power Tools snap-in... " -NoNewline 
		try
		{
			$snapInName = "Microsoft.TeamFoundation.PowerShell"
			if ((Get-PSSnapin | ? { $_.Name -eq $snapInName }) -eq $null)
			{
				Add-PSSnapin $snapInName #2>&1 | Out-Null
			}
			Write-Host "DONE."
		}
		catch 
		{
			Write-Host "ERROR ($error.Exception.Message)."
		}
	}
}

Function Import_TfsStandardTools
{
	Process
	{
		Write-Host "Importing TFS standard tools... " -NoNewline 
		$vsToolsPath = $env:VS120COMNTOOLS
		$devEnvDir = [System.IO.Path]::GetFullPath($(Join-Path $vsToolsPath "..\IDE"))
		$env:env:Path += $devEnvDir
		Write-Host "DONE."
	}
}

Function Show_Banner
{
	Process
	{
		$module = Get-Module TfsCmdlets
		Write-Host ""
		Write-Host "$($module.Name) version $($module.Version)"
		Write-Host ""
	}
}

Function Get-ScriptDirectory
{
	Process
	{
		$Invocation = (Get-Variable MyInvocation -Scope 1).Value;
		if($Invocation.PSScriptRoot)
		{
			$Invocation.PSScriptRoot;
		}
		Elseif($Invocation.MyCommand.Path)
		{
			Split-Path $Invocation.MyCommand.Path
		}
		else
		{
			$Invocation.InvocationName.Substring(0,$Invocation.InvocationName.LastIndexOf("\"));
		}
	}
}

Function Prompt
{
	Process
	{
		"[TFS] $($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
	}
}

#Script main body

Import_TfsCmdlets
Import_TfsPowerTools
Import_TfsStandardTools
Show_Banner

