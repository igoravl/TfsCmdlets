Set-Alias -Name 'ctfs' -Value 'Connect-TfsTeamProjectCollection'
Set-Alias -Name 'ctp' -Value 'Connect-TfsTeamProjectCollection'
Set-Alias -Name 'ctfsp' -Value 'Connect-TfsTeamProjectCollection'
Set-Alias -Name 'itfs' -Value 'Invoke-TfsRestApi'

# Aliases for renamed cmdlets

Set-Alias -Name 'Start-TfsBuild' -Value 'Start-TfsPipeline'
Set-Alias -Name 'Disable-TfsBuildDefinition' -Value 'Disable-TfsPipeline'
Set-Alias -Name 'Enable-TfsBuildDefinition' -Value 'Enable-TfsPipeline'
Set-Alias -Name 'Get-TfsBuildDefinition' -Value 'Get-TfsPipeline'
Set-Alias -Name 'Resume-TfsBuildDefinition' -Value 'Resume-TfsPipeline'
Set-Alias -Name 'Suspend-TfsBuildDefinition' -Value 'Suspend-TfsPipeline'
Set-Alias -Name 'Get-TfsBuildDefinitionFolder' -Value 'Get-TfsPipelineFolder'
Set-Alias -Name 'New-TfsBuildDefinitionFolder' -Value 'New-TfsPipelineFolder'
Set-Alias -Name 'Remove-TfsBuildDefinitionFolder' -Value 'Remove-TfsPipelineFolder'
