---
title: Get-TfsConfigurationServer
breadcrumbs: [ "ConfigServer" ]
parent: "ConfigServer"
description: "Gets information about a configuration server.Gets information about a configuration server."
remarks: 
parameterSets: 
  "_All_": [ Credential, Current, Server ] 
  "Get by server":  
    Server: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
      position: "1"  
  "Get current":  
    Current: 
      type: "SwitchParameter"  
      required: true 
parameters: 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Current" 
    description: "Returns the configuration server specified in the last call to Connect-TfsConfigurationServer (i.e. the \"current\" configuration server)" 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument." 
    globbing: false 
    position: 1 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet."
outputs: 
  - type: "Microsoft.TeamFoundation.Client.TfsConfigurationServer" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/ConfigServer/Get-TfsConfigurationServer"
aliases: 
examples: 
---
