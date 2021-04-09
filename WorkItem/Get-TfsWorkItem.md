---
title: Get-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Gets the contents of one or more work items. "
remarks: 
parameterSets: 
  "_All_": [ AreaPath, AsOf, BoardColumn, BoardColumnDone, ChangedBy, ChangedDate, Collection, CreatedBy, CreatedDate, Deleted, Description, Ever, Fields, IncludeLinks, IterationPath, Priority, Project, Query, Reason, Revision, ShowWindow, State, StateChangeDate, Tags, Team, TimePrecision, Title, ValueArea, Where, WorkItem, WorkItemType ] 
  "Query by revision":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Revision: 
      type: "int"  
    ShowWindow: 
      type: "SwitchParameter"  
    Team: 
      type: "object"  
  "Query by date":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    AsOf: 
      type: "DateTime"  
      required: true  
    Collection: 
      type: "object"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Team: 
      type: "object"  
  "Get deleted":  
    WorkItem: 
      type: "object"  
      position: "0"  
    Deleted: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Team: 
      type: "object"  
  "Simple query":  
    AreaPath: 
      type: "string"  
    AsOf: 
      type: "DateTime"  
    BoardColumn: 
      type: "string[]"  
    BoardColumnDone: 
      type: "bool"  
    ChangedBy: 
      type: "object"  
    ChangedDate: 
      type: "DateTime[]"  
    Collection: 
      type: "object"  
    CreatedBy: 
      type: "object[]"  
    CreatedDate: 
      type: "DateTime[]"  
    Description: 
      type: "string[]"  
    Ever: 
      type: "SwitchParameter"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    IterationPath: 
      type: "string"  
    Priority: 
      type: "int[]"  
    Project: 
      type: "object"  
    Reason: 
      type: "string[]"  
    State: 
      type: "string[]"  
    StateChangeDate: 
      type: "DateTime[]"  
    Tags: 
      type: "string[]"  
    Team: 
      type: "object"  
    TimePrecision: 
      type: "SwitchParameter"  
    Title: 
      type: "string[]"  
    ValueArea: 
      type: "string[]"  
    WorkItemType: 
      type: "string[]"  
  "Query by WIQL":  
    Query: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Team: 
      type: "object"  
    TimePrecision: 
      type: "SwitchParameter"  
  "Query by filter":  
    Where: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Fields: 
      type: "string[]"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Team: 
      type: "object"  
    TimePrecision: 
      type: "SwitchParameter" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Title" 
    description: "Specifies the title to look up for in a work item. Wildcards are supported. When a wildcard is used, matches a portion of the title (uses the operator \"contains\" in the WIQL query). Otherwise, matches the whole field with the operator \"=\", unless -Ever is also specified. In that case, uses the operator \"was ever\". " 
    globbing: false 
    type: "string[]" 
  - name: "Description" 
    description: "Specifies the description to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "AreaPath" 
    description: "Specifies the area path to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description: "Specifies the iteration path to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string" 
  - name: "WorkItemType" 
    description: "Specifies the work item type to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
    aliases: [ Type ] 
  - name: "Type" 
    description: "Specifies the work item type to look up for in a work item. Wildcards are supported. This is an alias of the WorkItemType parameter." 
    globbing: false 
    type: "string[]" 
    aliases: [ Type ] 
  - name: "State" 
    description: "Specifies the state (field 'System.State') to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "Reason" 
    description: "Specifies the reason (field 'System.Reason') to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "ValueArea" 
    description: "Specifies the Value Area (field 'Microsoft.VSTS.Common.ValueArea') to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "BoardColumn" 
    description: "Specifies the board column to look up for in a work item. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "BoardColumnDone" 
    description: "Specifies whether the work item is in the sub-column Doing or Done in a board. " 
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "CreatedBy" 
    description: "Specifies the name or email of the user that created the work item. " 
    globbing: false 
    type: "object[]" 
  - name: "CreatedDate" 
    description: "Specifies the date when the work item was created. " 
    globbing: false 
    type: "DateTime[]" 
  - name: "ChangedBy" 
    description: "Specifies the name or email of the user that did the latest change to the work item. " 
    globbing: false 
    type: "object" 
  - name: "ChangedDate" 
    description: "Specifies the date of the latest change to the work item. " 
    globbing: false 
    type: "DateTime[]" 
  - name: "StateChangeDate" 
    description: "Specifies the date of the most recent change to the state of the work item. " 
    globbing: false 
    type: "DateTime[]" 
  - name: "Priority" 
    description: "Specifies the priority of the work item. " 
    globbing: false 
    type: "int[]" 
  - name: "Tags" 
    description: "Specifies the tags to look up for in a work item. When multiple tags are supplied, they are combined with an OR operator - in other works, returns  work items that contain ANY ofthe supplied tags. " 
    globbing: false 
    type: "string[]" 
  - name: "Ever" 
    description: "Switches the query to historical query mode, by changing operators to \"WAS EVER\" where possible. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Revision" 
    description: "Specifies a work item revision number to retrieve. When omitted, returns the latest revision of the work item. " 
    globbing: false 
    type: "int" 
    aliases: [ rev ] 
    defaultValue: "0" 
  - name: "rev" 
    description: "Specifies a work item revision number to retrieve. When omitted, returns the latest revision of the work item. This is an alias of the Revision parameter." 
    globbing: false 
    type: "int" 
    aliases: [ rev ] 
    defaultValue: "0" 
  - name: "AsOf" 
    description: "Returns the field values as they were defined in the work item revision that was the latest revision by the date specified. " 
    required: true 
    globbing: false 
    type: "DateTime" 
    defaultValue: "1/1/0001 12:00:00 AM" 
  - name: "Query" 
    description: "Specifies a query written in WIQL (Work Item Query Language) " 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "WIQL" 
    description: "Specifies a query written in WIQL (Work Item Query Language) This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "QueryText" 
    description: "Specifies a query written in WIQL (Work Item Query Language) This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "SavedQuery" 
    description: "Specifies a query written in WIQL (Work Item Query Language) This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "QueryPath" 
    description: "Specifies a query written in WIQL (Work Item Query Language) This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "Fields" 
    description: "Specifies which fields should be retrieved. When omitted, defaults to a set of standard fields that include Id, Title, Description, some state-related fields and more. " 
    globbing: false 
    type: "string[]" 
    defaultValue: "System.AreaPath, System.TeamProject, System.IterationPath, System.WorkItemType, System.State, System.Reason, System.CreatedDate, System.CreatedBy, System.ChangedDate, System.ChangedBy, System.CommentCount, System.Title, System.BoardColumn, System.BoardColumnDone, Microsoft.VSTS.Common.StateChangeDate, Microsoft.VSTS.Common.Priority, Microsoft.VSTS.Common.ValueArea, System.Description, System.Tags" 
  - name: "Where" 
    description: "Specifies a filter clause (the portion of a WIQL query after the WHERE keyword). " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "TimePrecision" 
    description: "Fetches work items in \"time-precision mode\": search criteria in WIQL queries take into account time information as well, not only dates. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "ShowWindow" 
    description: "Opens the specified work item in the default web browser. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Deleted" 
    description: "Gets deleted work items. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeLinks" 
    description: "Gets information about all links and attachments in the work item. When omitted, only fields are retrieved. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Get-TfsWorkItem"
aliases: 
examples: 
---
