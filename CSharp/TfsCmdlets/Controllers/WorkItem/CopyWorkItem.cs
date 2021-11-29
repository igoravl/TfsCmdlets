using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Controllers;
using TfsCmdlets.Models;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    internal class CopyWorkItemController: ControllerBase<WebApiWorkItem>
    {
        public override IEnumerable<WebApiWorkItem> Invoke(ParameterDictionary parameters)
        {
            var wi = Data.GetItem<WebApiWorkItem>(parameters);

            var tp = Data.GetProject(parameters.Override(new
            {
                Project = (string) wi.Fields["System.TeamProject"]
            }));

            var destinationProject = Data.GetProject(parameters.Override(new
            {
                Project = parameters.Get<string>(nameof(CopyWorkItem.DestinationProject), tp.Name)
            }));

            var wit = Data.GetItem<WebApiWorkItemType>(parameters.Override(new
            {
                Type = parameters.Get<object>(nameof(CopyWorkItem.NewType), wi.Fields["System.WorkItemType"])
            }));

            if (!PowerShell.ShouldProcess($"Work item #{wi.Id} (\"{wi.Fields["System.Title"]}\")", "Create a copy of work item")) return null;

            var patch = new JsonPatchDocument();
            

            foreach (var argName in _parameterMap.Keys.Where(f => HasParameter(f) && !fields.ContainsKey(f)))
            {
                var value = parameters.Get<object>(argName);
                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = $"/fields/{_parameterMap[argName]}",
                    Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
                });
            }

            var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);

            //        return client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
            //            .GetResult("Error creating work item");

            //var flags = WorkItemCopyFlags.None;

            //if (IncludeAttachments)
            //{
            //    flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyFiles
            // }

            //if (IncludeLinks)
            //{
            //    flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyLinks
            // }

            //client.CreateWorkItemAsync()


            // copy = wi.Copy(witd, flags)

            // if (!SkipSave)
            //{
            //    copy.Save()
            // }

            //if (Passthru = = "Original")
            //{
            //    WriteObject(wi); return;
            //}

            //if (Passthru = = "Copy")
            //{
            //    WriteObject(copy); return;
            //}
        }
    }
}
