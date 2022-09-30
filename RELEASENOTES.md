# TfsCmdlets Release Notes

## Version 2.6.0 (_30/Sep/2022_)

This release fixes a bug in `Get-TfsWorkItemQuery` and `Get-TfsWorkItemQueryFolder`, and adds two new cmdlets.

## New cmdlets

* `Undo-TfsWorkItemQueryRemoval` and `Undo-TfsWorkItemQueryFolderRemoval` allow you to undo the deletion of a query or query folder. This is useful when you accidentally delete a query or query folder and want to restore it.

To restore a deleted query:

```powershell
# You can either pipe the deleted query from Get-TfsWorkItemQuery to Undo-TfsWorkItemQueryRemoval...
Get-TfsWorkItemQuery 'My Deleted Query' -Scope Personal -Deleted | Undo-TfsWorkItemQueryRemoval

# ... or you can specify the query directly when calling Undo-TfsWorkItemQueryRemoval
Undo-TfsWorkItemQueryRemoval 'My Deleted Query' -Scope Personal
```

The same applies to query folders - with the distinction that folder can be restored recursively by specifying the `-Recursive` switch. When `-Recursive` is omitted, only the folder itself is restored, without any of its contents. You can then restore its contents by issuing further calls to `Undo-TfsWorkItemQueryRemoval` and/or `Undo-TfsWorkItemQueryFolderRemoval`.

```powershell
# You can either pipe the deleted folder from Get-TfsWorkItemQueryFolder to Undo-TfsWorkItemQueryFolderRemoval...
Get-TfsWorkItemQueryFolder 'My Deleted Folder' -Scope Personal -Deleted | Undo-TfsWorkItemQueryRemoval -Recursive

# ... or you can specify the folder directly when calling Undo-TfsWorkItemQueryFolderRemoval
Undo-TfsWorkItemQueryFolderRemoval 'My Deleted Folder' -Scope Personal -Recursive
```

## Fixes

* Fixes a bug in `Get-TfsWorkItemQuery` and `Get-TfsWorkItemQueryFolder` where the `-Deleted` switch was not respected and deleted items would not be returned.

-----------------------

## Previous Versions

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
