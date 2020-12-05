---
title: New-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Creates a new team. "
remarks: 
parameterSets: 
  "_All_": [ BacklogIteration, Collection, DefaultAreaPath, DefaultIterationMacro, Description, IterationPaths, NoBacklogIteration, NoDefaultArea, Passthru, Project, Team ] 
  "__AllParameterSets":  
    Team: 
      type: "string"  
      position: "0"  
      required: true  
    BacklogIteration: 
      type: "string"  
    Collection: 
      type: "object"  
    DefaultAreaPath: 
      type: "string"  
    DefaultIterationMacro: 
      type: "string"  
    Description: 
      type: "string"  
    IterationPaths: 
      type: "object"  
    NoBacklogIteration: 
      type: "SwitchParameter"  
    NoDefaultArea: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Team" 
    description: "Specifies the name of the new team. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the new team. This is an alias of the Team parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "DefaultAreaPath" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board. When omitted, an area path may still be associated to this team depending on whether NoAutomaticAreaPath is set " 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "TeamFieldValue" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board. When omitted, an area path may still be associated to this team depending on whether NoAutomaticAreaPath is set This is an alias of the DefaultAreaPath parameter." 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "NoDefaultArea" 
    description: "Do not associate an area path automatically to the new team. When omitted, an area path is created (if needed) and then is set as the default area path / team field " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "BacklogIteration" 
    description: "Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration. " 
    globbing: false 
    type: "string" 
    defaultValue: "\\" 
  - name: "IterationPaths" 
    description: "Specifies the backlog iteration paths that are associated with this team. Provide a list of iteration paths in the form '/path1/path2'. " 
    globbing: false 
    type: "object" 
  - name: "DefaultIterationMacro" 
    description: "Specifies the default iteration macro. When omitted, defaults to \"@CurrentIteration\". " 
    globbing: false 
    type: "string" 
    defaultValue: "@CurrentIteration" 
  - name: "NoBacklogIteration" 
    description: "Do not associate an iteration path automatically to the new team. When omitted, an iteration path is created (if needed) and then is set as the default backlog iteration " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Description" 
    description: "Specifies a description of the new team. " 
    globbing: false 
    type: "string" 
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
  - type: "System.String" 
    description: "Specifies the name of the new team. "
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.WebApiTeam" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/New-TfsTeam"
aliases: 
examples: 
---
