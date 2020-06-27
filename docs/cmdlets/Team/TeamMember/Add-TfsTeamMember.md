---
title: Add-TfsTeamMember
breadcrumbs: [ "Team", "TeamMember" ]
parent: "Team.TeamMember"
description: "Adds new members to a team."
remarks: 
parameterSets: 
  "_All_": [ Collection, Member, Project, Team ] 
  "__AllParameterSets":  
    Member: 
      type: "object"  
      position: "0"  
      required: true  
    Team: 
      type: "object"  
      position: "1"  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Member" 
    description: "Specifies the member (user or group) to add to the given team." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Team" 
    description: "Specifies the team to which the member is added." 
    globbing: false 
    position: 1 
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
    description: "Specifies the member (user or group) to add to the given team."
outputs: 
  - type: "TfsCmdlets.HttpClient.TeamAdmins" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Team/TeamMember/Add-TfsTeamMember"
aliases: 
examples: 
---
