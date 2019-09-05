# What's new in 2.0.0-beta1

## New version 2.0

TfsCmdlets is has been gradually rebuilt with three main goals in mind:

- Replace most of the reliance on the old TFS Client Object Model with the new Azure DevOps REST API;
- Make the functions/cmdlets easier to code and mantain, expediting the development process;
- Create a new automated test suite.

Since the last release, a lot has changed. See some highlights:

### New Build Process

Now we have new (but still evolving) [build](https://dev.azure.com/tfscmdlets/TfsCmdlets/_build?definitionId=2) and [release](https://dev.azure.com/tfscmdlets/TfsCmdlets/_release?definitionId=4&view=mine) processes based on Azure DevOps.

### New cmdlets

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

### Deprecated cmdlets

The following cmdlets are deprecated:

- Get-TfsHttpClient
- Get-TfsXamlBuildAgent
- Get-TfsXamlBuildController
- Get-TfsXamlBuildDefinition
- Get-TfsXamlBuildQueue
- New-TfsXamlBuildService

### Azure DevOps REST API

Cmdlets now use the new Azure DevOps REST API. Old cmdlets are still in the process of being migrated from the old TFS Client Object model to the new REST APIs. That is likely a breaking change, as the objects returned by the cmdlets will change. The Work Item-related cmdlets are the most affected.

### PowerShell Core

TfsCmdlets currently is **not** compatible with PowerShell Core, due to its reliance on the TFS Client Object Model (which, in turn, requires Windows and .NET Framework).

As we move towards Azure DevOps REST API, TfsCmdlets will become compatible with PS Core and thus cross-platform.

------------------------

## Version 2.0.0-beta.6 (_02/Sep/2019_)

### Improvements

- Add new group membership management cmdlets:
  - Add-TfsGroupMember
  - Get-TfsGroupMember
  - Remove-TfsGroupMember

### Bug fixes

N/A

### Known issues

N/A

------------------------

## Previous Versions

### 1.0.0-alpha7 (_22/Oct/2015_)

#### Improvements

- Added essential help comments to all cmdlets. Future versions will improve the documentation quality and depth.

#### Bug Fixes

- Fix Nuget and Chocolatey package icons
- Fix bug in Get-TfsWorkItemType that would not return any WITDs

#### Known Issues

- N/A

### 1.0.0-alpha6 (_22/Oct/2015_)

#### Improvements

- Enable build from cmdline with VS 2015 ([ab2325b](https://github.com/igoravl/tfscmdlets/commit/ab2325bae7cce788292d8532742a230756d1fd06))
- Change PoShTools detection from error to warning ([09e62f3](https://github.com/igoravl/tfscmdlets/commit/09e62f3b034e1706fb5845b3f8588658f99a21f8))
- Skip PoShTools detection in AppVeyor ([47cafa4](https://github.com/igoravl/tfscmdlets/commit/47cafa40f16c3e9c7d6f18594154f994d74cfb9c))
- Add custom type detection logic ([b10f32c](https://github.com/igoravl/tfscmdlets/commit/b10f32c5538576ea3cec7bf9f8b8d4c96eddba56))

#### Bug Fixes

- Fix commit message with apostrophe breaking build ([8066ab8](https://github.com/igoravl/tfscmdlets/commit/8066ab8310fa21111e09c5ecba306914edb6e4ab))
- Add proper parameter initialization for credentials ([d0c4d6c](https://github.com/igoravl/tfscmdlets/commit/d0c4d6c7d28682f43ae730904d802ebf4a2d4584))
- Fix handling of current config server ([d7b53f](https://github.com/igoravl/tfscmdlets/commit/d7b53fca74a66f22f793bed39f1ef3bdf642ae83))

#### Known Issues

- N/A

### 1.0.0-alpha5 (_10/Sep/2015_)

#### Improvements

- Add cascade disconnection: When calling a "higher" Disconnect cmdlet (e.g. Disconnect-TfsConfigurationServer), the "lower" ones (e.g. TeamProjectCollection, TeamProject) are cascade-invoked, in other to prevent inconsistent connection information.

#### Bug fixes

- Fix conditional use of -Title in New-TfsWorkItem ([96a8a60](https://github.com/igoravl/tfscmdlets/commit/818af6e9d6ba3f30e976f3ef20d6070ac50fa3e7))
- Fix argument naming and pipelined return in Get-TfsWorkItem ([0a118125](https://github.com/igoravl/tfscmdlets/commit/0a11812554b447f4418e11454911ca5f53f34924))
- Fix return when passing -Current in Get-TfsConfigurationServer ([58647d2f](https://github.com/igoravl/tfscmdlets/commit/58647d2f29d84d6f5db4e0022062c2dd30cfaba1))

#### Known Issues

- N/A

### 1.0.0-alpha4 (_03/Sep/2015_)

#### Improvements

- Remove dependency on .NET 3.5

#### Bug fixes

- N/A

#### Known Issues

- N/A

### 1.0.0-alpha3 (_03/Sep/2015_)

#### Improvements

- Add help comments to the Areas & Iterations functions

#### Bug fixes

- Fix an issue in the AssemblyResolver implementation. Previously it was implemented as a scriptblock and was running into some race conditions that would crash PowerShell. Switched to a pure .NET implementation in order to avoid the race condition.

#### Known Issues

- TfsCmdlets has currently a dependency on .NET 3.5 and won't load in computers with only .NET 4.x installed. Workaround is to install .NET 3.5. Next version will no longer depend on .NET 3.5.
