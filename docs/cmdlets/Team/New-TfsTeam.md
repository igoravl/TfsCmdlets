---
title: New-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Creates a new team. "
remarks: 
parameterSets: 
  "_All_": [ AreaPaths, BacklogIteration, Collection, DefaultAreaPath, DefaultIterationMacro, Description, IterationPaths, NoBacklogIteration, NoDefaultArea, Passthru, Project, Server, Team ] 
  "Set team settings":  
    Team: 
      type: "string"  
      position: "0"  
      required: true  
    AreaPaths: 
      type: "string[]"  
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
      type: "string[]"  
    NoBacklogIteration: 
      type: "SwitchParameter"  
    NoDefaultArea: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
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
  - name: "AreaPaths" 
    description: "Specifies the backlog area path(s) that are associated with this team. Wildcards are supported. When the path ends with an asterisk, all child area paths will be included recursively. Otherwise, only the area itself (without its children) will be included. To include the children of the default area path, use the wildcard character (*) without a path. " 
    globbing: false 
    type: "string[]" 
  - name: "BacklogIteration" 
    description: "Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration. " 
    globbing: false 
    type: "string" 
    defaultValue: "\\" 
  - name: "DefaultIterationMacro" 
    description: "Specifies the default iteration macro. When omitted, defaults to \"@CurrentIteration\". " 
    globbing: false 
    type: "string" 
    defaultValue: "@CurrentIteration" 
  - name: "IterationPaths" 
    description: "Specifies the backlog iteration path(s) that are associated with this team. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "NoBacklogIteration" 
    description: "Do not associate an iteration path automatically to the new team. When omitted, an iteration path is created (if needed) and then is set as the default backlog iteration " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Description" 
    description: "Specifies a description of the new team. " 
    globbing: false 
    type: "string" 
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
  - type: "System.String" 
    description: "Specifies the name of the new team. "
outputs: 
  - type: "TfsCmdlets.Models.Team" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/New-TfsTeam"
aliases: 
examples: 
---
