using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(Models.ClassificationNode), CustomBaseClass = typeof(CopyClassificationNodeController))]
    partial class CopyAreaController { }

    [CmdletController(typeof(Models.ClassificationNode), CustomBaseClass = typeof(CopyClassificationNodeController))]
    partial class CopyIterationController { }

    internal abstract class CopyClassificationNodeController: ControllerBase
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var projectName = tp.Name;
            var sourceNodes = Data.GetItems<ClassificationNode>();
            var destination = Parameters.Get<object>("Destination");
            var destinationProject = Data.GetProject(new { Project = Parameters.Get<object>("DestinationProject", tp) });
            var force = Parameters.Get<bool>("Force");
            var recurse = Parameters.Get<bool>("Recurse");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");

            if (sourceNodes == null) yield break;

            ClassificationNode destinationNode = null;

            if (!Data.TestItem<ClassificationNode>(new { Node = destination, Project = destinationProject }))
            {
                if (force)
                {
                    destinationNode = Data.NewItem<ClassificationNode>(new { Node = destination, Project = destinationProject });
                }
                else
                { 
                    ErrorUtil.ThrowIfNotFound(destinationNode, "Destination", destination); 
                }
            }
            else
            {
                destinationNode = Data.GetItem<ClassificationNode>(new { Node = destination, Project = destinationProject });
            }

            Logger.Log($"Destination node: '{destinationNode.FullPath}'");

            foreach (var sourceNode in sourceNodes)
            {
                var nodePath = $@"{destinationNode.RelativePath}\{sourceNode.Name}";

                if (!PowerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'",
                    $"Copy {structureGroup} '{sourceNode.FullPath}' to '{destinationProject.Name}\\{nodePath}'")) continue;

                if (!Data.TestItem<ClassificationNode>(new { Node = nodePath, Project = destinationProject }))
                {
                    yield return Data.NewItem<ClassificationNode>(new
                    {
                        Node = nodePath,
                        Project = destinationProject,
                        Force = true,
                        StartDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.StartDate : null),
                        FinishDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.FinishDate : null)
                    });
                }

                if (recurse)
                {
                    var children = sourceNode.GetChildren();

                    foreach (var child in children)
                    {
                        nodePath = sourceNode.Name + "\\" + child.RelativePath.Substring(sourceNode.RelativePath.Length + 1);

                        if (!PowerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'",
                            $"Copy {structureGroup} '{sourceNode.FullPath}' to '{destinationProject.Name}\\{nodePath}'")) continue;

                        yield return Data.NewItem<ClassificationNode>(new
                        {
                            Node = $@"{destinationNode.RelativePath}\{nodePath}",
                            Project = destinationProject,
                            Force = true,
                            StartDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.StartDate : null),
                            FinishDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.FinishDate : null)
                        });
                    }
                }
            }
        }

        [ImportingConstructor]
        protected CopyClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}