#define ITEM_TYPE Microsoft.TeamFoundation.Build.WebApi.Folder
Function New-TfsBuildDefinitionFolder
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

        # Description of the new build folder
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

        if(-not $PSCmdlet.ShouldProcess($tp.Name, "Create build folder '$Folder'"))
        {
            return
        }

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient' -Collection $tpc

        $newFolder = New-Object 'ITEM_TYPE' -Property @{
            Description = $Description
        }

        $Folder = $Folder.ToString().Trim('\\')

        CALL_ASYNC($client.CreateFolderAsync($newFolder, $tp.Name, "\\$Folder"), "Error creating folder '$Folder'")

        if($Passthru)
        {
            return $result
        }
    }
}
