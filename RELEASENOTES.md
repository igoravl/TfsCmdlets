# TfsCmdlets Release Notes

## Version 2.2.0 (_05/Feb/2022_)

This release marks the adoption of .NET Source Generators as a core part of the TfsCmdlets development process.

Although it is entirely opaque (and mostly irrelevant) to the end-user, we expect that they will allow us to have shorter and more stable releases in the future.

Now, back to the release notes :-) We have new cmdlets, a bunch of improvements and bug fixes, and a few minor _breaking changes_.

## New cmdlets

* **Extension Management**
  * `Enable-TfsExtension`
  * `Disable-TfsExtension`
  * `Get-TfsExtension`
  * `Install-TfsExtension`
  * `Uninstall-TfsExtension`
* **Git**
  * `Get-TfsGitItem`
* **Identities, Users and Groups**
  * `Get-TfsGroup`
  * `New-TfsGroup`
  * `Remove-TfsGroup`
  * `Get-TfsUser`
* **Pipeline**
  * `Get-TfsBuildDefinition`
  * `Enable-TfsBuildDefinition`
  * `Disable-TfsBuildDefinition`
  * `Suspend-TfsBuildDefinition`
  * `Resume-TfsBuildDefinition`
* **Organization**
  * `Connect-TfsOrganization`
  * `Disconnect-TfsOrganization`  
  * `Get-TfsOrganization`
* **Team project**
  * `Import-TfsTeamProjectAvatar`
  * `Export-TfsTeamProjectAvatar`
  * `Remove-TfsTeamProjectAvatar`
* **Work Items**
  * `Enable-TfsWorkItemTag`
  * `Disable-TfsWorkItemTag`
  * `Undo-TfsWorkItemRemoval`

## Improvements
  
