---
title: Uninstall-TfsExtension
breadcrumbs: [ "ExtensionManagement" ]
parent: "ExtensionManagement"
description: "Uninstalls one of more extensions from the specified organization/collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Extension, Publisher, Server ] 
  "__AllParameterSets":  
    Extension: 
      type: "object"  
      position: "0"  
      required: true  
    Publisher: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Extension" 
    description: "Specifies the ID of the extension to uninstall. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Publisher" 
    description: "Specifies the ID of the publisher of the extension. " 
    globbing: false 
    position: 1 
    type: "string" 
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
    description: "Specifies the ID of the extension to uninstall. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ExtensionManagement/Uninstall-TfsExtension"
aliases: 
examples: 
---
