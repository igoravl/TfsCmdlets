# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop

# Configure assembly resolver

_Log "Loading module TfsCmdlets from $PSScriptRoot"

_RegisterAssemblyResolver
_ImportRequiredAssembly 'Microsoft.TeamFoundation.Client'
