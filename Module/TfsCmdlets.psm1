# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop

_ImportRequiredAssembly 'Microsoft.TeamFoundation.Client'
