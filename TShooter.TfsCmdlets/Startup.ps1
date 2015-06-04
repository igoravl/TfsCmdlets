Function _SetConsoleColors
{
	Process
	{
		$Host.UI.RawUI.BackgroundColor = "DarkMagenta"
		$Host.UI.RawUI.ForegroundColor = "White"
		Clear-Host
	}
}

Function _ImportTfsCmdlets
{
	Process
	{
		Write-Host "Importing TfsCmdlets module from $(_GetScriptDirectory)... " -NoNewline 
		Import-Module "$(_GetScriptDirectory)\TfsCmdlets.psd1"
		Write-Host "DONE."
	}
}

Function _ImportTfsPowerTools
{
	Process
	{
		Write-Host "Importing TFS Power Tools snap-in... " -NoNewline 

		try
		{
			$snapInName = "Microsoft.TeamFoundation.PowerShell"
			if ((Get-PSSnapin | ? { $_.Name -eq $snapInName }) -eq $null)
			{
				Add-PSSnapin $snapInName 2>&1 | Out-Null
			}
			Write-Host "DONE."
		}
		catch 
		{
			Write-Host "ERROR ($error.Exception.Message)."
		}
	}
}

Function _ImportTfsStandardTools
{
	Process
	{
		Write-Host "Importing TFS standard tools... " -NoNewline 
		$vsToolsPath = $env:VS120COMNTOOLS
		$devEnvDir = [System.IO.Path]::GetFullPath($(Join-Path $vsToolsPath "..\IDE"))
		$env:Path += ";$devEnvDir"
		Write-Host "DONE."
	}
}

Function _ShowBanner
{
	Process
	{
		$module = Get-Module TfsCmdlets
		Write-Host ""
		Write-Host "$($module.Name) version $($module.Version)"
		Write-Host ""
	}
}

Function _GetScriptDirectory
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
		if (Test-Path variable:global:TfsConnection)
		{
			$tfsConnectionText = "@$($Global:TfsConnection.Uri.Host)/$($Global:TfsConnection.Uri.Segments[$Global:TfsConnection.Uri.Segments.Length-1])"
		}
		"[TFS${tfsConnectionText}] $($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "
	}
}

#Script main body

_SetConsoleColors
#_ImportTfsCmdlets
#_ImportTfsPowerTools
#_ImportTfsStandardTools
#_ShowBanner
