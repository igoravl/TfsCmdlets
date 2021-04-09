---
title: Get-TfsWorkItemQuery
breadcrumbs: [ "WorkItem", "Query" ]
parent: "WorkItem.Query"
description: "Gets the definition of one or more work item saved queries. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Deleted, Project, Query, Scope ] 
  "__AllParameterSets":  
    Query: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Deleted: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Scope: 
      type: "string" 
parameters: 
  - name: "Query" 
    description: "Specifies one or more saved queries to return. Wildcards supported. When omitted, returns all saved queries in the given scope of the given team project. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "Path" 
    description: "Specifies one or more saved queries to return. Wildcards supported. When omitted, returns all saved queries in the given scope of the given team project. This is an alias of the Query parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Scope" 
    description: "Specifies the scope of the returned item. Personal refers to the \"My Queries\" folder\", whereas Shared refers to the \"Shared Queries\" folder. When omitted defaults to \"Both\", effectively searching for items in both scopes. " 
    globbing: false 
    type: "string" 
    defaultValue: "Both" 
  - name: "Deleted" 
    description: "Returns deleted items. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Query/Get-TfsWorkItemQuery"
aliases: 
examples: 
---
