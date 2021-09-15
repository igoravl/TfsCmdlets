using System;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Gets the members of a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public class GetTeamMember : CmdletBase
    {
        /// <summary>
        /// Specifies the team from which to get its members.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Team { get; set; }

        /// <summary>
        /// Specifies the member (user or group) to get from the given team. Wildcards are supported.
        /// When omitted, all team members are returned.
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

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var (_, _, t) = GetCollectionProjectAndTeam();

        //    var group = GetItem<Models.Identity>(new
        //    {
        //        Identity = t.Id,
        //        QueryMembership = (Recurse? TfsQueryMembership.Expanded: TfsQueryMembership.Direct)
        //    });

        //    if (group == null) throw new ArgumentException($"Invalid or non-existent team '{Team}'");

        //    this.Log($"Returning members from team '{t.Name}'");

        //    foreach(var memberId in group.MemberIds)
        //    {
        //        var member = GetItem<Models.Identity>(new {
        //            Identity = memberId
        //        });

        //        if (member.DisplayName.IsLike(Member) || 
        //            member.UniqueName.IsLike(Member))
        //        {
        //            WriteObject(member);
        //        }
        //    }
        //}
    }
}