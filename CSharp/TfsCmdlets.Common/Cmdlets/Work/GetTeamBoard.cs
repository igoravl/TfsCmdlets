using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Work
{
    [Cmdlet(VerbsCommon.Get, "TeamBoard")]
    [OutputType(typeof(Board))]
    public class GetTeamBoard: BaseCmdlet
    {
/*
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Board { get; set; } = "*";

        [Parameter()]
        public SwitchParameter SkipDetails { get; set; }

        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.WebApi"
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
    }

    protected override void ProcessRecord()
    {
        if (Board is Microsoft.TeamFoundation.Work.WebApi.Board) { this.Log("Input item is of type Microsoft.TeamFoundation.Work.WebApi.Board; returning input item immediately, without further processing."; WriteObject(Board }); return;);

        t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();

        ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name)

        this.Log($"Getting boards matching "{Board}" in team "{t.Name}"");

        task = client.GetBoardsAsync(ctx); result = task.Result; if(task.IsFaulted) { _throw new Exception("Error retrieving team boards" task.Exception.InnerExceptions })

        boardRefs = result | Where-Object Name -like Board

        this.Log($"Found {{boardRefs}.Count} boards matching "Board" in team "$(t.Name)"");

        if(SkipDetails.IsPresent)
        {
            this.Log("SkipDetails switch is present. Returning board references without details");
            WriteObject(boardRefs); return;
        }

        foreach(b in boardRefs)
        {
            this.Log($"Fetching details for board "{{b}.Name}"");

            task = client.GetBoardAsync(ctx, b.Id); result = task.Result; if(task.IsFaulted) { _throw new Exception("Error fetching board data" task.Exception.InnerExceptions })
            Write-Output result
        }
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
