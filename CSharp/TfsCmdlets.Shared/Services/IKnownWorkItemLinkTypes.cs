namespace TfsCmdlets.Services
{
    public interface IKnownWorkItemLinkTypes
    {
        string GetReferenceName(WorkItemLinkType linkType);

        WorkItemLinkType GetLinkType(string referenceName);

        IDictionary<WorkItemLinkType, string> LinkTypes { get; }
    }
}