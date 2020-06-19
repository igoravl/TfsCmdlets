using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the area/iteration node object
    /// </summary>
    public class BacklogLevelConfiguration : PSObject
    {
        public static implicit operator WebApiBacklogLevelConfiguration(BacklogLevelConfiguration b) =>
            b.InnerObject;

        private readonly WorkItemTrackingHttpClient _client;

        internal BacklogLevelConfiguration(WebApiBacklogLevelConfiguration b,
            string project, string team) : base(b)
        {
            this.AddNoteProperty(nameof(Project), project);
            this.AddNoteProperty(nameof(Team), team);
        }

        private WebApiBacklogLevelConfiguration InnerObject => (WebApiBacklogLevelConfiguration)BaseObject;

        internal string Id => InnerObject.Id;

        internal string Name => InnerObject.Name;

        internal string Project => (string) this.GetProperty(nameof(Project)).Value;

        internal string Team => (string) this.GetProperty(nameof(Team)).Value;

   }
}