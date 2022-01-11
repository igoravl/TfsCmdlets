using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
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