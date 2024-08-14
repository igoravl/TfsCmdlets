using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets the members of a Azure DevOps group
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
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

    [CmdletController(typeof(Models.Identity))]
    partial class GetGroupMemberController
    {
        protected override IEnumerable Run()
        {
            var group = Parameters.Get<object>(nameof(GetGroupMember.Group));
            var member = Parameters.Get<string>(nameof(AddGroupMember.Member));
            var recurse = Parameters.Get<bool>(nameof(GetGroupMember.Recurse));

            var g = Data.GetItem<Models.Identity>(new
            {
                Identity = group,
                QueryMembership = (recurse ? QueryMembership.Expanded : QueryMembership.Direct)
            });

            if (g == null) throw new ArgumentException($"Invalid or non-existent group '{group}'");

            Logger.Log($"Returning members from group '{group}'");

            foreach (var memberId in g.MemberIds)
            {
                var m = Data.GetItem<Models.Identity>(new
                {
                    Identity = memberId
                });

                if (m.DisplayName.IsLike(member) ||
                    m.UniqueName.IsLike(member))
                {
                    yield return m;
                }
            }
        }
    }
}