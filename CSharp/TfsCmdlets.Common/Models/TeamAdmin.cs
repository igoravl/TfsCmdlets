using System;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a Team Adminstrator
    /// </summary>
    public class TeamAdmin: Models.Identity
    {
        public TeamAdmin(Models.Identity admin, WebApiTeam team)
            : base((Microsoft.VisualStudio.Services.Identity.Identity)admin.BaseObject)
        {
            AddProperty(nameof(TeamId), team.Id);
            AddProperty(nameof(ProjectId), team.ProjectId);
            AddProperty(nameof(TeamName), team.Name);
            AddProperty(nameof(ProjectName), team.ProjectName);
        }

        public Guid TeamId => (Guid)Properties[nameof(TeamId)].Value;

        public Guid ProjectId => (Guid)Properties[nameof(ProjectId)].Value;

        public string TeamName => (string)Properties[nameof(TeamName)].Value;

        public string ProjectName => (string)Properties[nameof(ProjectName)].Value;
    }
}