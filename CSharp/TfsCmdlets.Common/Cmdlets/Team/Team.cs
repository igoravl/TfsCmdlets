using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Cmdlets.Team
{
    public class Team: PSObject
    {
        public static implicit operator WebApiTeam(Team c) => c.InnerTeam;
        public static implicit operator Team(WebApiTeam c) => new Team(c);

        internal Team(WebApiTeam t) : base(t)
        {
        }

        internal WebApiTeam InnerTeam => BaseObject as WebApiTeam;
        
        internal string Name => InnerTeam.Name;
        
        internal Guid Id => InnerTeam.Id;
        
        internal string ProjectName => InnerTeam.ProjectName;

        internal string Url => InnerTeam.Url;

        internal string Description => InnerTeam.Description;

        internal Guid ProjectId => InnerTeam.ProjectId;

        internal WebApiIdentity Identity => InnerTeam.Identity;

        internal IEnumerable<Microsoft.VisualStudio.Services.WebApi.TeamMember> TeamMembers
        {
            get => this.GetProperty(nameof(TeamMembers)).Value as IEnumerable<Microsoft.VisualStudio.Services.WebApi.TeamMember>;
            set => this.GetProperty(nameof(TeamMembers)).Value = value;
        }

        internal TeamSetting Settings
        {
            get => this.GetProperty(nameof(Settings)).Value as TeamSetting;
            set => this.GetProperty(nameof(Settings)).Value = value;
        }
    }
}