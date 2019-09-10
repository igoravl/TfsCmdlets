#define ITEM_TYPE Microsoft.TeamFoundation.Build.WebApi.Folder
Function Remove-TfsBuildDefinitionFolder
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

        # Remove folder containing builds
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
        $folders = Get-TfsBuildFolder -Folder $Folder -Project $Project -Collection $Collection

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
                $subFolders = (Get-TfsBuildFolder -Folder $path -Project $Project -Collection $Collection)

                if($subFolders.Count -gt 0)
                {
                    _throw "Folder '$($f.Path)' has $($subFolders.Count) sub-folder(s). To delete it, use the -Recurse argument."
                }

                _Log "Folder '$($f.Path)' has no sub-folders"
            }

            GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$f.Project.Name)

            GET_CLIENT('Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient')

            if(-not $Force.IsPresent)
            {
                _Log "Force argument not set. Check if folder '$($f.Path)' has build definitions"

                CALL_ASYNC($client.GetDefinitionsAsync2($tp.Name, [string]$null, [string]$null, [string]$null, [Microsoft.TeamFoundation.Build.WebApi.DefinitionQueryOrder]::None, $null, $null, $null, $null, $f.Path), "Error fetching build definitions in folder '$($f.Path)'")

                if($result.Count -gt 0)
                {
                    _throw "Folder '$($f.Path)' has $($result.Count) build definition(s). To delete it, use the -Force argument."
                }

                _Log "Folder '$($f.Path)' has no build definitions"
            }

            CALL_ASYNC($client.DeleteFolderAsync($tp.Name, $f.Path))
        }
    }
}
