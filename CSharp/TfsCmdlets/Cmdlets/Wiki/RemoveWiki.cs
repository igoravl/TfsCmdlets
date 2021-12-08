using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWiki", SupportsShouldProcess = true, DefaultParameterSetName = "Remove code wiki")]
    [TfsCmdlet(CmdletScope.Project)]
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