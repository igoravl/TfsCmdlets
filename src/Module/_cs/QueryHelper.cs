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
                return (path == null) || (item.Path == path) ?
                    item as QueryDefinition : 
                    item is QueryFolder ? GetQueryDefinitionFromPath(item as QueryFolder, path) : null;
            })
            .FirstOrDefault(item => item != null);
        }

        public static QueryFolder GetPersonalFolder(QueryHierarchy root)
        {
            return root.OfType<QueryFolder>().FirstOrDefault(item => item.IsPersonal);
        }

        public static QueryFolder GetSharedFolder(QueryHierarchy root)
        {
            return root.OfType<QueryFolder>().FirstOrDefault(item => !item.IsPersonal);
        }

        public static QueryFolder GetQueryFolderFromPath(QueryFolder folder, string path)
        {
            return folder.Select<QueryItem, QueryFolder>(item =>
            {
                return (path == null) || (item.Path == path) ?
                    item as QueryFolder : item is QueryFolder ?
                    GetQueryFolderFromPath(item as QueryFolder, path) : null;
            })
            .FirstOrDefault(item => item != null);
        }

        public static QueryFolder CreateFolder(QueryFolder rootFolder, string path)
        {
            var limit = path.Contains("/") ? path.IndexOf("/") : path.Length;
            var folderName = System.Text.RegularExpressions.Regex.Match(path, "([^/]*)").Groups[1].Value;

            if (!rootFolder.Contains(folderName))
            {
                var folder = new QueryFolder(folderName, rootFolder);
                rootFolder.Add(folder);
                rootFolder = folder;
            }
            else
            {
                rootFolder = (QueryFolder) rootFolder[folderName];
            }

            if (limit < path.Length)
            {
                return CreateFolder(rootFolder, path.Substring(limit + 1));
            }

            rootFolder.Project.QueryHierarchy.Save();

            return rootFolder;
        }
    }
}
