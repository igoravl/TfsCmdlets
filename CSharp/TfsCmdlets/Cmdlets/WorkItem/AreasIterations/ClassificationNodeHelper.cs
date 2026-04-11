using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal static class ClassificationNodeHelper
    {
        public static IEnumerable GetNodes(
            IEnumerable nodes,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            INodeUtil nodeUtil,
            IWorkItemTrackingHttpClient client,
            ILogger logger)
        {
            foreach (var node in nodes)
                foreach (var result in GetNode(node, structureGroup, tp, nodeUtil, client, logger))
                    yield return result;
        }

        private static IEnumerable GetNode(
            object node,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            INodeUtil nodeUtil,
            IWorkItemTrackingHttpClient client,
            ILogger logger)
        {
            string path;

            switch (node)
            {
                case WorkItemClassificationNode n:
                {
                    yield return new ClassificationNode(n, tp.Name);
                    yield break;
                }
                case string s when s.Equals("\\") || s.Equals("/"):
                {
                    path = "\\";
                    break;
                }
                case string s when !string.IsNullOrEmpty(s) && s.IsWildcard():
                {
                    path = nodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'),
                        includeScope: true,
                        excludePath: false,
                        includeLeadingSeparator: true,
                        includeTrailingSeparator: false,
                        includeTeamProject: true);
                    break;
                }
                case string s when !string.IsNullOrEmpty(s):
                {
                    path = nodeUtil.NormalizeNodePath(s, tp.Name, structureGroup.ToString().TrimEnd('s'),
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

            var depth = 2;

            if (path.IsWildcard())
            {
                depth = 4;
                logger.Log($"Preparing to recursively search for pattern '{path}'");

                var root = new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, "\\", depth)
                        .GetResult($"Error retrieving {structureGroup} from path '{path}'"),
                    tp.Name);

                foreach (var n in GetChildNodes(root, path, client))
                {
                    yield return n;
                }

                yield break;
            }

            logger.Log($"Getting {structureGroup} under path '{path}'");

            yield return new ClassificationNode(client.GetClassificationNodeAsync(tp.Name, structureGroup, path, depth)
                .GetResult($"Error retrieving {structureGroup} from path '{path}'"), tp.Name);
        }

        public static IEnumerable NewNode(
            string node,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            bool force,
            DateTime? startDate,
            DateTime? finishDate,
            bool hasStartDate,
            bool hasFinishDate,
            INodeUtil nodeUtil,
            IWorkItemTrackingHttpClient client,
            IPowerShellService powerShell,
            IDataManager data,
            ILogger logger)
        {
            var nodeType = structureGroup.ToString().TrimEnd('s');
            var nodePath = nodeUtil.NormalizeNodePath(node, tp.Name, scope: nodeType, includeTeamProject: false);
            var parentPath = Path.GetDirectoryName(nodePath);
            var nodeName = Path.GetFileName(nodePath);

            if (!powerShell.ShouldProcess($"Team Project {tp.Name}", $"Create node '{nodePath}'")) yield break;

            if (!string.IsNullOrEmpty(parentPath) && !data.TestItem<ClassificationNode>(new { Node = parentPath }))
            {
                if (!force)
                {
                    logger.LogError(new Exception($"Parent node '{parentPath}' does not exist. Check the path or use -Force the create any missing parent nodes."));
                    yield break;
                }

                data.NewItem<ClassificationNode>(new { Node = parentPath });
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = nodeName
            };

            if (hasStartDate)
            {
                if (!hasFinishDate)
                {
                    logger.LogError(new ArgumentException("When specifying iteration dates, both dates must be supplied."));
                    yield break;
                }

                logger.Log($"Setting iteration dates to '{startDate}' and '{finishDate}'");

                patch.Attributes = new Dictionary<string, object>()
                {
                    ["startDate"] = startDate,
                    ["finishDate"] = finishDate
                };
            }

            var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, parentPath)
                .GetResult($"Error creating node {nodePath}");

            yield return new ClassificationNode(result, tp.Name);
        }

        public static IEnumerable RemoveNodes(
            IEnumerable<ClassificationNode> nodes,
            object moveTo,
            bool recurse,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            IWorkItemTrackingHttpClient client,
            IPowerShellService powerShell,
            IDataManager data,
            ILogger logger)
        {
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var moveToNode = data.GetItem<ClassificationNode>(new { Node = moveTo });

            if (moveToNode == null)
            {
                logger.LogError(new Exception($"Invalid or non-existent node '{moveTo}'. To remove nodes, supply a valid node in the -MoveTo argument"));
                return null;
            }

            logger.Log($"Remove nodes and move orphaned work items to node '{moveToNode.FullPath}'");

            foreach (var node in nodes)
            {
                if (!powerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Remove {structureGroupName} '{node.RelativePath}'")) continue;

                if (node.ChildCount > 0 && !recurse && !powerShell.ShouldContinue($"The {structureGroupName} at '{node.RelativePath}' " +
                    "has children and the Recurse parameter was not specified. If you continue, all children will be removed with " +
                    "the item. Are you sure you want to continue?"))
                {
                    continue;
                }

                client.DeleteClassificationNodeAsync(node.TeamProject, structureGroup, node.RelativePath, moveToNode.Id)
                    .Wait($"Error removing node '{node.FullPath}'");
            }

            return null;
        }

        public static IEnumerable RenameNode(
            IEnumerable<ClassificationNode> nodesToRename,
            string newName,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            INodeUtil nodeUtil,
            IWorkItemTrackingHttpClient client,
            IPowerShellService powerShell)
        {
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            newName = nodeUtil.NormalizeNodePath(newName);

            if (newName.Contains("\\"))
            {
                throw new ArgumentException($"New name cannot contain backslashes: {newName}");
            }

            foreach (var nodeToRename in nodesToRename)
            {
                if (!powerShell.ShouldProcess($"{structureGroupName} '{nodeToRename.FullPath}'", $"Rename to '{newName}'"))
                {
                    continue;
                }

                var patch = new WorkItemClassificationNode()
                {
                    Name = newName,
                    Attributes = nodeToRename.Attributes
                };

                yield return new ClassificationNode(client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath)
                    .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name);
            }
        }

        public static IEnumerable MoveNodes(
            IEnumerable<ClassificationNode> sourceNodes,
            object destination,
            bool force,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            IWorkItemTrackingHttpClient client,
            IPowerShellService powerShell,
            IDataManager data,
            ILogger logger)
        {
            var projectName = tp.Name;

            if (sourceNodes == null) yield break;

            if (!data.TryGetItem<ClassificationNode>(out var destinationNode, new { Node = destination }))
            {
                if (!force)
                {
                    ErrorUtil.ThrowIfNotFound(destinationNode, "Destination", destination);
                    yield break;
                }

                logger.Log($"Creating missing destination node '{destinationNode}'");

                destinationNode = data.NewItem<ClassificationNode>(new { Node = destination });
            }

            logger.Log($"Destination node: '{destinationNode.FullPath}'");

            foreach (var sourceNode in sourceNodes)
            {
                logger.Log($"Source node: '{sourceNode.FullPath}'");

                if (!powerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'", $"Move {structureGroup} '{sourceNode.FullPath}'"))
                {
                    yield break;
                }

                var patch = new WorkItemClassificationNode()
                {
                    Id = sourceNode.Id
                };

                var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, destinationNode.RelativePath)
                    .GetResult($"Error moving node '{sourceNode.RelativePath}' to '{destinationNode.RelativePath}'");

                yield return new ClassificationNode(result, projectName);
            }
        }

        public static IEnumerable CopyNodes(
            IEnumerable<ClassificationNode> sourceNodes,
            object destination,
            object destinationProject,
            bool force,
            bool recurse,
            TreeStructureGroup structureGroup,
            WebApiTeamProject tp,
            IWorkItemTrackingHttpClient client,
            IPowerShellService powerShell,
            IDataManager data,
            ILogger logger)
        {
            var destProject = data.GetItem<WebApiTeamProject>(new { Project = destinationProject ?? tp });

            if (sourceNodes == null) yield break;

            ClassificationNode destinationNode = null;

            if (!data.TestItem<ClassificationNode>(new { Node = destination, Project = destProject }))
            {
                if (force)
                {
                    destinationNode = data.NewItem<ClassificationNode>(new { Node = destination, Project = destProject });
                }
                else
                {
                    ErrorUtil.ThrowIfNotFound(destinationNode, "Destination", destination);
                }
            }
            else
            {
                destinationNode = data.GetItem<ClassificationNode>(new { Node = destination, Project = destProject });
            }

            logger.Log($"Destination node: '{destinationNode.FullPath}'");

            foreach (var sourceNode in sourceNodes)
            {
                var nodePath = $@"{destinationNode.RelativePath}\{sourceNode.Name}";

                if (!powerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'",
                    $"Copy {structureGroup} '{sourceNode.FullPath}' to '{destProject.Name}\\{nodePath}'")) continue;

                if (!data.TestItem<ClassificationNode>(new { Node = nodePath, Project = destProject }))
                {
                    yield return data.NewItem<ClassificationNode>(new
                    {
                        Node = nodePath,
                        Project = destProject,
                        Force = true,
                        StartDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.StartDate : null),
                        FinishDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.FinishDate : null)
                    });
                }

                if (recurse)
                {
                    var children = GetChildNodes(sourceNode, "**", client);

                    foreach (var child in children)
                    {
                        nodePath = sourceNode.Name + "\\" + child.RelativePath.Substring(sourceNode.RelativePath.Length + 1);

                        if (!powerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'",
                            $"Copy {structureGroup} '{sourceNode.FullPath}' to '{destProject.Name}\\{nodePath}'")) continue;

                        yield return data.NewItem<ClassificationNode>(new
                        {
                            Node = $@"{destinationNode.RelativePath}\{nodePath}",
                            Project = destProject,
                            Force = true,
                            StartDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.StartDate : null),
                            FinishDate = (structureGroup == TreeStructureGroup.Iterations ? sourceNode.FinishDate : null)
                        });
                    }
                }
            }
        }

        public static IEnumerable TestNode(IDataManager data)
        {
            yield return data.TestItem<ClassificationNode>();
        }

        public static IEnumerable<ClassificationNode> GetChildNodes(
            ClassificationNode node,
            string pattern,
            IWorkItemTrackingHttpClient client)
        {
            return GetNodesRecursively(node, pattern, client);
        }

        private static IEnumerable<ClassificationNode> GetNodesRecursively(
            ClassificationNode node,
            string pattern,
            IWorkItemTrackingHttpClient client)
        {
            if (node.ChildCount == 0)
            {
                node = new ClassificationNode(client.GetClassificationNodeAsync(node.ProjectName, node.StructureGroup, node.RelativePath, 2)
                        .GetResult($"Error retrieving {node.StructureGroup} from path '{node.RelativePath}'"),
                    node.ProjectName);
            }

            if (node.ChildCount == 0) yield break;

            foreach (var c in node.Children.Select(n => new ClassificationNode(n, node.ProjectName)))
            {
                if (c.Path.IsLikeGlob(pattern)) yield return c;

                foreach (var n in GetNodesRecursively(c, pattern, client))
                {
                    yield return n;
                }
            }
        }
    }
}
