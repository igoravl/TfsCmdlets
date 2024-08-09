using System.Text.RegularExpressions;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(INodeUtil))]
    public class NodeUtilImpl : INodeUtil
    {
        public string NormalizeNodePath(string path, string projectName = "", string scope = "",
            bool includeScope = false, bool excludePath = false, bool includeLeadingSeparator = false,
            bool includeTrailingSeparator = false, bool includeTeamProject = false, char separator = '\\')
        {
            if (path == null) throw new ArgumentNullException("path");
            if (includeTeamProject && string.IsNullOrEmpty(projectName)) throw new ArgumentNullException("projectName");
            if (includeScope && string.IsNullOrEmpty(scope)) throw new ArgumentNullException("scope");
            if (excludePath && !includeScope && !includeTeamProject) throw new ArgumentException("excludePath is only valid when either includeScope or includeTeamProject are true");

            var newPath = new StringBuilder();

            var customSep = (separator != '/' && separator != '\\') ? $"|{separator}" : "";

            projectName = Regex.Replace(projectName?? string.Empty, @$"[/|\\{customSep}]+", separator.ToString()).Trim(separator);
            scope = Regex.Replace(scope?? string.Empty, @$"[/|\\{customSep}]+", separator.ToString()).Trim(separator);
            path = Regex.Replace(path?? string.Empty, @$"[/|\\{customSep}]+", separator.ToString()).Trim(separator);

            if (includeLeadingSeparator) { newPath.Append(separator); }
            if (includeTeamProject) { newPath.Append(projectName); newPath.Append(separator); }
            if (includeScope) { newPath.Append(scope); newPath.Append(separator); }

            if (!excludePath)
            {
                if (path.Equals(projectName, StringComparison.OrdinalIgnoreCase) || path.StartsWith($@"{projectName}{separator}", StringComparison.OrdinalIgnoreCase))
                {
                    if (path.Equals(projectName, StringComparison.OrdinalIgnoreCase))
                    {
                        path = "";
                    }
                    else{
                        var escapedProject = Regex.Escape(projectName);
                        var escapedScope = Regex.Escape(scope);
                        var escapedSep = Regex.Escape(separator.ToString());
                        var pattern = $@"^{escapedProject}{escapedSep}({escapedScope}{separator}?)?";

                        path = Regex.Replace(path, pattern, "");
                    }
                }
                else if (path.Equals(scope, StringComparison.OrdinalIgnoreCase) || path.StartsWith($"{scope}{separator}", StringComparison.OrdinalIgnoreCase))
                {
                    if(path.Length > scope.Length)
                    {
                        path = path.Substring(path.IndexOf(separator) + 1);
                    }
                    else
                    {
                        path = "";
                    }
                }

                newPath.Append(path);
            }

            if (includeTrailingSeparator && newPath.Length > 0 && !newPath[newPath.Length - 1].Equals(separator))
            {
                newPath.Append(separator);
            }
            else if (!includeTrailingSeparator && newPath.Length > 1 && newPath[newPath.Length - 1].Equals(separator))
            {
                newPath.Remove(newPath.Length - 1, 1);
            }

            return newPath.ToString();
        }
    }
}