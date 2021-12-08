using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using TfsCmdlets.Extensions;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [CmdletController(typeof(WebApiProcess))]
    partial class NewProcessTemplateController
    {
        public override IEnumerable<WebApiProcess> Invoke()
        {
            throw new NotImplementedException();
            //    {
            //        var tpc = Collection;

            //        var process = parameters.Get<string>(nameof(NewProcessTemplate.ProcessTemplate));
            //        var description = parameters.Get<string>(nameof(NewProcessTemplate.Description));
            //        var referenceName = parameters.Get<string>(nameof(NewProcessTemplate.ReferenceName));
            //        var parent = Data.GetItem<WebApiProcess>(new { ProcessTemplate = parameters.Get<object>(nameof(NewProcessTemplate.Parent)) });
            //        var exists = TestItem<WebApiProcess>();
            //        var force = parameters.Get<bool>(nameof(NewProcessTemplate.Force));

            //        if (!PowerShell.ShouldProcess(tpc, $"{(exists ? "Overwrite" : "Create")} process '{process}', inheriting from '{parent.Name}'")) return null;

            //        if (exists && !(force || ShouldContinue($"Are you sure you want to overwrite existing process '{process}'?"))) return null;

            //        var client = Data.GetClient<WorkItemTrackingProcessHttpClient>();

            //        var tmpProcessName = exists? $"{process}_{(new Random().Next()):X}": process;

            //        var newProcess = client.CreateNewProcessAsync(new CreateProcessModel()
            //        {
            //            Name = tmpProcessName,
            //            Description = description,
            //            ParentProcessTypeId = parent.Id,
            //            ReferenceName = referenceName
            //        }).GetResult($"Error creating process '{tmpProcessName}'");

            //        if(exists)
            //        {
            //            RemoveItem();
            //            Parameters["ProcessTemplate"] = tmpProcessName;
            //            Parameters["NewName"] = process;
            //            RenameItem();
            //        }

            //        return GetItem<WebApiProcess>(new { ProcessTemplate = process });
            //    }
            //}
        }
    }
}