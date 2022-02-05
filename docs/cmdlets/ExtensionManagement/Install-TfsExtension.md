---
title: Install-TfsExtension
breadcrumbs: [ "ExtensionManagement" ]
parent: "ExtensionManagement"
description: "Installs an extension in the specified organization/collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Extension, Publisher, Server, Version ] 
  "__AllParameterSets":  
    Extension: 
      type: "string"  
      position: "0"  
      required: true  
    Publisher: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Server: 
      type: "object"  
    Version: 
      type: "string" 
parameters: 
  - name: "Extension" 
    description: "Specifies the ID of the extension to install. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ ExtensionId ] 
  - name: "ExtensionId" 
    description: "Specifies the ID of the extension to install. This is an alias of the Extension parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ ExtensionId ] 
  - name: "Publisher" 
    description: "Specifies the ID of the publisher of the extension. " 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 1 
    type: "string" 
    aliases: [ PublisherId ] 
  - name: "PublisherId" 
    description: "Specifies the ID of the publisher of the extension. This is an alias of the Publisher parameter." 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 1 
    type: "string" 
    aliases: [ PublisherId ] 
  - name: "Version" 
    description: "Specifies the version of the extension to install. When omitted, installs the latest version. " 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
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
  - type: "System.String" 
    description: "Specifies the ID of the extension to install. " 
  - type: "System.String" 
    description: "Specifies the ID of the publisher of the extension. " 
  - type: "System.String" 
    description: "Specifies the version of the extension to install. When omitted, installs the latest version. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ExtensionManagement/Install-TfsExtension"
aliases: 
examples: 
---
