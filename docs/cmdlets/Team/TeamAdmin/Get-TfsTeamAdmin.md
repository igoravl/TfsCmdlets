---
title: Get-TfsTeamAdmin
breadcrumbs: [ "Team", "TeamAdmin" ]
parent: "Team.TeamAdmin"
description: "Gets the administrators of a team."
remarks: 
parameterSets: 
  "_All_": [ Admin, Collection, Project, Team ] 
  "__AllParameterSets":  
    Team: 
      type: "object"  
      position: "0"  
    Admin: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Admin" 
    description: "Specifies the administrator to get from the given team. Wildcards are supported. When omitted, all administrators are returned." 
    globbing: false 
    position: 1 
    type: "string" 
    defaultValue: "*" 
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
  - type: "Microsoft.VisualStudio.Services.Identity.Identity" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Team/TeamAdmin/Get-TfsTeamAdmin"
aliases: 
examples: 
---