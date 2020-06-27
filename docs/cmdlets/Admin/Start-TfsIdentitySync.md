---
title: Start-TfsIdentitySync
breadcrumbs: [ "Admin" ]
parent: "Admin"
description: "Triggers an Identity Sync server job."
remarks: 
parameterSets: 
  "_All_": [ Credential, Server, Wait ] 
  "__AllParameterSets":  
    Server: 
      type: "object"  
      position: "0"  
    Credential: 
      type: "object"  
    Wait: 
      type: "SwitchParameter" 
parameters: 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Wait" 
    description: "Waits until the job finishes running. If omitted, the identity sync job will run asynchronously." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Admin/Start-TfsIdentitySync"
aliases: 
examples: 
---
