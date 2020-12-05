---
title: Get-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Gets one of more team project collections (organizations in Azure DevOps). "
remarks: 
parameterSets: 
  "_All_": [ Collection, Credential, Current, Server ] 
  "Get by collection":  
    Collection: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
    Server: 
      type: "object"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true 
parameters: 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    position: 0 
    type: "object" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument. " 
    globbing: false 
    type: "object" 
  - name: "Current" 
    description: "Returns the team project collection specified in the last call to Connect-TfsTeamProjectCollection (i.e. the \"current\" project collection) " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.WebApi.VssConnection" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProjectCollection/Get-TfsTeamProjectCollection"
aliases: 
examples: 
---
