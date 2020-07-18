---
title: New-TfsIteration
breadcrumbs: [ "WorkItem", "AreasIterations" ]
parent: "WorkItem.AreasIterations"
description: "Creates a new Iteration in the given Team Project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, Node, Passthru, Project ] 
  "__AllParameterSets":  
    Node: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Node" 
    description: "Specifies the path of the new Iteration. When supplying a path, use a backslash (\"\\\\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the iteration name." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Iteration,Path ] 
  - name: "Iteration" 
    description: "Specifies the path of the new Iteration. When supplying a path, use a backslash (\"\\\\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the iteration name.This is an alias of the Node parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Iteration,Path ] 
  - name: "Path" 
    description: "Specifies the path of the new Iteration. When supplying a path, use a backslash (\"\\\\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the iteration name.This is an alias of the Node parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Iteration,Path ] 
  - name: "Force" 
    description: "Allows the cmdlet to create parent nodes if they're missing." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
  - type: "System.String" 
    description: "Specifies the path of the new Iteration. When supplying a path, use a backslash (\"\\\\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the iteration name."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/AreasIterations/New-TfsIteration"
aliases: 
examples: 
---
