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
    public class ClassificationNode : ModelBase<WorkItemClassificationNode>
    {
        private readonly WorkItemTrackingHttpClient _client;
        private readonly string _rootPath;
        private readonly string _relativePath;

        public ClassificationNode(WorkItemClassificationNode n, string projectName, WorkItemTrackingHttpClient client)
            : base(n)
        {
            ProjectName = projectName;
            _rootPath = $"\\{ProjectName}\\{InnerObject.StructureType}";
            _relativePath = (_rootPath.Length == InnerObject.Path.Length) ?
                string.Empty : InnerObject.Path.Substring(_rootPath.Length + 1);
            _client = client;
            FixNodePath();
        }

        public string ProjectName { get; set; }

        public TreeStructureGroup StructureGroup => (TreeStructureGroup)InnerObject.StructureType;

        public string TeamProject => Path.Substring(1, Path.IndexOf('\\', 2)-1);

        public string FullPath => Path.Replace($@"\{TeamProject}\{StructureGroup.ToString().TrimEnd('s')}", $@"\{TeamProject}");

        public string Path => InnerObject.Path;

        public string Name => InnerObject.Name;

        public int Id => InnerObject.Id;

        public Guid Identifier => InnerObject.Identifier;

        public IDictionary<string,object> Attributes => InnerObject.Attributes;

        public string RelativePath => _relativePath;

        public bool HasChildren => InnerObject.HasChildren != null && InnerObject.HasChildren.Value;

        public int ChildCount => InnerObject?.Children?.ToList().Count ?? 0;

        public DateTime? StartDate => InnerObject.Attributes?["startDate"] as DateTime?;

        public DateTime? FinishDate => InnerObject.Attributes?["finishDate"] as DateTime?;

        public IEnumerable<WorkItemClassificationNode> Children => InnerObject.Children;

        public IEnumerable<ClassificationNode> GetChildren(string pattern = "**", bool recurse = true) =>
            GetNodesRecursively(this, pattern);

        private IEnumerable<ClassificationNode> GetNodesRecursively(ClassificationNode node, string pattern)
        {
            if (node.ChildCount == 0 && _client != null)
            {
                node = new ClassificationNode(_client.GetClassificationNodeAsync(ProjectName, StructureGroup, node.RelativePath, 2)
                        .GetResult($"Error retrieving {StructureGroup} from path '{node.RelativePath}'"),
                    ProjectName, _client);
            }

            if (node.ChildCount == 0) yield break;

            foreach (var c in node.Children.Select(n => new ClassificationNode(n, ProjectName, _client)))
            {
                if(c.Path.IsLike(pattern)) yield return c;

                foreach (var n in GetNodesRecursively(c, pattern))
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
            if (!string.IsNullOrEmpty(InnerObject.Path))
            {
                return;
            }

            var structureGroup = StructureGroup.ToString();
            var decodedUrl = Uri.UnescapeDataString(InnerObject.Url);
            var path = decodedUrl.Substring(decodedUrl.IndexOf($"/{StructureGroup}") + 1).Replace(structureGroup, "").Replace("/", "\\");
            InnerObject.Path = $@"\{ProjectName}\{structureGroup.TrimEnd('s')}{path}";
        }
    }
}