---
title: Set-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Changes the details of a team."
remarks: 
parameterSets: 
  "_All_": [ AreaPaths, BacklogIteration, BacklogVisibilities, BugsBehavior, Collection, Default, DefaultAreaPath, DefaultIterationMacro, Description, IterationPaths, Passthru, Project, Team, WorkingDays ] 
  "__AllParameterSets":  
    Team: 
      type: "object"  
      position: "0"  
    AreaPaths: 
      type: "IEnumerable`1"  
    BacklogIteration: 
      type: "string"  
    BacklogVisibilities: 
      type: "Hashtable"  
    BugsBehavior: 
      type: "string"  
    Collection: 
      type: "object"  
    Default: 
      type: "SwitchParameter"  
    DefaultAreaPath: 
      type: "string"  
    DefaultIterationMacro: 
      type: "string"  
    Description: 
      type: "string"  
    IterationPaths: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    WorkingDays: 
      type: "IEnumerable`1" 
parameters: 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet.This is an alias of the Team parameter." 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Description" 
    description: "Specifies a new description" 
    globbing: false 
    type: "string" 
  - name: "DefaultAreaPath" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board." 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "TeamFieldValue" 
    description: "Specifies the team's default area path (or \"team field\"). The default area path is assigned automatically to all work items created in a team's backlog and/or board.This is an alias of the DefaultAreaPath parameter." 
    globbing: false 
    type: "string" 
    aliases: [ TeamFieldValue ] 
  - name: "AreaPaths" 
    description: "Specifies the backlog area paths that are associated with this team. Provide a list of area paths in the form '/path1/path2/[*]'. When the path ends with an asterisk, all child area path will be included recursively. Otherwise, only the area itself (without its children) will be included." 
    globbing: false 
    type: "IEnumerable`1" 
  - name: "BacklogIteration" 
    description: "Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration." 
    globbing: false 
    type: "string" 
    defaultValue: "\\" 
  - name: "IterationPaths" 
    description: "Specifies the backlog iteration paths that are associated with this team. Provide a list of iteration paths in the form '/path1/path2'." 
    globbing: false 
    type: "object" 
  - name: "DefaultIterationMacro" 
    description: "Specifies the default iteration macro. When omitted, defaults to \"@CurrentIteration\"." 
    globbing: false 
    type: "string" 
    defaultValue: "@CurrentIteration" 
  - name: "BugsBehavior" 
    description: "Specifies how bugs should behave when added to a board." 
    globbing: false 
    type: "string" 
  - name: "BacklogVisibilities" 
    description: "Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible." 
    globbing: false 
    type: "Hashtable" 
  - name: "Default" 
    description: "Sets the supplied team as the default team project team." 
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
  - name: "WorkingDays" 
    description: "Specifies the team's Working Days. When omitted, defaults to Monday thru Friday" 
    globbing: false 
    type: "IEnumerable`1" 
    defaultValue: "monday, tuesday, wednesday, thursday, friday"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet."
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.WebApiTeam" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Team/Set-TfsTeam"
aliases: 
examples: 
---
