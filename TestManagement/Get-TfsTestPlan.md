---
title: Get-TfsTestPlan
breadcrumbs: [ "TestManagement" ]
parent: "TestManagement"
description: "Gets the contents of one or more test plans. "
remarks: 
parameterSets: 
  "_All_": [ Active, Collection, NoPlanDetails, Owner, Project, TestPlan ] 
  "__AllParameterSets":  
    TestPlan: 
      type: "object"  
      position: "0"  
    Active: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    NoPlanDetails: 
      type: "SwitchParameter"  
    Owner: 
      type: "string"  
    Project: 
      type: "object" 
parameters: 
  - name: "TestPlan" 
    description: "Specifies the test plan name. Wildcards are supported. When omitted, returns all test cases in the given team project. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
    defaultValue: "*" 
  - name: "Id" 
    description: "Specifies the test plan name. Wildcards are supported. When omitted, returns all test cases in the given team project. This is an alias of the TestPlan parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the test plan name. Wildcards are supported. When omitted, returns all test cases in the given team project. This is an alias of the TestPlan parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Id,Name ] 
    defaultValue: "*" 
  - name: "Owner" 
    description: "Gets only the plans owned by the specified user. " 
    globbing: false 
    type: "string" 
  - name: "NoPlanDetails" 
    description: "Get only basic properties of the test plan. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Active" 
    description: "Get only the active plans. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TestManagement/Get-TfsTestPlan"
aliases: 
examples: 
---
