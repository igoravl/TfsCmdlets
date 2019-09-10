#define ITEM_TYPE Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder
Function Get-TfsReleaseDefinitionFolder
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
        [Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder]
        $QueryOrder = [Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder]::None,

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

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient')

        if(_IsWildCard $Folder)
        {
            CALL_ASYNC($client.GetFoldersAsync($tp.Name, '\\', $QueryOrder))
            $result = $result | Where-Object { ($_.Path -Like $Folder) -or ($_.Name -like $Folder) }
        }
        else
        {
            CALL_ASYNC($client.GetFoldersAsync($tp.Name, "\\$($Folder.Trim('\\'))", $QueryOrder), "Error fetching build folders")
        }
        
        return $result | Add-Member -Name Project -MemberType NoteProperty -PassThru -Value (New-Object 'Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference' -Property @{
            Name = $tp.Name
        })
    }
}
