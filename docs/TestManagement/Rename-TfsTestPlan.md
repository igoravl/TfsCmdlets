---
title: Rename-TfsTestPlan
breadcrumbs: [ "TestManagement" ]
parent: "TestManagement"
description: "Renames a test plans."
remarks: 
parameterSets: 
  "_All_": [ Collection, NewName, Passthru, Project, TestPlan ] 
  "__AllParameterSets":  
    TestPlan: 
      type: "object"  
      position: "0"  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "TestPlan" 
    description: "Specifies the test plan name." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
  - name: "Id" 
    description: "Specifies the test plan name.This is an alias of the TestPlan parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
  - name: "Name" 
    description: "Specifies the test plan name.This is an alias of the TestPlan parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
  - name: "NewName" 
    description: "Specifies the new name of the item. Enter only a name - i.e., for items that support paths, do not enter a path and name." 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies the test plan name."
outputs: 
  - type: "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TestManagement/Rename-TfsTestPlan"
aliases: 
examples: 
---
