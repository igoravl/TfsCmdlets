---
title: New-TfsTestPlan
breadcrumbs: [ "TestManagement" ]
parent: "TestManagement"
description: "Creates a new test plan."
remarks: 
parameterSets: 
  "_All_": [ AreaPath, Collection, EndDate, IterationPath, Owner, Passthru, Project, StartDate, TestPlan ] 
  "__AllParameterSets":  
    TestPlan: 
      type: "string"  
      position: "0"  
      required: true  
    AreaPath: 
      type: "string"  
    Collection: 
      type: "object"  
    EndDate: 
      type: "DateTime"  
    IterationPath: 
      type: "string"  
    Owner: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    StartDate: 
      type: "DateTime" 
parameters: 
  - name: "TestPlan" 
    description: "Specifies the test plan name." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the test plan name.This is an alias of the TestPlan parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "AreaPath" 
    description: "Specifies the owner of the new test plan." 
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description: "Specifies the owner of the new test plan." 
    globbing: false 
    type: "string" 
  - name: "StartDate" 
    description: "Specifies the start date of the test plan." 
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "EndDate" 
    description: "Specifies the end date of the test plan." 
    globbing: false 
    type: "DateTime" 
    defaultValue: "01/01/0001 00:00:00" 
  - name: "Owner" 
    description: "Specifies the owner of the new test plan." 
    globbing: false 
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
outputs: 
  - type: "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TestManagement/New-TfsTestPlan"
aliases: 
examples: 
---
