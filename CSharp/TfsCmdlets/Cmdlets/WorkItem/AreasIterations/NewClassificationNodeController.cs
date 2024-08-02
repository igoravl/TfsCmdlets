using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal abstract class NewClassificationNodeController: ControllerBase
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        [Import]
        private IWorkItemTrackingHttpClient Client { get; set; }

        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var node = Parameters.Get<string>("Node");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var force = Parameters.Get<bool>("Force");

            var nodeType = structureGroup.ToString().TrimEnd('s');
            var nodePath = NodeUtil.NormalizeNodePath(node, tp.Name, scope: nodeType, includeTeamProject: false);
            var parentPath = Path.GetDirectoryName(nodePath);
            var nodeName = Path.GetFileName(nodePath);

            if (!PowerShell.ShouldProcess($"Team Project {tp.Name}", $"Create node '{nodePath}'")) yield break;

            if (!string.IsNullOrEmpty(parentPath) && !Data.TestItem<ClassificationNode>(new { Node = parentPath }))
            {
                if (!force)
                {
                    Logger.LogError(new Exception($"Parent node '{parentPath}' does not exist. Check the path or use -Force the create any missing parent nodes."));
                    yield break;
                }

                Data.NewItem<ClassificationNode>(new { Node = parentPath });
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = nodeName
            };

            if (Parameters.HasParameter("StartDate"))
            {
                if (!Parameters.HasParameter("FinishDate"))
                {
                    Logger.LogError(new ArgumentException("When specifying iteration dates, both dates must be supplied."));
                    yield break;
                }

                var startDate = Parameters.Get<DateTime?>("StartDate");
                var finishDate = Parameters.Get<DateTime?>("FinishDate");

                Logger.Log($"Setting iteration dates to '{startDate}' and '{finishDate}'");

                patch.Attributes = new Dictionary<string, object>()
                {
                    ["startDate"] = startDate,
                    ["finishDate"] = finishDate
                };
            }

            var result = Client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, parentPath)
                .GetResult($"Error creating node {nodePath}");

            yield return new ClassificationNode(result, tp.Name, Client);
        }

        [ImportingConstructor]
        protected NewClassificationNodeController(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
        }
    }
}