using System;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsIdentity = TfsCmdlets.Services.Identity;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets the members of a Azure DevOps group
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGroupMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public class GetGroupMember : BaseCmdlet
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
        [Parameter()]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var group = GetInstanceOf<TfsIdentity>(new
            {
                Identity = Group,
                QueryMembership = (Recurse? TfsQueryMembership.Expanded: TfsQueryMembership.Direct)
            });

            if (group == null) throw new ArgumentException($"Invalid or non-existent group '{Group}'");

            this.Log($"Returning members from group '{Group}'");

            foreach(var memberId in group.MemberIds)
            {
                var member = GetInstanceOf<TfsIdentity>(new {
                    Identity = memberId
                });

                if (member.DisplayName.IsLike(Member) || 
                    member.UniqueName.IsLike(Member))
                {
                    WriteObject(member);
                }
            }
        }
    }
}