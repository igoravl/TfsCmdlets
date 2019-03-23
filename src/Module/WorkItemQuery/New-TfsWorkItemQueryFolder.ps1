<#
.SYNOPSIS
    Create a new work items query in the given Team Project.

.PARAMETER Path
    Specifies the path of the new work item query folder.
    When supplying a path, use a slash ("/") between the path segments. Leading and trailing backslashes are optional.  The last segment in the path will be the query name.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function New-TfsWorkItemQueryFolder
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [string]
        [Alias("Name")]
        [Alias("Path")]
        $Folder,

        [Parameter()]
        [ValidateSet('Personal', 'Shared')]
        [string]
        $Scope = 'Personal',

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [switch]
        $Passthru
    )

    Begin
    {
        _RegisterQueryHelper
    }

    Process
    {
        if($PSCmdlet.ShouldProcess($Folder, 'Create work item query folder'))
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            #$tpc = $tp.Store.TeamProjectCollection
            #$store = $tp.Store

            if ($Scope -eq 'Shared')
            {
                $rootFolder = 'Shared Queries'
            }
            else
            {
                $rootFolder = 'My Queries'
            }

            $normalizedPath = _NormalizeQueryPath -Path $Folder -RootFolder $rootFolder -ProjectName $tp.Name

            Write-Verbose "New-TfsWorkItemQueryFolder: Creating folder '$Folder'"

            $queryFolder = [TfsCmdlets.QueryHelper]::CreateFolder($tp.QueryHierarchy, $normalizedPath)

            if ($Passthru)
            {
                return $queryFolder
            }
        }
    }
}
