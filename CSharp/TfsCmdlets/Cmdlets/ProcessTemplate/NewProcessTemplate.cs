using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using TfsCmdlets.Extensions;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Creates a new inherited process.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsProcessTemplate", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiProcess))]
    public partial class NewProcessTemplate : NewCmdletBase<WebApiProcess>
    {
        /// <summary>
        /// Specifies the name of the process to create.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name")]
        public string ProcessTemplate { get; set; }

        /// <summary>
        /// Specifies the description of the new process.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the reference name of the new process. When omitted, a random name 
        /// will be automatically generated and assigned by the server.
        /// </summary>
        [Parameter()]
        public string ReferenceName { get; set; }

        /// <summary>
        /// Specifies the name of the parent process from which the new process will inherit.
        /// </summary>
        [Parameter()]
        public object Parent { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing process.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class ProcessDataService
    {
        protected override WebApiProcess DoNewItem()
        {
            var tpc = GetCollection();

            var process = GetParameter<string>(nameof(NewProcessTemplate.ProcessTemplate));
            var description = GetParameter<string>(nameof(NewProcessTemplate.Description));
            var referenceName = GetParameter<string>(nameof(NewProcessTemplate.ReferenceName));
            var parent = GetItem<WebApiProcess>(new { ProcessTemplate = GetParameter<object>(nameof(NewProcessTemplate.Parent)) });
            var exists = TestItem<WebApiProcess>();
            var force = GetParameter<bool>(nameof(NewProcessTemplate.Force));

            if (!ShouldProcess(tpc, $"{(exists ? "Overwrite" : "Create")} process '{process}', inheriting from '{parent.Name}'")) return null;

            if (exists && !(force || ShouldContinue($"Are you sure you want to overwrite existing process '{process}'?"))) return null;

            var client = GetClient<WorkItemTrackingProcessHttpClient>();

            var tmpProcessName = exists? $"{process}_{(new Random().Next()):X}": process;

            var newProcess = client.CreateNewProcessAsync(new CreateProcessModel()
            {
                Name = tmpProcessName,
                Description = description,
                ParentProcessTypeId = parent.Id,
                ReferenceName = referenceName
            }).GetResult($"Error creating process '{tmpProcessName}'");

            if(exists)
            {
                RemoveItem();
                Parameters["ProcessTemplate"] = tmpProcessName;
                Parameters["NewName"] = process;
                RenameItem();
            }

            return GetItem<WebApiProcess>(new { ProcessTemplate = process });
        }
    }
}