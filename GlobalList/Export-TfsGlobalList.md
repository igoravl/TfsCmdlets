---
title: Export-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Exports the contents of one or more Global Lists to XML. "
remarks: "This cmdlets generates an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to \"witadmin exportgloballist\" "
parameterSets: 
  "_All_": [ Collection, GlobalList ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the global list to be exported. Wildcards are supported. When omitted, it defaults to all global lists in the supplied team project collection. When using wilcards, a single XML document will be producer containing all matching global lists. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the global list to be exported. Wildcards are supported. When omitted, it defaults to all global lists in the supplied team project collection. When using wilcards, a single XML document will be producer containing all matching global lists. This is an alias of the GlobalList parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "System.String" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/GlobalList/Export-TfsGlobalList"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Export-TfsGlobalList | Out-File \"gl.xml\"" 
    remarks: "Exports all global lists in the current project collection to a file called gl.xml." 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Export-TfsGlobalList -Name \"Builds - *\"" 
    remarks: "Exports all build-related global lists (with names starting with \"Build - \") and return the resulting XML document."
---
