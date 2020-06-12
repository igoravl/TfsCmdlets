using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Attaches a team project collection database to a Team Foundation Server installation.
    /// </summary>
    [Cmdlet(VerbsData.Mount, "TfsTeamProjectCollection", ConfirmImpact = ConfirmImpact.Medium)]
    public partial class MountTeamProjectCollection : BaseCmdlet
    {

        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Name")]
        public string Collection { get; set; }

        [Parameter()]
        public string Description { get; set; }

        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseServer { get; set; }

        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseName { get; set; }

        [Parameter(ParameterSetName = "Use connection string", Mandatory = true)]
        public string ConnectionString { get; set; }

        [Parameter()]
        [ValidateSet("Started", "Stopped")]
        public string InitialState { get; set; } = "Started";

        [Parameter()]
        public SwitchParameter Clone { get; set; }

        [Parameter()]
        public int PollingInterval { get; set; } = 5;

        /// <summary>
        /// Specifies the maximum period of time this cmdlet should wait for the attach procedure 
        /// to complete. By default, it waits indefinitely until the collection servicing completes.
        /// </summary>
        [Parameter()]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Server;
    }
}