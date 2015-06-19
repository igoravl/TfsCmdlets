#============================
# Area & Iteration cmdlets
#============================
<#
.SYNOPSIS
	Create a new Area on Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
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

<#
.SYNOPSIS
	Get an specific Area of one Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
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
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection
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

<#
.SYNOPSIS
	Create a new Iteration on Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
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
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection
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
            $iterationNode = Set-TfsIteration @PSBoundParameters
        }

        return $iterationNode
    }
}

<#
.SYNOPSIS
	Get an specific Iteration of one Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
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

    Process
    {
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection

		if ($Path)
		{
			$Path = _NormalizePath $Path
			$Name = Split-Path $Path -Leaf
			$ParentPath = Split-Path $Path -Parent
		}
		
		return _GetCssNode -Project $Project -Path $ParentPath -Scope Iteration -Name $Name -Collection $tpc
    }
}

<#
.SYNOPSIS
	Set Iteration Dates of an specific Iteration of one Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
Function Set-TfsIteration
{
    param
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

    Process
    {
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService4")

		$iterationNode = _GetCssNode -Project $Project -Path $ParentPath -Scope Iteration -Name $Name -Collection $tpc

	    if (!$iterationNode)
		{
			throw "Invalid iteration path: $Iteration"
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
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection
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