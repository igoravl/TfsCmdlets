---
title: Export-TfsProcessTemplate
breadcrumbs: [ "ProcessTemplate" ]
parent: "ProcessTemplate"
description: "Exports a XML-based process template definition to disk. "
remarks: "This cmdlet offers a functional replacement to the \"Export Process Template\" feature found in Team Explorer. All files pertaining to the specified process template (work item defininitons, reports, saved queries, process configuration and so on) are downloaded from the given Team Project Collection and saved in a local directory, preserving the directory structure required to later re-import it. This is specially handy to do small changes to a process template or to create a new process template based on an existing one. "
parameterSets: 
  "_All_": [ Collection, DestinationPath, Force, NewDescription, NewName, ProcessTemplate, Server ] 
  "__AllParameterSets":  
    ProcessTemplate: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    DestinationPath: 
      type: "string"  
    Force: 
      type: "SwitchParameter"  
    NewDescription: 
      type: "string"  
    NewName: 
      type: "string"  
    Server: 
      type: "object" 
parameters: 
  - name: "ProcessTemplate" 
    description: "Specifies the name of the process template(s) to be exported. Wildcards are supported. When omitted, all process templates in the given project collection are exported. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the process template(s) to be exported. Wildcards are supported. When omitted, all process templates in the given project collection are exported. This is an alias of the ProcessTemplate parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "Process" 
    description: "Specifies the name of the process template(s) to be exported. Wildcards are supported. When omitted, all process templates in the given project collection are exported. This is an alias of the ProcessTemplate parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "DestinationPath" 
    description: "Path to the target directory where the exported process template (and related files) will be saved. A folder with the process template name will be created under this path. When omitted, templates are exported in the current directory. " 
    globbing: false 
    type: "string" 
  - name: "NewName" 
    description: "Saves the exported process template with a new name. Useful when exporting a base template which will be used as a basis for a new process template. When omitted, the original name is used. " 
    globbing: false 
    type: "string" 
  - name: "NewDescription" 
    description: "Saves the exported process template with a new description. Useful when exporting a base template which will be used as a basis for a new process template.  When omitted, the original description is used. " 
    globbing: false 
    type: "string" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing destination folder. " 
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
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ProcessTemplate/Export-TfsProcessTemplate"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Export-TfsProcessTemplate -Process \"Scrum\" -DestinationPath C:\\PT -Collection http://vsalm:8080/tfs/DefaultCollection" 
    remarks: "Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\\PT\\Scrum directory in the local computer." 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Export-TfsProcessTemplate -Process \"Scrum\" -DestinationPath C:\\PT -Collection http://vsalm:8080/tfs/DefaultCollection -NewName \"MyScrum\" -NewDescription \"A customized version of the Scrum process template\"" 
    remarks: "Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\\PT\\MyScrum directory in the local computer. Notice that the process template is being renamed from Scrum to MyScrum, so that it can be later reimported as a new process template instead of overwriting the original one."
---
