---
title: New-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Creates a new work item in a team project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Fields, Passthru, Project, Title, Type ] 
  "__AllParameterSets":  
    Type: 
      type: "object"  
      position: "0"  
      required: true  
    Title: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Fields: 
      type: "Hashtable"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Type" 
    description: "Specifies the type of the work item." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Title" 
    description: "Specifies the title of the work item." 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Fields" 
    description: "Specifies the names and the corresponding values for the fields to be set in the work item." 
    globbing: false 
    type: "Hashtable" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the type of the work item."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/New-TfsWorkItem"
aliases: 
examples: 
---
