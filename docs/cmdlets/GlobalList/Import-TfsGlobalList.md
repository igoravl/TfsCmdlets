---
title: Import-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Imports one or more Global Lists from an XML document"
remarks: "This cmdletsimports an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to \"witadmin importgloballist\""
parameterSets: 
  "_All_": [ Collection, Force, InputObject ] 
  "__AllParameterSets":  
    InputObject: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter" 
parameters: 
  - name: "InputObject" 
    description: "XML document object containing one or more global list definitions." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Xml ] 
  - name: "Xml" 
    description: "XML document object containing one or more global list definitions.This is an alias of the InputObject parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Xml ] 
  - name: "Force" 
    description: "Allows the cmdlet to import a global list that already exists." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "XML document object containing one or more global list definitions."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/GlobalList/Import-TfsGlobalList"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Get-Content gl.xml | Import-GlobalList" 
    remarks: "Imports the contents of an XML document called gl.xml to the current project collection"
---
