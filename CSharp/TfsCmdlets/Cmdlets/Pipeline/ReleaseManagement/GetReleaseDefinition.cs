using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets information from one or more release definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(ReleaseDefinition))]
    partial class GetReleaseDefinition
    {
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Definition { get; set; } = "*";
    }
}