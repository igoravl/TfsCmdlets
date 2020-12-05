---
title: Get-TfsRegistryValue
breadcrumbs: [ "Admin", "Registry" ]
parent: "Admin.Registry"
description: "Gets the value of a given Team Foundation Server registry entry. "
remarks: "The 'Get-TfsRegistry' cmdlet retrieves the value of a TFS registry entry at the given path and scope. Registry entries can be scoped to the server, to a collection or to a specific user. "
parameterSets: 
  "_All_": [ Collection, Path, Scope, Server ] 
  "__AllParameterSets":  
    Path: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Scope: 
      type: "RegistryScope"  
    Server: 
      type: "object" 
parameters: 
  - name: "Path" 
    description: "Specifies the full path of the TFS Registry key " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
  - name: "Scope" 
    description: "Specifies the scope under which to search for the key. When omitted, defaults to the Server scope. Possible values: User, Collection, Server" 
    globbing: false 
    type: "RegistryScope" 
    defaultValue: "Server" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
outputs: 
  - type: "System.Object" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Admin/Registry/Get-TfsRegistryValue"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'" 
    remarks: "Gets the current value of the 'EmailEnabled' key in the TFS Registry"
---
