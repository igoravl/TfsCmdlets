using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Work
{
    [Cmdlet(VerbsCommon.Get, "TeamBoardCardRuleSetting")]
    [OutputType(typeof(BoardCardRuleSettings))]
    public class GetTeamBoardCardRuleSetting: BaseCmdlet
    {
/*
        [Parameter()]
        [SupportsWildcards()]
        public object Board { get; set; } = "*",

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
        if(Board is Microsoft.TeamFoundation.Work.WebApi.Board)
        {
            boards = @(Board.Name)
            Team = ([Uri] b.Links.Links.team.Href).Segments[-1]
            Project = ([Uri] b.Links.Links.project.Href).Segments[-1]

            this.Log($"Getting card rules for board {{Board}.Name} in team Team");
        }
        elseif (Board.ToString().Contains("*"))
        {
            this.Log($"Getting card rules for boards matching "{Board}" in team Team");

            boards = (Get-TfsTeamBoard -Board Board -SkipDetails -Team Team -Project Project -Collection Collection).Name

            this.Log($"{{boards}.Count} board(s) found matching "Board"");
        }
        else
        {
            this.Log($"Getting card rules for board {{Board}.Name} in team Team");

            boards = @(Board)
        }

        t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
        client = Get-TfsRestClient "Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient" -Collection tpc

        foreach(boardName in boards)
        {
            ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name)

            this.Log($"Fetching card rule settings for board {boardName}");
            
            task = client.GetBoardCardRuleSettingsAsync(ctx,boardName); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving card rule settings for board "{Board}"" task.Exception.InnerExceptions })

            Write-Output result `
                | Add-Member -Name "Team" -MemberType NoteProperty -Value t.Name -PassThru `
                | Add-Member -Name "Project" -MemberType NoteProperty -Value tp.Name -PassThru
        }
    }
}
*/
}
}
