---
title: New-TfsWorkItemQuery
breadcrumbs: [ "WorkItem", "Query" ]
parent: "WorkItem.Query"
description: "Create a new work items query in the given Team Project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Passthru, Project, Query, Scope, Server, Wiql ] 
  "__AllParameterSets":  
    Query: 
      type: "string"  
      position: "0"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Scope: 
      type: "string"  
    Server: 
      type: "object"  
    Wiql: 
      type: "string" 
parameters: 
  - name: "Query" 
    description: "Specifies one or more saved queries to return. Wildcards supported. When omitted, returns all saved queries in the given scope of the given team project. " 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
  - name: "Path" 
    description: "Specifies one or more saved queries to return. Wildcards supported. When omitted, returns all saved queries in the given scope of the given team project. This is an alias of the Query parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
  - name: "Scope" 
    description: "Specifies the scope of the returned item. Personal refers to the \"My Queries\" folder\", whereas Shared refers to the \"Shared Queries\" folder. When omitted defaults to \"Both\", effectively searching for items in both scopes. " 
    globbing: false 
    type: "string" 
    defaultValue: "Both" 
  - name: "Wiql" 
    description: "Specifies the query definition text in WIQL (Work Item Query Language). " 
    globbing: false 
    type: "string" 
    aliases: [ Definition ] 
  - name: "Definition" 
    description: "Specifies the query definition text in WIQL (Work Item Query Language). This is an alias of the Wiql parameter." 
    globbing: false 
    type: "string" 
    aliases: [ Definition ] 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Query/New-TfsWorkItemQuery"
aliases: 
examples: 
---
