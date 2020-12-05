---
title: Set-TfsIteration
breadcrumbs: [ "WorkItem", "AreasIterations" ]
parent: "WorkItem.AreasIterations"
description: "Modifies the dates of an iteration. "
remarks: 
parameterSets: 
  "_All_": [ Collection, FinishDate, Node, Passthru, Project, StartDate ] 
  "__AllParameterSets":  
    Node: 
      type: "object"  
      position: "0"  
    FinishDate: 
      type: "DateTime"  
      required: true  
    StartDate: 
      type: "DateTime"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Node" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "Path" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node). This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "Iteration" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node). This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Iteration ] 
  - name: "StartDate" 
    description: "Specifies the start date of the iteration. To clear the start date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null). " 
    required: true 
    globbing: false 
    type: "DateTime" 
  - name: "FinishDate" 
    description: "Sets the finish date of the iteration. To clear the finish date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null). " 
    required: true 
    globbing: false 
    type: "DateTime" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name, URI or path of an Iteration. Wildcards are supported. When  omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node). "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/AreasIterations/Set-TfsIteration"
aliases: 
examples: 
---
