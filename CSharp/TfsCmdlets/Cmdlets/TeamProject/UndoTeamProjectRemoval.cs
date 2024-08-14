using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Undeletes one or more team projects. 
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class UndoTeamProjectRemoval
    {
        /// <summary>
        /// Specifies the name of the Team Project to undelete.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject))]
    partial class UndoTeamProjectRemovalController
    {
        [Import]
        private IRestApiService RestApiService { get; }

        protected override IEnumerable Run()
        {
            var project = Parameters.Get<object>("Project");
            var references = new List<WebApiTeamProjectRef>();

            switch (project)
            {
                case WebApiTeamProjectRef tpRef:
                {
                    references.Add(tpRef);
                    break;
                }
                case string s:
                {
                    references.AddRange(Data.GetItems<WebApiTeamProject>(new { Project = s, Deleted = true }));
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid team project '{project}'");
                }
            }

            foreach (var tp in references)
            {
                if (!PowerShell.ShouldProcess($"[Organization: {Collection.DisplayName}]/[Project: {tp.Name}]", "Restore deleted team project")) continue;

                RestApiService.InvokeAsync(
                        Data.GetCollection(),
                        $"/_apis/projects/{tp.Id}",
                        "PATCH",
                        $"{{\"state\":1,\"name\":\"{tp.Name}\"}}")
                    .GetResult($"Error restoring team project '{tp.Name}'");

                yield return Data.GetItem<WebApiTeamProject>(tp);
            }
        }
    }
}