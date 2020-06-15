using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Base implementation for all area/iteration cmdlets
    /// </summary>
    public abstract class BaseClassificationNodeCmdlet : BaseCmdlet
    {
        protected private abstract TreeStructureGroup StructureGroup { get; }
    }
}