---
title: Get-TfsServiceHookNotificationHistory
breadcrumbs: [ "ServiceHook" ]
parent: "ServiceHook"
description: "Gets the notification history for a given service hook subscription "
remarks: 
parameterSets: 
  "_All_": [ Collection, From, Server, Status, Subscription, To ] 
  "__AllParameterSets":  
    Subscription: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    From: 
      type: "DateTime"  
    Server: 
      type: "object"  
    Status: 
      type: "NotificationStatus"  
    To: 
      type: "DateTime" 
parameters: 
  - name: "Subscription" 
    description: "Specifies the subscription to get the notification history from. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "From" 
    description: "Specifies the beginning of a date interval to filter notifications on. " 
    globbing: false 
    type: "DateTime" 
  - name: "To" 
    description: "Specifies the end of a date interval to filter notifications on. " 
    globbing: false 
    type: "DateTime" 
  - name: "Status" 
    description: "Specifies the notification status to filter on. Possible values: Queued, Processing, RequestInProgress, Completed" 
    globbing: false 
    type: "NotificationStatus" 
    defaultValue: "0" 
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
    description: "Specifies the subscription to get the notification history from. " 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ServiceHook/Get-TfsServiceHookNotificationHistory"
aliases: 
examples: 
---
