# TfsCmdlets Release Notes

## Version 2.4.0 (_23/May/2022_)

This release adds support for interactive logons in PowerShell Core (6+) shells.

## Improvements

* All the `Connect-*` cmdlets now support interactive (`-Interactive`) logon in PowerShell Core (6+) shells. Previously, only Windows PowerShell (5.*) terminals supported interactive authentication. **NOTE**: Interactive logons in PowerShell Core require Azure DevOps Services. TFS / Azure DevOps Server remain unsupported for interactive logons in PowerShell Core. To connect to an on-premises server in a PowerShell Core shell, you're still required to use either username/password credentials or a personal access token.
* Some minor consistency improvements to the way Work Item Query cmdlets `Get-TfsWorkItemQuery` and `Get-TfsWorkItemQueryFolder` handle paths. Additionally, `Get-TfsWorkItemQueryFolder` can now return the "root" folders (My Queries and/or Shared Queries) when specifying `/` as the folder path. That comes in handy when you want to e.g. use some Security APIs that require the ID of the folders all the way from the beginning of the hierarchy.

## Fixes

* Under certain circumstances, `Get-TfsWorkItem` would return an invalid ID, due to a change in the response from the WorkItem REST API (fixes [#172](https://github.com/igoravl/TfsCmdlets/issues/172))
* Fix a bug in `New-TfsWorkItemQuery` and `New-TfsWorkItemQueryFolder`, where queries and folders could not be created when their parent did not exist.

-----------------------

## Previous Versions

### Version 2.3.1 (_08/Apr/2022_)

See release notes [here](Docs/ReleaseNotes/2.3.1.md).

### Version 2.3.0 (_04/Mar/2022_)

See release notes [here](Docs/ReleaseNotes/2.3.0.md).

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
