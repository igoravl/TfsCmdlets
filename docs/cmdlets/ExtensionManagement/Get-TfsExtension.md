---
title: Get-TfsExtension
breadcrumbs: [ "ExtensionManagement" ]
parent: "ExtensionManagement"
description: "Gets one or more installed extensions in the specified collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Extension, IncludeDisabledExtensions, IncludeErrors, IncludeInstallationIssues, Publisher, Server ] 
  "__AllParameterSets":  
    Extension: 
      type: "object"  
      position: "0"  
    Publisher: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    IncludeDisabledExtensions: 
      type: "SwitchParameter"  
    IncludeErrors: 
      type: "SwitchParameter"  
    IncludeInstallationIssues: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Extension" 
    description: "Specifies the ID or the name of the extensions. Wilcards are supported. When omitted, returns all extensions installed in the specified organization/collection. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Publisher" 
    description: "Specifies the ID or the name of the publisher. Wilcards are supported. When omitted, returns all extensions installed in the specified organization/collection. " 
    globbing: false 
    position: 1 
    type: "string" 
    defaultValue: "*" 
  - name: "IncludeDisabledExtensions" 
    description: "Includes disabled extensions in the result. When omitted, disabled extensions are not included in the result. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeErrors" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeInstallationIssues" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
  - type: "Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ExtensionManagement/Get-TfsExtension"
aliases: 
examples: 
---
