---
title: Disable-TfsExtension
breadcrumbs: [ "ExtensionManagement" ]
parent: "ExtensionManagement"
description: "Disables an extension installed in the specified collection/organization. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Extension, Passthru, Publisher, Server ] 
  "__AllParameterSets":  
    Extension: 
      type: "object"  
      position: "0"  
    Publisher: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Extension" 
    description: "Specifies the ID or the name of the extensions. Wildcards are supported. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Publisher" 
    description: "Specifies the ID or the name of the publisher. Wildcards are supported. " 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
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
    description: "Specifies the ID or the name of the extensions. Wildcards are supported. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ExtensionManagement/Disable-TfsExtension"
aliases: 
examples: 
---
