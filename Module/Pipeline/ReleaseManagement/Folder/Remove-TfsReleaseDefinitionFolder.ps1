#define ITEM_TYPE Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder
Function Remove-TfsReleaseDefinitionFolder
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the folder path
        [Parameter(Position=0, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias('Path')]
        [SupportsWildcards()]
        [object]
        $Folder,

        # Remove folders recursively
        [Parameter()]
        [switch]
        $Recurse,

        # Remove folder containing Releases
        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )
    
    Process
    {
        $folders = Get-TfsReleaseDefinitionFolder -Folder $Folder -Project $Project -Collection $Collection

        foreach($f in $folders)
        {
            if(-not $PSCmdlet.ShouldProcess($f.Project.Name, "Remove folder '$($f.Path)'"))
            {
                continue
            }

            if(-not $Recurse.IsPresent)
            {
                _Log "Recurse argument not set. Check if folder '$($f.Path)' has sub-folders"

                $path = "$($f.Path.TrimEnd('\\'))\\**"
                $subFolders = (Get-TfsReleaseDefinitionFolder -Folder $path -Project $Project -Collection $Collection)

                if($subFolders.Count -gt 0)
                {
                    _throw "Folder '$($f.Path)' has $($subFolders.Count) sub-folder(s). To delete it, use the -Recurse argument."
                }

                _Log "Folder '$($f.Path)' has no sub-folders"
            }

            GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$f.Project.Name)

            GET_CLIENT('Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient')

            if(-not $Force.IsPresent)
            {
                _Log "Force argument not set. Check if folder '$($f.Path)' has release definitions"

                CALL_ASYNC($client.GetReleaseDefinitionsAsync($tp.Name, [string]$null, [Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts.ReleaseDefinitionExpands]::None, [string]$null, $null, $null, $null, $null, $f.Path), "Error fetching release definitions in folder '$($f.Path)'")

                if($result.Count -gt 0)
                {
                    _throw "Folder '$($f.Path)' has $($result.Count) release definition(s). To delete it, use the -Force argument."
                }

                _Log "Folder '$($f.Path)' has no release definitions"
            }

            CALL_ASYNC($client.DeleteFolderAsync($tp.Name, $f.Path))
        }
    }
}
