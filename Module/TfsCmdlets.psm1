# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop

# Configure assembly resolver

_RegisterAssemblyResolver
_ImportRequiredAssembly 'Microsoft.TeamFoundation.Client'
