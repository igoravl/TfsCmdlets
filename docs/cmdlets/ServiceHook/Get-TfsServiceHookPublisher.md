---
title: Get-TfsServiceHookPublisher
breadcrumbs: [ "ServiceHook" ]
parent: "ServiceHook"
description: "Gets one or more service hook publishers. "
remarks: "Service hook publishers are the components inside of Azure DevOps that can publish (send) notifications triggered by event such as \"work item changed\" or \"build queued\". Use this cmdlet to list the available publishers and get the ID of the desired one to be able to manage service hook subscriptions. "
parameterSets: 
  "_All_": [ Collection, Publisher, Server ] 
  "__AllParameterSets":  
    Publisher: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Publisher" 
    description: "Specifies the name or ID of the service hook publisher to return. Wildcards are supported. When omitted, returns all service hook consumers currently supported the current by Azure DevOps organization / TFS collection. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name or ID of the service hook publisher to return. Wildcards are supported. When omitted, returns all service hook consumers currently supported the current by Azure DevOps organization / TFS collection. This is an alias of the Publisher parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Id" 
    description: "Specifies the name or ID of the service hook publisher to return. Wildcards are supported. When omitted, returns all service hook consumers currently supported the current by Azure DevOps organization / TFS collection. This is an alias of the Publisher parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
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
  - type: "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ServiceHook/Get-TfsServiceHookPublisher"
aliases: 
examples: 
---
