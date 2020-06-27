---
title: Get-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Gets the contents of one or more work items."
remarks: 
parameterSets: 
  "_All_": [ AreaPath, AsOf, BoardColumn, BoardColumnDone, ChangedBy, ChangedDate, Collection, CreatedBy, CreatedDate, Deleted, Description, Filter, IterationPath, Macros, Priority, Project, Query, Reason, Revision, ShowWindow, State, StateChangeDate, Tags, Team, Title, ValueArea, WorkItem, WorkItemType ] 
  "Query by revision":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
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
    Project: 
      type: "object"  
    Team: 
      type: "object"  
  "Simple query":  
    AreaPath: 
      type: "string"  
    BoardColumn: 
      type: "string"  
    BoardColumnDone: 
      type: "bool"  
    ChangedBy: 
      type: "object"  
    ChangedDate: 
      type: "DateTime"  
    Collection: 
      type: "object"  
    CreatedBy: 
      type: "object"  
    CreatedDate: 
      type: "DateTime"  
    Description: 
      type: "string"  
    IterationPath: 
      type: "string"  
    Priority: 
      type: "int"  
    Project: 
      type: "object"  
    Reason: 
      type: "string"  
    State: 
      type: "string"  
    StateChangeDate: 
      type: "DateTime"  
    Tags: 
      type: "string[]"  
    Team: 
      type: "object"  
    Title: 
      type: "string"  
    ValueArea: 
      type: "string"  
    WorkItemType: 
      type: "string"  
  "Query by WIQL":  
    Query: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Macros: 
      type: "Hashtable"  
    Project: 
      type: "object"  
    Team: 
      type: "object"  
  "Query by filter":  
    Filter: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Team: 
      type: "object" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem." 
    required: true 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Title" 
    description:  
    globbing: false 
    type: "string" 
  - name: "Description" 
    description:  
    globbing: false 
    type: "string" 
  - name: "AreaPath" 
    description:  
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description:  
    globbing: false 
    type: "string" 
  - name: "WorkItemType" 
    description:  
    globbing: false 
    type: "string" 
  - name: "State" 
    description:  
    globbing: false 
    type: "string" 
  - name: "Reason" 
    description:  
    globbing: false 
    type: "string" 
  - name: "ValueArea" 
    description:  
    globbing: false 
    type: "string" 
  - name: "BoardColumn" 
    description:  
    globbing: false 
    type: "string" 
  - name: "BoardColumnDone" 
    description:  
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "CreatedBy" 
    description:  
    globbing: false 
    type: "object" 
  - name: "CreatedDate" 
    description:  
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "ChangedBy" 
    description:  
    globbing: false 
    type: "object" 
  - name: "ChangedDate" 
    description:  
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "StateChangeDate" 
    description:  
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "Priority" 
    description:  
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "Tags" 
    description:  
    globbing: false 
    type: "string[]" 
  - name: "Revision" 
    description: "Specifies a work item revision number to retrieve. When omitted, returns the latest revision of the work item." 
    globbing: false 
    type: "int" 
    aliases: [ rev ] 
    defaultValue: "0" 
  - name: "rev" 
    description: "Specifies a work item revision number to retrieve. When omitted, returns the latest revision of the work item.This is an alias of the Revision parameter." 
    globbing: false 
    type: "int" 
    aliases: [ rev ] 
    defaultValue: "0" 
  - name: "AsOf" 
    description:  
    required: true 
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "Query" 
    description: "Specifies a query written in WIQL (Work Item Query Language)" 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "WIQL" 
    description: "Specifies a query written in WIQL (Work Item Query Language)This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "QueryText" 
    description: "Specifies a query written in WIQL (Work Item Query Language)This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "SavedQuery" 
    description: "Specifies a query written in WIQL (Work Item Query Language)This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "QueryPath" 
    description: "Specifies a query written in WIQL (Work Item Query Language)This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ WIQL,QueryText,SavedQuery,QueryPath ] 
  - name: "Filter" 
    description:  
    required: true 
    globbing: false 
    type: "string" 
  - name: "Macros" 
    description:  
    globbing: false 
    type: "Hashtable" 
  - name: "ShowWindow" 
    description: "Opens the specified work item in the default web browser." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Deleted" 
    description: "Gets deleted work items." 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Get-TfsWorkItem"
aliases: 
examples: 
---
