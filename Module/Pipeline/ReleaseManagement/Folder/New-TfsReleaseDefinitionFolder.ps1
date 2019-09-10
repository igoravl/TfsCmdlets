#define ITEM_TYPE Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder
Function New-TfsReleaseDefinitionFolder
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the folder path
        [Parameter(Position=0, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias('Path')]
        [object]
        $Folder,

        # Description of the new release folder
        [Parameter()]
        [string]
        $Description,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
    )
    
    Process
    {
        GET_TEAM_PROJECT($tp,$tpc)

        if(-not $PSCmdlet.ShouldProcess($tp.Name, "Create release folder '$Folder'"))
        {
            return
        }

        GET_CLIENT('Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient')

        $newFolder = New-Object 'ITEM_TYPE' -Property @{
            Description = $Description
            Path = "\\$($Folder.ToString().Trim('\\'))"
        }

        CALL_ASYNC($client.CreateFolderAsync($newFolder, $tp.Name), "Error creating folder '$Folder'")

        if($Passthru)
        {
            return $result
        }
    }
}
