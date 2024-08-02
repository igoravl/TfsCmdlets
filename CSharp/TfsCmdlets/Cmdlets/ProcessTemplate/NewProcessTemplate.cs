using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Creates a new inherited process.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(WebApiProcess))]
    partial class NewProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process to create.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name", "Process")]
        public string ProcessTemplate { get; set; }

        /// <summary>
        /// Specifies the description of the new process.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the reference name of the new process. When omitted, a random name 
        /// will be automatically generated and assigned by the server.
        /// </summary>
        [Parameter]
        public string ReferenceName { get; set; }

        /// <summary>
        /// Specifies the name of the parent process from which the new process will inherit.
        /// </summary>
        [Parameter(Mandatory = true)]
        public object Parent { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing process.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiProcess), Client=typeof(IWorkItemTrackingProcessHttpClient))]
    partial class NewProcessTemplateController
    {
        protected override IEnumerable Run()
        {
            var parent = GetItem<WebApiProcess>(new { ProcessTemplate = Parent });
            var exists = TestItem<WebApiProcess>();

            if (!PowerShell.ShouldProcess(Collection, $"{(exists ? "Overwrite" : "Create")} process '{ProcessTemplate}', inheriting from '{parent.Name}'")) yield break;

            if (exists && !(Force || PowerShell.ShouldContinue($"Are you sure you want to overwrite existing process '{ProcessTemplate}'?"))) yield break;

            var tmpProcessName = exists ? $"{ProcessTemplate}_{new Random().Next():X}" : ProcessTemplate;

            Client.CreateNewProcessAsync(new CreateProcessModel() {
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
