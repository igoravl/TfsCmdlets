using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;

namespace TfsCmdlets.Controllers.ProcessTemplate
{
    [CmdletController(typeof(WebApiProcess))]
    partial class NewProcessTemplateController
    {
        protected override IEnumerable Run()
        {
            var parent = GetItem<WebApiProcess>(new { ProcessTemplate = Parent });
            var exists = TestItem<WebApiProcess>();

            if (!PowerShell.ShouldProcess(Collection, $"{(exists ? "Overwrite" : "Create")} process '{ProcessTemplate}', inheriting from '{parent.Name}'")) yield break;

            if (exists && !(Force || PowerShell.ShouldContinue($"Are you sure you want to overwrite existing process '{ProcessTemplate}'?"))) yield break;

            var client = Data.GetClient<WorkItemTrackingProcessHttpClient>();

            var tmpProcessName = exists ? $"{ProcessTemplate}_{new Random().Next():X}" : ProcessTemplate;

            client.CreateNewProcessAsync(new CreateProcessModel() {
                    Name = tmpProcessName,
                    Description = Description,
                    ParentProcessTypeId = parent.Id,
                    ReferenceName = ReferenceName})
                .GetResult($"Error creating process '{tmpProcessName}'");

            if (exists)
            {
                Data.RemoveItem<WebApiProcess>();
                Data.RenameItem<WebApiProcess>(new { ProcessTemplate = tmpProcessName, NewName = ProcessTemplate });
            }

            yield return GetItem<WebApiProcess>();
        }
    }
}