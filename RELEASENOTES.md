# Changelog

All notable changes to TfsCmdlets will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

## [2.10.0] - 2026-03-27

### Added

- `-AzureLogin` switch parameter for all credential-bearing cmdlets (e.g. `Connect-TfsTeamProjectCollection`, `Connect-TfsOrganization`, and all cmdlets that accept credential parameters). Uses `DefaultAzureCredential` from the Azure.Identity SDK to authenticate to Azure DevOps by inheriting the current Azure authentication context (Azure CLI, Managed Identity, Visual Studio, Environment Variables, etc.). Tokens are short-lived (~1 hour) and are automatically renewed when they expire.
- `Azure.Identity` NuGet package dependency for Azure credential support.

## [2.9.2] - 2025-07-30

### Fixed

- `Get-TfsGitItem`: Fixed an issue where the cmdlet would fail when specifying a commit SHA.

## [2.9.1] - 2025-06-05

### Fixed

- `Get-TfsVersion`: Adds support for Azure DevOps Services version "20" (currently mapped to 2022).
- `Remove-TfsTeamProject`: Removes a double-confirmation prompt when using the `-Hard` switch, simplifying the deletion process.
- `Set-TfsWorkItem`: Fixed an issue where the actual work item ID wasn't being used when retrieving work item type information, causing some field updates to fail.

## [2.9.0] - 2024-08-15

### Added

- `Get-ProcessFieldDefinition` — Gets information from one or more organization-wide work item fields.
- `New-ProcessFieldDefinition` — Creates a new organization-wide work item field.
- `Remove-ProcessFieldDefinition` — Removes an organization-wide work item field.

### Changed

- `Get-TfsGitBranch`: Added a new `-Compare` argument to get the "Compare" (base) branch of a given repository.
- `Connect-TfsTeamProjectCollection`, `Connect-TfsOrganization`: Now throws an error when trying to connect with invalid credentials instead of silently going into "anonymous mode".
- **BREAKING**: `Get-TfsGitBranch`: `-Repository` parameter is now mandatory.
- **BREAKING**: `Get-TfsGitBranchPolicy`: Both `-Branch` and `-Repository` parameters are now mandatory.

### Fixed

- `Get-TfsArtifact`: Fixed an issue where it wouldn't list deleted packages.
- `Get-TfsArtifactFeed`: Fixed an issue where it would ignore the `-Project` argument and thus not filter feeds by project.
- `Get-TfsWorkItemTag`: Fixed an issue where it would fail when given a list of tags as input.
- `Get-TfsWorkItemType`: Fixed an issue where it would throw a "Parameter count mismatch" error when trying to get the work item type of a given work item.

## [2.8.2] - 2024-07-24

### Changed

- `Add-TfsWorkItemLink`: Added support for specifying the arguments `-SuppressNotification` and `-BypassRules`.
- When a cmdlet fails, a full exception dump is now sent to the verbose output stream in addition to the short error message.

### Fixed

