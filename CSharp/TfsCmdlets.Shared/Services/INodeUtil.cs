namespace TfsCmdlets.Services
{
    public interface INodeUtil
    {
        string NormalizeNodePath(string path, string projectName = "", string scope = "",
            bool includeScope = false, bool excludePath = false, bool includeLeadingSeparator = false,
            bool includeTrailingSeparator = false, bool includeTeamProject = false, char separator = '\\');
    }
}