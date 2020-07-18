---
title: Search-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Gets the contents of one or more work items."
remarks: 
parameterSets: 
  "_All_": [ Collection, Project, Query, Results ] 
  "__AllParameterSets":  
    Query: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Results: 
      type: "int" 
parameters: 
  - name: "Query" 
    description: "Specifies the text to search for. Supports the Quick Filter syntax described in https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax" 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
  - name: "Results" 
    description: "Specifies the maximum quantity of results. Supports between 1 and 1000 results. When omitted, defaults to 100. Currently this cmdlet does not support result pagination." 
    globbing: false 
    type: "int" 
    defaultValue: "100" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.String" 
    description: "Specifies the text to search for. Supports the Quick Filter syntax described in https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax"
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Search-TfsWorkItem" 
  - text: "https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax" 
    uri: 
aliases: 
examples: 
---
