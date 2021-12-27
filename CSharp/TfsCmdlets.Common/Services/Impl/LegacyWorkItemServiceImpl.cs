using System.Xml.Linq;

namespace TfsCmdlets.Services
{
    [Export(typeof(IWorkItemStore))]
    public class WorkItemStoreImpl : IWorkItemStore
    {
        public XDocument ExportGlobalLists()
        {
            throw new NotImplementedException();
        }

        public string ExportWorkItemType(string projectName, string workItemTypeName, bool includeGlobalLists)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetWorkItemTypes(string projectName)
        {
            throw new NotImplementedException();
        }

        public string ImportWorkItemType(string projectName, string xmlDefinition)
        {
            throw new NotImplementedException();
        }

        [ImportingConstructor]
        public WorkItemStoreImpl()
        {
        }
    }
}