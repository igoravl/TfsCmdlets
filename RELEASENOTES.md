# TfsCmdlets Release Notes

## Version 2.3.0 (_25/Mar/2022_)

This release adds initial support for Azure Artifacts and fixes a few bugs related to team membership handling.

## New cmdlets

### Artifacts

* [`Get-TfsArtifact`](https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifact)
* [`Get-TfsArtifactFeed`](https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifactFeed)
* [`Get-TfsArtifactVersion`](https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifactVersion)
* [`Get-TfsArtifactFeedView`](https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifactFeedView)

### Git

* [`Remove-TfsGitBranch`](https://tfscmdlets.dev/docs/cmdlets/Git/Branch/Remove-TfsGitBranch)

## Improvements
  
* Add `-AreaPaths` argument to `New-TfsTeam` so team area paths can be defined at team creation time.
* `Get-TfsWorkItem` supports long result sets again. In the previous release, query results were limited to 200 work items due to a limitation in the Azure DevOps "Get Work Items Batch" API. In this version we added back the original behavior as a fallback logic: to fetch work items one at a time to circumvent the limitation. Though slower, it can fetch any number of work items (fixes [#164](https://github.com/igoravl/TfsCmdlets/issues/164)).

## Fixes

* `Add-TfsTeamAdmin` limited team administrators to users (groups could not be added as admins), even though Azure DevOps supports it. This release litfs this restriction.
* Fixes a bug introduced in the last version where `Get-TfsTeamMember` and `Get-TfsTeamAdmin` would not return group members
* Under some circumstances `New-TfsTeam` would not respect `-NoDefaultArea` and `-NoBacklogIteration`

-----------------------

## Previous Versions

### Version 2.2.1 (_10/Feb/2022_)

See release notes [here](Docs/ReleaseNotes/2.2.1.md).

### Version 2.2.0 (_05/Feb/2022_)

See release notes [here](Docs/ReleaseNotes/2.2.0.md).

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
