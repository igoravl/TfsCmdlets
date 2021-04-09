---
title: Get-TfsTeam
breadcrumbs: [ "Team" ]
parent: "Team"
description: "Gets information about one or more teams. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Current, Default, IncludeSettings, Project, QueryMembership, Team ] 
  "Get by team":  
    Team: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    IncludeSettings: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    QueryMembership: 
      type: "SwitchParameter"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    IncludeSettings: 
      type: "SwitchParameter"  
    QueryMembership: 
      type: "SwitchParameter"  
  "Get default team":  
    Default: 
      type: "SwitchParameter"  
      required: true  
    IncludeSettings: 
      type: "SwitchParameter"  
    QueryMembership: 
      type: "SwitchParameter" 
parameters: 
  - name: "Team" 
    description: "Specifies the team to return. Accepted values are its name, its ID, or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported. When omitted, all teams in the given team project are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the team to return. Accepted values are its name, its ID, or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported. When omitted, all teams in the given team project are returned. This is an alias of the Team parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "QueryMembership" 
    description: "Get team members (fills the Members property with a list of Microsoft.VisualStudio.Services.WebApi.TeamMember objects). When omitted, only basic team information (such as name, description and ID) are returned. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeSettings" 
    description: "Gets the team's backlog settings (fills the Settings property with a Microsoft.TeamFoundation.Work.WebApi.TeamSetting object) " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Current" 
    description: "Returns the team specified in the last call to Connect-TfsTeam (i.e. the \"current\" team) " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Default" 
    description: "Returns the default team in the given team project. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.WebApiTeam" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Get-TfsTeam"
aliases: 
examples: 
---
