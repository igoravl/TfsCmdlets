using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(GetClassificationNodeController))]
    partial class GetAreaController { }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(GetClassificationNodeController))]
    partial class GetIterationController { }

    internal abstract class GetClassificationNodeController: ControllerBase
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        protected override IEnumerable Run()
        {
            var node = Parameters.Get<object>("Node");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var tp = Data.GetProject();
            string path;

            switch (node)
            {
                case WorkItemClassificationNode n:
                    {
                        yield return new ClassificationNode(n, tp.Name, null);
                        yield break;
                    }
                case string s when s.Equals("\\") || s.Equals("/"):
                    {
                        path = "\\";
                        break;
                    }
                case string s when !string.IsNullOrEmpty(s) && s.IsWildcard():
                    {
                        path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'), 
                            includeScope: true, 
                            excludePath: false, 
                            includeLeadingSeparator: true, 
                            includeTrailingSeparator: false, 
                            includeTeamProject: true);
                        break;
                    }
                case string s when !string.IsNullOrEmpty(s):
                    {
                        path = NodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'), 
                            includeScope: false, 
                            excludePath: false, 
                            includeLeadingSeparator: true, 
                            includeTrailingSeparator: false, 
                            includeTeamProject: false);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid or non-existent node {node}");
                    }
            }

            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var depth = 2;

            if (path.IsWildcard())
            {
                depth = 4;
                Logger.Log($"Preparing to recursively search for pattern '{path}'");

                var root = new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, "\\", depth)
                        .GetResult($"Error retrieving {structureGroup} from path '{path}'"),
                    tp.Name, client);

                foreach (var n in root.GetChildren(path))
                {
                    yield return n;
                }

                yield break;
            }

            Logger.Log($"Getting {structureGroup} under path '{path}'");

            yield return new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, path, depth)
                .GetResult($"Error retrieving {structureGroup} from path '{path}'"), tp.Name, null);
        }

        [ImportingConstructor]
        protected GetClassificationNodeController(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
        }
    }
}