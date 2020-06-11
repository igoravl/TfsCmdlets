using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsIdentity = TfsCmdlets.Services.Identity;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Represents a Team Adminstrator
    /// </summary>
    public class TeamAdmin: TfsIdentity
    {
        internal TeamAdmin(TfsIdentity admin, WebApiTeam team) : base(admin)
        {
            this.AddNoteProperty(nameof(TeamId), team.Id);
            this.AddNoteProperty(nameof(ProjectId), team.ProjectId);
        }

        internal Guid TeamId => (Guid)Properties[nameof(TeamId)].Value;

        internal Guid ProjectId => (Guid)Properties[nameof(ProjectId)].Value;
    }
}