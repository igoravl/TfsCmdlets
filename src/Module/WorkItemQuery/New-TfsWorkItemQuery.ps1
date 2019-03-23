<#
.SYNOPSIS
Create a new work items query in the given Team Project.

.PARAMETER Query
Specifies the path of the new work item query.
When supplying a path, use a slash ("/") between the path segments. Leading and trailing slashes are optional.  The last segment in the path will be the query name.

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

        [Parameter(Mandatory=$true)]
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
        $SkipSave,

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
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $qh = $tp.GetQueryHierarchy2($true)
        $qh.GetChildrenAsync().Wait()

        $rootFolder = ($qh.GetChildren() | Where-Object IsPersonal -eq ($Scope -eq 'Personal'))
        $fullPath = _NormalizeQueryPath -Path "$Folder/$Query" -RootFolder $rootFolder.Name -ProjectName $tp.Name
        $queryPath = $fullPath.Substring(0, $fullPath.LastIndexOf('/'))
        $queryName = $fullPath.Substring($fullPath.LastIndexOf('/')+1)
        $relativeQueryPath = $fullPath.Substring($rootFolder.Name.Length + $tp.Name.Length + 2)
        $relativeFolderPath = $queryPath.Substring($rootFolder.Name.Length + $tp.Name.Length + 2)

        if (-not $PSCmdlet.ShouldProcess($queryName, "Create work item query under folder '$queryPath'"))
        {
            return
        }

        Write-Verbose "New-TfsWorkItemQuery: Check if query '$relativeQueryPath' exists"

        $existingQuery = Get-TfsWorkItemQuery -Query $relativeQueryPath -Scope $Scope -Project $Project -Collection $Collection

        if ($existingQuery)
        {
            if (-not $Force)
            {
                throw "Work item query '$fullPath' already exists. To overwrite an existing query, use the -Force switch"
            }

            Write-Verbose "New-TfsWorkItemQuery: Existing query '$fullPath' will be overwritten"

            $existingQuery.Delete()
            $existingQuery.Save()
        }

        Write-Verbose "New-TfsWorkItemQuery: Creating query '$queryName' in folder '$queryPath'"

        $queryFolder = Get-TfsWorkItemQueryFolder -Folder $relativeFolderPath -Scope $Scope -Project $Project -Collection $Collection

        if (-not $queryFolder)
        {
            Write-Verbose "New-TfsWorkItemQuery: Destination folder $queryFolder not found"

            if ($Force)
            {
                Write-Verbose "New-TfsWorkItemQuery: -Force switch specified. Creating missing folder"
                $queryFolder = New-TfsWorkItemQueryFolder -Path $queryPath -Project $tp.Name -Passthru
            }
            else
            {
                throw "Invalid or non-existent work item query folder $queryPath."
            }
        }

        if ($Definition -match "select \*")
        {
            Write-Warning "New-TfsWorkItemQuery: Queries containing 'SELECT *' may not work in Visual Studio. Consider replacing * with a list of fields."
        }

        $q = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition2' -ArgumentList $queryName, $Definition, $queryFolder

        if (-not $SkipSave)
        {
            $q.Save()
        }
        else
        {
            Write-Verbose "New-TfsWorkItemQuery: -SkipSave switch specified. Newly created query will not be saved."
        }

        if ($Passthru -or $SkipSave)
        {
            return $q
        }
    }
}
