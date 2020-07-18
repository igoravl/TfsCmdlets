---
title: Get-TfsTeamBacklogLevel
breadcrumbs: [ "Team", "Backlog" ]
parent: "Team.Backlog"
description: "Gets information about one or more backlog levels of a given team."
remarks: 
parameterSets: 
  "_All_": [ Backlog, Collection, Project, Team ] 
  "__AllParameterSets":  
    Backlog: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Team: 
      type: "object" 
parameters: 
  - name: "Backlog" 
    description: "Specifies one or more backlog level configurations to be returned. Valid values are the name (e.g. \"Stories\") or the ID (e.g. \"Microsoft.RequirementCategory\") of the backlog level to return. Wilcards are supported. When omitted, returns all backlogs levels of the given team." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies one or more backlog level configurations to be returned. Valid values are the name (e.g. \"Stories\") or the ID (e.g. \"Microsoft.RequirementCategory\") of the backlog level to return. Wilcards are supported. When omitted, returns all backlogs levels of the given team.This is an alias of the Backlog parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
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
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet."
outputs: 
  - type: "Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Team/Backlog/Get-TfsTeamBacklogLevel"
aliases: 
examples: 
---
