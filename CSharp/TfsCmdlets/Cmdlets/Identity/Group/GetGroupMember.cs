using System;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets the members of a Azure DevOps group
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGroupMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetGroupMember
    {
        /// <summary>
        /// Specifies the group fom which to get its members.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Group { get; set; }

        /// <summary>
        /// Specifies the member (user or group) to get from the given group. Wildcards are supported.
        /// When omitted, all group members are returned.
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Member { get; set; } = "*";

        /// <summary>
        /// Recursively expands all member groups, returning the users and/or groups contained in them
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }
    }
}