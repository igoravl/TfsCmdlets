using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    [Cmdlet(VerbsCommon.Get, "BuildDefinitionFolder")]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.Folder))]
    public class GetBuildDefinitionFolder: BaseCmdlet
    {
/*
        # Specifies the folder path
        [Parameter(Position=0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; } = "**",

        # Query order
        [Parameter()]
        [Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder]
        QueryOrder = Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder.None,

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }
    
    protected override void ProcessRecord()
    {
        if (Folder is Microsoft.TeamFoundation.Build.WebApi.Folder) { this.Log("Input item is of type Microsoft.TeamFoundation.Build.WebApi.Folder; returning input item immediately, without further processing."; WriteObject(Folder }); return;);

        if(Folder.Project.Name) {Project = Folder.Project.Name}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

        if(_IsWildCard Folder)
        {
            task = client.GetFoldersAsync(tp.Name, "\", QueryOrder); result = task.Result; if(task.IsFaulted) { _throw new Exception( task.Exception.InnerExceptions })
            WriteObject(result | Where-Object { (_.Path -Like Folder) || (_.Name -like Folder) }); return;
        }

        
        task = client.GetFoldersAsync(tp.Name, $"\{{Folder}.Trim("\"})", QueryOrder); result = task.Result; if(task.IsFaulted) { _throw new Exception( "Error fetching build folders" task.Exception.InnerExceptions })
        
        WriteObject(result); return;
    }
}
*/
}
}
