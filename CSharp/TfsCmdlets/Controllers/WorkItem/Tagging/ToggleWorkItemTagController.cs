using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.WorkItem.Tagging
{
    [CmdletController(typeof(WebApiTagDefinition), CustomBaseClass = typeof(ToggleWorkItemTagController))]
    partial class EnableWorkItemTagController { }

    [CmdletController(typeof(WebApiTagDefinition), CustomBaseClass = typeof(ToggleWorkItemTagController))]
    partial class DisableWorkItemTagController { }

    internal abstract class ToggleWorkItemTagController: ControllerBase<WebApiTagDefinition>
    {
        public override IEnumerable<WebApiTagDefinition> Invoke()
        {
            var tags = Data.GetItems<WebApiTagDefinition>();
            var tp = Data.GetProject();
            var enabled = Parameters.Get<bool>("Enabled");
            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            foreach (var tag in tags)
            {
                if (!PowerShell.ShouldProcess(tp, $"{(enabled? "Enable": "Disable")} work item tag '{tag.Name}'")) continue;

                yield return client.UpdateTagAsync(tp.Id, tag.Id, tag.Name, enabled)
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