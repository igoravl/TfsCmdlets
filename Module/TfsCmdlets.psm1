_Log "Loading module TfsCmdlets from $PSScriptRoot"

# Initialize variables

$script:IsDesktop = ($PSEdition -ne 'Core')
$script:IsCore = -not $script:IsDesktop

# Configure assembly resolver

_RegisterAssemblyResolver

# Load essential assemblies

_ImportRequiredAssembly 'Newtonsoft.Json'
_ImportRequiredAssembly 'Microsoft.TeamFoundation.Client'
_ImportRequiredAssembly 'Microsoft.VisualStudio.Services.WebApi'