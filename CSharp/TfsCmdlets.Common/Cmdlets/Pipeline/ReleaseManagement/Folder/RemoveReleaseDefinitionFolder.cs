using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.Folder
{
    [Cmdlet(VerbsCommon.Remove, "ReleaseDefinitionFolder", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public class RemoveReleaseDefinitionFolder : BaseCmdlet
    {
        /*
                # Specifies the folder path
                [Parameter(Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Path")]
                [SupportsWildcards()]
                public object Folder { get; set; }

                # Remove folders recursively
                [Parameter()]
                public SwitchParameter Recurse { get; set; }

                # Remove folder containing Releases
                [Parameter()]
                public SwitchParameter Force { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

            protected override void ProcessRecord()
            {
                folders = Get-TfsReleaseDefinitionFolder -Folder Folder -Project Project -Collection Collection

                foreach(f in folders)
                {
                    if(! ShouldProcess(f.Project.Name, $"Remove folder "{{f}.Path}""))
                    {
                        continue
                    }

                    if(! Recurse.IsPresent)
                    {
                        this.Log($"Recurse argument not set. Check if folder "{{f}.Path}" has sub-folders");

                        path = $"{{f}.Path.TrimEnd("\"})\**"
                        subFolders = (Get-TfsReleaseDefinitionFolder -Folder path -Project Project -Collection Collection)

                        if(subFolders.Count -gt 0)
                        {
                            _throw new Exception($"Folder "{{f}.Path}" has $(subFolders.Count) sub-folder(s). To delete it, use the -Recurse argument.")
                        }

                        this.Log($"Folder "{{f}.Path}" has no sub-folders");
                    }

                    if(f.Project.Name) {Project = f.Project.Name}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                    client = Get-TfsRestClient "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient" -Collection tpc

                    if(! Force.IsPresent)
                    {
                        this.Log($"Force argument not set. Check if folder "{{f}.Path}" has release definitions");

                        task = client.GetReleaseDefinitionsAsync(tp.Name, string]null, [Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts.ReleaseDefinitionExpands.None, [string]null, null, null, null, null, f.Path); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error fetching release definitions in folder "{{f}.Path}"" task.Exception.InnerExceptions })

                        if(result.Count -gt 0)
                        {
                            _throw new Exception($"Folder "{{f}.Path}" has $(result.Count) release definition(s). To delete it, use the -Force argument.")
                        }

                        this.Log($"Folder "{{f}.Path}" has no release definitions");
                    }

                    task = client.DeleteFolderAsync(tp.Name, f.Path); result = task.Result; if(task.IsFaulted) { _throw new Exception( task.Exception.InnerExceptions })
                }
            }
        }
        */
    }
}
