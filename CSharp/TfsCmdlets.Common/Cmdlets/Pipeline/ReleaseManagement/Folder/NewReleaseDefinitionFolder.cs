using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.Folder
{
    /// <summary>
    /// Creates a new release definition folder.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsReleaseDefinitionFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public class NewReleaseDefinitionFolder : BaseCmdlet
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();


        //         # Specifies the folder path
        //         [Parameter(Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
        //         [Alias("Path")]
        //         public object Folder { get; set; }

        //         # Description of the new release folder
        //         [Parameter()]
        //         public string Description { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Passthru { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         if(! ShouldProcess(tp.Name, $"Create release folder "{Folder}""))
        //         {
        //             return
        //         }

        //         var client = GetClient<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient>();

        //         newFolder = new Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder() -Property @{
        //             Description = Description
        //             Path = $"\{{Folder}.ToString(}.Trim("\"))"
        //         }

        //         task = client.CreateFolderAsync(newFolder, tp.Name); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error creating folder "{Folder}"" task.Exception.InnerExceptions })

        //         if(Passthru)
        //         {
        //             WriteObject(result); return;
        //         }
        //     }
        // }
    }
}
