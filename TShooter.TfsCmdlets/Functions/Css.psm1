#============================
# Area & Iteration cmdlets
#============================

Function New-TfsArea
{
    Param
    (
		[Parameter(ParameterSetName="Full path", Mandatory=$true, Position=0)]
        [ValidateNotNullOrEmpty()]
		[string]
		$Path,

		[Parameter(ParameterSetName="Split path", Mandatory=$true, Position=0)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Name,

		[Parameter(ParameterSetName="Split path", Mandatory=$true)]
        [ValidateNotNull()]
		[string]
		$ParentPath = "",

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Project,

		[Parameter()]
		[object]
		$Collection
    )

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection $Collection
	}

    Process
    {
		if ($Path)
		{
			$Path = _NormalizePath $Path
			$Name = Split-Path $Path -Leaf
			$ParentPath = Split-Path $Path -Parent
		}
		
		return _NewCssNode -Project $Project -Path $ParentPath -Scope Area -Name $Name -Collection $tpc
    }
}

Function Get-TfsArea
{
    Param
    (
		[Parameter(ParameterSetName="Full path", Mandatory=$true, Position=0)]
        [ValidateNotNullOrEmpty()]
		[string]
		$Path,

		[Parameter(ParameterSetName="Split path", Mandatory=$true, Position=0)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Name,

		[Parameter(ParameterSetName="Split path", Mandatory=$true)]
        [ValidateNotNull()]
		[string]
		$ParentPath = "",

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Project,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])})] 
		$Collection
    )

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection -Current -Collection $Collection
	}

    Process
    {
		if ($Path)
		{
			$Path = _NormalizePath $Path
			$Name = Split-Path $Path -Leaf
			$ParentPath = Split-Path $Path -Parent
		}
		
		return _GetCssNode -Project $Project -Path $ParentPath -Scope Area -Name $Name -Collection $tpc
    }
}

Function New-TfsIteration
{
    Param
    (
		[Parameter(ParameterSetName="Full path", Mandatory=$true, Position=0)]
        [ValidateNotNullOrEmpty()]
		[string]
		$Path,

		[Parameter(ParameterSetName="Split path", Mandatory=$true, Position=0)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Name,

		[Parameter(ParameterSetName="Split path", Mandatory=$true)]
        [ValidateNotNull()]
		[string]
		$ParentPath = "",

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Project,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])})] 
		$Collection,

        [Parameter()]
		[DateTime]
        $StartDate,
    
        [Parameter()]
		[DateTime]
        $EndDate
    )

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection -Current -Collection $Collection
	}

    Process
    {
		if ($Path)
		{
			$Path = _NormalizePath $Path
			$Name = Split-Path $Path -Leaf
			$ParentPath = Split-Path $Path -Parent
		}
		
		$iterationNode = _NewCssNode -Project $Project -Path $ParentPath -Scope Iteration -Name $Name -Collection $tpc

        if ($StartDate)
        {
            $iterationNode = Set-IterationDates @PSBoundParameters
        }

        return $iterationNode
    }
}

Function Get-TfsIteration
{
    Param
    (
		[Parameter(ParameterSetName="Full path", Mandatory=$true, Position=0)]
        [ValidateNotNullOrEmpty()]
		[string]
		$Path,

		[Parameter(ParameterSetName="Split path", Mandatory=$true, Position=0)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Name,

		[Parameter(ParameterSetName="Split path", Mandatory=$true)]
        [ValidateNotNull()]
		[string]
		$ParentPath = "",

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$Project,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])})] 
		$Collection
    )

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection -Current -Collection $Collection
	}

    Process
    {
		if ($Path)
		{
			$Path = _NormalizePath $Path
			$Name = Split-Path $Path -Leaf
			$ParentPath = Split-Path $Path -Parent
		}
		
		return _GetCssNode -Project $Project -Path $ParentPath -Scope Iteration -Name $Name -Collection $tpc
    }
}

Function Set-TfsIteration
{
    param
    (
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[object]
		$Iteration,

		[Parameter()]
		[string]
		$Project,

		[Parameter()]
		[DateTime]
		$StartDate,

		[Parameter()]
		[DateTime]
		$EndDate,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])})] 
		$Collection
    )

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection -Current -Collection $Collection
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService4")
	}

    Process
    {
		if ($Iteration -is [Microsoft.TeamFoundation.Server.NodeInfo])
		{
			$iterationNode = $Iteration
		}
		else
		{
			$iterationNode = _GetCssNode -Project $Project -Path (Split-Path $Iteration -Parent) -Scope Iteration -Name (Split-Path $Iteration -Leaf) -Collection $Collection

	        if (!$iterationNode)
			{
				throw "Invalid iteration path: $Iteration"
			}
		}

		if ($StartDate)
		{
			[void]$cssService.SetIterationDates($iterationNode.Uri, $StartDate, $EndDate)
		}

        return $cssService.GetNode($iterationNode.Uri)
    }
}

# =================
# Helper Functions
# =================

Function _GetCssNode
{
    Param($Project, $Name, $Scope, $Path, $Collection)

	Begin
	{
		$tpc = Get-TfsTeamProjectCollection -Current -Collection $Collection
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")
	}

    Process
    {
        $nodePath = _NormalizePath "$Project\\$Scope\\$Path\\$Name"

        return $cssService.GetNodeFromPath($nodePath)
    }
}

Function _NewCssNode
{
    param ($Project, $Path, $Scope, $Name, $Collection)

	Begin
	{
		$tpc = $Collection
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")
	}

    Process
    {
        try
        {
			$parentPath = _NormalizePath "$Project\\$Scope\\$Path"
            $parentNode = $cssService.GetNodeFromPath($parentPath)
        }
        catch
        {
            $parentNode = _NewCssNode -Project $Project -Path (Split-Path $Path -Parent) -Name (Split-Path $Path -Leaf) -Scope $Scope -Collection $Collection
        }

        $nodeUri = $cssService.CreateNode($Name, $parentNode.Uri)

        return $cssService.GetNode($nodeUri)
    }
}

Function _NormalizePath
{
	Param ($Path)

	if([string]::IsNullOrWhiteSpace($Path))
	{
		return [string]::Empty
	}

	$newPath = $Path.Replace("\\", "\")

	if ($newPath.StartsWith("\"))
	{
		$newPath = $newPath.Substring(1)
	}

	if ($newPath.EndsWith("\"))
	{
		$newPath = $newPath.Substring(0, $newPath.Length-1)
	}

	return $newPath
}