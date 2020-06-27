---
title: Copy-TfsTestPlan
breadcrumbs: [ "TestManagement" ]
parent: "TestManagement"
description: "Clones a test plan and, optionally, its test suites and test cases."
remarks: "The Copy-TfsTestPlan copies (\"clones\") a test plan to help duplicate test suites and/or test cases. Cloning is useful if you want to branch your application into two versions. After copying, the tests for the two versions can be changed without affecting each other. When you clone a test suite, the following objects are copied from the source test plan to the destination test plan: * Test cases (note: Each new test case retains its shared steps. A link is made between the source and new test cases. The new test cases do not have test runs, bugs, test results, and build information); * Shared steps referenced by cloned test cases; * Test suites (note: The following data is retained - Names and hierarchical structure of the test suites; Order of the test cases; Assigned testers; Configurations); * Action Recordings linked from a cloned test case; * Links and Attachments; * Test configuration. The items below are only copied when using -CloneRequirements: * Requirements-based suites; * Requirements work items (product backlog items or user stories); * Bug work items, when in a project that uses the Scrum process template or any other project in which the Bug work item type is in the Requirements work item category. In other projects, bugs are not cloned."
parameterSets: 
  "_All_": [ AreaPath, CloneRequirements, Collection, CopyAllSuites, CopyAncestorHierarchy, DeepClone, DestinationProject, DestinationWorkItemType, IterationPath, NewName, Passthru, Project, RelatedLinkComment, SuiteIds, TestPlan ] 
  "__AllParameterSets":  
    AreaPath: 
      type: "string"  
    CloneRequirements: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    CopyAllSuites: 
      type: "SwitchParameter"  
    CopyAncestorHierarchy: 
      type: "SwitchParameter"  
    DeepClone: 
      type: "SwitchParameter"  
    DestinationProject: 
      type: "object"  
    DestinationWorkItemType: 
      type: "string"  
    IterationPath: 
      type: "string"  
    NewName: 
      type: "string"  
    Passthru: 
      type: "string"  
    Project: 
      type: "object"  
    RelatedLinkComment: 
      type: "string"  
    SuiteIds: 
      type: "int[]"  
    TestPlan: 
      type: "object" 
parameters: 
  - name: "TestPlan" 
    description:  
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "This is an alias of the TestPlan parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ id ] 
  - name: "NewName" 
    description:  
    globbing: false 
    type: "string" 
  - name: "DestinationProject" 
    description:  
    globbing: false 
    type: "object" 
    aliases: [ Destination ] 
  - name: "Destination" 
    description: "This is an alias of the DestinationProject parameter." 
    globbing: false 
    type: "object" 
    aliases: [ Destination ] 
  - name: "AreaPath" 
    description:  
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description:  
    globbing: false 
    type: "string" 
  - name: "DeepClone" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "CopyAllSuites" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    aliases: [ Recurse ] 
    defaultValue: "False" 
  - name: "Recurse" 
    description: "This is an alias of the CopyAllSuites parameter." 
    globbing: false 
    type: "SwitchParameter" 
    aliases: [ Recurse ] 
    defaultValue: "False" 
  - name: "CopyAncestorHierarchy" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "CloneRequirements" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "DestinationWorkItemType" 
    description:  
    globbing: false 
    type: "string" 
    defaultValue: "Test Case" 
  - name: "SuiteIds" 
    description:  
    globbing: false 
    type: "int[]" 
  - name: "RelatedLinkComment" 
    description:  
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
    type: "string" 
    defaultValue: "None"
inputs: 
  - type: "System.Object" 
    description: 
outputs: 
  - type: "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TestManagement/Copy-TfsTestPlan"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Copy-TfsTestPlan -TestPlan \"My test plan\" -Project \"SourceProject\" -Destination \"TargetProject\" -NewName \"My new test plan\"" 
    remarks: 
---
