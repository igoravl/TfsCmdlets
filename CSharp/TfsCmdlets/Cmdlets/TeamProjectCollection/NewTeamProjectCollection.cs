using System;
using System.Management.Automation;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Creates a new team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    [OutputType(typeof(Connection))]
    [DesktopOnly]
    public class NewTeamProjectCollection : CmdletBase
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public object Collection { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseServer { get; set; }

        [Parameter(ParameterSetName = "Use database server")]
        public string DatabaseName { get; set; }

        [Parameter(ParameterSetName = "Use connection string", Mandatory = true)]
        public string ConnectionString { get; set; }

        [Parameter]
        public SwitchParameter Default { get; set; }

        [Parameter]
        public SwitchParameter UseExistingDatabase { get; set; }

        [Parameter]
        [ValidateSet("Started", "Stopped")]
        public string InitialState { get; set; } = "Started";

        [Parameter] 
        [ValidateRange(5, 60)]
        public int PollingInterval { get; set; } = 5;

        [Parameter] 
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;

        [Parameter]
        public object Server { get; set; }

        [Parameter]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;

        [Parameter]
        public SwitchParameter Passthru { get; set; }

        // TODO


    }
}
