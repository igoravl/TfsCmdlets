namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IKnownWorkItemLinkTypes)), Shared]
    public class KnownWorkItemLinkTypes : IKnownWorkItemLinkTypes
    {
        public IDictionary<WorkItemLinkType, string> LinkTypes { get; } = new Dictionary<WorkItemLinkType, string>()
        {
            [WorkItemLinkType.Parent] = "System.LinkTypes.Hierarchy-Reverse",
            [WorkItemLinkType.Child] = "System.LinkTypes.Hierarchy-Forward",
            [WorkItemLinkType.Related] = "System.LinkTypes.Related",
            [WorkItemLinkType.Predecessor] = "System.LinkTypes.Dependency-Reverse",
            [WorkItemLinkType.Successor] = "System.LinkTypes.Dependency-Forward",
            [WorkItemLinkType.Duplicate] = "System.LinkTypes.Duplicate-Forward",
            [WorkItemLinkType.DuplicateOf] = "System.LinkTypes.Duplicate-Reverse",
            [WorkItemLinkType.Tests] = "System.VSTS.Common.TestedBy-Reverse",
            [WorkItemLinkType.TestedBy] = "System.VSTS.Common.TestedBy-Forward",
            [WorkItemLinkType.TestCase] = "Microsoft.VSTS.TestCase.SharedStepReferencedBy-Forward",
            [WorkItemLinkType.SharedSteps] = "Microsoft.VSTS.TestCase.SharedStepReferencedBy-Reverse",
            [WorkItemLinkType.References] = "Microsoft.VSTS.TestCase.SharedParameterReferencedBy-Reverse",
            [WorkItemLinkType.ReferencedBy] = "Microsoft.VSTS.TestCase.SharedParameterReferencedBy-Forward",
            [WorkItemLinkType.ProducesFor] = "System.LinkTypes.Remote.Dependency-Forward",
            [WorkItemLinkType.ConsumesFrom] = "System.LinkTypes.Remote.Dependency-Reverse",
            [WorkItemLinkType.RemoteRelated] = "System.LinkTypes.Remote.Related",
            [WorkItemLinkType.AttachedFile] = "AttachedFile",
            [WorkItemLinkType.Hyperlink] = "Hyperlink",
            [WorkItemLinkType.ArtifactLink] = "ArtifactLink"
        };

        public WorkItemLinkType GetLinkType(string referenceName)
            => LinkTypes.First(x => x.Value.Equals(referenceName, StringComparison.OrdinalIgnoreCase)).Key;

        public string GetReferenceName(WorkItemLinkType linkType)
            => LinkTypes[linkType];
    }
}