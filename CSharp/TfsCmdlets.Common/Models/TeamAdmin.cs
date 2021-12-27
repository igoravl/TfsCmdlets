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
        public TeamAdmin(Models.Identity admin, WebApiTeam team)
            : base((Microsoft.VisualStudio.Services.Identity.Identity)admin.BaseObject)
        {
            this.AddNoteProperty(nameof(TeamId), team.Id);
            this.AddNoteProperty(nameof(ProjectId), team.ProjectId);
        }

        public Guid TeamId => (Guid)Properties[nameof(TeamId)].Value;

        public Guid ProjectId => (Guid)Properties[nameof(ProjectId)].Value;
    }
}