- Fixed an issue where `Get-TfsTeam` would not work when specifying the `-Default` parameter.
- Fixed an issue where `Invoke-TfsRestApi` would ignore the parameters passed via the `-Parameters` argument ([#228](https://github.com/igoravl/TfsCmdlets/issues/228)).

## [2.8.1] - 2024-07-16

### Fixed

- Fixed an issue where the `New-TfsUser` cmdlet could throw an error when not supplying project entitlements via the `-Project` argument.

## [2.8.0] - 2024-07-09

### Added

- `New-TfsUser` — Creates a new user in the organization and optionally adds them to projects.
- `Remove-TfsUser` — Removes a user from the organization.

## [2.7.1] - 2024-07-03

### Fixed

- Fixed an issue with the Chocolatey publishing process. No changes to cmdlets.

## [2.7.0] - 2024-07-03

### Added

- `-SuppressNotifications` switch for `New-TfsWorkItem` and `Set-TfsWorkItem` to suppress notifications when creating or updating work items.

## [2.6.1] - 2024-05-15

### Fixed

- `Get-TfsWorkItem`: Fixed error when the `-Fields` parameter was "*" ([#211](https://github.com/igoravl/TfsCmdlets/issues/211)).
- `Invoke-TfsRestApi`: Fixed error when Azure DevOps API responses were missing the `content-type` header.
- `Get-TfsArea`, `Get-TfsIteration`: Fixed error when team projects contained regex-reserved characters (such as parentheses). This also affected `New-TfsTeam` and `Set-TfsTeam`.
- `Get-TfsWorkItem`: Fixed issue where the `-AreaPath` and `-IterationPath` parameters would not work when the path either started with a backslash or did not contain the team project name.
- Added the installed module version to the _Azure DevOps Shell_ startup command to prevent loading an older version of the module.

## [2.6.0] - 2022-09-30

### Added

- `Undo-TfsWorkItemQueryRemoval` — Restores a deleted work item query.
- `Undo-TfsWorkItemQueryFolderRemoval` — Restores a deleted work item query folder (supports `-Recursive`).

### Fixed

- `Get-TfsWorkItemQuery`, `Get-TfsWorkItemQueryFolder`: Fixed a bug where the `-Deleted` switch was not respected.

## [2.5.1] - 2022-08-22

### Fixed

- `New-TfsWorkItem`: Fixed issue where AreaPath and IterationPath arguments were switched ([#191](https://github.com/igoravl/TfsCmdlets/issues/191)).

## [2.5.0] - 2022-08-03

### Added

- `Start-TfsBuild` — Starts a pipeline (YAML) / build (Classic).
- `Get-TfsTeamProjectMember` — Returns the members of a team project ([#59](https://github.com/igoravl/TfsCmdlets/issues/59)).

### Fixed

- `New-TfsWorkItem`, `Set-TfsWorkItem`: Fixed a bug where IterationPath was not being set.

## [2.4.1] - 2022-07-20

### Changed

- Identity fields in `New-TfsWorkItem` and `Set-TfsWorkItem` (such as _"Assigned To"_) now support either email addresses or user display names as valid values.

### Fixed

- `Set-TfsWorkItem`: Fixed failure with type conversion error ([#172](https://github.com/igoravl/TfsCmdlets/issues/172)).
- `New-TfsWorkItem`, `Set-TfsWorkItem`: Fixed bug where changes to the **AssignedTo** field would not be reflected.

## [2.4.0] - 2022-05-23

### Changed

- All `Connect-*` cmdlets now support interactive (`-Interactive`) logon in PowerShell Core (6+) shells. Previously, only Windows PowerShell (5.*) supported interactive authentication. Interactive logons in PowerShell Core require Azure DevOps Services.
- `Get-TfsWorkItemQueryFolder` can now return the "root" folders (My Queries and/or Shared Queries) when specifying `/` as the folder path.

### Fixed

- `Get-TfsWorkItem`: Fixed returning an invalid ID due to a change in the WorkItem REST API response ([#172](https://github.com/igoravl/TfsCmdlets/issues/172)).
- `New-TfsWorkItemQuery`, `New-TfsWorkItemQueryFolder`: Fixed issue where queries and folders could not be created when their parent did not exist.

## [2.3.1] - 2022-04-07

### Fixed

- `Get-TfsTeam`, `Get-TfsTeamProject`: Fixed limitation to a maximum of 100 results.
- `Get-TfsTeamProjectCollection` (`Get-TfsOrganization`): Fixed "Invalid or non-existent Collection System.Object[]" error ([#165](https://github.com/igoravl/TfsCmdlets/issues/165)).
- Fixed a caching bug in the handling of the `-Project` parameter that could lead to the wrong project being returned.
- Fixed pipelining bugs in several cmdlets (most noticeably `Get-TfsRepository`).
- Improved readability of ShouldProcess (Confirm / WhatIf) output in several cmdlets.

## [2.3.0] - 2022-04-03

### Added

- `Get-TfsArtifact` — Lists artifacts.
- `Get-TfsArtifactFeed` — Lists artifact feeds.
- `Get-TfsArtifactVersion` — Lists artifact versions.
- `Get-TfsArtifactFeedView` — Lists artifact feed views.
- `Remove-TfsGitBranch` — Removes a Git branch.

### Changed

- `New-TfsTeam`: Added `-AreaPaths` argument so team area paths can be defined at team creation time.
- `Get-TfsWorkItem`: Supports long result sets again. Added fallback logic to fetch work items one at a time to circumvent the 200-item batch API limitation ([#164](https://github.com/igoravl/TfsCmdlets/issues/164)).

### Fixed

- `Add-TfsTeamAdmin`: Groups can now be added as team administrators (previously limited to users).
- `Get-TfsTeamMember`, `Get-TfsTeamAdmin`: Fixed issue where group members would not be returned.
- `New-TfsTeam`: Fixed issue where `-NoDefaultArea` and `-NoBacklogIteration` were not respected.

## [2.2.1] - 2022-02-10

### Changed

- `New-TfsGitRepository`: Can now fork existing Git repositories.
- `Get-TfsGitRepository`: Can retrieve information about the parent (forked) repository.

### Fixed

- `New-TfsWorkItem`: Error when not specifying area/iteration paths ([#158](https://github.com/igoravl/TfsCmdlets/issues/158)).
- `New-TfsWorkItem`: Error when passing backslash to area/iteration paths ([#159](https://github.com/igoravl/TfsCmdlets/issues/159)).
- `Set-TfsTeam`: Error when setting recursive area path value ([#160](https://github.com/igoravl/TfsCmdlets/issues/160)).
- `New-TfsTeam`: Error when calling with DefaultAreaPath and/or BacklogIteration arguments set to non-existing paths.

## [2.2.0] - 2022-02-05

### Added

- `Enable-TfsExtension`, `Disable-TfsExtension`, `Get-TfsExtension`, `Install-TfsExtension`, `Uninstall-TfsExtension` — Extension management cmdlets.
- `Get-TfsGitItem` — Gets items from a Git repository.
- `Get-TfsGroup`, `New-TfsGroup`, `Remove-TfsGroup`, `Get-TfsUser` — Identity, user and group cmdlets.
- `Get-TfsBuildDefinition`, `Enable-TfsBuildDefinition`, `Disable-TfsBuildDefinition`, `Suspend-TfsBuildDefinition`, `Resume-TfsBuildDefinition` — Pipeline cmdlets.
- `Connect-TfsOrganization`, `Disconnect-TfsOrganization`, `Get-TfsOrganization` — Organization cmdlets.
- `Import-TfsTeamProjectAvatar`, `Export-TfsTeamProjectAvatar`, `Remove-TfsTeamProjectAvatar` — Team project avatar cmdlets.
- `Enable-TfsWorkItemTag`, `Disable-TfsWorkItemTag`, `Undo-TfsWorkItemRemoval` — Work item cmdlets.

### Changed

- `Import-TfsWorkItemType`, `Export-TfsWorkItemType`: Now support importing from / exporting to files.
- `Enter-TfsShell`: Added `-NoProfile` ([#145](https://github.com/igoravl/TfsCmdlets/issues/145)).
- Most `Get-*` cmdlets now support retrieving multiple items at once ([#155](https://github.com/igoravl/TfsCmdlets/issues/155)).
- Removed trailing slash from org name in prompt ([#148](https://github.com/igoravl/TfsCmdlets/issues/148)).
- `Get-TfsTeamProject`: Added `-IncludeDetails` switch. Project details (e.g. process template) are no longer fetched by default for performance (**BREAKING**).
- `Get-TfsWorkItem`: Now makes batch API calls to improve performance. Results are limited to 200 work items per call (**BREAKING**).
- `Invoke-TfsRestApi`: Can now save the response to a file via `-Destination`. Binary content is returned as a byte array.
- `Set-TfsTeam`: New arguments `-OverwriteAreaPaths` and `-OverwriteIterationPaths`. Both `-AreaPaths` and `-IterationPaths` now support wildcards.
- `Get-TfsTeamProject`: Added filter by process template ([#104](https://github.com/igoravl/TfsCmdlets/issues/104)).

### Removed

- Temporarily removed legacy cmdlets that rely on the old TFS Client Object Model: `Start-TfsIdentitySync`, `Connect-TfsConfigurationServer`, `Disconnect-TfsConfigurationServer`, `Get-TfsConfigurationServer`, `Get-TfsRegisteredConfigurationServer`, `Export-TfsGlobalList`, `Get-TfsGlobalList`, `Import-TfsGlobalList`, `New-TfsGlobalList`, `Remove-TfsGlobalList`, `Rename-TfsGlobalList`, `Set-TfsGlobalList`, `Start-TfsXamlBuild`, `Dismount-TfsTeamProjectCollection`, `Mount-TfsTeamProjectCollection`, `Start-TfsTeamProjectCollection`, `Stop-TfsTeamProjectCollection`, `Copy-TfsWorkItem`.

### Fixed

- `Set-TfsTeam`: Fixed bug where the `-IterationPaths` argument was ignored.
- `Get-TfsGroupMember`: Fixed issue with PS Core and Azure DevOps Services ([#149](https://github.com/igoravl/TfsCmdlets/issues/149)).
- `New-TfsWorkItem`: Fixed failure when specifying area and iteration without prefixing team project ([#147](https://github.com/igoravl/TfsCmdlets/issues/147)).
- `Get-TfsWorkItem`: Fixed error when using the `-Description` parameter.

## [2.1.4] - 2021-11-30

### Fixed

- `Get-TfsIdentity`: Fixed error when used with Azure DevOps Services. Now uses collection (organization) scope instead of configuration server.
- `Invoke-TfsRestApi`: Fixed error calling alternate hosts under Windows PowerShell (Desktop).

## [2.1.3] - 2021-11-25

### Changed

- `Invoke-TfsRestApi`: Now automatically unwraps the "value" property in the response (**BREAKING**). Use `-NoAutoUnwrap` to preserve previous behavior.

### Fixed

- `Invoke-TfsRestApi`: Fixed error when calling different hosts in the same session ([#152](https://github.com/igoravl/TfsCmdlets/issues/152)).

## [2.1.2] - 2021-09-10

### Changed

- All cmdlets now have ConfirmImpact set to Medium. Destructive actions use ShouldContinue() with a `-Force` parameter ([#124](https://github.com/igoravl/TfsCmdlets/issues/124)).

## [2.1.1] - 2021-09-08

### Fixed

- `New-TfsArea`, `New-TfsIteration`: Fixed "Parent node '\\' does not exist" error.

## [2.1.0] - 2021-08-13

### Added

- `Enable-TfsGitRepository`, `Disable-TfsGitRepository` — Enable/disable Git repositories ([#131](https://github.com/igoravl/TfsCmdlets/issues/131)).

### Changed

- Refactored project structure to a single multi-targeting project (TfsCmdlets) instead of three separate projects.

## [2.0.1] - 2021-08-02

### Fixed

- Added missing directive to the manifest file so that cmdlets are properly exported/exposed. **Required update** for users on v2.0.0.

## [2.0.0] - 2021-08-02

### Changed

- `New-TfsTeamProject`: New defaults for `SourceControl` (Git) and `ProcessTemplate` (default process) ([#116](https://github.com/igoravl/TfsCmdlets/issues/116)).
- `New-TfsIteration`: Added `StartDate` and `FinishDate` arguments.
- `Get-TfsTeamProject`, `Get-TfsWorkItem`: Added `-ErrorAction` support.

### Fixed

- Iteration argument completer is listing Areas ([#137](https://github.com/igoravl/TfsCmdlets/issues/137)).
- `Rename-TfsIteration`: No longer removes iteration dates ([#135](https://github.com/igoravl/TfsCmdlets/issues/135)).
- `New-TfsWorkItem`, `Set-TfsWorkItem`: Fixed error using `-Area` and `-Iteration` arguments ([#133](https://github.com/igoravl/TfsCmdlets/issues/133)).
- `Set-TfsWorkItem`: Fixed error setting field values ([#132](https://github.com/igoravl/TfsCmdlets/issues/132)).
- `Get-TfsWorkItem`: Fixed `-ShowWindow` ([#123](https://github.com/igoravl/TfsCmdlets/issues/123)).

## [2.0.0-rc.5] - 2021-04-17

### Added

- `Set-TfsTeamProject` — Adds support to editing team project details (currently supports setting the team project icon).

### Changed

- Replaced Azure DevOps CI pipeline with GitHub Actions.

### Fixed

- Fixed documentation site generation.

## [2.0.0-rc.4] - 2021-04-05

### Changed

- `Get-TfsWorkItem`: Can now receive a URL to a work item as a parameter.
- `Get-TfsWorkItemLink`: New `-LinkType` argument to filter by well-known link types.
- Added format to WorkItemRelation output.
- **BREAKING**: The pipeline input parameter for `Get-TfsWorkItem` has changed from **Project** to **WorkItem**.

### Fixed

- Fixed bug in parameter override logic.
- Fixed StructureGroup value in `New-TfsIteration`.

## [2.0.0-rc.3] - 2021-02-19

### Added

- `Export-TfsWorkItemAttachment` — Downloads work item attachment file contents.
- `Get-TfsWorkItemLink` — Gets links and attachment metadata from work items (migrated).
- `Get-TfsWorkItem`: New `-IncludeLinks` switch to fetch link/attachment information.

## [2.0.0-rc.2] - 2020-11-30

### Changed

- Documentation updates.

## [2.0.0-rc.1] - 2020-11-21

### Added

- `Get-TfsWiki`, `New-TfsWiki`, `Remove-TfsWiki` — Wiki management cmdlets.

## [2.0.0-beta.16] - 2020-10-26

### Added

- `Get-TfsRegistryValue`, `Set-TfsRegistryValue` — TFS Registry cmdlets.

### Changed

- `Get-TfsConfigurationServer`: Can now be run in PowerShell Core.
- `Get-TfsVersion`: Updated to recognize Azure DevOps Server 2020.

## [2.0.0-beta.15] - 2020-07-21

### Changed

- WiX (MSI) installer: Added custom bitmaps, changed installation scope from machine-wide to user-wide, added license agreement text.

## [2.0.0-beta.14] - 2020-07-19

### Changed

- Migrated `New-TfsWorkItem` and `Remove-TfsWorkItem` to .NET cmdlets.

## [2.0.0-beta.13] - 2020-07-18

### Changed

- Migrated service hook cmdlets (`Get-TfsServiceHookConsumer`, `Get-TfsServiceHookNotificationHistory`, `Get-TfsServiceHookPublisher`, `Get-TfsServiceHookSubscription`) to .NET cmdlets.

## [2.0.0-beta.12] - 2020-07-14

### Added

- PowerShell 7 (Core) support — most cmdlets now run in both Windows PowerShell and PowerShell 7.
- `Connect-TfsTeam`, `Disconnect-TfsTeam`, `Enter-TfsShell`, `Exit-TfsShell`, `Get-TfsReleaseDefinition`, `Get-TfsVersion`, `New-TfsProcessTemplate`, `New-TfsTestPlan`, `Remove-TfsWorkItemTag`, `Rename-TfsGlobalList`, `Rename-TfsTeamProject`, `Rename-TfsTestPlan`, `Search-TfsWorkItem`, `Undo-TfsTeamProjectRemoval`.
- Published [documentation site](https://tfscmdlets.dev).
- All cmdlets now have synopsis and parameter documentation.
- Argument completers for `-Server`, `-Collection` and `-Project`.

### Changed

- `Get-TfsCredential` renamed to `New-TfsCredential` (**BREAKING**).
- `Get-TfsPolicyType` renamed to `Get-GitTfsPolicyType` (**BREAKING**).
- Several cmdlets renamed for consistency (**BREAKING**): `Get-TfsTeamBacklog` → `Get-TfsTeamBacklogLevel`, `Get-TfsTeamBoardCardRuleSettings` → `Get-TfsTeamBoardCardRule`, `Set-TfsTeamBoardCardRuleSettings` → `Set-TfsTeamBoardCardRule`.

### Removed

- `Set-TfsArea` (use `Rename-TfsArea` or `Move-TfsArea` instead).
- `Set-TfsWorkItemBoardStatus` (use `Set-TfsWorkItem` instead).

## [2.0.0-beta.11] - 2020-01-21

### Added

- `Enter-TfsShell`, `Exit-TfsShell` — Azure DevOps Shell mode.
- `Invoke-TfsRestApi` — Streamline calls to the Azure DevOps REST API.
- `Get-TfsVersion` — Returns version information on a team project collection / organization.
- Argument completers for `-Server`, `-Collection` and `-Project`.
- New aliases for `Connect-*` cmdlets: `ctfssvr`, `ctfs`, `ctfstp`, `ctfsteam`.

### Changed

- Converted work item query cmdlets (`*-TfsWorkItemQuery`, `*-TfsWorkItemQueryFolder`) to REST API.
- Converted PowerShell Format/Types XML files to YAML (built with [ps1xmlgen](https://github.com/igoravl/ps1xmlgen)).

### Fixed

- Fixed iteration processing in `Set-TfsTeam`.
- Fixed classification node (area/iteration) retrieval for old APIs.
- Fixed Azure DevOps Shell command prompt.
- Fixed `Disconnect-*` issues.

## [2.0.0-beta.10] - 2019-09-12

### Deprecated

- The `MoveBy` argument in `Set-TfsClassificationNode` (and related area/iteration cmdlets) now shows a deprecation warning and is ignored.

### Fixed

- Fixed issue with Area/Iteration cmdlets not processing pipelines correctly.

## [2.0.0-beta.9] - 2019-09-10

### Added

- Build definition folder cmdlets: `Get-TfsBuildDefinitionFolder`, `New-TfsBuildDefinitionFolder`, `Remove-TfsBuildDefinitionFolder`.
- Release definition folder cmdlets: `Get-TfsReleaseDefinitionFolder`, `New-TfsReleaseDefinitionFolder`, `Remove-TfsReleaseDefinitionFolder`.

## [2.0.0-beta.8] - 2019-09-06

### Changed

- Area/iteration cmdlets ported to REST API. New `*-TfsClassificationNode` cmdlets with `-StructureGroup` argument; `*-TfsArea` and `*-TfsIteration` are now aliases.

### Fixed

- Fixed bug in `Connect-TfsTeamProjectCollection` when passing a credential.

## [2.0.0-beta.6] - 2019-09-02

### Added

- `Add-TfsGroupMember`, `Get-TfsGroupMember`, `Remove-TfsGroupMember` — Group membership management cmdlets.

## [1.0.0-alpha7] - 2015-10-22

### Added

- Added essential help comments to all cmdlets.

### Fixed

- Fixed Nuget and Chocolatey package icons.
- Fixed bug in `Get-TfsWorkItemType` that would not return any WITDs.

## [1.0.0-alpha6] - 2015-10-22

### Changed

- Enable build from cmdline with VS 2015.
- Added custom type detection logic.

### Fixed

- Fixed commit message with apostrophe breaking build.
- Fixed handling of current config server.
- Added proper parameter initialization for credentials.

## [1.0.0-alpha5] - 2015-09-10

### Changed

- Cascade disconnection: When calling a "higher" Disconnect cmdlet, the "lower" ones are cascade-invoked automatically.

### Fixed

- Fixed conditional use of `-Title` in `New-TfsWorkItem`.
- Fixed argument naming and pipelined return in `Get-TfsWorkItem`.
- Fixed return when passing `-Current` in `Get-TfsConfigurationServer`.

## [1.0.0-alpha4] - 2015-09-03

### Changed

- Removed dependency on .NET 3.5.

## [1.0.0-alpha3] - 2015-09-03

### Added

- Added help comments to the Areas & Iterations functions.

### Fixed

- Fixed an issue in the AssemblyResolver implementation. Switched from scriptblock to pure .NET implementation to avoid race conditions.
