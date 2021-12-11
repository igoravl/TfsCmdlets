using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Gets one or more Team Project Collection addresses registered in the current computer.
    /// </summary>
    [TfsCmdlet(CmdletScope.None, OutputType = typeof(string))]
    partial class GetRegisteredTeamProjectCollection
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string Collection { get; set; } = "*";
    }
}
