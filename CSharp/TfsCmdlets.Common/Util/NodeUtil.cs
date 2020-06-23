using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Util
{
    internal static class NodeUtil
    {
        internal static string NormalizeNodePath(string path, string projectName, string scope = "",
            bool includeScope = false, bool excludePath = false, bool includeLeadingSeparator = false,
            bool includeTrailingSeparator = false, bool includeTeamProject = false, char separator = '\\')
        {
            // Log "Normalizing path 'Path' with arguments (_DumpObj PSBoundParameters)"


            var newPath = new StringBuilder();

            if (includeLeadingSeparator) { newPath.Append(separator); }
            if (includeTeamProject) { newPath.Append(projectName); newPath.Append(separator); }
            if (includeScope) { newPath.Append(scope); newPath.Append(separator); }

            if (!excludePath)
            {
                path = Regex.Replace(path, @"[/|\\]+", separator.ToString()).Trim(' ', separator);

                if (path.Equals(projectName) || path.StartsWith($"{projectName}{separator}"))
                {
                    if (Regex.IsMatch(path, $@"^{projectName}\{separator}{scope}\{separator}"))
                    {
                        path = path.Substring($"{projectName}{separator}{scope}{separator}".Length);
                    }
                    if (Regex.IsMatch(path, $@"^{projectName}\{separator}"))
                    {
                        path = path.Substring($"{projectName}{separator}".Length);
                    }
                    else if (path.Equals(projectName, StringComparison.OrdinalIgnoreCase))
                    {
                        path = "";
                    }
                }
                else if (path.Equals(scope) || path.StartsWith($"{scope}{separator}"))
                {
                    if (Regex.IsMatch(path, $@"^{scope}{separator}"))
                    {
                        path = path.Substring(path.IndexOf(separator) + 1);
                    }
                    else if (path.Equals(scope, StringComparison.OrdinalIgnoreCase))
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
            else if (!includeTrailingSeparator && newPath.Length > 0 && newPath[newPath.Length - 1].Equals(separator))
            {
                newPath.Remove(newPath.Length - 1, 1);
            }

            return newPath.ToString();
        }
    }
}