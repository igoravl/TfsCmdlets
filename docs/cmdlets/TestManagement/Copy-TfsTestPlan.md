---
title: Copy-TfsTestPlan
breadcrumbs: [ "TestManagement" ]
parent: "TestManagement"
description: "Clone a test plan and, optionally, its test suites and test cases. "
remarks: "The Copy-TfsTestPlan copies (\"clones\") a test plan to help duplicate test suites and/or test cases. Cloning is useful if you want to branch your application into two versions. After copying, the tests for the two versions can be changed without affecting each other. When you clone a test suite, the following objects are copied from the source test plan to the destination test plan: * Test cases (note: Each new test case retains its shared steps. A link is made between the source and new test cases. The new test cases do not have test runs, bugs, test results, and build information); * Shared steps referenced by cloned test cases; * Test suites (note: The following data is retained - Names and hierarchical structure of the test suites; Order of the test cases; Assigned testers; Configurations); * Action Recordings linked from a cloned test case; * Links and Attachments; * Test configuration. The items below are only copied when using -CloneRequirements: * Requirements-based suites; * Requirements work items (product backlog items or user stories); * Bug work items, when in a project that uses the Scrum process template or any other project in which the Bug work item type is in the Requirements work item category. In other projects, bugs are not cloned. "
parameterSets: 
  "_All_": [ AreaPath, CloneRequirements, Collection, CopyAncestorHierarchy, DeepClone, Destination, DestinationWorkItemType, IterationPath, NewName, Passthru, Project, Recurse, RelatedLinkComment, Server, SuiteIds, TestPlan ] 
  "__AllParameterSets":  
    TestPlan: 
      type: "object"  
      position: "0"  
      required: true  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    AreaPath: 
      type: "string"  
    CloneRequirements: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    CopyAncestorHierarchy: 
      type: "SwitchParameter"  
    DeepClone: 
      type: "SwitchParameter"  
    Destination: 
      type: "object"  
    DestinationWorkItemType: 
      type: "string"  
    IterationPath: 
      type: "string"  
    Passthru: 
      type: "string"  
    Project: 
      type: "object"  
    Recurse: 
      type: "SwitchParameter"  
    RelatedLinkComment: 
      type: "string"  
    Server: 
      type: "object"  
    SuiteIds: 
      type: "int[]" 
parameters: 
  - name: "TestPlan" 
    description: "Specifies the name of the test plan to clone. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies the name of the test plan to clone. This is an alias of the TestPlan parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "NewName" 
    description: "Specifies the name of the new test plan. " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Destination" 
    description: "Specifies the name of the team project where the test plan will be cloned into. When omitted, the test plan is cloned into the same team project of the original test plan. " 
    globbing: false 
    type: "object" 
  - name: "AreaPath" 
    description: "Specifies the area path where the test plan will be cloned into. When omitted, the test plan is cloned into the same area path of the original test plan. " 
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description: "Specifies the iteration path where the test plan will be cloned into. When omitted, the test plan is cloned into the same iteration path of the original test plan. " 
    globbing: false 
    type: "string" 
  - name: "DeepClone" 
    description: "Clones all the referenced test cases. When omitted, only the test plan is cloned; the original test cases are only referenced in the new plan, not duplicated. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Recurse" 
    description: "Clone all test suites recursively. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "CopyAncestorHierarchy" 
    description: "Copies ancestor hierarchy. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "CloneRequirements" 
    description: "Clones requirements referenced by the test plan. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "DestinationWorkItemType" 
    description: "Specifies the name of the workitem type of the clone. " 
    globbing: false 
    type: "string" 
    defaultValue: "Test Case" 
  - name: "SuiteIds" 
    description: "Clones only the specified suites. " 
    globbing: false 
    type: "int[]" 
  - name: "RelatedLinkComment" 
    description: "Specifies the comment of the Related link that is created ato point to the original test plan. " 
    globbing: false 
    type: "string" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "string" 
    defaultValue: "None" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
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
    description: "Specifies the name of the test plan to clone. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TestManagement/Copy-TfsTestPlan"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Copy-TfsTestPlan -TestPlan \"My test plan\" -Project \"SourceProject\" -Destination \"TargetProject\" -NewName \"My new test plan\"" 
    remarks: 
---
