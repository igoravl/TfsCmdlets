---
title: Get-TfsTeamBoard
breadcrumbs: [ "Team", "Board" ]
parent: "Team.Board"
description: "Gets one or more team boards. "
remarks: 
parameterSets: 
  "_All_": [ Board, Collection, Project, Team ] 
  "__AllParameterSets":  
    Board: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Team: 
      type: "object" 
parameters: 
  - name: "Board" 
    description: "Specifies the board name. Wildcards are supported. When omitted, returns all boards in the given team. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the board name. Wildcards are supported. When omitted, returns all boards in the given team. This is an alias of the Board parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
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
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.Work.WebApi.Board" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Board/Get-TfsTeamBoard"
aliases: 
examples: 
---
