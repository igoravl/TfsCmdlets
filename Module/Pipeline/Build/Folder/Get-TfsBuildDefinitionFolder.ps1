#define ITEM_TYPE Microsoft.TeamFoundation.Build.WebApi.Folder
Function Get-TfsBuildDefinitionFolder
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

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient' -Collection $tpc

        if(_IsWildCard $Folder)
        {
            CALL_ASYNC($client.GetFoldersAsync($tp.Name, '\\', $QueryOrder))
            return $result | Where-Object { ($_.Path -Like $Folder) -or ($_.Name -like $Folder) }
        }

        
        CALL_ASYNC($client.GetFoldersAsync($tp.Name, "\\$($Folder.Trim('\\'))", $QueryOrder), "Error fetching build folders")
        
        return $result
    }
}
