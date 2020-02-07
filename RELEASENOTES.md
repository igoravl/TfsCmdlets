# TfsCmdlets Release Notes

## Version 2.0.0-beta.11 (_21/Jan/2020_)

### Improvements

#### New cmdlets: `Enter-TfsShell`, `Exit-TfsShell`, `Get-TfsVersion`, `Invoke-TfsRestApi`

The new `Enter-TfsShell` and `Exit-TfsShell` cmdlets streamline the invocation of the "Azure DevOps Shell" mode. When invoked, shows a banner with the module version and activates the custom console prompt. The custom prompt displays the currently connected Azure DevOps org/server.

`Invoke-TfsRestApi` can be used to streamline calls to the Azure DevOps REST API in those scenarios/APIs not yet covered by TfsCmdlets.

Lastly, `Get-TfsVersion` returns version information on a given team project collection / organization. Currently, only Azure DevOps Services organizations are supported. Support for TFS and Azure DevOps Server will be added in a future release.

#### Convert work item query-related cmdlets (`*-TfsWorkItemQuery`, `*-TfsWorkItemQueryFolder`) to REST API

In the process, they've been generalized and converted to aliases to their new "generic" counterparts (`*-TfsWorkItemQueryItem`), much like the Area/Iteration cmdlets.

#### Convert PowerShell Format/Types XML files (*.Format.ps1xml, *.Types.ps1xml) to YAML

Now both **TfsCmdlets.Types.ps1xml** and **TfsCmdlets.Format.ps1xml** files are generated during build time from YAML files with [ps1xmlgen](https://github.com/igoravl/ps1xmlgen). That offers a much better experience to edit/mantain PowerShell's type/format XML files.

#### Argument completers

This improvement is way overdue, but now we have the first set of argument completers. Any cmdlets with the arguments `-Server`, `-Collection` and `-Project` can be "Tab-completed".

#### New aliases for Connect-* cmdlets

Now, to connect to a Azure DevOps (or TFS) collection/organization/project/team you can use optionally use one of the aliases below:

* Connect-TfsConfigurationServer
  * ctfssvr
* Connect-TfsTeamProjectCollection
  * Connect-AzdoOrganization
  * Connect-TfsOrganization
  * ctfs
* Connect-TfsTeamProject
  * ctfstp
* Connect-TfsTeam
  * ctfsteam

### Bug fixes

- Fix iteration processing in Set-TfsTeam ([72f0fc0](https://github.com/igoravl/TfsCmdlets/pull/98/commits/72f0fc0cdcba7d41c8341efd0b0304303058907e)), ([e15d1ee](https://github.com/igoravl/TfsCmdlets/pull/98/commits/e15d1ee0bff2e8a5bd20b26e11d1b41413eb79b9))
- Fix build when in Release configuration ([6a795ce](https://github.com/igoravl/TfsCmdlets/pull/98/commits/6a795ce49331e37fbd7319f26c1e452d0135a7f6))
- Fix classification node (area/iteration) retrieval for old APIs ([0df3616](https://github.com/igoravl/TfsCmdlets/pull/98/commits/0df3616868a15054125261555ecefed1d830d599))
- Fix TFS Client Library version retrieval ([a9ac849](https://github.com/igoravl/TfsCmdlets/pull/98/commits/a9ac849643f5738e9616ae5c01a12c93c4d1345c))

### Known issues

- Incremental build is currently disabled in the default Visual Studio Code Build task, as it's a bit inconsistent.

------------------------

## Previous Versions

### Version 2.0.0-beta.10 (_12/Sep/2019_)

#### Improvements

- Not an improvement per se, but the *MoveBy* argument in the Set-TfsClassificationNode cmdlet (and related area/iteration ones) now displays a 'deprecated' warning when MoveBy is specified. The argument is then ignored.

#### Bug fixes

- Fix an issue with Area/Iteration cmdlets not processing pipelines correctly

#### Known issues

- N/A

### Version 2.0.0-beta.9 (_10/Sep/2019_)

#### Improvements

- Add folder management cmdlets for Build and Release Definitions:
  - Build
    - Get-TfsBuildDefinitionFolder
    - New-TfsBuildDefinitionFolder
    - Remove-TfsBuildDefinitionFolder
  - Release
    - Get-TfsReleaseDefinitionFolder
    - New-TfsReleaseDefinitionFolder
    - Remove-TfsReleaseDefinitionFolder

#### Bug fixes

N/A

#### Known issues

- Set-TfsArea and Set-TfsIteration no longer support reordering of node (`-MoveBy` argument). Still trying to figure out how to do it with the REST API

### Version 2.0.0-beta.8 (_06/Sep/2019_)

#### Improvements

- Area/iteration cmdlets have been ported to the new REST API
- New "generic" versions of the area/iteration cmdlets are now available. `*-TfsClassificationNode` cmdlets have a `-StructureGroup` argument that accepts either 'Areas' or 'Iterations'. Actually, area and iteration cmdlets (`*-TfsArea` and `*-TfsIteration`) are now merely aliases to their respective `*-TfsClassificationNode` counterparts.

#### Bug fixes

- Fix a bug in Connect-TfsTeamProjectCollection when passing a credential ([27dd30](https://github.com/igoravl/TfsCmdlets/commit/27dd302e1b243436229c3f44fa138c22952718b3))

#### Known issues

- Set-TfsArea and Set-TfsIteration no longer support reordering of node (`-MoveBy` argument). Still trying to figure out how to do it with the REST API

### Version 2.0.0-beta.6 (_02/Sep/2019_)

#### Improvements

- Add new group membership management cmdlets:
  - Add-TfsGroupMember
  - Get-TfsGroupMember
  - Remove-TfsGroupMember

#### Bug fixes

N/A

#### Known issues

N/A

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
