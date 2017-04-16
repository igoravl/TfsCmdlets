using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq;

namespace TfsCmdlets
{
    public static class QueryHelper
    {
        public static QueryDefinition GetQueryDefinitionFromPath(QueryFolder folder, string path)
        {
            return folder.Select<QueryItem, QueryDefinition>(item =>
            {
                return item.Path == path ?
                    item as QueryDefinition : item is QueryFolder ?
                    GetQueryDefinitionFromPath(item as QueryFolder, path) : null;
            })
            .FirstOrDefault(item => item != null);
        }

        public static QueryFolder GetQueryFolderFromPath(QueryFolder folder, string path)
        {
            return folder.Select<QueryItem, QueryFolder>(item =>
            {
                return item.Path == path ?
                    item as QueryFolder : item is QueryFolder ?
                    GetQueryFolderFromPath(item as QueryFolder, path) : null;
            })
            .FirstOrDefault(item => item != null);
        }

        public static QueryFolder CreateFolder(string path)
        {
            
        }
    }
}
