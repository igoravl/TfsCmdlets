using System;
using System.Collections;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem", DefaultParameterSetName="Query by text")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class GetWorkItem: BaseCmdlet
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position=0, Mandatory=true, ParameterSetName="Query by revision")]
        [Parameter(Position=0, Mandatory=true, ParameterSetName="Query by date")]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        [Parameter(ParameterSetName="Query by revision")]
        [Alias("rev")]
        public int Revision { get; set; }

        [Parameter(Mandatory=true, ParameterSetName="Query by date")]
        public DateTime AsOf { get; set; }

        [Parameter(Mandatory=true, ParameterSetName="Query by WIQL")]
        [Alias("WIQL", "QueryText", "SavedQuery", "QueryPath")]
        public string Query { get; set; }

        // # [Parameter(Mandatory=true, ParameterSetName="Query by filter")]
        // # [string[]]
        // # Fields,

        [Parameter(Mandatory=true, ParameterSetName="Query by filter")]
        public string Filter { get; set; }

        [Parameter(Position=0, Mandatory=true, ParameterSetName="Query by text")]
        public string Text { get; set; }

        [Parameter()]
        public Hashtable Macros { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

//     protected override void BeginProcessing()
//     {
//         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
//     }

//         /// <summary>
//         /// Performs execution of the command
//         /// </summary>
//         protected override void ProcessRecord()
//     {
//         if (Project)
//         {
//             tp = this.GetProject();
//             tpc = tp.Store.TeamProjectCollection
//             store = tp.Store
//         }
//         else
//         {
//             tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
//             store = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")
//         }

//         if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
//         {
//             if ((! Revision) && (! AsOf))
//             {
//                 WriteObject(WorkItem); return;
//             }
//         }

//         switch(ParameterSetName)
//         {
//             "Query by revision" {
//                 WriteObject(_GetWorkItemByRevision WorkItem Revision store); return;
//             }

//             "Query by date" {
//                 WriteObject(_GetWorkItemByDate WorkItem AsOf store); return;
//             }

//             "Query by text" {
//                 localMacros = @{TfsQueryText=Text}
//                 Wiql = "SELECT * FROM WorkItems WHERE [System.Title] CONTAINS @TfsQueryText OR [System.Description] CONTAINS @TfsQueryText"
//                 WriteObject(_GetWorkItemByWiql Wiql localMacros tp store ); return;
//             }

//             "Query by filter" {
//                 Wiql = $"SELECT * FROM WorkItems WHERE {Filter}"
//                 WriteObject(_GetWorkItemByWiql Wiql Macros tp store ); return;
//             }

//             "Query by WIQL" {
// 				this.Log($"Get-TfsWorkItem: Running query by WIQL. Query: {Query}");
//                 WriteObject(_GetWorkItemByWiql Query Macros tp store ); return;
//             }

//             "Query by saved query" {
//                 WriteObject(_GetWorkItemBySavedQuery StoredQueryPath Macros tp store ); return;
//             }
//         }
//     }
// }

// Function _GetWorkItemByRevision(WorkItem, Revision, store)
// {
//     if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
//     {
//         ids = @(WorkItem.Id)
//     }
//     elseif (WorkItem is int)
//     {
//         ids = @(WorkItem)
//     }
//     elseif (WorkItem is int[])
//     {
//         ids = WorkItem
//     }
//     else
//     {
//         throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
//     }

//     if (Revision is int && Revision -gt 0)
//     {
//         foreach(id in ids)
//         {
//             store.GetWorkItem(id, Revision)
//         }
//     }
//     elseif (Revision is int[])
//     {
//         if (ids.Count != Revision.Count)
//         {
//             throw new Exception("When supplying a list of IDs and Revisions, both must have the same number of elements")
//         }
//         for(i = 0; i -le ids.Count-1; i++)
//         {
//             store.GetWorkItem(ids[i], Revision[i])
//         }
//     }
//     else
//     {
//         foreach(id in ids)
//         {
//             store.GetWorkItem(id)
//         }
//     }
// }

// Function _GetWorkItemByDate(WorkItem, AsOf, store)
// {
//     if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
//     {
//         ids = @(WorkItem.Id)
//     }
//     elseif (WorkItem is int)
//     {
//         ids = @(WorkItem)
//     }
//     elseif (WorkItem is int[])
//     {
//         ids = WorkItem
//     }
//     else
//     {
//         throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
//     }

//     if (AsOf is datetime[])
//     {
//         if (ids.Count != AsOf.Count)
//         {
//             throw new Exception("When supplying a list of IDs and Changed Dates (AsOf), both must have the same number of elements")
//         }
//         for(i = 0; i -le ids.Count-1; i++)
//         {
//             store.GetWorkItem(ids[i], AsOf[i])
//         }
//     }
//     else
//     {
//         foreach(id in ids)
//         {
//             store.GetWorkItem(id, AsOf)
//         }
//     }
// }

// Function _GetWorkItemByWiql(QueryText, Macros, Project, store)
// {
// 	if (QueryText -notlike "select*")
// 	{
// 		q = Get-TfsWorkItemQueryItem -ItemType Query -Query QueryText -Project Project

// 		if (! q)
// 		{
// 			throw new Exception($"Work item query "{QueryText}" is invalid or non-existent.")
// 		}

// 		if (q.Count -gt 1)
// 		{
// 			throw new Exception($"Ambiguous query name "{QueryText}". {q.Count} queries were found matching the specified name/pattern:`n`n - " + (q -join "`n - "))
// 		}

// 		QueryText = q.QueryText
// 	}

//     if (! Macros && ((QueryText -match $"@project") || ({QueryText} -match "@me")))
//     {
//         Macros = @{}
//     }

//     if (QueryText -match "@project")
//     {
// 		if (! Project)
// 		{
// 			Project = Get-TfsTeamProject -Current
// 		}

//         if (! Macros.ContainsKey("Project"))
//         {
//             Macros["Project"] = Project.Name
//         }
//     }

//     if (QueryText -match "@me")
//     {
//         user = null
//         store.TeamProjectCollection.GetAuthenticatedIdentity([ref] user)
//         Macros["Me"] = user.DisplayName
//     }

// 	this.Log($"Get-TfsWorkItem: Running query {QueryText}");

//     wis = store.Query(QueryText, Macros)

//     # foreach(wi in wis)
//     # {
//     #     if(Fields)
//     #     {
//     #         foreach(f in Fields)
//     #         {
//     #             wi | Add-Member -Name (_GetEncodedFieldName f.ReferenceName) -MemberType ScriptProperty -Value `
//     #                 {f.Value}.GetNewClosure() `
//     #                 {param(Value) f.Value = Value}.GetNewClosure()
//     #         }
//     #     }
//     # }

//     WriteObject(wis); return;
// }
    }
}
