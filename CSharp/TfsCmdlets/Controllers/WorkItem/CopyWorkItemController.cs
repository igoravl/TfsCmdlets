using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class CopyWorkItemController
    {
        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var wi = Data.GetItem<WebApiWorkItem>();
            var tp = Data.GetProject(new { Project = (string)wi.Fields["System.TeamProject"] });
            var destinationProject = Data.GetProject(new { Project = Parameters.Get<string>("DestinationProject", tp.Name) });
            var wit = Data.GetItem<WebApiWorkItemType>(new { Type = Parameters.Get<object>("NewType", wi.Fields["System.WorkItemType"]) });
            var fields = Parameters.Get<Hashtable>("Fields");

            if (!PowerShell.ShouldProcess($"Work item #{wi.Id} (\"{wi.Fields["System.Title"]}\")", "Create a copy of work item")) return null;

            var patch = new JsonPatchDocument();

            foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !fields.ContainsKey(f)))
            {
                var value = Parameters.Get<object>(argName);

                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = $"/fields/{Parameters.Get<object>(argName)}",
                    Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
                });
            }

            // var client = Data.GetClient<WorkItemTrackingHttpClient>();

            // return client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
            //     .GetResult("Error creating work item");

            // var flags = WorkItemCopyFlags.None;

            // if (IncludeAttachments)
            // {
            //     flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyFiles
            // }

            // if (IncludeLinks)
            // {
            //     flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyLinks
            // }

            // client.CreateWorkItemAsync();

            // var copy = wi.Copy(witd, flags);

            // if (!SkipSave) copy.Save();

            // yield return (passthru.Equals("Original"))? wi: copy;

            throw new NotImplementedException();
        }
    }
}
