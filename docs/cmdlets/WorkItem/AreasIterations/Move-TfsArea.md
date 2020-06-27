---
title: Move-TfsArea
breadcrumbs: [ "WorkItem", "AreasIterations" ]
parent: "WorkItem.AreasIterations"
description: "Gets one or more Work Item Areas from a given Team Project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Destination, Force, Node, Passthru, Project ] 
  "__AllParameterSets":  
    Node: 
      type: "object"  
      position: "0"  
    Destination: 
      type: "object"  
      position: "1"  
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
    description: "Specifies the name, URI or path of a Work Area. Wildcards are supported. When omitted, all Areas in the given Team Project are returned. To supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional. When supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node)." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Area ] 
    defaultValue: "\\**" 
  - name: "Path" 
    description: "Specifies the name, URI or path of a Work Area. Wildcards are supported. When omitted, all Areas in the given Team Project are returned. To supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional. When supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Area ] 
    defaultValue: "\\**" 
  - name: "Area" 
    description: "Specifies the name, URI or path of a Work Area. Wildcards are supported. When omitted, all Areas in the given Team Project are returned. To supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional. When supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node).This is an alias of the Node parameter." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Path,Area ] 
    defaultValue: "\\**" 
  - name: "Destination" 
    description: "Specifies the name and/or path of the destination parent node." 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ MoveTo ] 
  - name: "MoveTo" 
    description: "Specifies the name and/or path of the destination parent node.This is an alias of the Destination parameter." 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ MoveTo ] 
  - name: "Force" 
    description: "Allows the cmdlet to create destination parent node(s) if they're missing." 
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
  - type: "System.Object" 
    description: "Specifies the name, URI or path of a Work Area. Wildcards are supported. When omitted, all Areas in the given Team Project are returned. To supply a path, use a backslash ('\\') between the path segments. Leading and trailing backslashes are optional. When supplying a URI, use URIs in the form of 'vstfs:///Classification/Node/{GUID}' (where {GUID} is the unique identifier of the given node)."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/AreasIterations/Move-TfsArea"
aliases: 
examples: 
---