* `Import-TfsWorkItemType` and `Export-TfsWorkItemType` now support importing from / exporting to files (and not only to the console)
* Add -NoProfile to Enter-TfsShell ([#145](https://github.com/igoravl/TfsCmdlets/issues/145))
* Most Get-* cmdlets now support retrieving multiple items at once by supplying a list of IDs/names/etc. (e.g. `Get-TfsTeamProject Proj1, Proj2`). That, by consequence, fixes issue [#155](https://github.com/igoravl/TfsCmdlets/issues/155)
* Remove trailing slash from org name in prompt ([#148](https://github.com/igoravl/TfsCmdlets/issues/148))
* Filter team project by process template ([#104](https://github.com/igoravl/TfsCmdlets/issues/104))
* `Get-TfsWorkItem` now makes batch API calls to improve performance. The gains are expressive - an order of magnitude in many cases.
* `Invoke-TfsRestApi` can now save the response to a file, via its `-Destination` argument. Besides, binary content (such as 'application/octet-stream') is returned as a byte array (instead of a string) when outputting the results to the console.
* `Set-TfsTeam` has new arguments `-OverwriteAreaPaths` and `-OverwriteIterationPaths` to control its behavior when setting default areas and sprint iterations, respectively. Additionally, both -AreaPaths and -IterationPaths now support wildcards to simplify the addition of multiple items at once.

## Fixes

* Fixes a bug in `Set-TfsTeam` where the -IterationPaths argument was ignored, and the sprint iterations were not set. Now sprint iterations are set as expected.
* Get-TfsGroupMember does not work with PS Core and Azure DevOps Services ([#149](https://github.com/igoravl/TfsCmdlets/issues/149))
* New-TfsWorkItem fails when specifying area and iteration without prefixing team project ([#147](https://github.com/igoravl/TfsCmdlets/issues/147))
* Fixes a bug in `Get-TfsWorkItem` where when using the `-Description` an error is thrown.
* And many other under-the-hood fixes caught during the development of this release

## BREAKING CHANGES

### Removed cmdlets

Some legacy cmdlets (primarily those that rely on the old TFS Client Object Model) have been temporarily removed. They _may_ return in a future release when we determine the best course of action to support legacy cmdlets moving forward.

In the meantime, please consider using a previous release if you need them.

The removed cmdlets are:

* **Administration** cmdlets
  * `Start-TfsIdentitySync`
* **Configuration Server** cmdlets
  * `Connect-TfsConfigurationServer`
  * `Disconnect-TfsConfigurationServer`
  * `Get-TfsConfigurationServer`
  * `Get-TfsRegisteredConfigurationServer`
* **Global List** cmdlets
  * `Export-TfsGlobalList`
  * `Get-TfsGlobalList`
  * `Import-TfsGlobalList`
  * `New-TfsGlobalList`
  * `Remove-TfsGlobalList`
  * `Rename-TfsGlobalList`
  * `Set-TfsGlobalList`
* **Pipeline** cmdlets
  * `Start-TfsXamlBuild`
* **Team Project Collection** cmdlets
  * `Dismount-TfsTeamProjectCollection`
  * `Mount-TfsTeamProjectCollection`
  * `Start-TfsTeamProjectCollection`
  * `Stop-TfsTeamProjectCollection`
* **Work Item** cmdlets
  * `Copy-TfsWorkItem`

### Get-TfsTeamProject -IncludeDetails

Every team project returned by `Get-TfsTeamProject` used to make a second API call to retrieve the details of the team project, such as its process template. That additional roundtrip meant that the command would take longer to complete. However, those details are rarely needed in most scenarios.

Therefore, to improve performance for the most common situations, `Get-TfsTeamProject` no longer includes project details by default.

When needed - the most common scenario is to get the process template name of a given project - you must include the `-IncludeDetails` switch.

### Get-TfsWorkItem and max work items per call

The change in `Get-TfsWorkItem` to use batch calls greatly improved performance, but the downside is that now you're limited to up to 200 work items returned in a single call. That limit is imposed by the backend REST API and can't be circumvented.

We're exploring alternatives for those scenarios when one needs to retrieve more than 200 work items (e.g., when doing an export).

-----------------------

## Previous Versions

### Version 2.1.4 (_30/Nov/2021_)

See release notes [here](Docs/ReleaseNotes/2.1.4.md).

### Version 2.1.3 (_25/Nov/2021_)

See release notes [here](Docs/ReleaseNotes/2.1.3.md).

### Version 2.1.2 (_10/Sep/2021_)

See release notes [here](Docs/ReleaseNotes/2.1.2.md).

### Version 2.1.1 (_08/Sep/2021_)

See release notes [here](Docs/ReleaseNotes/2.1.1.md).

### Version 2.1.0 (_13/Aug/2021_)

See release notes [here](Docs/ReleaseNotes/2.1.0.md).

### Version 2.0.1 (_02/Aug/2021_)

See release notes [here](Docs/ReleaseNotes/2.0.1.md).

### Version 2.0.0 (_02/Aug/2021_)

See release notes [here](Docs/ReleaseNotes/2.0.0.md).

### Version 2.0.0-rc.5 (_17/Apr/2021_)

See release notes [here](Docs/ReleaseNotes/2.0.0-rc.5.md).

### Version 2.0.0-rc.4 (_05/Apr/2021_)

See release notes [here](Docs/ReleaseNotes/2.0.0-rc.4.md).

### Version 2.0.0-rc.3 (_19/Feb/2021_)

See release notes [here](Docs/ReleaseNotes/2.0.0-rc.3.md).

### Version 2.0.0-rc.2 (_30/Nov/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-rc.2.md).

### Version 2.0.0-rc.1 (_21/Nov/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-rc.1.md).

### Version 2.0.0-beta.16 (_26/Oct/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.16.md).

### Version 2.0.0-beta.15 (_21/Jul/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.15.md).

### Version 2.0.0-beta.14 (_19/Jul/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.14.md).

### Version 2.0.0-beta.13 (_18/Jul/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.13.md).

### Version 2.0.0-beta.12 (_14/Jul/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.12.md).

### Version 2.0.0-beta.11 (_21/Jan/2020_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.11.md).

### Version 2.0.0-beta.10 (_12/Sep/2019_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.10.md).

### Version 2.0.0-beta.9 (_10/Sep/2019_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.9.md).

### Version 2.0.0-beta.8 (_06/Sep/2019_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.8.md).

### Version 2.0.0-beta.6 (_02/Sep/2019_)

See release notes [here](Docs/ReleaseNotes/2.0.0-beta.6.md).

### 1.0.0-alpha7 (_22/Oct/2015_)

See release notes [here](Docs/ReleaseNotes/1.0.0-alpha7.md).

### 1.0.0-alpha6 (_22/Oct/2015_)

See release notes [here](Docs/ReleaseNotes/1.0.0-alpha6.md).

### 1.0.0-alpha5 (_10/Sep/2015_)

See release notes [here](Docs/ReleaseNotes/1.0.0-alpha5.md).

### 1.0.0-alpha4 (_03/Sep/2015_)

See release notes [here](Docs/ReleaseNotes/1.0.0-alpha4.md).

### 1.0.0-alpha3 (_03/Sep/2015_)

See release notes [here](Docs/ReleaseNotes/1.0.0-alpha3.md).
