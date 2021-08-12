using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the team board object
    /// </summary>
    public class Board : PSObject
    {
        private WebApiBoard InnerObject => (WebApiBoard)BaseObject;

        internal Board(WebApiBoard b, string project, string team) : base(b)
        {
            this.AddNoteProperty(nameof(Project), project);
            this.AddNoteProperty(nameof(Team), team);
        }

        internal Guid Id => InnerObject.Id;

        internal string Name => InnerObject.Name;

        internal string Project => (string) this.GetProperty(nameof(Project)).Value;

        internal string Team => (string) this.GetProperty(nameof(Team)).Value;

   }
}