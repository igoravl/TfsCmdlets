---
title: Set-TfsTeamBoardCardRule
breadcrumbs: [ "Team", "Board" ]
parent: "Team.Board"
description: "Set the card rule settings of the specified backlog board. "
remarks: 
parameterSets: 
  "_All_": [ CardStyleRuleFilter, CardStyleRuleName, CardStyleRuleSettings, Collection, Passthru, Project, Rules, Server, TagStyleRuleFilter, TagStyleRuleName, TagStyleRuleSettings, Team, WebApiBoard ] 
  "Bulk set":  
    WebApiBoard: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Rules: 
      type: "BoardCardRuleSettings"  
    Server: 
      type: "object"  
    Team: 
      type: "object"  
  "Set individual rules":  
    WebApiBoard: 
      type: "object"  
      position: "0"  
    CardStyleRuleFilter: 
      type: "string"  
    CardStyleRuleName: 
      type: "string"  
    CardStyleRuleSettings: 
      type: "Hashtable"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
    TagStyleRuleFilter: 
      type: "string"  
    TagStyleRuleName: 
      type: "string"  
    TagStyleRuleSettings: 
      type: "Hashtable"  
    Team: 
      type: "object" 
parameters: 
  - name: "WebApiBoard" 
    description: "Specifies the board name. Wildcards are supported. When omitted, returns card rules for all boards in the given team. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Rules" 
    description:  
    globbing: false 
    type: "BoardCardRuleSettings" 
  - name: "CardStyleRuleName" 
    description:  
    globbing: false 
    type: "string" 
  - name: "CardStyleRuleFilter" 
    description:  
    globbing: false 
    type: "string" 
  - name: "CardStyleRuleSettings" 
    description:  
    globbing: false 
    type: "Hashtable" 
  - name: "TagStyleRuleName" 
    description:  
    globbing: false 
    type: "string" 
  - name: "TagStyleRuleFilter" 
    description:  
    globbing: false 
    type: "string" 
  - name: "TagStyleRuleSettings" 
    description:  
    globbing: false 
    type: "Hashtable" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Team" 
    description: "Specifies the name of the Team, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeam (if any). For more details, see the Get-TfsTeam cmdlet. " 
    globbing: false 
    type: "object" 
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
    description: "Specifies the board name. Wildcards are supported. When omitted, returns card rules for all boards in the given team. "
outputs: 
  - type: "Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Board/Set-TfsTeamBoardCardRule"
aliases: 
examples: 
---
