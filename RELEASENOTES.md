# TfsCmdlets Release Notes

## Version 2.10.0 (_13/Oct/2024_)

This release adds new pipeline-related cmdlets.

## New cmdlets

- **`Get-TfsPipelineRun`**: Gets one or more pipeline (build) runs in a team project.
- **`Remove-TfsPipelineRun`**: Removes a pipeline run entry from the pipeline execution history.
- **`Start-TfsPipelineRun`**: Queues (starts) a new pipeline run.
- **`Stop-TfsPipelineRun`**: Cancels (stops) a running pipeline.

## Fixes

- **`Connect-TfsOrganization`** (and **`Connect-TfsTeamProjectCollection`**): Resolved an issue where connections, including failed ones, were being cached and reused in subsequent calls. This caused problems when attempting to reconnect after a failure due to invalid credentials, as the cached connection (with the invalid credentials) would be reused.

## Changes and enhancements

- **Renamed build-related cmdlets**: All build-related cmdlets have been renamed to better align with the new Azure Pipelines terminology. Instead of `Build`, they now use `Pipeline` in their names. Aliases have been added to the old cmdlets to maintain backward compatibility. For new scripts, prefer the new names. Old scripts won't break, but they should be updated to use the new names. The renamed cmdlets are:
  - Disable-TfsBuildDefinition -> **Disable-TfsPipeline**
  - Enable-TfsBuildDefinition -> **Enable-TfsPipeline**
  - Get-TfsBuildDefinition -> **Get-TfsPipeline**
  - Get-TfsBuildDefinitionFolder -> **Get-TfsPipelineFolder**
  - New-TfsBuildDefinitionFolder -> **New-TfsPipelineFolder**
  - Remove-TfsBuildDefinitionFolder -> **Remove-TfsPipelineFolder**
  - Resume-TfsBuildDefinition -> **Resume-TfsPipeline**
  - Start-TfsBuild -> **Start-TfsPipelineRun**
  - Suspend-TfsBuildDefinition -> **Suspend-TfsPipeline**
- **`Connect-TfsOrganization`** (and **`Connect-TfsTeamProjectCollection`**): Improved error handling when connecting with invalid/expired personal access tokens - now it throws an error with a more descriptive message.

-----------------------

## Previous Versions

### Version 2.9.0 (_15/Aug/2024_)

See release notes [here](Docs/ReleaseNotes/2.9.0.md).

### Version 2.8.2 (_24/Jul/2024_)

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
