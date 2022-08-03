using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, DefaultParameterSetName = "Remove code wiki")]
    partial class RemoveWiki
    {
        /// <summary>
        /// Specifies the Wiki to be deleted.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Remove code wiki")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Wiki { get; set; }

        /// <summary>
        /// Deletes the provisioned ("project") Wiki of the specified Team Project.
        /// </summary>
        [Parameter(ParameterSetName = "Remove Project Wiki", Mandatory=true)]
        public SwitchParameter ProjectWiki { get; set; }
    }
}