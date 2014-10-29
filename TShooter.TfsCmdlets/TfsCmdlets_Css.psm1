#============================
# Area & Iteration cmdlets
#============================

Function New-Area
{
    param
    (
        [Parameter(Mandatory=$true)] [string] 
        $CollectionUrl,
    
        [Parameter(Mandatory=$true)] [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
        $Path,
    
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential = [System.Management.Automation.PSCredential]::Empty
    )

    Process
    {
        $tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
        $areaPath = ("\" + $ProjectName + "\Area\" + $Path).Replace("\\", "\")
    
        _NewCssNode -TeamProjectCollection $tpc -Path $areaPath
    }
}

Function New-Iteration
{
    param
    (
        [Parameter(Mandatory=$true)] [string] 
        $CollectionUrl,
    
        [Parameter(Mandatory=$true)] [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
        $Path,

        [Parameter()] [DateTime]
        $StartDate,
    
        [Parameter()] [DateTime]
        $EndDate,
    
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential = [System.Management.Automation.PSCredential]::Empty
    )

    Process
    {
        $tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
        $iterationPath = ("\" + $ProjectName + "\Iteration\" + $Path).Replace("\\", "\")
    
        $iterationNode = _NewCssNode -TeamProjectCollection $tpc -Path $iterationPath

        if ($StartDate)
        {
            $iterationNode = Set-IterationDates @PSBoundParameters
        }

        return $iterationNode
    }
}

Function Set-IterationDates
{
    param
    (
        [Parameter(Mandatory=$true)] [string] 
        $CollectionUrl,
    
        [Parameter(Mandatory=$true)] [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
        $Path,

        [Parameter(Mandatory=$true)] [Nullable[DateTime]]
        $StartDate,
    
        [Parameter(Mandatory=$true)] [Nullable[DateTime]]
        $EndDate,
    
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential = [System.Management.Automation.PSCredential]::Empty
    )

    Process
    {
        $tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService4")

        $iterationPath = ("\" + $ProjectName + "\Iteration\" + $Path).Replace("\\", "\")
        $iterationNode = _GetCssNode -TeamProjectCollection $tpc -Path $iterationPath

        if (!$iterationNode)
        {
            throw "Invalid iteration path: $Path"
        }

        [void]$cssService.SetIterationDates($iterationNode.Uri, $StartDate, $EndDate)

        return _GetCssNode -TeamProjectCollection $tpc -Path $iterationPath
    }
}

# =================
# Helper Functions
# =================

Function _GetCssNode
{
    param
    (
        [Parameter(Mandatory=$true)] [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
        $TeamProjectCollection,

        [Parameter(Mandatory=$true)] [string]
        $Path,

        [Parameter()] [switch]
        $CreateIfMissing
    )

    Process
    {
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")

        try
        {
            $cssService.GetNodeFromPath($Path)
        }
        catch
        {
            if($CreateIfMissing.IsPresent)
            {
                _NewCssNode $TeamProjectCollection $Path
            }
        }
    }
}

Function _NewCssNode
{
    param
    (
        [Parameter(Mandatory=$true)] [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
        $TeamProjectCollection,

        [Parameter(Mandatory=$true)] [string]
        $Path
    )

    Process
    {
        $i = $Path.LastIndexOf("\")

        if ($i -eq -1)
        {
            throw "Node Path must be in format '\<Project>\<Area|Iteration>\Node1\Node2\Node-n'"
        }

        $parentPath = $Path.Substring(0, $i)
        $nodeName = $Path.Substring($i+1)
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")

        try
        {
            $parentNode = $cssService.GetNodeFromPath($parentPath)
        }
        catch
        {
            $parentNode = _NewCssNode -TeamProjectCollection $TeamProjectCollection -Path $parentPath
        }

        $nodeUri = $cssService.CreateNode($nodeName, $parentNode.Uri)

        return $cssService.GetNode($nodeUri)
    }
}
