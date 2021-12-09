using System;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a Team Adminstrator
    /// </summary>
    partial class TeamAdmin: Models.Identity
    {
        internal TeamAdmin(Models.Identity admin, WebApiTeam team)
            : base((Microsoft.VisualStudio.Services.Identity.Identity)admin.BaseObject)
        {
            this.AddNoteProperty(nameof(TeamId), team.Id);
            this.AddNoteProperty(nameof(ProjectId), team.ProjectId);
        }

        internal Guid TeamId => (Guid)Properties[nameof(TeamId)].Value;

        internal Guid ProjectId => (Guid)Properties[nameof(ProjectId)].Value;
    }
}