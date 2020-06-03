# New version 2.0

TfsCmdlets is has been gradually rebuilt with three main goals in mind:

- Replace most of the reliance on the old TFS Client Object Model with the new Azure DevOps REST API;
- Make the functions/cmdlets easier to code and mantain, expediting the development process;
- Create a new automated test suite.

> **Note**: Version 2.0 aims to be as much compatible with 1.0 as possible from a syntax perspective. In many cases, it can be used as a drop-in replacement in scripts built on v1.0. However, keep in mind that it is a **breaking** release. YMMV.

Since the last release, a lot has changed. See some highlights:

## New Build Process

Now we have new (but still evolving) [build](https://dev.azure.com/tfscmdlets/TfsCmdlets/_build?definitionId=2) and [release](https://dev.azure.com/tfscmdlets/TfsCmdlets/_release?definitionId=4&view=mine) processes based on Azure DevOps.

## New cmdlets

Although they're technically advanced functions, I guess there's nothing wrong in calling our "commands" cmdlets, right?

In any case, there are 20+ new cmdlets (with many more coming).

The new cmdlets (as of Aug 22, 2019) are:

- Administration
  - Start-TfsIdentitySync
- Areas and Iterations
  - Test-TfsArea
  - Test-TfsIteration
- Git
  - Get-TfsGitBranch
  - Get-TfsGitBranchPolicy
  - Get-TfsGitPolicyType
- Helpers
  - Get-TfsRestClient
- Service Hooks
  - Get-TfsServiceHookConsumer
  - Get-TfsServiceHookNotificationHistory
  - Get-TfsServiceHookPublisher
  - Get-TfsServiceHookSubscription
- Team Project
  - Remove-TfsTeamProject
- Test Case Management
  - Copy-TfsTestPlan
  - Get-TfsTestPlan
  - Remove-TfsTestPlan
- Work (Teams Backlogs and Boards)
  - Get-TfsTeamBacklog
  - Get-TfsTeamBoard
  - Get-TfsTeamBoardCardRuleSettings
  - Set-TfsTeamBoardCardRuleSettings
  - Set-TfsWorkItemBoardStatus
- Work Item
  - Get-TfsWorkItemQueryFolder
  - Get-TfsWorkItemTag
  - Move-TfsWorkItem
  - New-TfsWorkItemQueryFolder
  - New-TfsWorkItemTag
  - Remove-TfsWorkItemQueryFolder
  - Remove-TfsWorkItemTag
  - Rename-TfsWorkItemTag

## Deprecated cmdlets

The following cmdlets are deprecated:

- Get-TfsXamlBuildAgent
- Get-TfsXamlBuildController
- Get-TfsXamlBuildDefinition
- Get-TfsXamlBuildQueue
- New-TfsXamlBuildService

## Renamed cmdlets

- Get-TfsHttpClient -> Get-TfsRestClient

## Azure DevOps REST API

Cmdlets now use the new Azure DevOps REST API. Old cmdlets are still in the process of being migrated from the old TFS Client Object model to the new REST APIs. That is likely a breaking change, as the objects returned by the cmdlets will change. The Work Item-related cmdlets are the most affected.

## PowerShell Core

TfsCmdlets currently is **not** compatible with PowerShell Core, due to its reliance on the TFS Client Object Model (which, in turn, requires Windows and .NET Framework).

As we move towards Azure DevOps REST API, TfsCmdlets will become compatible with PS Core and thus cross-platform.
