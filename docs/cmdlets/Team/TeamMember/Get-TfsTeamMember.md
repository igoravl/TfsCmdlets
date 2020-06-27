---
title: Get-TfsTeamMember
breadcrumbs: [ "Team", "TeamMember" ]
parent: "Team.TeamMember"
description: "Gets the members of a team."
remarks: 
parameterSets: 
  "_All_": [ Collection, Member, Recurse, Team ] 
  "__AllParameterSets":  
    Team: 
      type: "object"  
      position: "0"  
    Member: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Recurse: 
      type: "SwitchParameter" 
parameters: 
  - name: "Team" 
    description: "Specifies the team from which to get its members." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Member" 
    description: "Specifies the member (user or group) to get from the given team. Wildcards are supported. When omitted, all team members are returned." 
    globbing: false 
    position: 1 
    type: "string" 
    defaultValue: "*" 
  - name: "Recurse" 
    description: "Recursively expands all member groups, returning the users and/or groups contained in them" 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the team from which to get its members."
outputs: 
  - type: "Microsoft.VisualStudio.Services.Identity.Identity" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Team/TeamMember/Get-TfsTeamMember"
aliases: 
examples: 
---
