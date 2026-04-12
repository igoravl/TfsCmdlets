using System.Xml.Linq;

namespace TfsCmdlets.Services
{
    public interface IWorkItemStore
    {
        XDocument ExportGlobalLists();

        IEnumerable<object> GetWorkItemTypes(string projectName);

        string ExportWorkItemType(string projectName, string workItemTypeName, bool includeGlobalLists);

        string ImportWorkItemType(string projectName, string xmlDefinition);

    }
}