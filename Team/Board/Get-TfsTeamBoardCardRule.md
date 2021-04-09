---
title: Get-TfsTeamBoardCardRule
breadcrumbs: [ "Team", "Board" ]
parent: "Team.Board"
description: "Gets one or more team board card rules. "
remarks: 
parameterSets: 
  "_All_": [ Board, Collection, Project, Rule, RuleType, Team ] 
  "__AllParameterSets":  
    Rule: 
      type: "object"  
      position: "0"  
    Board: 
      type: "object"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    RuleType: 
      type: "CardRuleType"  
    Team: 
      type: "object" 
parameters: 
  - name: "Rule" 
    description: "Specifies the rule name. Wildcards are supported. When omitted, returns all card rules in the given board. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "RuleType" 
    description: "Specifies the kind of rule to return. When omitted, returns both rule types (card color and tag color). Possible values: CardColor, TagColor, All" 
    globbing: false 
    type: "CardRuleType" 
    defaultValue: "All" 
  - name: "Board" 
    description: "Specifies the board name. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the board name. "
outputs: 
  - type: "Microsoft.TeamFoundation.Work.WebApi.Rule" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Team/Board/Get-TfsTeamBoardCardRule"
aliases: 
examples: 
---
