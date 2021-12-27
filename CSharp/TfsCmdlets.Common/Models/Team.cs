using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates a WebApiTeam object
    /// </summary>
    public class Team: PSObject
    {
        private WebApiTeam InnerTeam => BaseObject as WebApiTeam;
        
        /// <summary>
        /// Converts to WebApiTeam
        /// </summary>
        public static implicit operator WebApiTeam(Team c) => c.InnerTeam;

        /// <summary>
        /// Converts from WebApiTeam
        /// </summary>
        public static implicit operator Team(WebApiTeam c) => new Team(c);

        public Team(WebApiTeam t) : base(t)
        {
        }

        public string Name => InnerTeam.Name;
        
        public Guid Id => InnerTeam.Id;
        
        public string ProjectName => InnerTeam.ProjectName;

        public string Url => InnerTeam.Url;

        public string Description => InnerTeam.Description;

        public Guid ProjectId => InnerTeam.ProjectId;

        public WebApiIdentity Identity => InnerTeam.Identity;

        public IEnumerable<Microsoft.VisualStudio.Services.WebApi.TeamMember> TeamMembers
        {
            get => this.GetProperty(nameof(TeamMembers)).Value as IEnumerable<Microsoft.VisualStudio.Services.WebApi.TeamMember>;
            set => this.GetProperty(nameof(TeamMembers)).Value = value;
        }

        public TeamSetting Settings
        {
            get => this.GetProperty(nameof(Settings)).Value as TeamSetting;
            set => this.GetProperty(nameof(Settings)).Value = value;
        }
    }
}