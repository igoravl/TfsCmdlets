using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.Folder
{
    [Cmdlet(VerbsCommon.Get, "ReleaseDefinitionFolder")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public class GetReleaseDefinitionFolder: BaseCmdlet
    {
/*
        # Specifies the folder path
        [Parameter(Position=0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; } = "**",

        # Query order
        [Parameter()]
        [Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder]
        QueryOrder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder.None,

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }
    
    protected override void ProcessRecord()
    {
        if (Folder is Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder) { this.Log("Input item is of type Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder; returning input item immediately, without further processing."; WriteObject(Folder }); return;);

        tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient" -Collection tpc

        if(_IsWildCard Folder)
        {
            task = client.GetFoldersAsync(tp.Name, "\", QueryOrder); result = task.Result; if(task.IsFaulted) { _throw new Exception( task.Exception.InnerExceptions })
            result = result | Where-Object { (_.Path -Like Folder) || (_.Name -like Folder) }
        }
        else
        {
            task = client.GetFoldersAsync(tp.Name, $"\{{Folder}.Trim("\"})", QueryOrder); result = task.Result; if(task.IsFaulted) { _throw new Exception( "Error fetching build folders" task.Exception.InnerExceptions })
        }
        
        WriteObject(result | Add-Member -Name Project -MemberType NoteProperty -PassThru -Value (new Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference() -Property @{); return;
            Name = tp.Name
        })
    }
}
*/
}
}
