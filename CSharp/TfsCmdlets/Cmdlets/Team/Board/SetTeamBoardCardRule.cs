using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Set the card rule settings of the specified backlog board.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsTeamBoardCardRuleSetting", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(BoardCardRuleSettings))]
    public class SetTeamBoardCardRule : SetCmdletBase<WebApiBoard>
    {
        /// <summary>
        /// Specifies the board name. Wildcards are supported. When omitted, returns card rules 
        /// for all boards in the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        public object Board { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Team { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter()]
        //         public object Board { get; set; }

        //         [Parameter(ParameterSetName="Bulk set")]
        //         [Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings]
        //         Rules,

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public string CardStyleRuleName { get; set; }

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public string CardStyleRuleFilter { get; set; }

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public hashtable CardStyleRuleSettings { get; set; }

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public string TagStyleRuleName { get; set; }

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public string TagStyleRuleFilter { get; set; }

        //         [Parameter(ParameterSetName="Set individual rules")]
        //         public hashtable TagStyleRuleSettings { get; set; }

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Team { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.WebApi"
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
        //     }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         Write-Verbose $"Getting card rules for team {Team}"

        //         if(Board is Microsoft.TeamFoundation.Work.WebApi.Board)
        //         {
        //             boards = @(Board.Name)
        //             Team = ([Uri] b.Links.Links.team.Href).Segments[-1]
        //             Project = ([Uri] b.Links.Links.project.Href).Segments[-1]
        //         }
        //         elseif (Board.ToString().Contains("*"))
        //         {
        //             boards = (Get-TfsTeamBoard -Board Board -Team Team -Project Project -Collection Collection).Name
        //         }
        //         else
        //         {
        //             boards = @(Board)
        //         }

        //         t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team '{Team}'."}; if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
        //         var client = GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();

        //         foreach(boardName in boards)
        //         {
        //             if(! ShouldProcess(boardName, "Set board card rule settings"))
        //             {
        //                 continue
        //             }

        //             ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name)

        //             task = client.GetBoardCardRuleSettingsAsync(ctx,boardName); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving card rule settings for board '{Board}'" task.Exception.InnerExceptions })

        //             Write-Output result `
        //                 | Add-Member -Name "Team" -MemberType NoteProperty -Value t.Name -PassThru `
        //                 | Add-Member -Name "Project" -MemberType NoteProperty -Value tp.Name -PassThru
        //         }
        //     }
        // }
    }
}
