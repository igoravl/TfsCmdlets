---
title: Remove-TfsTeamAdmin
breadcrumbs: [ "Team", "TeamAdmin" ]
parent: "Team.TeamAdmin"
description: "Removes an administrator from a team. "
remarks: 
parameterSets: 
  "_All_": [ Admin, Collection, Project, Team ] 
  "__AllParameterSets":  
    Admin: 
      type: "object"  
      position: "0"  
    Team: 
      type: "object"  
      position: "1"  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Admin" 
    description: "Specifies the administrator to remove from the team. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    position: 1 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the administrator to remove from the team. "
outputs: 
  - type: "TfsCmdlets.HttpClient.TeamAdmins" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/TeamAdmin/Remove-TfsTeamAdmin"
aliases: 
examples: 
---
