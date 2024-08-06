# TfsCmdlets Release Notes

## Version 2.9.0 (_03/Aug/2024_)

This release adds some new process-related cmdlets.

## New cmdlets

- `Get-TfsWorkItemField`: Gets information from one or more organization-wide work item fields
- `New-TfsWorkItemField`: Creates a new organization-wide work item field
- `Remove-TfsWorkItemField`: Removes an organization-wide work item field

## Fixes and enhancements

- Fixed an issue with `Get-TfsArtifact` where it wasn't listing deleted packages.
- Fixed an issue with `Get-TfsArtifactFeed` where it would ignore the -Project argument and thus not filter feeds by project.
- Fixed an issue with `Get-TfsWorkItemTag` where it would fail when given a list of tags as input.
- Fixed an issue with `Get-TfsWorkItemType` where it would throw a "Parameter count mismatch" error when trying to get the work item type of a given work item.
- Now `Connect-TfsTeamProjectCollection` (and its counterpart `Connect-TfsOrganization`) throws an error when trying to connect with invalid credentials instead of silently going into "anonymous mode". That help preventing subtle script errors where the lack of authorization would only be noticed later in the script, when trying to actually perform some command that required valid credentials. Now you get the warning that something is wrong as early in the script as possible.

-----------------------

## Previous Versions

## Version 2.8.2 (_24/Jul/2024_)

See release notes [here](Docs/ReleaseNotes/2.8.2.md).

### Version 2.8.1 (_16/Jul/2024_)

See release notes [here](Docs/ReleaseNotes/2.8.1.md).

### Version 2.8.0 (_09/Jul/2024_)

See release notes [here](Docs/ReleaseNotes/2.8.0.md).

### Version 2.7.1 (_03/Jul/2024_)

See release notes [here](Docs/ReleaseNotes/2.7.1.md).

### Version 2.7.0 (_03/Jul/2024_)

See release notes [here](Docs/ReleaseNotes/2.7.0.md).

### Version 2.6.1 (_15/May/2024_)

See release notes [here](Docs/ReleaseNotes/2.6.1.md).

### Version 2.6.0 (_30/Sep/2022_)

See release notes [here](Docs/ReleaseNotes/2.6.0.md).

### Version 2.5.1 (_22/Aug/2022_)

See release notes [here](Docs/ReleaseNotes/2.5.1.md).

### Version 2.5.0 (_03/Aug/2022_)

See release notes [here](Docs/ReleaseNotes/2.5.0.md).

### Version 2.4.1 (_20/Jul/2022_)

See release notes [here](Docs/ReleaseNotes/2.4.1.md).

### Version 2.4.0 (_23/May/2022_)

See release notes [here](Docs/ReleaseNotes/2.4.0.md).

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
