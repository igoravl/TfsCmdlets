using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    internal abstract class ToggleWorkItemTagController: ControllerBase
    {
        [Import]
        private ITaggingHttpClient Client { get; set; }
        
        protected override IEnumerable Run()
        {
            var tags = Data.GetItems<WebApiTagDefinition>();
            var tp = Data.GetProject();
            var enabled = Parameters.Get<bool>("Enabled");

            foreach (var tag in tags)
            {
                if (!PowerShell.ShouldProcess(tp, $"{(enabled? "Enable": "Disable")} work item tag '{tag.Name}'")) continue;

                yield return Client.UpdateTagAsync(tp.Id, tag.Id, tag.Name, enabled)
                    .GetResult($"Error renaming work item tag '{tag.Name}'");
            }
        }

        [ImportingConstructor]
        protected ToggleWorkItemTagController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}