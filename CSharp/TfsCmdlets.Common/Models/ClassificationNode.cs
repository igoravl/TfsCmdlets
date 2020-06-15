using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WebApiNode = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the area/iteration node object
    /// </summary>
    public class ClassificationNode : PSObject
    {
        private readonly WorkItemTrackingHttpClient _client;

        internal ClassificationNode(WorkItemClassificationNode n,
            string projectName, WorkItemTrackingHttpClient client) : base(n)
        {
            ProjectName = projectName;
            _client = client;
            FixNodePath();
        }

        private WorkItemClassificationNode InnerNode => (WorkItemClassificationNode)BaseObject;

        internal string ProjectName { get; set; }

        internal TreeStructureGroup StructureGroup => (TreeStructureGroup)InnerNode.StructureType;

        internal string TeamProject => Path.Substring(1, Path.IndexOf('\\', 2)-1);

        internal string FullPath => Path.Replace($@"\{TeamProject}\{StructureGroup.ToString().TrimEnd('s')}", $@"\{TeamProject}");

        internal string Path => InnerNode.Path;

        internal string Name => InnerNode.Name;

        internal int Id => InnerNode.Id;

        /// <summary>
        /// Get a node's relative path
        /// </summary>
        internal string RelativePath => InnerNode.Path.Substring(InnerNode.Path.IndexOf('\\',
            InnerNode.Path.Substring(0, InnerNode.Path.IndexOf('\\', 1) + 1).Length));

        internal bool HasChildren => InnerNode.HasChildren != null && InnerNode.HasChildren.Value;

        internal int ChildCount => InnerNode?.Children?.ToList().Count ?? 0;

        internal IEnumerable<WorkItemClassificationNode> Children => InnerNode.Children;

        internal IEnumerable<ClassificationNode> GetChildren(string pattern = "**", bool recurse = true) =>
            GetNodesRecursively(this, pattern, recurse);

        private IEnumerable<ClassificationNode> GetNodesRecursively(ClassificationNode node, string pattern, bool recurse)
        {
            if (node.ChildCount == 0 && _client != null)
            {
                node = new ClassificationNode(_client.GetClassificationNodeAsync(ProjectName, StructureGroup, node.RelativePath, recurse ? 1 : 2)
                        .GetResult($"Error retrieving {StructureGroup} from path '{node.RelativePath}'"),
                    ProjectName, _client);
            }

            if (node.ChildCount == 0) yield break;

            foreach (var c in node.Children.Select(n => new ClassificationNode(n, ProjectName, _client)))
            {
                if(c.Path.IsLike(pattern)) yield return c;

                foreach (var n in GetNodesRecursively(c, pattern, true))
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Fill a missing node path. Older versions of the REST API don't populate the Path property.
        /// </summary>
        private void FixNodePath()
        {
            if (!string.IsNullOrEmpty(InnerNode.Path))
            {
                return;
            }

            var structureGroup = StructureGroup.ToString();
            var decodedUrl = Uri.UnescapeDataString(InnerNode.Url);
            var path = decodedUrl.Substring(decodedUrl.IndexOf($"/{StructureGroup}") + 1).Replace(structureGroup, "").Replace("/", "\\");
            InnerNode.Path = $@"\{ProjectName}\{structureGroup.TrimEnd('s')}{path}";
        }
    }
}