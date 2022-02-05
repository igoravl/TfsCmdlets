---
title: Set-TfsRegistryValue
breadcrumbs: [ "Admin", "Registry" ]
parent: "Admin.Registry"
description: "Sets the value of a given Team Foundation Server registry entry. "
remarks: "The 'Set-TfsRegistry' cmdlet changes the value of a TFS registry key to the value specified in the command. "
parameterSets: 
  "_All_": [ Collection, Passthru, Path, Scope, Server, Value ] 
  "__AllParameterSets":  
    Path: 
      type: "string"  
      position: "0"  
      required: true  
    Value: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
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
  - name: "Value" 
    description: "Specifies the new value of the Registry key. To remove an existing value, set it to $null " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Scope" 
    description: "Specifies the scope under which to search for the key. When omitted, defaults to the Server scope. Possible values: User, Collection, Server" 
    globbing: false 
    type: "RegistryScope" 
    defaultValue: "Server" 
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
outputs: 
  - type: "System.Object" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Admin/Registry/Set-TfsRegistryValue"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'" 
    remarks: "Gets the current value of the 'EmailEnabled' key in the TFS Registry"
---
