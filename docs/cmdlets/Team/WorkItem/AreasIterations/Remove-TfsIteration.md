---
title: Remove-TfsIteration
breadcrumbs: [ "WorkItem", "AreasIterations" ]
parent: "WorkItem.AreasIterations"
description: "Deletes one or more Iterations from a given Team Project."
remarks: 
parameterSets: 
  "_All_": [ Collection, MoveTo, Node, Project, Recurse ] 
  "__AllParameterSets":  
    Node: 
      type: "object"  
      position: "0"  
    MoveTo: 
      type: "object"  
      position: "1"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Recurse: 
      type: "SwitchParameter" 
parameters: 
  - name: "Node" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node)." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "Path" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "Iteration" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "MoveTo" 
    description: "Specifies the new parent node for the work items currently assigned to the node being deleted, if any. When omitted, defaults to the root node (the \"\\\" node, at the team project level)." 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ NewPath ] 
    defaultValue: "\\" 
  - name: "NewPath" 
    description: "Specifies the new parent node for the work items currently assigned to the node being deleted, if any. When omitted, defaults to the root node (the \"\\\" node, at the team project level).This is an alias of the MoveTo parameter." 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ NewPath ] 
    defaultValue: "\\" 
  - name: "Recurse" 
    description: "Removes node(s) recursively." 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node)."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/AreasIterations/Remove-TfsIteration"
aliases: 
examples: 
---
