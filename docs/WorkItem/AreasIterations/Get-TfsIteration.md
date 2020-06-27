---
title: Get-TfsIteration
breadcrumbs: [ "WorkItem", "AreasIterations" ]
parent: "WorkItem.AreasIterations"
description: "Gets one or more Iterations from a given Team Project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Node, Project ] 
  "__AllParameterSets":  
    Node: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Node" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node)." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
    defaultValue: "\\**" 
  - name: "Path" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
    defaultValue: "\\**" 
  - name: "Iteration" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
    defaultValue: "\\**" 
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
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/AreasIterations/Get-TfsIteration"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Get-TfsIteration" 
    remarks: "Returns all iterations in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)" 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Get-TfsIteration '\\**\\Support' -Project Tailspin" 
    remarks: "Performs a recursive search and returns all iterations named 'Support' that may exist in a team project called Tailspin"
---
