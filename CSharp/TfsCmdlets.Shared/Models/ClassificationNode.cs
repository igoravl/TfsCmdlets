using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the area/iteration node object
    /// </summary>
    public class ClassificationNode : ModelBase<WorkItemClassificationNode>
    {
        private readonly string _rootPath;
        private readonly string _relativePath;

        public ClassificationNode(WorkItemClassificationNode n, string projectName)
            : base(n)
        {
            ProjectName = projectName;
            _rootPath = $"\\{ProjectName}\\{InnerObject.StructureType}";
            _relativePath = (_rootPath.Length == InnerObject.Path.Length) ?
                string.Empty : InnerObject.Path.Substring(_rootPath.Length + 1);
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