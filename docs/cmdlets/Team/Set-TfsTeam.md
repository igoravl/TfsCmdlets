---
title: Set-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Changes the details of a team. "
remarks: 
parameterSets: 
  "_All_": [ AreaPaths, BacklogIteration, BacklogVisibilities, BugsBehavior, Collection, Default, DefaultAreaPath, DefaultIterationMacro, Description, Force, IterationPaths, OverwriteAreaPaths, OverwriteIterationPaths, Passthru, Project, Server, Team, WorkingDays ] 
  "Set team settings":  
    Team: 
      type: "object"  
      position: "0"  
    AreaPaths: 
      type: "string[]"  
    BacklogIteration: 
      type: "string"  
    BacklogVisibilities: 
      type: "Hashtable"  
    BugsBehavior: 
      type: "BugsBehavior"  
    Collection: 
      type: "object"  
    DefaultAreaPath: 
      type: "string"  
    DefaultIterationMacro: 
      type: "string"  
    Description: 
      type: "string"  
    Force: 
      type: "SwitchParameter"  
    IterationPaths: 
      type: "string[]"  
    OverwriteAreaPaths: 
      type: "SwitchParameter"  
    OverwriteIterationPaths: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
    WorkingDays: 
      type: "DayOfWeek[]"  
  "Set default team":  
    Team: 
      type: "object"  
      position: "0"  
    Default: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. This is an alias of the Team parameter." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Default" 
    description: "Sets the specified team as the default team. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Description" 
    description: "Specifies a new description " 
    globbing: false 
    type: "string" 
  - name: "DefaultAreaPath" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board. " 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "TeamFieldValue" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board. This is an alias of the DefaultAreaPath parameter." 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "AreaPaths" 
    description: "Specifies the backlog area path(s) that are associated with this team. Wildcards are supported. When the path ends with an asterisk, all child area paths will be included recursively. Otherwise, only the area itself (without its children) will be included. To include the children of the default area path, use the wildcard character (*) without a path. " 
    globbing: false 
    type: "string[]" 
  - name: "OverwriteAreaPaths" 
    description: "Replaces the existing area paths with the specified list of area paths. When omitted, the new area paths are added alongside the previously defined ones. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "BacklogIteration" 
    description: "Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration. " 
    globbing: false 
    type: "string" 
    defaultValue: "\\" 
  - name: "DefaultIterationMacro" 
    description: "Specifies the default iteration macro. " 
    globbing: false 
    type: "string" 
  - name: "IterationPaths" 
    description: "Specifies the backlog iteration path(s) that are associated with this team. Wildcards are supported. " 
    globbing: false 
    type: "string[]" 
  - name: "OverwriteIterationPaths" 
    description: "Replaces the existing iteration paths with the specified list of iteration paths. When omitted, the new iteration paths are added alongside the previously defined ones. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "WorkingDays" 
    description: "Specifies the team's Working Days. " 
    globbing: false 
    type: "DayOfWeek[]" 
  - name: "BugsBehavior" 
    description: "Specifies how bugs should behave when added to a board. Possible values: Off, AsRequirements, AsTasks" 
    globbing: false 
    type: "BugsBehavior" 
    defaultValue: "Off" 
  - name: "BacklogVisibilities" 
    description: "Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible. " 
    globbing: false 
    type: "Hashtable" 
  - name: "Force" 
    description: "Allows the cmdlet to create target area and/or iteration nodes if they're missing. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
  - type: "System.Object" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.WebApiTeam" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Set-TfsTeam"
aliases: 
examples: 
---
