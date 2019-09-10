#define ITEM_TYPE Microsoft.TeamFoundation.Build.WebApi.Folder
Function Get-TfsBuildFolder
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the folder path
        [Parameter(Position=0)]
        [Alias('Path')]
        [SupportsWildcards()]
        [object]
        $Folder = '**',

        # Query order
        [Parameter()]
        [Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder]
        $QueryOrder = [Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder]::None,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )
    
    Process
    {
        CHECK_ITEM($Folder)

        GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$Folder.Project.Name)

        GET_CLIENT('Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient')

        if(_IsWildCard $Folder)
        {
            $path = '/'
        }
        else
        {
            $path = $Folder
        }

        CALL_ASYNC($client.GetFoldersAsync($tp.Name, $path, $QueryOrder))

        return $result | Where-Object Path -Like $Folder
    }
}
