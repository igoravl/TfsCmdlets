<#
.SYNOPSIS
Create a new work items query in the given Team Project.

.PARAMETER Query
Specifies the path of the new work item query.
When supplying a path, use a slash ("/") between the path segments. Leading and trailing backslashes are optional.  The last segment in the path will be the query name.

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
System.String
#>
Function New-TfsWorkItemQuery
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [Alias("Path")]
        [string]
        $Query,

        [Parameter()]
        [string]
        $Folder,

        [Parameter()]
        [ValidateSet('Personal', 'Shared')]
        [string]
        $Scope = 'Personal',

        [Parameter()]
        [string]
        $Definition,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [switch]
        $Passthru
    )

    Begin
    {
        _RegisterQueryHelper
    }

    Process
    {
        if ($PSCmdlet.ShouldProcess($Query, "Create work item query under folder '$Folder', in $Scope scope"))
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            #$tpc = $tp.Store.TeamProjectCollection
            #$store = $tp.Store
            $hierarchy = $tp.QueryHierarchy

            if ($Scope -eq 'Shared')
            {
                $rootFolder = $hierarchy[1].Name
            }
            else
            {
                $rootFolder = $hierarchy[0].Name
            }

            $normalizedPath = _NormalizeQueryPath -Path "$Folder/$Query" -RootFolder $rootFolder -ProjectName $tp.Name
            $queryPath = (Split-Path $normalizedPath -Parent).Replace('\', '/')
            $queryName = Split-Path $normalizedPath -Leaf

            Write-Verbose "New-TfsWorkItemQuery: Creating query '$queryName' in folder '$queryPath'"

            $queryFolder = [TfsCmdlets.QueryHelper]::GetQueryFolderFromPath($tp.QueryHierarchy, $queryPath)

            if (-not $queryFolder)
            {
                if ($Force)
                {
                    $queryFolder = New-TfsWorkItemQueryFolder -Path $queryPath -Project $tp.Name -Passthru
                }
                else
                {
                    throw "Invalid or non-existent work item query folder $queryPath."
                }
            }

            if ($Definition -match "select \*")
            {
                Write-Warning "Queries containing 'SELECT *' may not work in Visual Studio. Consider replacing * with a list of fields."
            }

            $q = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition' -ArgumentList $queryName, $Definition
            $queryFolder.Add($q)

            $tp.QueryHierarchy.Save()

            if ($Passthru)
            {
                return $q
            }
        }
    }
}
