---
title: Get-TfsServiceHookConsumer
breadcrumbs: [ "ServiceHook" ]
parent: "ServiceHook"
description: "Gets one or more service hook consumers. "
remarks: "Service hook consumers are the services that can consume (receive) notifications triggered by Azure DevOps. Examples of consumers available out-of-box with Azure DevOps are Microsoft Teams, Slack, Trello ou the generic WebHook consumer. Use this cmdlet to list the available consumers and get the ID of the desired one to be able to manage service hook subscriptions. "
parameterSets: 
  "_All_": [ Collection, Consumer ] 
  "__AllParameterSets":  
    Consumer: 
      type: "string"  
      position: "0"  
    Collection: 
      type: "object" 
parameters: 
  - name: "Consumer" 
    description: "Specifies the name or ID of the service hook consumer to return. Wildcards are supported. When omitted, all service hook consumers registered in the given project collection/organization are returned. " 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name or ID of the service hook consumer to return. Wildcards are supported. When omitted, all service hook consumers registered in the given project collection/organization are returned. This is an alias of the Consumer parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Id" 
    description: "Specifies the name or ID of the service hook consumer to return. Wildcards are supported. When omitted, all service hook consumers registered in the given project collection/organization are returned. This is an alias of the Consumer parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
outputs: 
  - type: "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ServiceHook/Get-TfsServiceHookConsumer"
aliases: 
examples: 
---
