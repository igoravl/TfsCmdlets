---
title: Get-TfsUser
breadcrumbs: [ "Identity", "User" ]
parent: "Identity.User"
description: "Gets information about one or more Azure DevOps users. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Current, Server, User ] 
  "Get by ID or Name":  
    User: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Server: 
      type: "object"  
  "Get current user":  
    Current: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "User" 
    description: "Specifies the user or group to be retrieved. Supported values are: User/group name, email, or ID " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ UserId ] 
    defaultValue: "*" 
  - name: "UserId" 
    description: "Specifies the user or group to be retrieved. Supported values are: User/group name, email, or ID This is an alias of the User parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ UserId ] 
    defaultValue: "*" 
  - name: "Current" 
    description: "Returns an identity representing the user currently logged in to the Azure DevOps / TFS instance " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
    description: "Specifies the user or group to be retrieved. Supported values are: User/group name, email, or ID "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Licensing.AccountEntitlement" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/User/Get-TfsUser"
aliases: 
examples: 
---
