using WebApiVariableGroup = Microsoft.TeamFoundation.DistributedTask.WebApi.VariableGroup;

namespace TfsCmdlets.Models.Pipeline
{
    /// <summary>
    /// Encapsulates the backlog level configuration object
    /// </summary>
    public class VariableGroup : ModelBase<WebApiVariableGroup>
    {
        public VariableGroup(WebApiVariableGroup original, WebApiTeamProject project)
            : base(original)
        {
            this.SetProperty(nameof(Project), project);
        }

        public string Name => InnerObject.Name;

        public string Project => (string)this.GetProperty(nameof(Project)).Value;

    }
}