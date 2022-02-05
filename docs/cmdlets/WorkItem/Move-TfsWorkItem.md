---
title: Move-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Moves a work item to a different team project in the same collection. "
remarks: 
parameterSets: 
  "_All_": [ Area, Collection, Comment, Iteration, Passthru, Project, Server, State, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Project: 
      type: "object"  
      position: "1"  
      required: true  
    Area: 
      type: "object"  
    Collection: 
      type: "object"  
    Comment: 
      type: "string"  
    Iteration: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
    State: 
      type: "string" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id ] 
  - name: "Id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id ] 
  - name: "Project" 
    description: "Specifies the team project where the work item will be moved to. " 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ Destination ] 
  - name: "Destination" 
    description: "Specifies the team project where the work item will be moved to. This is an alias of the Project parameter." 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ Destination ] 
  - name: "Area" 
    description: "Specifies the area path in the destination project where the work item will be moved to. When omitted, the work item is moved to the root area path in the destination project. " 
    globbing: false 
    type: "object" 
  - name: "Iteration" 
    description: "Specifies the iteration path in the destination project where the work item will be moved to. When omitted, the work item is moved to the root iteration path in the destination project. " 
    globbing: false 
    type: "object" 
  - name: "State" 
    description: "Specifies a new state for the work item in the destination project. When omitted, it retains the current state. " 
    globbing: false 
    type: "string" 
  - name: "Comment" 
    description: "Specifies a comment to be added to the history " 
    globbing: false 
    type: "string" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Move-TfsWorkItem"
aliases: 
examples: 
---
