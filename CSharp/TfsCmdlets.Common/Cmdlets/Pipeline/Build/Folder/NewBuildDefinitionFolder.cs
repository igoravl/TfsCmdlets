using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    [Cmdlet(VerbsCommon.New, "BuildDefinitionFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.Folder))]
    public class NewBuildDefinitionFolder : BaseCmdlet
    {
        /*
                # Specifies the folder path
                [Parameter(Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Path")]
                public object Folder { get; set; }

                # Description of the new build folder
                [Parameter()]
                public string Description { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void ProcessRecord()
            {
                tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                if(! ShouldProcess(tp.Name, $"Create build folder "{Folder}""))
                {
                    return
                }

                var client = tpc.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

                newFolder = new Microsoft.TeamFoundation.Build.WebApi.Folder() -Property @{
                    Description = Description
                }

                Folder = Folder.ToString().Trim("\")

                task = client.CreateFolderAsync(newFolder, tp.Name, $"\{Folder}"); result = task.Result; if(task.IsFaulted) { _throw new Exception( "Error creating folder "Folder"" task.Exception.InnerExceptions })

                if(Passthru)
                {
                    WriteObject(result); return;
                }
            }
        }
        */
    }
}
