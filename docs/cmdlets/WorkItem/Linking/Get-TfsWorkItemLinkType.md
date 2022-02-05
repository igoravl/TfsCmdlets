---
title: Get-TfsWorkItemLinkType
breadcrumbs: [ "WorkItem", "Linking" ]
parent: "WorkItem.Linking"
description: "Gets the work item link end types of a team project collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, LinkType, Server ] 
  "__AllParameterSets":  
    LinkType: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "LinkType" 
    description:  
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,EndLinkType,Type,Link ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "This is an alias of the LinkType parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,EndLinkType,Type,Link ] 
    defaultValue: "*" 
  - name: "EndLinkType" 
    description: "This is an alias of the LinkType parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,EndLinkType,Type,Link ] 
    defaultValue: "*" 
  - name: "Type" 
    description: "This is an alias of the LinkType parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,EndLinkType,Type,Link ] 
    defaultValue: "*" 
  - name: "Link" 
    description: "This is an alias of the LinkType parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,EndLinkType,Type,Link ] 
    defaultValue: "*" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Linking/Get-TfsWorkItemLinkType"
aliases: 
examples: 
---
